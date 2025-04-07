using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MinBankAPI.Data;
using MinBankAPI.Interfaces;
using MinBankAPI.Repositories;
using MinBankAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Added services to the container.
builder.Services.AddDbContext<AccountsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registered repositories and services
builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddScoped<IAPIAuthRepository, APIAuthService>();

// AdAddedd Authentication (JWT)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        var jwtKey = builder.Configuration["JwtSettings:SecretKey"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Added Authorization
builder.Services.AddAuthorization();

// Added controllers
builder.Services.AddControllers();

// Added Swagger for API Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Bank Accounts API",
        Version = "v1"
    });

    // Added SecurityDefinition for JWT Bearer authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    // Applied the security definition to all endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

var app = builder.Build();

// Applied pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AccountsDbContext>();
    dbContext.Database.Migrate(); // This applies any pending migrations
}

// Configured the HTTP request pipeline for Swagger UI and Authentication
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Bank Accounts V1");
        c.RoutePrefix = string.Empty;  // Optional: This will serve Swagger UI at the root URL (Ran into issues of the swagger UI not showing)
    });
}

app.UseHttpsRedirection();

// Added Authentication and Authorization middleware
app.UseAuthentication();  
app.UseAuthorization();

// Mapped controllers
app.MapControllers();

app.Run();
