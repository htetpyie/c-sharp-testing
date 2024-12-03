using Microsoft.Extensions.Configuration;

namespace Shared.Extensions;

public static class ConfigurationExtension
{

	public static IConfiguration AddStageConfig(this IConfigurationBuilder configurationBuilder, string currentRootPath)
	{
		var configuration = (IConfiguration)configurationBuilder;
		var stage = configuration.GetSection("Stage").Value;

		string jsonPath = Path.Combine(currentRootPath, "..", "Config", $"custom-setting-{stage}.json");
		//configurationBuilder.AddJsonFile(jsonPath, optional: false, reloadOnChange: true);
		return configurationBuilder.Build();
	}
}
