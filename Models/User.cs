using System.ComponentModel.DataAnnotations;

namespace note.Models;

public class User(string name, string email)
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; init; } = name;

    [Required]
    [MaxLength(100)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; init; } = email;
}