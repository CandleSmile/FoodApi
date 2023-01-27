using BusinessLayer;
using BusinessLayer.Services.Implementation;
using BusinessLayer.Services.Interfaces;
using DataLayer;
using DataLayer.Repositories.Implementation;
using DataLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.CookiePolicy;
using FoodApi.Middlewares;
using BusinessLayer.Contracts;
using Newtonsoft.Json;
using FoodApi.Models;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<FoodContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IDbLoaderService, DbLoaderService>();
//get allowed-origin from Configuration
string? origin = builder.Configuration["Allowed-origin"];
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowFoodApp",
                      policy =>
                      {
                      if (!String.IsNullOrEmpty(origin))
                          policy.WithOrigins(origin.Split(',')).AllowCredentials().AllowAnyMethod().AllowAnyHeader();
                          else
                              policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();

                      });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

//get data for tokens from Configuration
string? tokenKey = builder.Configuration.GetSection("Jwt")["Key"];
string? issuer = builder.Configuration.GetSection("Jwt")["Issuer"];
string? audience = builder.Configuration.GetSection("Jwt")["Audience"];

//get data for token cookies keys from Configuration
string? accessTokenKey = builder.Configuration.GetSection("CookiesKeys")["AccessTokenKey"];
string? refreshTokenKey = builder.Configuration.GetSection("CookiesKeys")["RefreshTokenKey"];

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {             
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey??"")),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = async (context) =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    try
                    {

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";


                        var authService = context.HttpContext.RequestServices
                        .GetRequiredService<IAuthService>();

                        string token = context.HttpContext.Request.Cookies[accessTokenKey];
                        string refreshToken = context.HttpContext.Request.Cookies[refreshTokenKey];
                        (var newJwtToken, var newRefreshToken) = await authService.RefreshTokens(token, refreshToken);

                        context.HttpContext.Response.Cookies.Append(accessTokenKey, newJwtToken,
                           new CookieOptions
                           {
                               MaxAge = TimeSpan.FromMinutes(60)
                           });
                        context.HttpContext.Response.Cookies.Append(refreshTokenKey, newRefreshToken,
                            new CookieOptions
                            {
                                MaxAge = TimeSpan.FromDays(1)
                            });

                        await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new Error((int)ErrorCodes.TokenWasRefreshed, "Token was refreshed"),new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }));
                    }
                    catch (Exception ex)
                    {
                        JsonConvert.SerializeObject(new Error((int)ErrorCodes.ErrorOnRefreshToken, ex.Message), new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        });
                    }
                }
            },
            OnChallenge = context =>
            {
                context.HandleResponse();
                return Task.CompletedTask;
            }
        };
    });

//swagger authorization
builder.Services.AddSwaggerGen(swagger =>
{
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseRouting();
app.UseCors();
app.UseToken();

app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandlerMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
