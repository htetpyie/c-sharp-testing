using System.Diagnostics;

namespace Basic.HttpClients;

public class HttpClientTest
{
	public static async void Run()
	{
		try
		{
			using var client = new HttpClient();

			using var response = await client.GetAsync("https://www.google.com");

			var watch = Stopwatch.StartNew();
			if (response.IsSuccessStatusCode)
			{
				string responeBody = await response.Content.ReadAsStringAsync();
				Console.WriteLine(responeBody);
			}
			else
			{
				Console.WriteLine("Fetch Error");
			}
			watch.Stop();
			Console.WriteLine($"Escapsed Time => {watch.Elapsed}");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			throw;
		}
	}
}
