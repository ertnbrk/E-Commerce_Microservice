using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderService.API.Middleware;
using OrderService.Application.Interfaces;
using OrderService.Application.UseCases;
using OrderService.Application.Validators;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Repositories;
using OrderService.Infrastructure.Services;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



//Add JWT Bearer
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductService, ProductHttpService>();
builder.Services.AddScoped<IUserService, UserHttpService>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();
builder.Services.AddScoped<IOrderUpdateService, OrderUpdateService>();

builder.Services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
builder.Services.AddScoped<IGetAllOrdersUseCase, GetAllOrdersUseCase>();
builder.Services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();
builder.Services.AddScoped<IGetOrdersByStatusUseCase, GetOrdersByStatusUseCase>();
builder.Services.AddScoped<IUpdateOrderStatusUseCase, UpdateOrderStatusUseCase>();
builder.Services.AddScoped<IDeleteOrderUseCase, DeleteOrderUseCase>();
builder.Services.AddScoped<IGetOrdersByUserIdUseCase, GetOrdersByUserIdUseCase>();
builder.Services.AddScoped<IGetOrdersByProductIdUseCase, GetOrdersByProductIdUseCase>();



builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();




builder.Services.AddHttpClient();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseGlobalExceptionMiddleware();
app.MapControllers();

app.Run();
