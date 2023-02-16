using System.ComponentModel.DataAnnotations;

namespace BlogLab.Models.ViewModels;

public class ApplicationUserCreate
{
    [Required]
    public string Username { get; set; }

    [Required]
    [MinLength(5, ErrorMessage = "Min length 5")]
    [MaxLength(30, ErrorMessage = "Max 30")]
    public string Password { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Fullname { get; set; }
}