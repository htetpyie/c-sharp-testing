using Microsoft.AspNetCore.Mvc;

namespace APIApp.Controllers.SerilogTest;

[ApiController]
[Route("[controller]")]
public class CurrencyController : BaseController
{
    [HttpGet("getCurrency")]
    public async Task<IActionResult> Index()
    {
        try
        {
            LogInformation("Api call to get currency");
            return Ok(await GetCurrency());
        }
        catch (Exception ex)
        {
            LogError(ex);
            return BadRequest(ex);
        }
    }

    private async Task<string> GetCurrency()
    {
        var apiLink = "https://forex.cbm.gov.mm/api/latest";
        string jsonReturn = string.Empty;

        using var httpClient = new HttpClient();
        var httpResponse = await httpClient.GetAsync(apiLink);
        if (httpResponse.IsSuccessStatusCode)
        {
            jsonReturn = await httpResponse.Content.ReadAsStringAsync();
        }

        return jsonReturn;
    }
}
