using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Services;
using UserService.Application.Interfaces;
using UserService.Application.UseCases;
using UserService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserService.API.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using UserService.Application.Validators;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80); // veya 5000 gibi
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//dependecy injection for db
builder.Services.AddHttpClient();

//Services
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

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// UseCases
builder.Services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
builder.Services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
builder.Services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
builder.Services.AddScoped<IGetUserByIdUseCase, GetUserByIdUseCase>();
builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
builder.Services.AddScoped<IUpdateUserStatusUseCase, UpdateUserStatusUseCase>();


builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure()));
var app = builder.Build();

// Configure the HTTP request pipeline.
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
