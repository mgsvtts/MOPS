using Domain.MerchItemAggregate.Repositories;
using Domain.OrderAggregate.Repositories;
using Domain.TypeAggregate.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Presentation.Controllers.MerchItems;
using Web.Extensions.Mapping;

namespace Web.Extensions;

public static class ServiceExtensions
{
    public static WebApplicationBuilder AddDomain(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IMerchItemRepository, MerchItemRepository>();
        builder.Services.AddTransient<ITypeRepository, TypeRepository>();
        builder.Services.AddTransient<IOrderRepository, OrderRepository>();
        builder.Services.AddTransient<IImageRepository, ImageRepository>();

        return builder;
    }

    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<DbContext>();

        return builder;
    }

    public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services.RegisterMapsterConfiguration();

        builder.Services.AddMediator();

        return builder;
    }

    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers().AddApplicationPart(typeof(MerchItemsController).Assembly);

        builder.Services.AddCors();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
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
