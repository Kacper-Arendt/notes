namespace noteApp.Dtos;

public class UserAuthForLoginDto(string email, string password)
{
    public string Email { get; set; } = email;
    
    public string Password { get; set; } = password;
}