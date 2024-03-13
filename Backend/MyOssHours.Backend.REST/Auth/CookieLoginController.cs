using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyOssHours.Backend.REST.Auth;

internal class HtAccessUserVerification(ILogger<HtAccessUserVerification> logger) : IUserValidator
{
    public async Task<bool> Validate(string email, string password)
    {
        // todo: make the file configurable
        if (!System.IO.File.Exists(".htaccess")) throw new FileNotFoundException($"Cannot find file .htaccess in {Environment.CurrentDirectory}");

        var userHtAccess = (await System.IO.File.ReadAllLinesAsync(".htaccess"))
            .FirstOrDefault(x => x.StartsWith(email.ToLower() + ":"));
        if (userHtAccess == null)
        {
            logger.LogDebug($"Cannot find user {email}");
            return false;
        }

        var pwdHash = userHtAccess.Split(':')[1];
        if (!pwdHash.Equals(Sha256(password)))
        {
            logger.LogDebug("Password wrong");
            return false;
        }

        return true;
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

public interface IUserValidator
{
    public Task<bool> Validate(string email, string password);
}

[Route("api/v1/[controller]")]
public class CookieLoginController(IHttpContextAccessor contextAccessor, IUserValidator userValidator, ILogger<CookieLoginController> logger)
    : ControllerBase
{
    // todo: make "user validator" configurable
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
    private readonly IUserValidator _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] CookieLoginModel login)
    {
        if (_contextAccessor.HttpContext is null) throw new InvalidOperationException("HttpContext is null");

        if (!(await _userValidator.Validate(login.Email, login.Password)))
            return Unauthorized();

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
    [Authorize()]
    public IActionResult Logout()
    {
        if (_contextAccessor.HttpContext is null) throw new InvalidOperationException("HttpContext is null");

        _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        return Ok();
    }

    [HttpGet("GetCurrentUser")]
    [Authorize()]
    public IActionResult GetCurrentUser()
    {
        if (_contextAccessor.HttpContext is null) throw new InvalidOperationException("HttpContext is null");

        var claims = _contextAccessor.HttpContext.User.Claims.ToList().Select(x => new { x.Type, x.Value });
        return Ok(claims);
    }
}