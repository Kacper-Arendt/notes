using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace note.Models;

public class Note(string content, string name): BaseEntity
{
    public int Id { get; init; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; init; } = name;
    
    [Required]
    public string Content { get; init; } = content;
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
}