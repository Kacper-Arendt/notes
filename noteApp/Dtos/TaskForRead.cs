using noteApp.Enums;
using noteApp.Models;

namespace noteApp.Dtos;

public class TaskForRead(
    string name,
    DateTime? dueDate,
    Priority? priority,
    Status status,
    List<TaskItem> tasksList)
{
    public Guid UserId { get; init; }

    public string Name { get; set; } = name;

    public DateTime? DueDate { get; set; } = dueDate;

    public Priority? Priority { get; set; } = priority;

    public Status Status { get; set; } = status;

    public List<TaskItem> TasksList { get; set; } = tasksList;

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }
}