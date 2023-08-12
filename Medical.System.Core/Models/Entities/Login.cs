namespace Medical.System.Core.Models.Entities;

public class Login
{
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Token { get; set; }
}