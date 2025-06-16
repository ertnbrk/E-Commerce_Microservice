using FluentValidation;
using FluentValidation.AspNetCore;
using Serilog;
using Shared.Logging.Logging;
using Shared.Messaging.Configuration;
using Shared.Messaging.Infrastructure;
using Shared.Messaging.Publisher;
using PaymentService.Application.Interfaces;
using PaymentService.Infrastructure.Persistence;
using PaymentService.Infrastructure.Repositories;
using PaymentService.Infrastructure.BackgroundServices;
using PaymentService.API.Middleware;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.UseCases;
using PaymentService.Application.UseCases;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});
// Logging
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment API", Version = "v1" });
});


builder.Services.AddValidatorsFromAssemblyContaining<CreatePaymentDtoValidator>();

// DbContext
builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IOutboxRepository, OutboxRepository>();


// UseCases
// UseCases
builder.Services.AddScoped<ICreatePaymentUseCase, CreatePaymentUseCase>();
builder.Services.AddScoped<IUpdatePaymentUseCase, UpdatePaymentUseCase>();
builder.Services.AddScoped<IDeletePaymentUseCase, DeletePaymentUseCase>();
builder.Services.AddScoped<IGetAllPaymentsUseCase, GetAllPaymentsUseCase>();
builder.Services.AddScoped<IGetPaymentByIdUseCase, GetPaymentByIdUseCase>();
builder.Services.AddScoped<IGetPaymentStatusUseCase, GetPaymentByStatusUseCase>();
builder.Services.AddScoped<IUpdatePaymentStatusUseCase, UpdatePaymentStatusUseCase>();
builder.Services.AddScoped<ICancelPaymentUseCase, CancelPaymentUseCase>();


// RabbitMQ Messaging
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddSingleton<QueueInitializer>();
builder.Services.AddScoped<IEventPublisher, RabbitMqPublisher>();

// Background Service for Outbox pattern
builder.Services.AddHostedService<OutboxPublisherService>();

// FluentValidation (ekleyeceksen buraya)
builder.Services.AddValidatorsFromAssemblyContaining<CreatePaymentDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Middleware
//builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();

// Pipeline
//app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// RabbitMQ queue setup
using (var scope = app.Services.CreateScope())
{
    try
    {
        var queueInitializer = scope.ServiceProvider.GetRequiredService<QueueInitializer>();
        queueInitializer.EnsureQueuesExist();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Queue initialization failed. RabbitMQ may be unavailable.");
        // Uygulamanın çalışmaya devam etmesini istiyorsan burayı boş bırak.
    }
}


app.Run();
