//using Microsoft.Extensions.DependencyInjection;

//namespace Ahu.Business;

//public class ServiceRegistration
//{
//    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
//    {
//        services.AddAutoMapper(typeof(AuthorMapper).Assembly);

//        services.AddScoped<IAuthorService, AuthorService>();
//        services.AddScoped<IBookService, BookService>();
//        services.AddScoped<IFileService, FileService>();

//        services.AddFluentValidation(o => o.RegisterValidatorsFromAssembly(typeof(AuthorPostDtoValidator).Assembly));

//        return services;
//    }
//}