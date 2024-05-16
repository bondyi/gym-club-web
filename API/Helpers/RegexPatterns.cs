namespace API.Helpers
{
    public static class RegexPatterns
    {
        public const string PHONE_NUMBER = "^\\+\\d{12}$";
        public const string PASSWORD = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{8,16}$\r\n";
    }
}
