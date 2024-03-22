using System.Security.Cryptography;
using System.Text;

namespace MyOssHours.Backend.REST.Auth;

internal class HtAccessUserVerification(ILogger<HtAccessUserVerification> logger) : IUserValidator
{
    public async Task<bool> Validate(string email, string password)
    {
        // todo: make the file configurable
        if (!File.Exists(".htaccess")) throw new FileNotFoundException($"Cannot find file .htaccess in {Environment.CurrentDirectory}");

        var userHtAccess = (await System.IO.File.ReadAllLinesAsync(".htaccess"))
            .FirstOrDefault(x => x.ToLower().StartsWith(email.ToLower() + ":"));
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