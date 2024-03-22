using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyOssHours.Backend.Infrastructure.Persistence.Model;

[Table("User")]
[Index(nameof(Email), IsUnique = true)]
public class UserEntity
{
    [Key]
    public long Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Uuid { get; set; }

    [MaxLength(128)]
    public string? Nickname { get; set; }

    [MaxLength(1024)]
    public required string Email { get; set; }
}