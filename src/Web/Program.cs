using Web.Extensions;

namespace Web;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var app = WebApplication.CreateBuilder(args)
            .AddDomain()
            .AddApplication()
            .AddInfrastructure()
            .AddPresentation()
            .Build();

        app.AddMiddlewares()
           .Run();
    }
}