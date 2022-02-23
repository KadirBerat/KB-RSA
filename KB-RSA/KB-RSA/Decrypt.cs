using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KB_RSA
{
    public static class Decrypt
    {
        private static UnicodeEncoding byteConverter = new UnicodeEncoding();
        private static string GetString(byte[] decryptedData)
        {
            return byteConverter.GetString(decryptedData);
        }

        private static byte[] GetBytes(string encryptedData)
        {
            return Convert.FromBase64String(encryptedData);
        }

        public static string DecryptWithKey(RSAParameters privateKey, string encryptedData)
        {
            byte[] dataBytes = GetBytes(encryptedData);
            byte[] decryptedData;
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                csp.ImportParameters(privateKey);
                decryptedData = csp.Decrypt(dataBytes, false);
            }
            return GetString(decryptedData);
        }

        public static string DecryptWithXMLKey(string privateKey, string encryptedData)
        {
            byte[] dataBytes = GetBytes(encryptedData);
            byte[] decryptedData;
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                csp.FromXmlString(privateKey);
                decryptedData = csp.Decrypt(dataBytes, false);
            }
            return GetString(decryptedData);
        }
    }
}
