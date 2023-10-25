using Ahu.API.Services;
using Ahu.Business.Helper;
using Ahu.Business.MappingProfiles;
using Ahu.Business.Services.Implementations;
using Ahu.Business.Services.Interfaces;
using Ahu.Business.Validators;
using Ahu.Core.Entities.Identity;
using Ahu.DataAccess.Contexts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Ahu.Business;

public static class ServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductMapper).Assembly);

        services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;

            options.User.RequireUniqueEmail = true;

            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            options.Lockout.AllowedForNewUsers = false;

        }).AddDefaultTokenProviders().AddTokenProvider<DataProtectorTokenProvider<AppUser>>("twofactor").AddEntityFrameworkStores<AppDbContext>();


        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISliderService, SliderService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IStoreDataService, StoreDataService>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IProductReviewService, ProductReviewService>();
        services.AddScoped<JwtService>();
        services.AddScoped<TokenEncoderDecoder>();

        services.AddFluentValidation(p => p.RegisterValidatorsFromAssembly(typeof(ProductPostDtoValidator).Assembly));

        return services;
    }
}