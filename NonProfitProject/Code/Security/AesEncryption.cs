using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace NonProfitProject.Code.Security
{
    public class AesEncryption
    {
        ///////////This code was not created by me. Credit goes to: https://damienbod.com/2020/08/19/symmetric-and-asymmetric-encryption-in-net-core/

        //used to create the key and IV
        public (string Key, string IVBase64) InitSymmetricEncryptionKeyIV()
        {
            var key = GetEncodedRandomString(32); // 256
            Aes cipher = CreateCipher(key);
            var IVBase64 = Convert.ToBase64String(cipher.IV);
            return (key, IVBase64);
        }
        //creates a random string
        private string GetEncodedRandomString(int length)
        {
            var base64 = Convert.ToBase64String(GenerateRandomBytes(length));
            return base64;
        }
        
        //generates random bytes
        private byte[] GenerateRandomBytes(int length)
        {
            var byteArray = new byte[length];
            RandomNumberGenerator.Fill(byteArray);
            return byteArray;
        }




        //keys should typically not be stored in the source code, however, I was unable to figure out how to use Azure key valut as I intended to do due to time constraint. In the real world, this key would be
        //store somewhere else besides the source code or the database. 
        //for now, we will be using this key and Iv that was generated through the code above.
        readonly string key = "KvaG2bTeHTgRFhu7T80CzNzWFTyvpuvbr/7N4IHmvHM=";
        readonly string IV = "I/Yum2/w3jAWolO4rzs+Ow==";


        //creates Cipher object using the key
        private Aes CreateCipher(string keyBase64)
        {
            // Default values: Keysize 256, Padding PKC27
            Aes cipher = Aes.Create();
            cipher.Mode = CipherMode.CBC;  // Ensure the integrity of the ciphertext if using CBC

            cipher.Padding = PaddingMode.ISO10126;
            cipher.Key = Convert.FromBase64String(keyBase64);

            return cipher;
        }
        /////////////////////(string text, string key, string IV)
        //encrypts text
        public string Encrypt(string text)
        {
            Aes cipher = CreateCipher(key);
            cipher.IV = Convert.FromBase64String(IV);

            ICryptoTransform cryptTransform = cipher.CreateEncryptor();
            byte[] plaintext = Encoding.UTF8.GetBytes(text);
            byte[] cipherText = cryptTransform.TransformFinalBlock(plaintext, 0, plaintext.Length);

            return Convert.ToBase64String(cipherText);
        }
        //decrypts encrypted text
        public string Decrypt(string encryptedText)
        {
            Aes cipher = CreateCipher(key);
            cipher.IV = Convert.FromBase64String(IV);

            ICryptoTransform cryptTransform = cipher.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] plainBytes = cryptTransform.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
