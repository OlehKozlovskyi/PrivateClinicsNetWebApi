using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PrivateClinicsNetWebApi.Controllers;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
using PrivateClinicsWebNet.Application.Exceptions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace PrivateClinicsNetWebApi.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _appointmentsPath = @"/appointments";
        private readonly IServiceProvider _serviceProvider;

        public ValidationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var requestUrl = request.Path.ToString();
            context.Request.EnableBuffering();

            if (requestUrl.Contains(_appointmentsPath))
            {
                await ProcessAppointmentValidation(request);
            }

            await _next(context);
        }

        private async Task ProcessAppointmentValidation(HttpRequest httpRequest)
        {
            Type type = typeof(AppointmentController);
            var actionMethod = httpRequest.RouteValues["action"].ToString();
            MethodInfo method = type.GetMethod(actionMethod);
            ParameterInfo[] listOfParameters = method.GetParameters();
            
            foreach (var parameter in listOfParameters)
            {
                var parameterType = parameter.ParameterType;
                await ValidateParameterIfApplicable(parameter, httpRequest);
            }
        }

        private async Task ValidateParameterIfApplicable(ParameterInfo parameter, HttpRequest httpRequest)
        {
            if (parameter.ParameterType == typeof(CreateAppointmentDto))
                await ValidateRequestAsync<CreateAppointmentDto>(httpRequest);

            if (parameter.ParameterType == typeof(PageRequestDto))
                await ValidateRequestAsync<PageRequestDto>(httpRequest);
   
            if (parameter.ParameterType == typeof(UpdateAppointmentDto))
                await ValidateRequestAsync<UpdateAppointmentDto>(httpRequest);
        }

        private async Task ValidateRequestAsync<T>(HttpRequest httpRequest)
        {
            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetService<IValidator<T>>();
            var requestBody = await JsonSerializer.DeserializeAsync<T>(httpRequest.Body);
            var validationResult = await validator.ValidateAsync(requestBody);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
