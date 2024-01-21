namespace note.Dtos;

public class NoteForCreateDto(string name, string content)
{
    public string Name { get; init; } = name;
    public string Content { get; init; } = content;
}