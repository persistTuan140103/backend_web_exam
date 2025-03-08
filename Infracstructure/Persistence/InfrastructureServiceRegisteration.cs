using Core.Abstraction.UnitOfWork;
using Core.Interfaces.Repositories;
using Google.Apis.Auth.AspNetCore3;
using Infracstructure.Identities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infracstructure.Persistence
{
    public static class InfrastructureServiceRegisteration
    {
        public static IServiceCollection AddDbContextService(this IServiceCollection services,
            IConfiguration configuration)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 36)));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository<ApplicationUser>, UserRepository>();

            return services;
        }

        public static IServiceCollection AddJwtService(this IServiceCollection services,
            IConfiguration configuration)
        {

            var jwtSettings = configuration.GetSection("JwtSettings");
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expireHours = Convert.ToInt32(jwtSettings["ExpireHours"]);
            var publickkeys = jwtSettings.GetSection("Publickeys").GetChildren();

            var validKeys = new List<SecurityKey>();

            foreach (var key in publickkeys)
            {
                var rsa = RSA.Create();
                rsa.ImportFromPem(key.Value.ToCharArray());
                validKeys.Add(new RsaSecurityKey(rsa));
            }
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    string secretKey = configuration["Jwt:SecretKey"];
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateLifetime = true
                        ,
                        ValidAudience = audience,
                        ValidateAudience = false,
                        
                        ValidIssuer = issuer,
                        ValidateIssuer = false,

                        IssuerSigningKeys = validKeys,
                        ValidateIssuerSigningKey = true

                    };
                });
            return services;
        }
        
        public static IServiceCollection AddGoogleService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                // This forces challenge results to be handled by Google OpenID Handler, so there's no
                // need to add an AccountController that emits challenges for Login.
                options.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                // This forces forbid results to be handled by Google OpenID Handler, which checks if
                // extra scopes are required and does automatic incremental auth.
                options.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                // Default scheme that will handle everything else.
                // Once a user is authenticated, the OAuth2 token info is stored in cookies.
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
                .AddCookie()
                .AddGoogleOpenIdConnect(options =>
                {
                    options.ClientId = configuration["Authentication_Google_ClientId"];
                    options.ClientSecret = configuration["Authentication_Google_ClientSecret"];
                });
            return services;
        }
    }
}
