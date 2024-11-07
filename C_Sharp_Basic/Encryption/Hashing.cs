using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Encryption;

public class Hashing
{
    public static void Run()
    {
        var md5 = new HashDelegate(HashUsingMD5);
        GenerateOutputs(md5);

        var sha256 = new HashDelegate(HashUsingSHA);
        GenerateOutputs(sha256);

        var hmac = new HashDelegate(HashUsingHMAC);
        GenerateOutputs(hmac);
    }

    private delegate string HashDelegate(string text);

    private static void GenerateOutputs(HashDelegate hashMethod)
    {
        Console.WriteLine($"\n ---- Using {hashMethod.Method.Name} -------");

        var text = "Hello World";
        var text1 = " var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(text));";

        var result = hashMethod(text);
        var result1 = hashMethod(text1);

        Console.WriteLine($"Hashed value of '{text}' => \n {result} \n length is => {result.Length}");
        Console.WriteLine($"Hashed value of '{text1}' => \n {result1} \n length is {result1.Length}");

    }

    private static string HashUsingMD5(string text)
    {
        var hashedResutl = string.Empty;

        var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(text));

        byte[] hashOne;

        using var hasher = MD5.Create();
        hashOne = hasher.ComputeHashAsync(strStreamOne).Result;
        hashedResutl = Convert.ToBase64String(hashOne);

        return hashedResutl;
    }

    private static string HashUsingSHA(string text)
    {
        var hashedResutl = string.Empty;

        var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(text));

        byte[] hashOne;

        using var hasher = SHA256.Create();
        hashOne = hasher.ComputeHashAsync(strStreamOne).Result;
        hashedResutl = Convert.ToBase64String(hashOne);

        return hashedResutl;
    }

    private static string HashUsingHMAC(string text)
    {
        var hashedResutl = string.Empty;

        var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(text));

        byte[] hashOne;
        byte[] key = Encoding.UTF8.GetBytes("superSecretH4shKey1!");

        using var hasher = new HMACSHA256(key);
        hashOne = hasher.ComputeHashAsync(strStreamOne).Result;
        hashedResutl = Convert.ToBase64String(hashOne);

        return hashedResutl;
    }
}
