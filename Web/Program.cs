using Infrastructure;
using Presentation.Controllers;
using Web.Extensions;
using Web.Extensions.Mapping;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDomain()
                        .AddApplication()
                        .AddInfrastructure()
                        .AddPresentation();

        var app = builder.Build();

        app.AddMiddlewares();
       
        app.Run();
    }
}
