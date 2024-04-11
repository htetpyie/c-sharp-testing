using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace C_Sharp_Basic;

public class HttpClientTest
{
    public static async void Run()
    {
        try
        {
            using var client = new HttpClient();

            using var response = await client.GetAsync("https://www.google.com");

            if (response.IsSuccessStatusCode)
            {
                string responeBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responeBody);
            }
            else
            {
                Console.WriteLine("Fetch Error");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
