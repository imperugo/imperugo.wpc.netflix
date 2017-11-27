using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;

namespace imperugo.wpc.netflix.apis.Swagger
{
	internal static class SwaggerHelper
	{
		public static string XmlCommentsFilePath
		{
			get
			{
				var basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
				var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
				return Path.Combine(basePath, fileName);
			}
		}

		public static Info CreateInfoForApiVersion(ApiVersionDescription description)
		{
			var info = new Info()
			{
				Title = $"wpc 2017 demo  {description.ApiVersion}",
				Version = description.ApiVersion.ToString(),
				Description = "All the APIs documented ",
				Contact = new Contact() { Name = "Ugo Lattanzi", Email = "imperugo@gmail.com" },
				TermsOfService = "Need to investigate",
				License = new License()
				{
					Name = "MIT",
					Url = "https://opensource.org/licenses/MIT"
				}
			};

			if (description.IsDeprecated)
			{
				info.Description += " This API version has been deprecated.";
			}

			return info;
		}
	}
}
