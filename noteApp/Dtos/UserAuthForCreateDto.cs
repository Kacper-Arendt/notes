namespace noteApp.Dtos;

public class UserAuthForCreateDto(string email, string password, string passwordConfirm)
{
    public string Email { get; set; } = email;

    public string Password { get; set; } = password;

    public string PasswordConfirm { get; set; } = passwordConfirm;
}