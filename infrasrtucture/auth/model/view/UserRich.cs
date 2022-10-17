using System.ComponentModel.DataAnnotations;
using System.Data;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.muni_org.model.entity;

namespace PISWF.infrasrtucture.auth.model.view;

public class UserRich
{
    [Required]
    [StringLength(15, MinimumLength = 3, ErrorMessage = "Длинна догина должна быть от 3 до 15 символов")]
    public string? Login { get; }
    
    public string? FirstName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public string? LastName { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [Phone]
    public string? Phone { get; set; }
    
    public Organization? Organization { get; set; }
    
    public Municipality? Municipality { get; set; }
    
    public List<Role> Roles { get; set; }
    
    [Required]
    private string password;
    public int Password
    {
        get => GetHashPassword();
    }

    private int GetHashPassword()
    {
        var res = 0;
        foreach (var chr in password)
        {
            res = (res + chr % 10) * 10;
        }
        return res;
    }

    public void ResetPassword(string password)
    {
        this.password = password;
    }
}