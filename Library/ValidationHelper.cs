using System;

namespace HYMAPSOPIR
{
    public static class ValidationHelper
    {
        public static bool IsEmpty(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        public static bool IsPasswordStrong(string password)
        {
            return !string.IsNullOrEmpty(password);
        }

        public static bool IsPasswordLengthValid(string password)
        {
            return password != null && password.Length >= 5;
        }
    }
}
