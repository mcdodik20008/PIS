using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PISWF.infrasrtucture.auth.model.view;

public class UserBasic
{
    [StringLength(15, MinimumLength = 3, ErrorMessage = "Длинна догина должна быть от 3 до 15 символов")]
    public string? Login { get; }

    public string? FirstName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public string? LastName { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
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
    
    public UserBasic(string? login, string password)
    {
        if (password.Length > 20)
            throw new SyntaxErrorException("Пароль должен быть меньше 20 символов");
        Login = login;
        this.password = password;
    }
    
    public UserBasic(UserBasic userBasic, string password)
    {
        if (password.Length > 20)
            throw new SyntaxErrorException("Пароль должен быть меньше 20 символов");
        Login = userBasic.Login;
        Phone = userBasic.Phone;
        Email = userBasic.Email;
        FirstName = userBasic.FirstName; 
        MiddleName = userBasic.MiddleName;
        LastName = userBasic.LastName;
        this.password = password;
    }
}