using QRCoder;
using static QRCoder.PayloadGenerator;

namespace Shared.Services;
public class QRService
{
	public byte[] GenerateQR(string text)
	{
		using var qrGenerator = new QRCodeGenerator();

		using QRCodeData qrCodeData = qrGenerator
			.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

		#region PNG Byte QR Code
		using PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
		byte[] qrCodeImage = qrCode.GetGraphic(20);
		#endregion

		return qrCodeImage;
	}

	public byte[] GenerateURLQR(string url)
	{
		using var qrGenerator = new QRCodeGenerator();

		using QRCodeData qrCodeData = qrGenerator
			.CreateQrCode(new Url(url));

		#region PNG Byte QR Code
		using PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
		byte[] qrCodeImage = qrCode.GetGraphic(20);
		#endregion

		return qrCodeImage;
	}

	public void SaveImage(byte[] qrCodeImage)
	{
		var filePath = Directory.GetCurrentDirectory() + "/QRImage.png";
		File.WriteAllBytes(filePath, qrCodeImage);
	}
}
