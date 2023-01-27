
using Microsoft.AspNetCore.Http.Extensions;

namespace FoodApi.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenMiddleware> _logger;
        private readonly IConfiguration _configuration;
        public TokenMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _next = next;
            _logger = loggerFactory?.CreateLogger<TokenMiddleware>() ??
            throw new ArgumentNullException(nameof(loggerFactory));
            _configuration = configuration;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies[_configuration.GetSection("CookiesKeys")["AccessTokenKey"]];
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
