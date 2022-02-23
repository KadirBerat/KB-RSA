# KB-RSA
 A classlibrary project released as nuget that makes it easy to use for key generation, encryption, and decryption with RSA.

All classes in the project are in static structure.

# KeySizes
   => enum KeySize: A list of accepted key sizes for RSA
   
# RSAKeyModel
   => publicKey, privateKey: Two property to store public and private key
   
# GenerateKey
   => RSAGenerateKey: Returns the public and private key it generates according to the key size sent into it as an RSAKeyModel object.
   => RSAGenerateXMLKey: It creates a folder on the desktop with the date and time of the day and saves the public and private key it generates according to the key size sent to it.
   
# Encrypt
   => EncryptWithKey: Encrypts the data with the key of type RSAParameters sent
   => EncryptWithXMLKey: Encrypts data with XML type key sent as string
   
# Encrypt
   => DecryptWithKey: Decrypts the data with the key of type RSAParameters sent
   => DecryptWithXMLKey: Decrypts data with XML type key sent as string
