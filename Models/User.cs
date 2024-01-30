using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace note.Models;

public class User(string email): BaseEntity
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = email;

    public UserAuthentication UserAuthentication { get; set; }
    public ICollection<Note>? Notes { get; }
}