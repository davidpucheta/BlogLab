using System.ComponentModel.DataAnnotations;

namespace BlogLab.Models.Account;

public class ApplicationUserCreate : ApplicationUserLogin
{
    [Required(ErrorMessage = "Fullname is required.")]
    [MinLength(10, ErrorMessage = "Must be at least 10-30 characters.")]
    [MaxLength(30, ErrorMessage = "Must be at least 10-30 characters.")]
    public string Fullname { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [MaxLength(30, ErrorMessage = "Can be at most 30 characters.")]
    public string Email { get; set; }
}