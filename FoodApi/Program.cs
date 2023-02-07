using AutoMapper;
using BusinessLayer;
using BusinessLayer.Services.Implementation;
using BusinessLayer.Services.Interfaces;
using DataLayer;
using DataLayer.Repositories.Implementation;
using DataLayer.Repositories.Interfaces;
using FoodApi.Configuration;
using FoodApi.JWTBearEvents;
using FoodApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

AppSettings settings = builder.Configuration.Get<AppSettings>();

string? connection = settings.ConnectionString;

// get data for tokens from Configuration
string? tokenKey = settings.Jwt.Key;
string? issuer = settings.Jwt.Issuer;
string? audience = settings.Jwt.Audience;

// get allowed-origin from Configuration
string? origin = settings.AllowedOriginUrl;

//get pathes to Images
PathToImages pathToImages = settings.PathToImages;

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<FoodContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthService>(x => new AuthService(x.GetRequiredService<IUnitOfWork>(), x.GetRequiredService<IMapper>(),
    x.GetRequiredService<IConfiguration>(), x.GetRequiredService<IHttpContextAccessor>(), TokenConstants.CookiesAccessTokenKey, TokenConstants.CookiesRefreshTokenKey));
builder.Services.AddTransient<ICategoryService>(x => new CategoryService(pathToImages.ImagePath, x.GetRequiredService<IUnitOfWork>(),
                x.GetRequiredService<IMapper>()));
builder.Services.AddTransient<IIngredientService, IngredientService>();
builder.Services.AddTransient<IMealService>(x => new MealService(pathToImages.ImagePath, pathToImages.ThumbnailPath, x.GetRequiredService<IUnitOfWork>(),
                x.GetRequiredService<IMapper>()));
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IDbLoaderService, DbLoaderService>();
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.Configure<PathToImages>(builder.Configuration.GetSection("PathToImages"));
builder.Services.AddScoped<CreationAccessTokenByRefreshToken>();

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
        options.EventsType = typeof(CreationAccessTokenByRefreshToken);
    });
builder.Services.AddAuthorization();

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