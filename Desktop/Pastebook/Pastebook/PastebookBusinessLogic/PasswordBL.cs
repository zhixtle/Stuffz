using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogic
{
    public class PasswordBL
    {
        HashComputer hashComputer = new HashComputer();

        public string GeneratePasswordHash(string plainTextPassword, out string salt)
        {
            salt = SaltGenerator.GetSaltString();
            string finalString = plainTextPassword + salt;
            return hashComputer.GetPasswordHashAndSalt(finalString);
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            string finalString = password + salt.Trim();
            return hash.Trim() == hashComputer.GetPasswordHashAndSalt(finalString);
        }
    }

    public static class SaltGenerator
    {
        private static RNGCryptoServiceProvider cryptoServiceProvider = null;
        private const int SALT_SIZE = 24;

        static SaltGenerator()
        {
            cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public static string GetSaltString()
        {
            byte[] saltBytes = new byte[SALT_SIZE];
            cryptoServiceProvider.GetNonZeroBytes(saltBytes);
            string saltString = BytesStringConverter.GetString(saltBytes);
            return saltString;
        }
    }

    public class HashComputer
    {
        public string GetPasswordHashAndSalt(string message)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = BytesStringConverter.GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);
            return BytesStringConverter.GetString(resultBytes);
        }
    }

    public static class BytesStringConverter
    {
        public static byte[] GetBytes(string stringToBytes)
        {
            return Encoding.ASCII.GetBytes(stringToBytes);
        }

        public static string GetString(byte[] byteArraytoString)
        {
            return Encoding.ASCII.GetString(byteArraytoString);
        }
    }
}
