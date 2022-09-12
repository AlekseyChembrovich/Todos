namespace Todos.AuthApi.Models;

public class RegisterUser
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string ReturnUrl { get; set; }
}
