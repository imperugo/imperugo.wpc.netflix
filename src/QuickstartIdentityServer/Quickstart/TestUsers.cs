// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer4.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser{SubjectId = "818727", Username = "imperugo", Password = "12345", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Ugo Lattanzi"),
                    new Claim(JwtClaimTypes.GivenName, "Ugo"),
                    new Claim(JwtClaimTypes.FamilyName, "Lattanzi"),
                    new Claim(JwtClaimTypes.Email, "imperugo@gmail.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://tostring.it"),
                }
            },
            new TestUser{SubjectId = "88421113", Username = "ilGaggi", Password = "12345", 
                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Gabriele Gaggi"),
                    new Claim(JwtClaimTypes.GivenName, "Gabriele"),
                    new Claim(JwtClaimTypes.FamilyName, "Gaggi"),
                    new Claim(JwtClaimTypes.Email, "Gabriele.Gaggi@brain-sys.it"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://brain-sys.it"),
                }
            },
	        new TestUser{SubjectId = "88421113", Username = "igor", Password = "12345",
		        Claims =
		        {
			        new Claim(JwtClaimTypes.Name, "Igor Damiani"),
			        new Claim(JwtClaimTypes.GivenName, "Igo"),
			        new Claim(JwtClaimTypes.FamilyName, "Damiani"),
			        new Claim(JwtClaimTypes.Email, "Igor.Damiani@brain-sys.it"),
			        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
			        new Claim(JwtClaimTypes.WebSite, "http://brain-sys.it"),
		        }
	        }
		};
    }
}