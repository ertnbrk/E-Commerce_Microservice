using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using ShippingService.Application.Interfaces;
using ShippingService.Application.UseCases;
using ShippingService.Infrastructure.Data;
using ShippingService.Infrastructure.Repositories;
using Shared.Logging.Logging;
using Shared.Messaging.Configuration;
using Shared.Messaging.Infrastructure;
using Shared.Messaging.Publisher;
using SharedKernel.Extensions;
using System.Text;
using ShippingService.Infrastructure.Services;
using ShippingService.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

/* JWT
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
*/
builder.Services.AddAuthorization();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Serilog
SerilogConfigurator.Configure("shippingservice");
builder.Host.UseSerilog();
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});
// DB
builder.Services.AddDbContext<ShippingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), opt =>
    {
        opt.CommandTimeout(60);
    }));

// Repositories & UseCases
builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
builder.Services.AddScoped<ICreateShipmentUseCase, CreateShipmentUseCase>();
builder.Services.AddScoped<IGetAllShipmentsUseCase, GetAllShipmentsUseCase>();
builder.Services.AddScoped<IGetShipmentByIdUseCase, GetShipmentByIdUseCase>();
builder.Services.AddScoped<IUpdateShipmentStatusUseCase, UpdateShipmentStatusUseCase>();

// RabbitMQ Publisher
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMqSettings"));
builder.Services.AddScoped<IEventPublisher, RabbitMqPublisher>();

// Hosted Service (eğer Outbox kullanılacaksa)
builder.Services.AddScoped<IOutboxRepository, OutboxRepository>();
builder.Services.AddHostedService<OutboxPublisherService>();

// Queue initializer
builder.Services.AddSingleton<QueueInitializer>();

// Validation
builder.Services.AddValidatorsFromAssemblyContaining<CreateShipmentDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

// HTTP Client (örnek)
builder.Services.AddHttpClient();

// Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Swagger (dev only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseAuthentication();
app.UseAuthorization();
app.UseGlobalExceptionMiddleware();
app.MapControllers();

// Queue setup & migration
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<QueueInitializer>();
    initializer.EnsureQueuesExist();

    var db = scope.ServiceProvider.GetRequiredService<ShippingDbContext>();
    db.Database.Migrate();
}

app.Run();
