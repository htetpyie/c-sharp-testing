namespace Shared.Models;

public class CustomSettingModel
{
	public DbConnectionModel? DbConnections { get; set; }
	public RedisModel? Redis { get; set; }
}

public record DbConnectionModel(string? MYSQLConnection, string? SQLConnection) { }

public record RedisModel(string URL, TimeSpan? CacheExpireTime) { }