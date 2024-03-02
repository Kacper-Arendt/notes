namespace note.Dtos;

public class NoteForUpdateDto(string name, string content)
{
        public Guid Id { get; init; }

        public string Name { get; init; } = name;        
        
        public string Content { get; init; } = content;
}