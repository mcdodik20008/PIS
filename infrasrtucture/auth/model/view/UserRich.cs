using System.Data;
using PISWF.infrasrtucture.auth.model.entity;
using PISWF.infrasrtucture.muni_org.model.entity;

namespace PISWF.infrasrtucture.auth.model.view;

public class UserRich
{
    public string? Login { get; }

    public string? FirstName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? Email { get; set; }
    
    public string? Phone { get; set; }
    
    public Organization? Organization { get; set; }
    
    public Municipality? Municipality { get; set; }
    
    public List<Role> Roles { get; set; }
    
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