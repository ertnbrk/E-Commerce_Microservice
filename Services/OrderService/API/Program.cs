using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderService.Application.Interfaces;
using OrderService.Application.UseCases;
using OrderService.Application.Validators;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Repositories;
using OrderService.Infrastructure.Services;
using System.Text;
using SharedKernel.Extensions;
using Serilog;
using Shared.Logging.Logging;
using Shared.Messaging.Publisher;
using Shared.Messaging.Infrastructure;
using Shared.Messaging.Configuration;
using OrderService.Infrastructure.BackgroundServices;


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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),opt => {
        opt.CommandTimeout(60);
    }));

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

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddScoped<IEventPublisher, RabbitMqPublisher>();

builder.Services.AddScoped<IOutboxRepository, OutboxRepository>();
builder.Services.AddHostedService<OutboxPublisherService>();

builder.Services.AddSingleton<QueueInitializer>();


builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

SerilogConfigurator.Configure("orderservice");
builder.Host.UseSerilog();


builder.Services.AddHttpClient();

builder.Services.AddHttpClient<IProductService, ProductHttpService>(client =>
{
    client.BaseAddress = new Uri("http://productservice"); // Docker içi isimlendirme!
});

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




using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<QueueInitializer>();
   // initializer.EnsureQueuesExist();
    var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
    db.Database.Migrate();
}


app.Run();
