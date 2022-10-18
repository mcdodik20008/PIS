using System.ComponentModel.DataAnnotations;

namespace PISWF.infrasrtucture.auth.model.view;

public class UserAuth
{
    [StringLength(15, MinimumLength = 3, ErrorMessage = "Длинна логина должна быть от 3 до 15 символов")]
    public string? Login { get; }
    
    [Phone]
    public string? Phone { get; set; }
    
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
    
    public UserAuth(string? login, string password, string phone)
    {
        Phone = phone;
        Login = login;
        this.password = password;
    }
}