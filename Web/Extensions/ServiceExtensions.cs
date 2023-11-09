using CloudinaryDotNet;
using Domain.MerchItemAggregate.Repositories;
using Domain.OrderAggregate.Repositories;
using Domain.TypeAggregate.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Presentation.Controllers;
using Web.Extensions.Mapping;

namespace Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddTransient<IMerchItemRepository, MerchItemRepository>();
        services.AddTransient<ITypeRepository, TypeRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IImageRepository, ImageRepository>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DbContext>();


        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.RegisterMapsterConfiguration();

        services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Application.Common.AssemblyReference).Assembly));

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers().AddApplicationPart(typeof(MerchItemsController).Assembly);

        services.AddCors();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
        .AllowAnyHeader());

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}
