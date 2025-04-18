using FluentValidation;
using RO.DevTest.Application;
using RO.DevTest.Application.Features.Products.Commands.CreateProductsCommand;
using RO.DevTest.Application.Features.Products.Commands.DeleteProductsCommand;
using RO.DevTest.Application.Features.Products.Commands.UpdateProductsCommand;
using RO.DevTest.Application.Interfaces;
using RO.DevTest.Domain.Abstract;
using RO.DevTest.Infrastructure.IoC;
using RO.DevTest.Infrastructure.Security;
using RO.DevTest.Persistence.IoC;
using RO.DevTest.Persistence.Repositories;
using RO.DevTest.WebApi.Middlewares;

namespace RO.DevTest.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.InjectPersistenceDependencies()
            .InjectInfrastructureDependencies();
        builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        // Add Mediatr to program
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationLayer).Assembly,
                typeof(Program).Assembly
            );
        });

        builder.Services.AddScoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
        builder.Services.AddScoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteProductCommandHandler).Assembly));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}