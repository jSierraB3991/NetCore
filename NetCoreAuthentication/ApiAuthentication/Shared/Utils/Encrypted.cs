namespace ApiAuthentication.Shared.Utils
{
    using System.Security.Cryptography;

    public class Encrypted
    {
        public static byte[] GetRandomSalt(int length)
        {
            var random = new RNGCryptoServiceProvider();
            byte[] salt = new byte[length];
            random.GetNonZeroBytes(salt);
            return salt;
        }

        public static byte[] SaltHashPassword(byte[] password, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();
            byte[] plainWithSaltBytes = new byte[password.Length + salt.Length];

            for (int i = 0; i < password.Length; i++) plainWithSaltBytes[i] = password[i];
            for (int i = 0; i < salt.Length; i++) plainWithSaltBytes[password.Length + i] = salt[i];

            return algorithm.ComputeHash(plainWithSaltBytes);
        }
    }
}
