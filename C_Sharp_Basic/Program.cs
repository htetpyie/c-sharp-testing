// See https://aka.ms/new-console-template for more information
using Shared.Services;

//AesEncryption.Run();
//QRGeneratorService.GenerateQR();
new QRService().GenerateQRWithImage();
//HttpClientTest.Run();
//EncryptionExample.RunDSA();
//new PDFExamples().GenerateUsingPDFSharpCore();

//JWTToken.Run();

Console.ReadLine();