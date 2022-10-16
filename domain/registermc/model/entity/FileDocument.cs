using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PISWF.domain.registermc.model.entity;

[Table("file_document")]
public class FileDocument
{
    [Key]
    [Required]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("name")]
    public string Name { get; set; }
    
    [Required]
    [Column("file_path")]
    public string FilePath { get; set; }
    
    [Required]
    [Column("file_type")]
    public string FileType { get; set; }
}