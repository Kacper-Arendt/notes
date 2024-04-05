using noteApp.Enums;

namespace noteApp.Dtos;

public class TaskForUpdate(
    string name,
    DateTime? dueDate,
    Priority? priority,
    Status status)
{
    public Guid UserId { get; init; }

    public string Name { get; set; } = name;

    public DateTime? DueDate { get; set; } = dueDate;

    public Priority? Priority { get; set; } = priority;

    public Status Status { get; set; } = status;
}