using Application.Interfaces.Authentication;
using Application.Interfaces.Persistance;
using Application.Interfaces.Services;
using Infrastructure.Authentication;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {

            services.AddDbContext<PatmsDbContext>();
            services.AddAuth(configuration);
            services.AddPersistance();
            services.AddSingleton<IDateTimeProvider, RealDateTimeProvider>();

            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            // add repos (scoped)
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            JwtSettings jwtSettings = new();
            configuration.Bind(nameof(JwtSettings), jwtSettings);

            _ = services.AddSingleton(Options.Create(jwtSettings));
            _ = services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            _ = services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                });
            return services;
        }
    }
}
