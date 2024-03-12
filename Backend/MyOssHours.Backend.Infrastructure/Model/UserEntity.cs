using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyOssHours.Backend.Infrastructure.Model;

[Table("User")]
internal class UserEntity
{
    public long Id { get; set; }
    public Guid Uuid { get; set; }
    [MaxLength(128)]
    public required string Nickname { get; set; }
    [MaxLength(1024)]
    public required string Email { get; set; }
}