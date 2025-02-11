// See https://aka.ms/new-console-template for more information
//AesEncryption.Run();
//QRGeneratorService.GenerateQR();
//new QRService().GenerateQRWithImage();
//HttpClientTest.Run();
//EncryptionExample.RunDSA();
//new PDFExamples().GenerateUsingPDFSharpCore();

//JWTToken.Run();

using Basic.DbBackup;

var ulId = Ulid.NewUlid();
var ulId2 = Ulid.NewUlid();

Console.WriteLine(ulId);
Console.WriteLine(ulId2);

MySqlBackup.PerformBackup();

Console.ReadLine();