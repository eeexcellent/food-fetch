using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using FoodFetch.Domain;
using FoodFetch.Domain.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;
using FoodFetch.Api;
using Microsoft.Extensions.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "FoodFetch API",
        Description = "An ASP.NET Core Web API for managing product orders, check status info about order, assign orders to users"
    });

    string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddOptions<FoodFetchOptions>()
    .Bind(builder.Configuration);

builder.Services.AddDomain();
builder.Services.AddDbContext<FoodFetchContext>(
    (IServiceProvider sp, DbContextOptionsBuilder c) =>
    {
        IOptionsMonitor<FoodFetchOptions> options = sp.GetRequiredService<IOptionsMonitor<FoodFetchOptions>>();
        _ = c.UseNpgsql(options.CurrentValue.FoodFetchConnectionString);
    }
);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

public partial class Program { }
