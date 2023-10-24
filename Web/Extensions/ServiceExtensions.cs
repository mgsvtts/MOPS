using Domain.MerchItemAggregate.Repositories;
using Domain.OrderAggregate.Repositories;
using Domain.TypeAggregate.Repositories;
using Infrastructure.Repositories;

namespace Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IMerchItemRepository, MerchItemRepository>();
        services.AddTransient<ITypeRepository, TypeRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();

        return services;
    }
}
