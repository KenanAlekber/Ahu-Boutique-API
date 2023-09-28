//namespace Ahu.DataAccess;

//public class ServiceRegistration
//{
//    public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddDbContext<AppDbContext>(options =>
//        {
//            options.UseSqlServer(configuration.GetConnectionString("Default"));
//        });

//        services.AddScoped<IAuthorRepository, AuthorRepository>();
//        services.AddScoped<IBookRepository, BookRepository>();

//        return services;
//    }
//}