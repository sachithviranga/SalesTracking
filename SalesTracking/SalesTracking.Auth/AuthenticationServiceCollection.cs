using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SalesTracking.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Auth
{
    public static class AuthenticationServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddScoped<IAuthHelper, AuthHelper>();

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(o =>
            //{
            //    o.Authority = configuration.GetSection("IdentiyServerTunnelConfigs").GetValue<string>("IdentityServerURL");
            //    o.Audience = configuration.GetSection("IdentiyServerTunnelConfigs").GetValue<string>("Audience");
            //    o.RequireHttpsMetadata = Convert.ToBoolean(configuration.GetSection("IdentiyServerTunnelConfigs").GetValue<string>("RequireHttpsMetadata"));
            //    o.TokenValidationParameters.ValidateAudience = false;
            //});

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration.GetSection("TokenSettings").GetValue<string>("Issuer"),
                    ValidateIssuer = true,
                    ValidAudience = Configuration.GetSection("TokenSettings").GetValue<string>("Audience"),
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("TokenSettings").GetValue<string>("Key"))),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });

            return services;
        }
    }
}
