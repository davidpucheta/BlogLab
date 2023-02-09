using BlogLab.Models.Blog;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BlogLab.Repository;

public class BlogRepository : IBlogRepository
{
    private readonly IConfiguration _configuration;

    public BlogRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<int> DeleteAsync(int blogId)
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();

        var affectedRows = await connection.ExecuteAsync(
            "Blog_Delete",
            new { BlogId = blogId },
            commandType: CommandType.StoredProcedure
        );

        return affectedRows;
    }

    public async Task<PagedResults<Blog>> GetAllAsync(BlogPaging blogPaging)
    { 
        var results = new PagedResults<Blog>();
        
        await using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();

        using var multi = await connection.QueryMultipleAsync(
            "Blog_GetAll", 
            new
            {
                Offset = (blogPaging.Page - 1) * blogPaging.PageSize,
                blogPaging.PageSize
            },
            commandType: CommandType.StoredProcedure
        );

        results.Items = multi.Read<Blog>();
        results.TotalCount = multi.ReadFirst<int>();


        return results;
    }

    public async Task<List<Blog>> GetAllByUserIdAsync(int applicationUserId)
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();

        var blogs = await connection.QueryAsync<Blog>(
            "Blog_GetByUserId",
            new { ApplicationUserId = applicationUserId },
            commandType: CommandType.StoredProcedure
        );

        return blogs.ToList();
    }

    public async Task<List<Blog>> GetAllFamousAsync()
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();

        var blogs = await connection.QueryAsync<Blog>(
        "Blog_GetAllFamous",
            new { },
            commandType: CommandType.StoredProcedure
        );

        return blogs.ToList();
    }

    public async Task<Blog?> GetAsync(int blogId)
    {
        await using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();

        var blog = await connection.QueryFirstOrDefaultAsync<Blog>(
            "Blog_Get",
            new { BlogId = blogId },
            commandType: CommandType.StoredProcedure
        );

        return blog;
    }

    public async Task<Blog?> UpsertAsync(BlogCreate blogCreate, int applicationUserId)
    {
        var dataTable = new DataTable();
        dataTable.Columns.Add("BlogId", typeof(string));
        dataTable.Columns.Add("Title", typeof(string));
        dataTable.Columns.Add("Content", typeof(string));
        dataTable.Columns.Add("PhotoId", typeof(string));

        dataTable.Rows.Add(
            blogCreate.BlogId,
            blogCreate.Title,
            blogCreate.Content,
            blogCreate.PhotoId
        );

        await using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await connection.OpenAsync();

        var newBlogId = await connection.ExecuteScalarAsync<int?>(
            "Blog_Upsert",
            new
            {
                Blog = dataTable.AsTableValuedParameter("dbo.BlogType"),
                ApplicationUserId = applicationUserId
            },
            commandType: CommandType.StoredProcedure
        );

        newBlogId ??= blogCreate.BlogId;

        return await GetAsync(newBlogId.Value);
    }
}