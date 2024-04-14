namespace noteApp.Dtos;

public class TaskListForRead(string title, List<TaskForRead> tasksList, DateTime createdOn, DateTime updatedOn)
{
    public Guid ListId { get; set; }
    
    public string Title { get; set; } = title;
    
    public List<TaskForRead> TasksList { get; set; } = tasksList;

    public DateTime CreatedOn { get; set; } = createdOn;

    public DateTime UpdatedOn { get; set; } = updatedOn;
}