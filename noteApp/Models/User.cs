using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace noteApp.Models;

public class User(string email): BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }    
    
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = email;

    public UserAuthentication UserAuthentication { get; set; }
    public ICollection<Note>? Notes { get; }
}