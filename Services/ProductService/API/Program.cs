using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Application.UseCases;
using ProductService.Application.Validators;
using SharedKernel.Extensions;
using ProductService.Infrastructure.Persistence;
using ProductService.Infrastructure.Repositories;
using Serilog;
using Shared.Logging.Logging;
using Shared.Messaging.Publisher;
using Shared.Messaging.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// UseCases
builder.Services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
builder.Services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
builder.Services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
builder.Services.AddScoped<IUpdateProductStatusUseCase, UpdateProductStatusUseCase>();
builder.Services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();

// Messaging
builder.Services.AddScoped<IEventPublisher, RabbitMqPublisher>();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Serilog
SerilogConfigurator.Configure("productservice");
builder.Host.UseSerilog();

var app = builder.Build();

// **Migration’ları otomatik uygula**
using (var scope = app.Services.CreateScope())
{
   
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseGlobalExceptionMiddleware();

app.MapControllers();

app.Run();
