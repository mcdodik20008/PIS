using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PISWF.infrasrtucture.auth.model.entity;

[Table("visibility")]
public class Visibility
{
    [Key]
    [Required]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("rate")]
    public string? Rate { get; set; }
}