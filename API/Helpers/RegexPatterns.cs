namespace API.Helpers
{
    public static class RegexPatterns
    {
        public const string PHONE_NUMBER = "^\\+\\d{12}$";
        public const string PASSWORD = "^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,16}$\r\n";
        public const string NAME = "^[a-zA-Zа-яА-Я\\s\\-]{2,16}$\r\n";
        public const string SURNAME = "^[a-zA-Zа-яА-Я\\s\\-]{2,32}$\r\n";
    }
}
