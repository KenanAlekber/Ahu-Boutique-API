using Ahu.Business.MappingProfiles;
using Ahu.Business.Services.Implementations;
using Ahu.Business.Services.Interfaces;
using Ahu.Business.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ahu.Business;

public static class ServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductMapper).Assembly);

        services.AddScoped<IProductService, ProductService>();

        services.AddFluentValidation(v => v.RegisterValidatorsFromAssembly(typeof(ProductPostDtoValidator).Assembly));

        return services;
    }
}