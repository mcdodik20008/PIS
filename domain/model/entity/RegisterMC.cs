using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PISWF.infrasrtucture.muni_org.model.entity;

namespace PISWF.domain.registermc.model.entity;

[Table("register-m-c")]

public class RegisterMC
{
    [Key]
    [Required]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Required]
    [Column("number")]
    public int Number { get; set; }
    
    [Required]
    [Column("valid_date", TypeName = "Date")]
    public DateTime ValidDate { get; set; }
    
    [Required]
    [Column("organization")]
    public Organization Organization { get; set; }
    
    [Required]
    [Column("municipality")]
    public Municipality Municipality { get; set; }
    
    [Required]
    [Column("year")]
    public int Year { get; set; }
    
    [Required]
    [Column("price")]
    public Double Price { get; set; }
    
    [Required]
    [Column("subvention_share")]
    public Double SubventionShare { get; set; }
    
    [Required]
    [Column("amount_money")]
    public Double AmountMoney { get; set; }
    
    [Required]
    [Column("share_funds_subvention")]
    public Double ShareFundsSubvention { get; set; }
    
    [Column("documents")]
    public List<FileDocument> Documents { get; set; }
}