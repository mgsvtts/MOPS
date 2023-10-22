using Domain.MerchItemAggregate.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Presentation.Controllers;
using System.Reflection.Metadata;
using Web.Extensions;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddApplicationPart(typeof(MerchItemsController).Assembly);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<Infrastructure.DbContext>(options 
            => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.RegisterMapsterConfiguration();
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Application.Common.AssemblyReference).Assembly));

        builder.Services.AddInfrastructure();

        var app = builder.Build();

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
