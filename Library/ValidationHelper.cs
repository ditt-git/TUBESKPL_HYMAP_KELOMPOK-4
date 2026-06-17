using System;

namespace HYMAPSOPIR
{
    public static class ValidationHelper
    {
        // KISS
        public static bool IsEmpty(string input) => string.IsNullOrWhiteSpace(input);
        public static bool IsPasswordNotEmpty(string password) => !string.IsNullOrEmpty(password);
        public static bool IsPasswordLengthValid(string password) => password != null && password.Length >= 5;
    }
}