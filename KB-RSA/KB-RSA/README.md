# KB-RSA
 A classlibrary project released as nuget that makes it easy to use for key generation, encryption, and decryption with RSA.
 
> All classes in the project are in static structure.

# KeySizes
```csharp 
public static class KeySizes {...}
```
- A list of accepted key sizes for RSA
```csharp
public enum KeySize 
{
   _384 = 384,
   _392 = 392,
   .
   .
   _2048 = 2048,
   .
   .
   _16384 = 16384
}
```
   
# RSAKeyModel
```csharp
public class RSAKeyModel {...}
```
- Two property to store public and private key
```csharp 
public RSAParameters publicKey { get; set; }
public RSAParameters privateKey { get; set; }
```
   
# GenerateKey
```csharp
public static class GenerateKey {...}
```
> key generation processes

- Returns the public and private key it generates according to the key size sent into it as an RSAKeyModel object.
```csharp
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
``` 

- It creates a folder on the desktop with the date and time of the day and saves the public and private key it generates according to the key size sent to it.
```csharp
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
        privateKeyWriter.WriteLine(privateKey);
        privateKeyWriter.Dispose();
        privateKeyWriter.Close();
        }
        return true;
}
```

> `DireDirectoryControl()` method
```csharp
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
    {
        using (FileStream fs = File.Create(publicKeyPath))
        {
            fs.Dispose();
            fs.Close();
        }
    }
    string privateKeyPath = $@"{filePath}\private.key";
    bool privateKeyCheck = File.Exists(privateKeyPath);
    if (privateKeyCheck == false)
    {
        using (FileStream fs = File.Create(privateKeyPath))
        {
            fs.Dispose();
            fs.Close();
        }
    }
    return filePath;
}
```
   
# Encrypt
```csharp
public static class Encrypt {...}
```
- Encrypts the data with the key of type RSAParameters sent
```csharp
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
```
- Encrypts data with XML type key sent as string
```csharp
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
```
> `GetBytes()` method
```csharp
private static byte[] GetBytes(string data)
{
    return byteConverter.GetBytes(data);
}
```
> `GetBase64String()` method
```csharp
private static string GetBase64String(byte[] encryptedData)
{
    return Convert.ToBase64String(encryptedData);
}
```
> byteConverter
```csharp
private static UnicodeEncoding byteConverter = new UnicodeEncoding();
```
   
# Decrypt
```csharp
public static class Decrypt {...}
```
- Decrypts the data with the key of type RSAParameters sent
```csharp
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
```
- Decrypts data with XML type key sent as string
```csharp
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
```
> `GetString()` method
```csharp
private static string GetString(byte[] decryptedData)
{
    return byteConverter.GetString(decryptedData);
}
```
> `GetBytes()` method
```csharp
private static byte[] GetBytes(string encryptedData)
{
    return Convert.FromBase64String(encryptedData);
}
```
> byteConverter
```csharp
private static UnicodeEncoding byteConverter = new UnicodeEncoding();
```
