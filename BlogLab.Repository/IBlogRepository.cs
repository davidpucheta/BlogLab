using BlogLab.Models.Blog;

namespace BlogLab.Repository;

public interface IBlogRepository
{
    Task<int> DeleteAsync(int blogId);

    Task<PagedResults<Blog>> GetAllAsync(BlogPaging blogPaging);

    Task<List<Blog>> GetAllByUserIdAsync(int applicationUserId);

    Task<List<Blog>> GetAllFamousAsync();

    Task<Blog> GetAsync(int blogId);

    Task<Blog> UpsertAsync(BlogCreate blogCreate, int applicationUserId);
}