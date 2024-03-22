using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyOssHours.Backend.REST.Auth;

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
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public IActionResult Logout()
    {
        if (_contextAccessor.HttpContext is null) throw new InvalidOperationException("HttpContext is null");

        _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        return Ok();
    }

    [HttpGet("GetCurrentUser")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public IActionResult GetCurrentUser()
    {
        if (_contextAccessor.HttpContext is null) throw new InvalidOperationException("HttpContext is null");

        var claims = _contextAccessor.HttpContext.User.Claims.ToList().Select(x => new { x.Type, x.Value });
        return Ok(claims);
    }

    #region Models

    public class CookieLoginModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    #endregion
}