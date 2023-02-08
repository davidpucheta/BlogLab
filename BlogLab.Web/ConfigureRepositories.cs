using BlogLab.Repository;

namespace BlogLab.Web;

public static class ConfigureRepositories
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IBlogCommentRepository, BlogCommentRepository>();
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
    }
}