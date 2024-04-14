namespace noteApp.Dtos;

public class TaskListForReadBasic(string title, DateTime createdOn, DateTime updatedOn)
{
    public Guid ListId { get; set; }

    public string Title { get; set; } = title;
    
    public DateTime CreatedOn { get; set; } = createdOn;

    public DateTime UpdatedOn { get; set; } = updatedOn;
}