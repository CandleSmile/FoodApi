namespace FoodApi.Middlewares
{
    using System.Net;
    using FoodApi.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
