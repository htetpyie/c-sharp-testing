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

```Scaffold-DbContext "Server=localhost;Database=admin_portal_log;User=root;Password=root"  Pomelo.EntityFrameworkCore.MySql -o LogDbContextModels --context LogDbContext -f

dotnet ef dbcontext scaffold "Server=localhost;Database=admin_portal_log;User=root;Password=root;" Pomelo.EntityFrameworkCore.MySql -o LogDbContextModels --context LogDbContext -f

dotnet ef dbcontext scaffold "Server=localhost;Database=admin_portal_config;User=root;Password=root;" Pomelo.EntityFrameworkCore.MySql -o ConfigDbContextModels --context ConfigDbContext -f
```
```
dotnet ef dbcontext scaffold "Server=localhost;Database=admin_portal;User=root;Password=root;" Pomelo.EntityFrameworkCore.MySql -o AppDbContextModels --context AppDbContext -t function -f
```

