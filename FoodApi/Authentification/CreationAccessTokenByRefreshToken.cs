using BusinessLayer.Services.Interfaces;
using FoodApi.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Utilities.ErrorHandle;

namespace FoodApi.JWTBearEvents
{
    public class CreationAccessTokenByRefreshToken : JwtBearerEvents
    {
        public override async Task Challenge(JwtBearerChallengeContext context)
        {
            try
            {
                if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.HandleResponse();
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    var authService = context.HttpContext.RequestServices
                    .GetRequiredService<IAuthService>();

                    string token = context.HttpContext.Request.Cookies[TokenConstants.CookiesAccessTokenKey];
                    string refreshToken = context.HttpContext.Request.Cookies[TokenConstants.CookiesRefreshTokenKey];
                    if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(refreshToken))
                    {
                        await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new Error((int)ErrorCodes.LoginExpired, "Token can't be refreshed"), new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        }));
                    }
                    else
                    {
                        var newJwtToken = await authService.RefreshToken(accessToken: token, refreshToken: refreshToken);

                        context.HttpContext.Response.Cookies.Append(TokenConstants.CookiesAccessTokenKey, newJwtToken,
                           new CookieOptions
                           {
                               MaxAge = TimeSpan.FromMinutes(60),
                           });

                        await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new Error((int)ErrorCodes.TokenWasRefreshed, "Token was refreshed"), new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        }));
                    }
                }
            }
            catch (Exception ex)
            {
                JsonConvert.SerializeObject(new Error((int)ErrorCodes.ErrorOnRefreshToken, ex.Message), new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });
            }
        }
    }
}
