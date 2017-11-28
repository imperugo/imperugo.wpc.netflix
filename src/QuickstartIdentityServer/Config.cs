// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Quickstart.UI;

namespace QuickstartIdentityServer
{
	public class Config
	{
		// scopes define the resources in your system
		public static IEnumerable<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
			};
		}

		public static IEnumerable<ApiResource> GetApiResources()
		{
			var wpcApi = new ApiResource("wpc-netflix-apis", "WPC Super Cool APIs");
			wpcApi.Scopes.Add(new Scope("www"));
			wpcApi.Scopes.Add(new Scope("admin"));

			return new List<ApiResource>
			{
				wpcApi
			};
		}

		// clients want to access resources (aka scopes)
		public static IEnumerable<Client> GetClients()
		{
			// client credentials client
			return new List<Client>
			{
				new Client
				{
					ClientName = "wpc-netflix-swagger",
					ClientId = "wpc-netflix-swagger",
					ClientSecrets = new List<Secret>
					{
						new Secret("super-secret-key".Sha256(), null)
					},
					Enabled = true,
					AllowAccessTokensViaBrowser = true,
					AllowedGrantTypes = GrantTypes.Implicit,
					AllowedScopes = new List<string>
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						"admin",
						"www"
					},
					RedirectUris = new List<string>()
					{
						"http://localhost:50584/swagger/o2c.html"
					},
					AccessTokenType = AccessTokenType.Jwt
				},
				new Client
				{
					ClientName = "wpc-netflix-www",
					ClientId = "wpc-netflix-www",
					AllowAccessTokensViaBrowser = true,
					RequireConsent = true,
					RedirectUris = new List<string>(),
					AllowedGrantTypes = GrantTypes.Implicit,
					AlwaysIncludeUserClaimsInIdToken = true,
					Enabled = true,
					AllowedScopes = new List<string>
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						"www"
					},
					AccessTokenType = AccessTokenType.Jwt,
				},

			};
		}

		public static List<TestUser> GetUsers()
		{
			return TestUsers.Users;
		}
	}
}