using System.ComponentModel.DataAnnotations;

namespace note.Models;

public class User(string email)
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = email;

    public UserAuthentication UserAuthentication { get; set; }
}