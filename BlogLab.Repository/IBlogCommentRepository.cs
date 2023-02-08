using BlogLab.Models.BlogComment;

namespace BlogLab.Repository;

public interface IBlogCommentRepository
{
    Task<int> DeleteAsync(int blogCommentId);

    Task<List<BlogComment>> GetAllAsync(int blogId);

    Task<BlogComment> GetAsync(int blogCommentId);

    Task<BlogComment> UpsertAsync(BlogCommentCreate blogCommentCreate, int applicationUserId);
}