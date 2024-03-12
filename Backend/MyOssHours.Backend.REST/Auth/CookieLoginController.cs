using System.Security.Claims;
using System.Security.Cryptography;
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
        if (_contextAccessor.HttpContext is null) throw new InvalidOperationException("HttpContext is null");

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
        if (_contextAccessor.HttpContext is null) throw new InvalidOperationException("HttpContext is null");

        _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        return Ok();
    }

    [HttpGet("GetCurrentUser")]
    public IActionResult GetCurrentUser()
    {
        if (_contextAccessor.HttpContext is null) throw new InvalidOperationException("HttpContext is null");

        var claims = _contextAccessor.HttpContext.User.Claims.ToList().Select(x => new { x.Type, x.Value });
        return Ok(claims);
    }

    static string Sha256(string randomString)
    {
        var crypt = SHA256.Create() ?? throw new InvalidOperationException("SHA256.Create() returned null");

        var hash = new StringBuilder();
        var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
        foreach (var theByte in crypto)
        {
            hash.Append(theByte.ToString("x2"));
        }
        return hash.ToString();
    }
}