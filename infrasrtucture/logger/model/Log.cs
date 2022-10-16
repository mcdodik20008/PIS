using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PISWF.infrasrtucture.auth.model.entity;

namespace PISWF.infrasrtucture.logger.model;

[Table("log")]
public class Log
{
    [Key]
    [Required]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("method_name")]
    public string MethodName { get; set; }
    
    [Required]
    [Column("log_date", TypeName = "Date")]
    public DateTime LogDate { get; set; }
    
    [Required]
    [Column("author")]
    public User Author { get; set; }

    [Required] 
    [Column("json_entity")] 
    public string JsonEntity { get; set; }
}