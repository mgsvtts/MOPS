using Domain.MerchItemAggregate.Repositories;
using Domain.OrderAggregate.Repositories;
using Domain.TypeAggregate.Repositories;
using FastEndpoints;
using FastEndpoints.Swagger;
using HealthChecks.UI.Client;
using Hellang.Middleware.ProblemDetails;
using Infrastructure.Common;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Net;
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
        DbConnection.Bind(builder.Configuration.GetConnectionString("DefaultConnection")!);

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
        ProblemDetailsExtensions.AddProblemDetails(builder.Services, x =>
        {
            x.IncludeExceptionDetails = (_, _) => builder.Environment.IsDevelopment();
        });

        builder.Services.AddCors();

        builder.Services.AddFastEndpoints();

        builder.Services.SwaggerDocument(x =>
        {
            x.AutoTagPathSegmentIndex = 0;
        });

        builder.Services.AddHealthChecks()
           .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

        return builder;
    }

    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        app.UseProblemDetails();

        app.UseCors(builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());

        app.MapHealthChecks("/api/accounts/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseFastEndpoints(x =>
        {
            x.Endpoints.RoutePrefix = "api";
            x.Endpoints.Configurator = config =>
            {
                config.AllowAnonymous();
                config.Description(b => b.Produces<Microsoft.AspNetCore.Mvc.ProblemDetails>((int)HttpStatusCode.BadRequest, "application/problem+json"));
            };
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI();
        }

        return app;
    }
}