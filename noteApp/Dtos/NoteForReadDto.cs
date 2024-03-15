namespace noteApp.Dtos;

public class NoteForReadDto(string name, string content)
{
        public Guid Id { get; init; }
        
        public string Name { get; init; } = name;
        
        public string Content { get; init; } = content;
        
        public Guid UserId { get; init; }
        
        public DateTime CreatedOn { get; set; }
        
        public DateTime UpdatedOn { get; set; }
}