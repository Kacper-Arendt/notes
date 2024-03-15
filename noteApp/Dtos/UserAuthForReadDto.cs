namespace noteApp.Dtos;

public class UserAuthForReadDto(string email)
{
    public string Email { get; set; } = email;
}