using System.ComponentModel.DataAnnotations;

namespace note.Models;

public class Note(string content, string name)
{
    public int Id { get; init; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; init; } = name;
    
    [Required]
    public string Content { get; init; } = content;


}