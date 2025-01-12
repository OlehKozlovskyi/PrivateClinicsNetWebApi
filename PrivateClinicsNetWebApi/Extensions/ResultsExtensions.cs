using Microsoft.AspNetCore.Mvc;
using PrivateClinicsWebNet.Infrastructure.Shared.Wrapper;

namespace PrivateClinicsNetWebApi.Extensions
{
    public static class ResultsExtensions
    {
        public static IActionResult ToResponse<T>(this Result<T> result)
        {
            if(result.IsSucceess)
                return new OkObjectResult(result.Data);
            return new BadRequestObjectResult(result.errorMessage);
        }
    }
}
