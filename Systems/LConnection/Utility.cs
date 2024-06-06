using System.Security.Cryptography;
using System.Text;

namespace Systems.Models
{
    /// <summary>
    /// 
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPass(string password)
        {
            using SHA256 mySHA256 = SHA256.Create();
            byte[] textBytes = Encoding.ASCII.GetBytes(password);
            byte[] hash = mySHA256.ComputeHash(textBytes);
            return Convert.ToBase64String(hash);
        }
    }
}
