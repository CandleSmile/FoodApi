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

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<FoodContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors();

//get data for tokens from Configuration
string? tokenKey = builder.Configuration.GetSection("Jwt")["Key"];
string? issuer = builder.Configuration.GetSection("Jwt")["Issuer"];
string? audience = builder.Configuration.GetSection("Jwt")["Audience"];

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {        
        options.TokenValidationParameters = new TokenValidationParameters
        {             
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey??"")),
            ValidateIssuerSigningKey = true,
            
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

app.UseCors(builder => builder.AllowAnyOrigin());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
