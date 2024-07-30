using Mapster;
using MapsterMapper;
using System.Reflection;

namespace Web.Extensions.Mapping;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        var assemblies = new List<Assembly>()
        {
            typeof(Application.Common.AssemblyReference).Assembly,
            typeof(Domain.Common.Mapping.AssemblyReference).Assembly,
            typeof(Infrastructure.Common.Mapping.AssemblyReference).Assembly,
            typeof(Presentation.Common.Mapping.AssemblyReference).Assembly,
        };

        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;

        typeAdapterConfig.Scan(assemblies.ToArray());

        services.AddSingleton<IMapper>(new Mapper(typeAdapterConfig));
    }
}