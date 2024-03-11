namespace MyOssHours.Backend.REST.Auth;

public class CookieLoginModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}