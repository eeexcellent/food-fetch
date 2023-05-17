using FoodFetch.Domain.Commands;
using FoodFetch.Domain.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace FoodFetch.Domain
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            _ = services.AddScoped<IRepository, Repository>();
            _ = services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

            return services;
        }
    }
}