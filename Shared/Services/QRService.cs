using QRCoder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using static QRCoder.PayloadGenerator;
using static QRCoder.SvgQRCode;

namespace Shared.Services;

public class QRCode : AbstractQRCode
{
	public QRCode(QRCodeData qrCodeData) : base(qrCodeData)
	{
	}

	public Bitmap GetGraphic(int pixelsPerModule,
		Color darkColor,
		Color lightColor,
		Bitmap icon = null,
		int iconSizePercent = 15,
		int iconBorderWidth = 0,
		bool drawQuietZones = true,
		Color? iconBackgroundColor = null)
	{
		int num = (base.QrCodeData.ModuleMatrix.Count - ((!drawQuietZones) ? 8 : 0)) * pixelsPerModule;
		int num2 = ((!drawQuietZones) ? (4 * pixelsPerModule) : 0);
		Bitmap bitmap = new Bitmap(num, num, PixelFormat.Format32bppArgb);
		using Graphics graphics = Graphics.FromImage(bitmap);
		using SolidBrush solidBrush2 = new SolidBrush(lightColor);
		using SolidBrush solidBrush = new SolidBrush(darkColor);
		graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		graphics.CompositingQuality = CompositingQuality.HighQuality;
		graphics.Clear(lightColor);
		bool flag = icon != null && iconSizePercent > 0 && iconSizePercent <= 100;
		for (int i = 0; i < num + num2; i += pixelsPerModule)
		{
			for (int j = 0; j < num + num2; j += pixelsPerModule)
			{
				SolidBrush brush = (base.QrCodeData.ModuleMatrix[(j + pixelsPerModule) / pixelsPerModule - 1][(i + pixelsPerModule) / pixelsPerModule - 1] ? solidBrush : solidBrush2);
				graphics.FillRectangle(brush, new Rectangle(i - num2, j - num2, pixelsPerModule, pixelsPerModule));
			}
		}

		if (flag)
		{
			float num3 = (float)(iconSizePercent * bitmap.Width) / 100f;
			float num4 = (flag ? (num3 * (float)icon.Height / (float)icon.Width) : 0f);
			float num5 = ((float)bitmap.Width - num3) / 2f;
			float num6 = ((float)bitmap.Height - num4) / 2f;
			RectangleF rect = new RectangleF(num5 - (float)iconBorderWidth, num6 - (float)iconBorderWidth, num3 + (float)(iconBorderWidth * 2), num4 + (float)(iconBorderWidth * 2));
			RectangleF destRect = new RectangleF(num5, num6, num3, num4);
			SolidBrush brush2 = (iconBackgroundColor.HasValue ? new SolidBrush(iconBackgroundColor.Value) : solidBrush2);
			if (iconBorderWidth > 0)
			{
				using GraphicsPath path = CreateRoundedRectanglePath(rect, iconBorderWidth * 2);
				graphics.FillPath(brush2, path);
			}

			graphics.DrawImage(icon, destRect, new RectangleF(0f, 0f, icon.Width, icon.Height), GraphicsUnit.Pixel);
		}

		graphics.Save();
		return bitmap;
	}

	internal GraphicsPath CreateRoundedRectanglePath(RectangleF rect, int cornerRadius)
	{
		GraphicsPath graphicsPath = new GraphicsPath();
		graphicsPath.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180f, 90f);
		graphicsPath.AddLine(rect.X + (float)cornerRadius, rect.Y, rect.Right - (float)(cornerRadius * 2), rect.Y);
		graphicsPath.AddArc(rect.X + rect.Width - (float)(cornerRadius * 2), rect.Y, cornerRadius * 2, cornerRadius * 2, 270f, 90f);
		graphicsPath.AddLine(rect.Right, rect.Y + (float)(cornerRadius * 2), rect.Right, rect.Y + rect.Height - (float)(cornerRadius * 2));
		graphicsPath.AddArc(rect.X + rect.Width - (float)(cornerRadius * 2), rect.Y + rect.Height - (float)(cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 0f, 90f);
		graphicsPath.AddLine(rect.Right - (float)(cornerRadius * 2), rect.Bottom, rect.X + (float)(cornerRadius * 2), rect.Bottom);
		graphicsPath.AddArc(rect.X, rect.Bottom - (float)(cornerRadius * 2), cornerRadius * 2, cornerRadius * 2, 90f, 90f);
		graphicsPath.AddLine(rect.X, rect.Bottom - (float)(cornerRadius * 2), rect.X, rect.Y + (float)(cornerRadius * 2));
		graphicsPath.CloseFigure();
		return graphicsPath;
	}
}
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
		using SvgQRCode qrCode = new SvgQRCode(qrCodeData);

		var imageByte = File.ReadAllBytes("wwwroot/img/aircodlogo.jpg");
		SvgLogo logo = new SvgLogo(imageByte);
		var qrCodeImage = qrCode.GetGraphic(pixelsPerModule: 20, Color.DarkBlue, Color.WhiteSmoke, logo: logo);
		#endregion
		var result = Encoding.UTF8.GetBytes(qrCodeImage);
		SaveImage(result);
		return result;
	}

	public void SaveImage(byte[] qrCodeImage)
	{
		var filePath = Directory.GetCurrentDirectory() + "/QRImage.svg";
		File.WriteAllBytes(filePath, qrCodeImage);
	}

	public byte[] GenerateQRWithImage()
	{
		var qrGenerator = new QRCodeGenerator();
		var qrCodedData = qrGenerator.CreateQrCode("QR test", QRCodeGenerator.ECCLevel.Q);
		QRCode qrCode = new QRCode(qrCodedData);
		Bitmap logoImage = new Bitmap(@"wwwroot/img/aircodlogo.jpg");

		using (Bitmap qrCodeAsBitmap = qrCode.GetGraphic(20, Color.Black, Color.WhiteSmoke, logoImage))
		{
			using (MemoryStream ms = new MemoryStream())
			{
				qrCodeAsBitmap.Save(ms, ImageFormat.Png);
				string base64String = Convert.ToBase64String(ms.ToArray());
				SaveImage(ms.ToArray());
				return ms.ToArray();
			}
		}
	}
}
