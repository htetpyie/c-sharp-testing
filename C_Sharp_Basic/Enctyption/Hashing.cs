using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Basic;

public class Hashing
{

    public static void Run()
    {
        var text = "Hello World";
        var text1 = " var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(text));";

        //var watch = Stopwatch.StartNew();
        var result = HashUsingMD5(text).Result;
        var result1 = HashUsingMD5(text1).Result;
        
        Console.WriteLine($"Hashed value of 'Hello World' => \n {result} \n length is => {result.Length}");
        Console.WriteLine($"Hashed value of 'var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(text));' => \n {result1} \n length is {result1.Length}");
        //watch.Stop();
        //Console.WriteLine($"Elapsed Time is => {watch.Elapsed.TotalMilliseconds}");

        Console.WriteLine("\n ---- Using SHA -------"); 
        var ShaResult = HashUsingMD5(text).Result;
        var ShaResult1 = HashUsingMD5(text1).Result;

        Console.WriteLine($"Hashed value of 'Hello World' => \n {ShaResult} \n length is => {ShaResult.Length}");
        Console.WriteLine($"Hashed value of 'var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(text));' => \n {ShaResult1} \n length is {ShaResult1.Length}");

    }

    private static async Task<string> HashUsingMD5(string text)
    {
        var hashedResutl = string.Empty;

        var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(text));

        byte[] hashOne;

        using var hasher = MD5.Create();
        hashOne = await hasher.ComputeHashAsync(strStreamOne);
        hashedResutl = Convert.ToBase64String(hashOne);

        return hashedResutl;
    }

    private static async Task<string> HashUsingSHA(string text)
    {
        var hashedResutl = string.Empty;

        var strStreamOne = new MemoryStream(Encoding.UTF8.GetBytes(text));

        byte[] hashOne;

        using var hasher = SHA256.Create();
        hashOne = await hasher.ComputeHashAsync(strStreamOne);
        hashedResutl = Convert.ToBase64String(hashOne);

        return hashedResutl;
    }
}
