using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PISWF.infrasrtucture.muni_org.model.entity;

namespace PISWF.infrasrtucture.auth.model.entity;

[Table("user")]
public class User
{
    [Key] 
    [Required]
    [Column("login")]
    public string? Login { get; set; }
    
    [Required]
    [Column("password")]
    public long Password { get; set; }
    
    [Column("first_name")]
    public string? FirstName { get; set; }
    
    [Column("middle_name")]
    public string? MiddleName { get; set; }
    
    [Column("last_name")]
    public string? LastName { get; set; }
    
    [Column("email")]
    public string? Email { get; set; }
    
    [Column("phone")]
    public string? Phone { get; set; }
    
    [Column("organization")]
    public Organization? Organization { get; set; }
    
    [Column("municipality")]
    public Municipality? Municipality { get; set; }
    
    [Column("roles")]
    public List<Role>? Roles { get; set; }
}