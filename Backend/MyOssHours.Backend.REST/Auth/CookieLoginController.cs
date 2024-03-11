using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace MyOssHours.Backend.REST.Auth;

[Route("api/v1/[controller]")]
public class CookieLoginController(IHttpContextAccessor contextAccessor)
    : ControllerBase
{
    // todo: make "user validator" configurable
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] CookieLoginModel login)
    {
        // todo: make the file configurable
        if (!System.IO.File.Exists(".htaccess")) return Unauthorized();
        var userHtAccess = (await System.IO.File.ReadAllLinesAsync(".htaccess"))
            .FirstOrDefault(x => x.StartsWith(login.Email.ToLower() + ":"));
        if (userHtAccess == null) return Unauthorized();
        var pwdHash = userHtAccess.Split(':')[1];
        if (!pwdHash.Equals(Sha256(login.Password))) return Unauthorized();

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email,login.Email),
            new(ClaimTypes.Role, "myosshours.contribute"),
            new(ClaimTypes.Role, "myosshours.read"),};

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
        };

        _contextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties).Wait();

        return Ok();
    }

    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        return Ok();
    }

    [HttpGet("GetCurrentUser")]
    public IActionResult GetCurrentUser()
    {
        var claims = _contextAccessor.HttpContext.User.Claims.ToList().Select(x => new { x.Type, x.Value });
        return Ok(claims);
    }

    static string Sha256(string randomString)
    {
        var crypt = new System.Security.Cryptography.SHA256Managed();
        var hash = new System.Text.StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
        foreach (byte theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }
        return hash.ToString();
    }
}