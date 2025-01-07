using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrivateClinicsWebNet.Application.Services;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.BusinessLogic.Repositories;
using PrivateClinicsWebNet.DataAccess;
using PrivateClinicsWebNet.DataAccess.Abstractions;
using PrivateClinicsWebNet.DataAccess.Entities;
using PrivateClinicsWebNet.DataAccess.Services;
using PrivateClinicsWebNet.Application.Abstractions;
using System.Text;
using PrivateClinicsWebNet.BusinessLogic.Factories;
using PrivateClinicsWebNet.Infrastructure.Migrator.Models;
using PrivateClinicsWebNet.Infrastructure.Migrator.Abstractions;
using PrivateClinicsWebNet.Infrastructure.Migrator.Services;

namespace PrivateClinicsNetWebApi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddPostgresDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection AddJwtTokenSettings(this IServiceCollection services,
            string sectionName, IConfiguration configuration)
        {
            services.Configure<JwtSecurityTokenSettings>(
                configuration.GetSection(sectionName));
            return services;
        }

        public static IServiceCollection AddUserMigrationOptions(this IServiceCollection services,
            string sectionName, IConfiguration configuration)
        {
            services.Configure<UserMigrationOptions>(
                configuration.GetSection(sectionName));
            return services;
        }

        public static IServiceCollection AddUserAuthorization(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        public static IServiceCollection AddUserAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSecurityTokenSettings:Issuer"],
                        ValidAudience = configuration["JwtSecurityTokenSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityTokenSettings:Key"]))
                    };
                });
            return services;
        }

        public static IServiceCollection AddMappers(this IServiceCollection services)
        {
            return services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IFileReader, JsonFileReader>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ITokenService, JwtTokenService>();
            services.AddScoped<IDataMigrationService, DataMigrationService>();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Private clinics network WebApi",
                    Version = "v1",
                    Description = "Task 3.Implementation data migration"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter bearer token"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });
            return services;
        }
    }
}
