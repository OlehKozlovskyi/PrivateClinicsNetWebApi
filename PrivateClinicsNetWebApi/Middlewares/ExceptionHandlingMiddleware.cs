using Microsoft.AspNetCore.Http;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PrivateClinicsWebNet.Infrastructure;
using PrivateClinicsWebNet.Infrastructure.AppointmentInfrastructure.Exceptions;
using System.Net;
using PrivateClinicsWebNet.BusinessLogic.Exceptions;

namespace PrivateClinicsNetWebApi.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandleExceptionAsync(context, error);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = Result<string>.Failure(error.Message);
            switch (error)
            {
                case UnsupportedAppointmentUserException e:
                    response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;
                case InvalidUserRoleException e:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                case InvalidUserTypeException e:
                    response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;
                default:
                    response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);
        }
    }
}
