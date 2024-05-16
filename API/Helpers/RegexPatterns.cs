namespace API.Helpers
{
    public static class RegexPatterns
    {
        public const string PHONE_NUMBER = "^\\+\\d{12}$";
        public const string PASSWORD = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,16}$\r\n";
    }
}
