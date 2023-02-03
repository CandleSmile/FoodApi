using AutoMapper;
using BusinessLayer;
using BusinessLayer.Services.Implementation;
using BusinessLayer.Services.Interfaces;
using DataLayer;
using DataLayer.Repositories.Implementation;
using DataLayer.Repositories.Interfaces;
using FoodApi.Configuration;
using FoodApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using Utilities.ErrorHandle;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// get data for tokens from Configuration
string? tokenKey = builder.Configuration.GetSection("Jwt")["Key"];
string? issuer = builder.Configuration.GetSection("Jwt")["Issuer"];
string? audience = builder.Configuration.GetSection("Jwt")["Audience"];

// get data for token cookies keys from Configuration
string? accessTokenKey = builder.Configuration.GetSection("CookiesKeys")["AccessTokenKey"];
string? refreshTokenKey = builder.Configuration.GetSection("CookiesKeys")["RefreshTokenKey"];

// get allowed-origin from Configuration
string? origin = builder.Configuration["Allowed-origin"];

//get pathes to Images
PathToImages pathToImages = builder.Configuration.GetSection("PathToImages").Get<PathToImages>();

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<FoodContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ICategoryService>(x => new CategoryService(pathToImages.ImagePath, x.GetRequiredService<IUnitOfWork>(),
                x.GetRequiredService<IMapper>()));
builder.Services.AddTransient<IIngredientService, IngredientService>();
builder.Services.AddTransient<IMealService>(x => new MealService(pathToImages.ImagePath, pathToImages.ThumbnailPath, x.GetRequiredService<IUnitOfWork>(),
                x.GetRequiredService<IMapper>()));
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IDbLoaderService, DbLoaderService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowFoodApp",
        policy =>
                      {
                          if (!string.IsNullOrEmpty(origin))
                          {
                              policy.WithOrigins(origin.Split(',')).AllowCredentials().AllowAnyMethod().AllowAnyHeader();
                          }
                          else
                          {
                              policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                          }
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey ?? string.Empty)),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = async (context) =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException) && !string.IsNullOrEmpty(accessTokenKey) && !string.IsNullOrEmpty(refreshTokenKey))
                {
                    try
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
                        var authService = context.HttpContext.RequestServices
                        .GetRequiredService<IAuthService>();

                        string token = context.HttpContext.Request.Cookies[accessTokenKey];
                        string refreshToken = context.HttpContext.Request.Cookies[refreshTokenKey];
                        var newJwtToken = await authService.RefreshToken(accessToken: token ?? string.Empty, refreshToken: refreshToken ?? string.Empty);

                        context.HttpContext.Response.Cookies.Append(accessTokenKey, newJwtToken,
                           new CookieOptions
                           {
                               MaxAge = TimeSpan.FromMinutes(60),
                           });

                        await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new Error((int)ErrorCodes.TokenWasRefreshed, "Token was refreshed"), new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        }));
                    }
                    catch (Exception ex)
                    {
                        JsonConvert.SerializeObject(new Error((int)ErrorCodes.ErrorOnRefreshToken, ex.Message), new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        });
                    }
                }
            },
        };
    });

// swagger authorization
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
});

app.UseHttpsRedirection();
app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
    RequestPath = new PathString("/files"),
    EnableDefaultFiles = true,
});
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always,
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