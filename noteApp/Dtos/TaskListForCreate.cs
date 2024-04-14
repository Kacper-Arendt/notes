namespace noteApp.Dtos;

public class TaskListForCreate(string title)
{
    public string Title { get; set; } = title;
}