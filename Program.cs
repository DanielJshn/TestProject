using System.Text;
using System.Text.Json;
using apief;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") 
                  .AllowAnyMethod()  
                  .AllowAnyHeader(); 
        });
});

builder.Services.AddDbContext<DataContextEF>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
string? tokenKeyString = builder.Configuration.GetSection(AuthHelp.KEY_TOKEN_KEY).Value;

if (string.IsNullOrEmpty(tokenKeyString))
{
    throw new ArgumentException("TokenKey is not configured.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:TokenKey"])),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:ValidIssuer"],
            ValidAudience = builder.Configuration["JwtSettings:ValidAudience"],
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception is SecurityTokenExpiredException)
                {
                    var apiResponse = new ApiResponse(success: false, message: "Token has expired");
                    var jsonResponse = JsonSerializer.Serialize(apiResponse);

                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync(jsonResponse);
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ILog, Log>();
builder.Services.AddScoped<IIdentityUser, IdentityUser>();

builder.Services.AddScoped<IAuthHelp, AuthHelp>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();

var app = builder.Build();

app.UseCors("AllowAngular");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
