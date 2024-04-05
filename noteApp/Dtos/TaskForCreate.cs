using noteApp.Enums;
using noteApp.Models;

namespace noteApp.Dtos;

public class TaskForCreate(
    string name,
    DateTime? dueDate,
    Priority? priority,
    Status status,
    List<TaskItem> tasksList)
{
    public string Name { get; set; } = name;

    public DateTime? DueDate { get; set; } = dueDate;

    public Priority? Priority { get; set; } = priority;

    public Status Status { get; set; } = status;
    public List<TaskItem> TasksList { get; set; } = tasksList;
}