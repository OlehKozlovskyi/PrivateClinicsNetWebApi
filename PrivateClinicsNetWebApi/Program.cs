using PrivateClinicsNetWebApi.Extensions;
using PrivateClinicsWebNet.Infrastructure.Migrator.Models;
using PrivateClinicsWebNet.DataAccess.Entities;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using PrivateClinicsNetWebApi.Middlewares;

namespace PrivateClinicsNetWebApi
{
    public class Program
    {
        private WebApplicationBuilder _builder;
        private IServiceCollection _services;
        private IConfiguration _configuration;

        public static void Main(string[] args)
        {
            var program = new Program();

            program.Run(args);
        }

        private void Run(string[] args)
        {
            _builder = WebApplication.CreateBuilder(args);
            var jwtSettings = new JwtSecurityTokenSettings();
            _builder.Configuration.GetSection("JwtSecurityTokenSettings").Bind(jwtSettings);
            _configuration = _builder.Configuration;
            _builder.Logging.ClearProviders();
            _builder.Logging.AddConsole();
            _services = _builder.Services;
            _services.AddJwtTokenSettings("JwtSecurityTokenSettings", _configuration);
            _services.AddUserMigrationOptions(nameof(UserMigrationOptions), _configuration);
            _services.AddPostgresDb(_configuration);
            _services.AddUserAuthorization();
            _services.AddUserAuthentication(jwtSettings);
            _services.AddCustomServices();
            _services.AddMappers();
            _services.AddSwagger();
            _builder.Services.AddControllers();
            var app = _builder.Build();
            app.UseMiddleware<ValidationMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation v1");
                    options.RoutePrefix = string.Empty;
                });
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
