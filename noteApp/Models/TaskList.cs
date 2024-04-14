using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noteApp.Models;

public class TaskList(string title) : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ListId { get; init; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = title;

    public List<TaskItem> TasksList { get; set; } = [];
}