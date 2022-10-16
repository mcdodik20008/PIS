using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PISWF.infrasrtucture.auth.model.entity;

[Table("role")]
public class Role
{
    [Key]
    [Required]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("users")]
    public List<User>? Users { get; set; }
}