namespace PrivateClinicsNetWebApi.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var temp = context.GetEndpoint();
            await _next(context);
        }
    }
}
