using System.Security.Cryptography;

namespace ExampleApiKey.Utils
{
    public static class ApiKeyGenerator
    {
        public static string GenerateApiKey()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32))
                .Replace("+", "")
                .Replace("/", "")
                .Substring(0, 32); //gera string de 32 caracteres
        }
    }
}
