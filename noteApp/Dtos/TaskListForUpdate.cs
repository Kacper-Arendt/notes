namespace noteApp.Dtos;

public class TaskListForUpdate(string title)
{
    public Guid ListId { get; init; }
    public string Title { get; set; } = title;
}