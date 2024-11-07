using Microsoft.EntityFrameworkCore;
using Service.Blog;

namespace MinimalAPI;

public static class Startup
{
    //private static IConfiguration _configuration;
    //
    //public static void Configure(IConfiguration configuration)
    //{
    //    _configuration = configuration;
    //}

    public static void InjectServices(this IServiceCollection service, IConfiguration configuration)
    {
        #region Add Postgre SQL
        var postgreConnection = configuration.GetSection("PostgreConnection").Value;
        service.AddDbContext<Database.PostgreDbContextModels.AppDbContext>(option =>
        {
            option.UseNpgsql(postgreConnection);
        });
        #endregion

        //Inject the services
        service.AddScoped<IBlogService, BlogService>();
    }
}
