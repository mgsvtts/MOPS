using Application.Queries.Common;
using CloudinaryDotNet;
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

public static class ProgramExtensions
{
    public static WebApplicationBuilder AddDomain(this WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection("Cloudinary");
        var cloudName = settings.GetValue<string>("CloudName");
        var apiKey = settings.GetValue<string>("ApiKey");
        var apiSecret = settings.GetValue<string>("ApiSecret");

        builder.Services.AddSingleton<IMerchItemRepository, MerchItemRepository>();
        builder.Services.AddSingleton<ITypeRepository, TypeRepository>();
        builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IImageRepository, ImageRepository>();
        builder.Services.AddScoped(x => new Cloudinary(new Account(cloudName, apiKey, apiSecret)));

        return builder;
    }

    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        DbConnection.Bind(builder.Configuration.GetConnectionString("DefaultConnection")!);

        return builder;
    }

    public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
    {
        PaginationLinks.SetUrls(builder.Configuration.GetValue<string>("Urls:ServerUrl")!);

        builder.Services.RegisterMapsterConfiguration();

        builder.Services.AddMediator(config =>
        {
            config.ServiceLifetime = ServiceLifetime.Scoped;
        });

        return builder;
    }

    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddMvcCore();

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

        app.MapHealthChecks("/api/mops/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.UseFastEndpoints(x =>
        {
            x.Endpoints.Configurator = config =>
            {
                config.AllowAnonymous();
                config.Description(b => b.Produces<Microsoft.AspNetCore.Mvc.ProblemDetails>((int)HttpStatusCode.BadRequest, "application/problem+json"));
            };
        });

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerGen();
            app.UseSwaggerUi();
        }

        return app;
    }
}