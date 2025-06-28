using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Services;
using UserService.Application.Interfaces;
using UserService.Application.UseCases;
using UserService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SharedKernel.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using UserService.Application.Validators;
using Serilog;
using Shared.Logging.Logging;
using Shared.Messaging.Publisher;
using Shared.Messaging.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Kestrel port
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});

// MVC / Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// HTTP Client
builder.Services.AddHttpClient();

// Serilog
SerilogConfigurator.Configure("userservice");
builder.Host.UseSerilog();

// Messaging
builder.Services.AddScoped<IEventPublisher, RabbitMqPublisher>();

// JWT Auth
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });
builder.Services.AddAuthorization();

// Repositories & UseCases
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
builder.Services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
builder.Services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
builder.Services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
builder.Services.AddScoped<IUpdateUserStatusUseCase, UpdateUserStatusUseCase>();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

// DbContext with retry
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure()));

var app = builder.Build();

// **Migration’ları otomatik uygula**
using (var scope = app.Services.CreateScope())
{
   
}

// Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseGlobalExceptionMiddleware();

app.MapControllers();

app.Run();
