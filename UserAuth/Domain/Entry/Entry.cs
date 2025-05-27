using System.ComponentModel.DataAnnotations;

namespace UserAuth.Domain.Entry;

public sealed class Entry
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; }

    public Entry(string email, string passwordHash)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
    }

    public static Entry Create(string email, string passwordHash)
    {
        return new Entry(email, passwordHash);
    }
    
}