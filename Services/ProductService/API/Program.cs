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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

builder.Services.AddScoped<IEventPublisher, RabbitMqPublisher>();



builder.Services.AddValidatorsFromAssemblyContaining<ProductDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();



SerilogConfigurator.Configure("productservice");
builder.Host.UseSerilog();


var app = builder.Build();

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
