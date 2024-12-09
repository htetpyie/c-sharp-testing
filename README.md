# [Git Docs](https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax)


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
##### Queries (rote to backup file location)
```
sudo mysqldump -u username -p dbname.tablename > backupfile.sql
mysql -u username -p < backupfile.sql
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
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

#### SQL Server
```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

#### PostgreSQL

```
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL.Design

Scaffold-DbContext "Host=localhost;Database=mydatabase;Username=myuser;Password=mypassword" Npgsql.EntityFrameworkCore.PostgreSQL -o PostgreDbContextModels

dotnet ef dbcontext scaffold "Host=localhost;Database=database;Username=username;Password=root" Npgsql.EntityFrameworkCore.PostgreSQL -o PostgreDbContextModels --context AppDbContext -f

"ConnectionString": "Server=localhost;Database=elearning;User Id=ed;Password=YourPassword;"
```
##### [Learn PostgreSql](https://neon.tech/postgresql/)
##### [Naming Convension](https://www.geeksforgeeks.org/postgresql-naming-conventions/)

* Table Name -> plural nouns, snake_case (products)
* Column Name -> snake_case with table name (product_id)
* Index Name -> idx_snake_case (idx_product_name)


#### [SQL Lite]
```
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
```

#### [Lite Db](https://www.litedb.org/)
```
dotnet add package LiteDB
```

## [QR Coder](https://github.com/codebude/QRCoder/wiki/Advanced-usage---QR-Code-renderers#2-overview-of-the-different-renderers)
```
QRCodeGenerator qrGenerator = new QRCodeGenerator();
QRCodeData qrCodeData = qrGenerator.CreateQrCode("The payload aka the text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
QRCode qrCode = new QRCode(qrCodeData);
Bitmap qrCodeImage = qrCode.GetGraphic(20);
```
#### [Refrences](https://medium.com/@umairg404/generate-qr-codes-in-net-core-minimal-api-with-qrcoder-library-6eeeceb7e9aa)

#### [ULID](https://github.com/Cysharp/Ulid)
```
dotnet add package Ulid
```

#### [Redis](https://redis.io/learn/develop/dotnet)
```
dotnet add package StackExchange.Redis
```

#### [Docker Containerization with .Net](https://learn.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=windows)

## To Learn
[.net 8 blazor](https://akifmt.github.io/dotnet/2024-07-28-blazor-radzen-.net8-authenticationauthorization-with-identity/)


### Reporting
#### Fast Report
[FastReport.Web](https://github.com/FastReports/FastReport.Documentation/blob/master/WebReport.md)

### [Microsoft Identity](https://medium.com/@mohamed.ebrahim.mohsen/net8-identity-register-login-email-confirmation-and-two-factor-authentication-2fa-c8acfbc3e14c) 
```
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools

```

```
builder.Services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddAuthorizationBuilder();
```

```
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("DataSource=app.db");
});

builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

app.MapIdentityApi<IdentityUser>();
```