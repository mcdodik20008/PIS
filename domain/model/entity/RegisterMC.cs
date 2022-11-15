using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PISWF.infrasrtucture.muni_org.model.entity;

namespace PISWF.domain.registermc.model.entity;

[Table("register-m-c")]

public class RegisterMC
{
    [Key]
    [ToExcel]
    [Required]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [ToExcel]
    [Required]
    [Column("number")]
    public int Number { get; set; }
    
    [ToExcel]
    [Required]
    [Column("valid_date", TypeName = "Date")]
    public DateTime ValidDate { get; set; }
    
    [ToExcel]
    [Required]
    [Column("location")]
    public string Location { get; set; }
    
    [ToExcel]
    [Required]
    [Column("action_date", TypeName = "Date")]
    public DateTime ActionDate { get; set; }
   
    [ToExcel]
    [Required]
    [Column("organization")]
    public Organization Organization { get; set; }
    
    [ToExcel]
    [Required]
    [Column("municipality")]
    public Municipality Municipality { get; set; }
    
    [ToExcel]
    [Required]
    [Column("omsu")]
    public string Omsu { get; set; }
    
    [ToExcel]
    [Required]
    [Column("year")]
    public int Year { get; set; }
    
    [ToExcel]
    [Required]
    [Column("price")]
    public Double Price { get; set; }
    
    [ToExcel]
    [Required]
    [Column("subvention_share")]
    public Double SubventionShare { get; set; }
    
    [ToExcel]
    [Required]
    [Column("amount_money")]
    public Double AmountMoney { get; set; }
    
    [ToExcel]
    [Required]
    [Column("share_funds_subvention")]
    public Double ShareFundsSubvention { get; set; }
    
    [Column("documents")]
    public List<FileDocument> Documents { get; set; }
}