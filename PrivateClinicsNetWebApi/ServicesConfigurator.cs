using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using PrivateClinicsWebNet.DataAccess;
using System.Text;
using Microsoft.AspNetCore.Identity;
using PrivateClinicsWebNet.DataAccess.Repositories;

namespace PrivateClinicsNetWebApi
{
    public class ServicesConfigurator
    {
        private readonly WebApplicationBuilder _webApplicationBuilder;
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        public ServicesConfigurator(WebApplicationBuilder builder, IConfiguration configuration)
        {
            _webApplicationBuilder = builder;
            _configuration = configuration;
            _services = _webApplicationBuilder.Services;
        }

        public void ConfigureServices()
        {
            ConfigPostgresDatabase();
            ConfigAuthorization();
            ConfigAuthentication();
            ConfigCustomServices();
            _services.AddOpenApi();
        }

        private void ConfigPostgresDatabase()
        {
            _services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));
        }

        private void ConfigAuthorization()
        {
            _services.AddIdentity<IdentityUser, IdentityRole>()
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
                        ValidIssuer = _configuration["Jwt:Issuer"],
                        ValidAudience = _configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                    };
                });
        }

        private void ConfigCustomServices()
        {
            _services.AddScoped<UserRepository>();
            _services.AddScoped<AuthService>();
        }
    }
}
