using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using PrivateClinicsWebNet.DataAccess;
using PrivateClinicsWebNet.BusinessLogic.Repositories;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;
using PrivateClinicsWebNet.Application.Services;
using PrivateClinicsWebNet.DataAccess.Services;
using PrivateClinicsWebNet.DataAccess.Abstractions;
using PrivateClinicsWebNet.BusinessLogic.Abstractions;
using PrivateClinicsWebNet.DataAccess.Entities;
using Microsoft.Extensions.Options;

namespace PrivateClinicsNetWebApi
{
    public class ServicesConfigurator
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        public ServicesConfigurator(WebApplicationBuilder builder)
        {
            _configuration = builder.Configuration;
            _services = builder.Services;
        }

        public void ConfigureServices()
        {
            ConfigJwtTokenSettings();
            ConfigPostgresDatabase();
            ConfigAuthorization();
            ConfigAuthentication();
            ConfigCustomServices();
            ConfigSwagger();
        }

        private void ConfigPostgresDatabase()
        {
            _services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));
        }

        private void ConfigJwtTokenSettings()
        {
            _services.Configure<JwtSecurityTokenSettings>(
                _configuration.GetSection("JwtSecurityTokenSettings"));
        }

        private void ConfigAuthorization()
        {
            _services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        private void ConfigAuthentication()
        {
            _services.AddAuthentication(options =>
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
                        ValidIssuer = _configuration["JwtSecurityTokenSettings:Issuer"],
                        ValidAudience = _configuration["JwtSecurityTokenSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityTokenSettings:Key"]))
                    };
                });
        }

        private void ConfigCustomServices()
        {
            _services.AddScoped<IUserRepository, UserRepository>();
            _services.AddScoped<IRoleRepository, RoleRepository>();
            _services.AddScoped<AuthService>();
            _services.AddScoped<RoleService>();
            _services.AddScoped<ITokenService, JwtTokenService>();
        }

        private void ConfigSwagger()
        {
            _services.AddEndpointsApiExplorer();
            _services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Private clinics network WebApi",
                    Version = "v1",
                    Description = "Task 1.Implementation of User Authorization and Registration"
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
        }
    }
}
