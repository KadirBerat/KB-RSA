using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static KB_RSA.KeySizes;

namespace KB_RSA
{
    public static class GenerateKey
    {
        public static RSAKeyModel RSAGenerateKey(KeySize keySize)
        {
            int ks = (int)keySize;
            RSAKeyModel model = new RSAKeyModel();
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(ks))
            {
                model.publicKey = csp.ExportParameters(false);
                model.privateKey = csp.ExportParameters(true);
            }
            return model;
        }

        public static bool RSAGenerateXMLKey(KeySize keySize)
        {
            int ks = (int)keySize;
            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(ks))
            {
                string filePath = DirectoryControl();
                string publicKeyPath = $@"{filePath}\public.key";
                string privateKeyPath = $@"{filePath}\private.key";

                string publicKey = csp.ToXmlString(false);
                TextWriter publicKeyWriter = new StreamWriter(publicKeyPath);
                publicKeyWriter.WriteLine(publicKey);
                publicKeyWriter.Dispose();
                publicKeyWriter.Close();

                string privateKey = csp.ToXmlString(true);
                TextWriter privateKeyWriter = new StreamWriter(privateKeyPath);
                privateKeyWriter.WriteLine(publicKey);
                privateKeyWriter.Dispose();
                privateKeyWriter.Close();
            }

            return true;
        }

        private static string DirectoryControl()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filePath += $@"\RSA_XML_Key_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}";
            bool folderCheck = File.Exists(filePath);
            if (folderCheck == false)
                Directory.CreateDirectory(filePath);

            string publicKeyPath = $@"{filePath}\public.key";
            bool pubicKeyCheck = File.Exists(publicKeyPath);
            if (pubicKeyCheck == false)
                File.Create(publicKeyPath);

            string privateKeyPath = $@"{filePath}\private.key";
            bool privateKeyCheck = File.Exists(privateKeyPath);
            if (privateKeyCheck == false)
                File.Create(privateKeyPath);

            return filePath;
        }
    }
}
