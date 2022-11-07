using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PISWF.infrasrtucture.muni_org.model.entity;

[Table("organization")]
public class Organization
{
    [Key]
    [Required]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}