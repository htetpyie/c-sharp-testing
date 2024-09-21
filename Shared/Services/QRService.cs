using QRCoder;

namespace Shared.Services;
public class QRService
{
	public byte[] GenerateQR(string text)
	{
		using var qrGenerator = new QRCodeGenerator();
		using QRCodeData qrCodeData = qrGenerator
			.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
		using PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
		//using QRCode qr = new QRCode(qrCodeData);

		byte[] qrCodeImage = qrCode.GetGraphic(20);

		return qrCodeImage;
	}
}
