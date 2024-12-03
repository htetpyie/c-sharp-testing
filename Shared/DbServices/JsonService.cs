using Newtonsoft.Json;

namespace Shared.DbServices
{
	public class JsonService
	{
		public List<T> ReadJson<T>(string jsonFilePath) where T : class
		{
			try
			{
				if (string.IsNullOrWhiteSpace(jsonFilePath) || !Path.Exists(jsonFilePath))
					return new List<T>();

				using var streamReader = new StreamReader(jsonFilePath);
				string jsonData = streamReader.ReadToEnd();

				return JsonConvert.DeserializeObject<List<T>>(jsonData);
			}
			catch (Exception)
			{
				return null;
			}
		}
	}
}
