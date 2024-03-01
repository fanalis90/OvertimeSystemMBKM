namespace API.Utilities.Handlers
{
    public class BCryptHandler
    {
        public static string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public static string HashPassword(string password) {
            return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
        }
        public static bool VerifyPassword(string password, string hashedPassword) {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
