namespace noteApp.Dtos;

public class TaskListForUpdate(string title)
{
    public string Title { get; set; } = title;
}