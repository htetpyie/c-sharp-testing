namespace Shared.Models;

public class CustomSettingModel
{
    public DbConnectionModel? DbConnections { get; set; }
}

public class DbConnectionModel
{
    public string? MYSQLConnection { get; set; }
    public string? SQLConnection { get; set; }
}
