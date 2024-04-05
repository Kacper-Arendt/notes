using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noteApp.Models;

public class UserAuthentication(byte[] passwordHash, byte[] salt)
{
    [Key]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    public byte[] PasswordHash { get; set; } = passwordHash;

    [Required]
    public byte[] Salt { get; set; } = salt;

    public User User { get; set; }
}