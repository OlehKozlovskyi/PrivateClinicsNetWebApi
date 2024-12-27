using Microsoft.AspNetCore.Authentication.JwtBearer;
using PrivateClinicsWebNet.DataAccess.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PrivateClinicsWebNet.DataAccess;
using System.Configuration;
using System.Text;
using PrivateClinicsNetWebApi.Extensions;
using PrivateClinicsWebNet.BusinessLogic.Entities;

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
            _configuration = _builder.Configuration;
            _services = _builder.Services;
            _services.AddJwtTokenSettings("JwtSecurityTokenSettings", _configuration);
            _services.AddPostgresDb(_configuration);
            _services.AddUserAuthorization();
            _services.AddUserAuthentication(_configuration);
            _services.AddCustomServices();
            _services.AddMappers();
            _services.AddSwagger();
            _builder.Services.AddControllers();
            var app = _builder.Build();
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
