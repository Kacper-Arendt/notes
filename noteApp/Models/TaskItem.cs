using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using noteApp.Enums;

namespace noteApp.Models;

public class TaskItem(
    string name,
    DateTime? dueDate,
    Priority? priority,
    Status status) : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid TaskId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = name;

    public DateTime? DueDate { get; set; } = dueDate;

    public Priority? Priority { get; set; } = priority;

    [Required]
    public Status Status { get; set; } = status;

    public Guid TaskListId { get; set; }
    public List<TaskItem> TasksList { get; set; }
}