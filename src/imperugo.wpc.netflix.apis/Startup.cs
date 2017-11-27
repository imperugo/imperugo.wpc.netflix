using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using imperugo.wpc.netflix.apis.Configuration;
using imperugo.wpc.netflix.apis.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace imperugo.wpc.netflix.apis
{
	public class Startup
	{
		private readonly RedisConfiguration redisConfiguration = new RedisConfiguration();
		private readonly UrlBuilderConfiguration urlBuilderConfiguration = new UrlBuilderConfiguration();

		private readonly IHostingEnvironment environment;

		public Startup(IConfiguration configuration, IHostingEnvironment environment)
		{
			this.environment = environment;

			configuration.GetSection("redis").Bind(redisConfiguration);
			configuration.GetSection("urlBuilder").Bind(urlBuilderConfiguration);
		}

		public void ConfigureServices(IServiceCollection services)
		{
			// ** Authentication ** //
			services.AddAuthentication("Bearer")
				.AddJwtBearer(options =>
				{
					options.Authority = urlBuilderConfiguration.Auth.Url;
					options.Audience = "wpc-netflix-apis";
					options.TokenValidationParameters = new TokenValidationParameters
					{
						NameClaimType = "name",
						RoleClaimType = "role"
					};
				});

			services
				.AddDataProtection()
				.SetApplicationName("wpc-netflix")
				.PersistKeysToRedis(redisConfiguration.Connection, "my-dataProtection-keys");

			services.AddDistributedRedisCache(options =>
			{
				options.Configuration = this.redisConfiguration.ConfigurationOptions.ToString();
			});

			// add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
			// note: the specified format code will format the version as "'v'major[.minor][-status]"
			services
				.AddMvcCore()
				.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");

			services.AddMvc();
			services.AddApiVersioning(o => o.ReportApiVersions = true);

			if (!this.environment.IsProduction())
			{
				services.AddSwaggerGen(
					options =>
					{
						// resolve the IApiVersionDescriptionProvider service
						// note: that we have to build a temporary service provider here because one has not been created yet
						var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

						// add a swagger document for each discovered API version
						// note: you might choose to skip or document deprecated API versions differently
						foreach (var description in provider.ApiVersionDescriptions)
						{
							options.SwaggerDoc(description.GroupName, SwaggerHelper.CreateInfoForApiVersion(description));
						}

						// add a custom operation filter which sets default values
						options.OperationFilter<SwaggerDefaultValues>();

						ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

						// integrate xml comments
						// options.IncludeXmlComments(SwaggerHelper.XmlCommentsFilePath);

						// Authorization API documentation
						options.OperationFilter<AuthResponsesOperationFilter>();
						options.OperationFilter<BadRequestResponseOperationFilter>();
						options.OperationFilter<TagByApiExplorerSettingsOperationFilter>();

						// Authentication
						var oAuth2Scheme = new OAuth2Scheme();
						oAuth2Scheme.AuthorizationUrl = urlBuilderConfiguration.Auth.Url + "/connect/authorize";
						oAuth2Scheme.Flow = "implicit";
						oAuth2Scheme.TokenUrl = urlBuilderConfiguration.Auth.Url + "/connect/authorize";
						oAuth2Scheme.Scopes = new ConcurrentDictionary<string, string>();
						oAuth2Scheme.Scopes.Add(new KeyValuePair<string, string>("admin", "Admin APIs access."));
						oAuth2Scheme.Scopes.Add(new KeyValuePair<string, string>("www", "Www APIs access."));

						options.AddSecurityDefinition("Bearer", oAuth2Scheme);
					});
			}
		}

		public void Configure(IApplicationBuilder app, IApiVersionDescriptionProvider provider)
		{
			if (environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseAuthentication();
			app.UseMvc();

			if (!this.environment.IsProduction())
			{
				app.UseSwagger();
				app.UseSwaggerUI(
					options =>
					{
						// https://gist.github.com/wallymathieu/e149735645232dfc0dd92f8e6fc71f9b
						options.ConfigureOAuth2("wpc-netflix-swagger", "6qP>]jQmdXQURk]fL]p4]%D6(92uSkby", "swagger-ui-realm", "API Swagger UI");

						// build a swagger endpoint for each discovered API version
						foreach (var description in provider.ApiVersionDescriptions)
						{
							options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
						}
					});
			}
		}
	}
}
