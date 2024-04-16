using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Basic.Enctyption
{
    public class EncryptionExample
    {
        public static void Run()
        {
            var dataStr = "This is corporate research! Dont read me!";
            var data = Encoding.UTF8.GetBytes(dataStr);
            var key = GenerateAESKey();

            var encryptedData = Encrypt(data, key, out var iv);

            var encryptedDataAsString = Encoding.UTF8.GetString(encryptedData);

            Console.WriteLine($"{dataStr} => \n key => {Encoding.UTF8.GetString(key)} \n iv => {Encoding.UTF8.GetString(iv)} \n encrypted string => {encryptedDataAsString}");

            var decryptedData = Decrypt(encryptedData, key, iv);
            var decryptedDataAsString = Encoding.UTF8.GetString(decryptedData);

            Console.WriteLine($"{dataStr} => \n key => {Encoding.UTF8.GetString(key)} \n iv => {Encoding.UTF8.GetString(iv)} \n decrypted string => {decryptedDataAsString}");

        }

        private static byte[] Encrypt(byte[] data, byte[] key, out byte[] iv)
        {
            using var aes = Aes.Create();

            aes.Mode = CipherMode.CBC; // better security
            aes.Key = key;
            aes.GenerateIV(); // IV = Initialization Vector

            using var encryptor = aes.CreateEncryptor();

            iv = aes.IV;
            return encryptor.TransformFinalBlock(data, 0, data.Length);
        }


        private static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC; // same as for encryption
                using (var decryptor = aes.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }
        }

        private static byte[] GenerateAESKey()
        {
            var rnd = RandomNumberGenerator.Create();
            var b = new byte[16];
            rnd.GetNonZeroBytes(b);

            return b;
        }
    }
}
