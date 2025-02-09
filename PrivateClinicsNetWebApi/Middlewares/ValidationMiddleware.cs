using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using PrivateClinicsNetWebApi.Controllers;
using PrivateClinicsWebNet.Application.DTOs.AppointmentDTOs;
using PrivateClinicsWebNet.Application.Exceptions;
using System.ComponentModel.DataAnnotations;
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
        private readonly IEnumerable<IValidator> _validators;

        public ValidationMiddleware(RequestDelegate next, IEnumerable<IValidator> validators)
        {
            _next = next;
            _validators = validators;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var requestUrl = request.Path.ToString();
            context.Request.EnableBuffering();

            if (requestUrl.Contains(_appointmentsPath))
            {
                await ValidateAppointmentRequest(request);
            }

            await _next(context);
        }

        private async Task ValidateAppointmentRequest(HttpRequest httpRequest)
        {
            Type type = typeof(AppointmentController);
            var actionMethod = httpRequest.RouteValues["action"].ToString();
            MethodInfo method = type.GetMethod(actionMethod);
            ParameterInfo[] listOfParameters = method.GetParameters();
            
            foreach (var parameter in listOfParameters)
            {
                var parameterType = parameter.ParameterType;
                var result = await IsRequestValid(parameter, httpRequest);
            }
        }

        private async Task<bool> IsRequestValid(ParameterInfo parameter, HttpRequest httpRequest)
        {
            ValidationResult validationResult;

            if (parameter.ParameterType == typeof(CreateAppointmentDto))
            {
                validationResult = await ValidateRequestAsync<CreateAppointmentDto>(httpRequest);
                if (!validationResult.IsValid)
                    throw new InvalidAppointmentDataException();
            }

            if (parameter.ParameterType == typeof(PageRequestDto))
            {
                validationResult = await ValidateRequestAsync<PageRequestDto>(httpRequest);
                if (!validationResult.IsValid)
                    throw new InvalidPageAndPageSizeException();
            }
                 
            if (parameter.ParameterType == typeof(UpdateAppointmentDto))
            {
                validationResult = await ValidateRequestAsync<UpdateAppointmentDto>(httpRequest);
                if (!validationResult.IsValid)
                    throw new InvalidAppointmentDataException();
            }
                
            return true;
        }

        private async Task<ValidationResult> ValidateRequestAsync<T>(HttpRequest httpRequest)
        {
            var validator = _validators.OfType<IValidator<T>>().SingleOrDefault();
            var requestBody = await JsonSerializer.DeserializeAsync<T>(httpRequest.Body);
            return await validator.ValidateAsync(requestBody);
        }
    }
}
