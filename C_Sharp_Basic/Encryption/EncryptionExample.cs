using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Basic;

public class EncryptionExample
{
    public static void RunAES()
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

    #region AES
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
    #endregion

    #region RSA

    public static void RunRSA()
    {
        var dataStr = "This is corporate research! Dont read me!";
        var data = Encoding.UTF8.GetBytes(dataStr);
        var keyLength = 2048; // size in bits
        GenerateKeys(keyLength, out var publicKey, out var privateKey);
        var encryptedData = Encrypt(data, publicKey);
        var encryptedDataAsString = Convert.ToHexString(encryptedData);
        var decryptedDataAsString = Encoding.UTF8.GetString(Decrypt(encryptedData, privateKey));
        Console.WriteLine("Encrypted Value:\n" + encryptedDataAsString);
        Console.WriteLine("Decrypted Value:\n" + decryptedDataAsString);
    }

    private static void GenerateKeys(int keyLength, out RSAParameters publicKey, out RSAParameters privateKey)
    {
        using (var rsa = RSA.Create())
        {
            rsa.KeySize = keyLength;
            publicKey = rsa.ExportParameters(includePrivateParameters: false);
            privateKey = rsa.ExportParameters(includePrivateParameters: true);
        }
    }

    private static byte[] Encrypt(byte[] data, RSAParameters publicKey)
    {
        using (var rsa = RSA.Create())
        {
            rsa.ImportParameters(publicKey);
            var result = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
            return result;
        }
    }

    public static byte[] Decrypt(byte[] data, RSAParameters privateKey)
    {
        using (var rsa = RSA.Create())
        {
            rsa.ImportParameters(privateKey);
            return rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
        }
    }

    #endregion

    #region DSA

    public static void RunDSA()
    {
        var dsa = DSA.Create();
        var dataStr = "This is corporate research! Dont read me!";
        var data = Encoding.UTF8.GetBytes(dataStr);

        var signedData = Sign(dsa, data);

        dsa.Dispose();

    }

    public static byte[] Sign(DSA dsa, byte[] data)
    {
        if (dsa is null)
            throw new NullReferenceException(nameof(dsa));

        var result = dsa.SignData(data, HashAlgorithmName.SHA256);

        return result;
    }

    public static bool VerifySignature(DSA dsa, byte[] data, byte[] signedData)
    {
        if (dsa is null)
            throw new NullReferenceException(nameof(dsa));

        return dsa.VerifyData(data, signedData, HashAlgorithmName.SHA256);
    }
    #endregion
}
