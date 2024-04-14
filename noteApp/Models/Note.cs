using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noteApp.Models;

public class Note(string content, string name) : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; init; } = name;

    [Required]
    public string Content { get; init; } = content;

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }
}