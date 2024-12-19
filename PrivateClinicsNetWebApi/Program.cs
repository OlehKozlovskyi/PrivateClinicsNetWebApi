using Microsoft.AspNetCore.Authentication.JwtBearer;
using PrivateClinicsWebNet.DataAccess.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PrivateClinicsWebNet.DataAccess;
using System.Configuration;
using System.Text;

namespace PrivateClinicsNetWebApi
{
    public class Program
    {
        private WebApplicationBuilder _builder;
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
            var configurator = new ServicesConfigurator(_builder);
            configurator.ConfigureServices();
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
                    options.RoutePrefix = string.Empty; // Swagger буде доступний за адресою: /
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
