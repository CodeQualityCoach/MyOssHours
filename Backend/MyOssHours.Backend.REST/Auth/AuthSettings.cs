namespace MyOssHours.Backend.REST.Auth
{
    public class AuthSettings
    {
        public string? Type { get; set; }
        public ValidatorSettings? Validator { get; set; } = new ValidatorSettings();
        public CookieSettings? Cookie { get; set; } = new CookieSettings();

        public class CookieSettings
        {
        }

        public class ValidatorSettings
        {
            public string? Type { get; set; }
            public string? Path { get; set; }
        }
    }
}
