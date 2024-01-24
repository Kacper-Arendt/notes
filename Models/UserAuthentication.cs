using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace note.Models;

[Index(nameof(Email), IsUnique = true)]
public class UserAuthentication(string email)
{
    [Key]
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = email;

    [Required]
    public byte[]? PasswordHash { get; set; }

    [Required]
    public byte[]? Salt { get; set; }
}