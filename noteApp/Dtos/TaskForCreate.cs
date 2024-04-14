using noteApp.Enums;

namespace noteApp.Dtos;

public class TaskForCreate(
    string name,
    DateTime? dueDate,
    Priority? priority,
    Status status,
    Guid taskListId)
{
    public string Name { get; set; } = name;

    public DateTime? DueDate { get; set; } = dueDate;

    public Priority? Priority { get; set; } = priority;

    public Status Status { get; set; } = status;
    
    public Guid TaskListId { get; set; } = taskListId;
}