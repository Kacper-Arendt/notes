namespace noteApp.helpers;

public class PasswordHelper(byte[] passwordSalt, byte[] passwordHash)
{
    public byte[] PasswordSalt { get; set; } = passwordSalt;
    public byte[] PasswordHash { get; set; } = passwordHash;
}