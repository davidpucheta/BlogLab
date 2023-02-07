using System.ComponentModel.DataAnnotations;

namespace BlogLab.Models.BlogComment;

public class BlogCommentCreate
{
    public int BlogCommentId { get; set; }

    public int? ParentBlogCommentId { get; set; }

    public int? BlogId { get; set; }

    [Required(ErrorMessage = "Username is required.")]
    [MinLength(10, ErrorMessage = "Must be 10-300 characters.")]
    [MaxLength(300, ErrorMessage = "Must be 10-300 characters.")]
    public string Content { get; set; }
}