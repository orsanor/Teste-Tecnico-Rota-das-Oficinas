using RO.DevTest.Application;
using RO.DevTest.Application.Features.Products;
using RO.DevTest.Domain.Abstract;
using RO.DevTest.Infrastructure.IoC;
using RO.DevTest.Persistence.IoC;
using RO.DevTest.Persistence.Repositories;

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
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductHandler).Assembly));


        builder.Services.InjectPersistenceDependencies()
            .InjectInfrastructureDependencies();

        // Add Mediatr to program
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(ApplicationLayer).Assembly,
                typeof(Program).Assembly
            );
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}