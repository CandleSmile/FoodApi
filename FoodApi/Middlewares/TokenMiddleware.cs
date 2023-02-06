using FoodApi.Configuration;
using Microsoft.AspNetCore.Http.Extensions;

namespace FoodApi.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenMiddleware> _logger;

        public TokenMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<TokenMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies[TokenConstants.CookiesAccessTokenKey];
            if (!string.IsNullOrEmpty(token))
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            _logger.LogInformation($"Request URL: {UriHelper.GetDisplayUrl(context.Request)}");
            await this._next(context);
        }
    }

    public static class TokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TokenMiddleware>();
        }
    }
}
