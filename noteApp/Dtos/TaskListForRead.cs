using noteApp.Models;

namespace noteApp.Dtos;

public class TaskListForRead(string title, List<TaskItem> tasksList, DateTime createdOn, DateTime updatedOn)
{
    public string Title { get; set; } = title;
    public List<TaskItem> TasksList { get; set; } = tasksList;

    public DateTime CreatedOn { get; set; } = createdOn;

    public DateTime UpdatedOn { get; set; } = updatedOn;
}