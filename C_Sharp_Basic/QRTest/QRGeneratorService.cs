using QRCoder;
using static QRCoder.PayloadGenerator;

namespace Basic.QRTest;

public class QRGeneratorService
{
	public static void GenerateQR()
	{
		using var qrGenerator = new QRCodeGenerator();
		using QRCodeData qrCodeData = qrGenerator
			.CreateQrCode(new Url("The text which should be encoded."), QRCodeGenerator.ECCLevel.Q);
		using PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

		byte[] qrCodeImage = qrCode.GetGraphic(20);


		var base64String = Convert.ToBase64String(qrCodeImage);
		var byteARr = Convert.FromBase64String(base64String);

		//write to file
		var filePath = Directory.GetCurrentDirectory() + "/QRImage.png";
		File.WriteAllBytes(filePath, qrCodeImage);

		Console.WriteLine(base64String);
	}
}
