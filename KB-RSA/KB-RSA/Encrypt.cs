using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KB_RSA
{
    public static class Encrypt
    {
        private static UnicodeEncoding byteConverter = new UnicodeEncoding();
        private static byte[] GetBytes(string data)
        {
            return byteConverter.GetBytes(data);
        }

        private static string GetBase64String(byte[] encryptedData)
        {
            return Convert.ToBase64String(encryptedData);
        }

        public static string EncryptWithKey(RSAParameters publicKey, string data)
        {
            byte[] dataBytes = GetBytes(data);
            byte[] encryptedData;
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                csp.ImportParameters(publicKey);
                encryptedData = csp.Encrypt(dataBytes, false);
            }
            return GetBase64String(encryptedData);
        }

        public static string EncryptWithXMLKey(string publicKey, string data)
        {
            byte[] dataBytes = GetBytes(data);
            byte[] encryptedData;
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider())
            {
                csp.FromXmlString(publicKey);
                encryptedData = csp.Encrypt(dataBytes, false);
            }
            return GetBase64String(encryptedData);
        }
    }
}
