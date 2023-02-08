using BlogLab.Services;

namespace BlogLab.Web;

public static class ConfigureServices
{
    public static void AddBlogServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPhotoService, PhotoService>();
    }
}