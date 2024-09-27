# [Git Docs](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax)

# C_Sharp_Basic


## Blazor
### 1. [Routing & navigating](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-8.0)
- Default static
- Router
- Route Parameters
- Navigation
``` 
 @attribute [Route(Constants.CounterRoute)]
 ```


## Minimal API


## Shared
- [Mapster](https://code-maze.com/mapster-aspnetcore-introduction/)

## Scaffolding

#### MySQL

```Scaffold-DbContext "Server=localhost;Database=admin_portal_log;User=root;Password=root"  Pomelo.EntityFrameworkCore.MySql -o LogDbContextModels --context LogDbContext -f

dotnet ef dbcontext scaffold "Server=localhost;Database=admin_portal_log;User=root;Password=root;" Pomelo.EntityFrameworkCore.MySql -o LogDbContextModels --context LogDbContext -f

dotnet ef dbcontext scaffold "Server=localhost;Database=admin_portal_config;User=root;Password=root;" Pomelo.EntityFrameworkCore.MySql -o ConfigDbContextModels --context ConfigDbContext -f

dotnet ef dbcontext scaffold "Server=localhost;Database=hppm;User=root;Password=root;" Pomelo.EntityFrameworkCore.MySql -o AppDbContextModels --context AppDbContext -f
dotnet ef dbcontext scaffold "Server=localhost;Database=hpmm;User=root;Password=root;" Pomelo.EntityFrameworkCore.MySql -o AppDbContextModels --context AppDbContext -t function -f
```

#### SQL Server
```
dotnet ef dbcontext scaffold "Data Source=.;Initial Catalog=HPPM;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o SQLDbContextModels --context SQLAppDbContext -f
```

## Database

#### Entity Framework Core
```
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools 
```

#### MySQL
```
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 8.0.0
```


#### SQL Server
```

```


## [QR Coder](https://github.com/codebude/QRCoder/wiki/Advanced-usage---QR-Code-renderers#2-overview-of-the-different-renderers)
```
QRCodeGenerator qrGenerator = new QRCodeGenerator();
QRCodeData qrCodeData = qrGenerator.CreateQrCode("The payload aka the text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
QRCode qrCode = new QRCode(qrCodeData);
Bitmap qrCodeImage = qrCode.GetGraphic(20);
```
#### [Refrences](https://medium.com/@umairg404/generate-qr-codes-in-net-core-minimal-api-with-qrcoder-library-6eeeceb7e9aa)


## To Learn
[.net 8 blazor](https://akifmt.github.io/dotnet/2024-07-28-blazor-radzen-.net8-authenticationauthorization-with-identity/)
