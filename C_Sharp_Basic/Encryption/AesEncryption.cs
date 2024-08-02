using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Encryption
{
	//https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.aes?view=net-8.0
	public class AesEncryption
	{
		public static void Run()
		{
			string original = "Here is some data to encrypt! မောင်မောင် မမ ထထက ဋ အ ဉီးစားပေးပါ။";

			using var myAes = Aes.Create();
			byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);
			var decrypted = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);
			Console.WriteLine($"Original String => {original}");
			Console.WriteLine($"Encrypted String => {Convert.ToBase64String(encrypted)}");
			Console.WriteLine($"Decrypted String => {decrypted}");
		}

		public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
		{
			// Check Arguments
			if (plainText.IsNullOrEmpty())
				throw new ArgumentNullException("plainText");
			if (Key.IsNullOrEmpty())
				throw new ArgumentNullException("Key");
			if (IV.IsNullOrEmpty())
				throw new ArgumentNullException("IV");
			byte[] encrypted;

			//Create an Aes Object
			//with the specified key and IV
			var aesAlg = Aes.Create();
			aesAlg.Key = Key;
			aesAlg.IV = IV;

			//Create an encryptor to perform the stream transform.
			ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

			//Create the streams used for encryption.
			using (MemoryStream msEncrypt = new MemoryStream())
			{
				using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
				{
					using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
					{
						//Write all data to the stream.
						swEncrypt.Write(plainText);
					}
					encrypted = msEncrypt.ToArray();
				}
			}

			return encrypted;
		}

		public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
		{
			// Check Arguments
			if(cipherText.IsNullOrEmpty()) throw new ArgumentNullException("Text");
			if (Key.IsNullOrEmpty()) throw new ArgumentNullException("Key");
			if (IV.IsNullOrEmpty()) throw new ArgumentNullException("IV");

			//Declare the string used to hold
			// the decrypted text.
			string plainText = null;

			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				// Create a decryptor to perform the stream transform.
				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				// Create the streams used for decryption.
				using (MemoryStream msDecrypt = new MemoryStream(cipherText))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{

							// Read the decrypted bytes from the decrypting stream
							// and place them in a string.
							plainText = srDecrypt.ReadToEnd();
						}
					}
				}
			}

			return plainText;
		}
	}
}