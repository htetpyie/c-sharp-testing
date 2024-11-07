using Mapster;
using MinimalAPI;
using Service.Blog;
using Shared.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.AddStageConfig(builder.Environment.ContentRootPath);
builder.Services.Configure<CustomSettingModel>(configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Redis
var redisConfiguration = builder.Configuration.GetSection("Redis")["URL"];
//var redis = ConnectionMultiplexer.Connect(redisConfiguration);
//builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
//builder.Services.AddSingleton<RedisService>();
builder.Services.InjectServices(configuration);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/blogs", async (IBlogService blogService) =>
{
    var blogs = await blogService.GetListAsync();
    return blogs;
}).WithName("Get Blogs by PostgreSQL");


app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/persons", () =>
{
    var person = Person.CreatePerson();

    PersonDto personDto = person.Adapt<PersonDto>();
    return personDto;

}).WithName("Get Person")
  .WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class Person
{
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Address? Address { get; set; }

    public static Person CreatePerson()
    {
        return new Person()
        {
            Title = "Mr.",
            FirstName = "Peter",
            LastName = "Pan",
            DateOfBirth = new DateTime(2000, 1, 1),
            Address = new Address()
            {
                Country = "Neverland",
                PostCode = "123N",
                Street = "Funny Street 2",
                City = "Neverwood"
            },
        };
    }
}

public class Address
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostCode { get; set; }
    public string? Country { get; set; }
}

public class PersonDto
{
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
}


public static class ConfigExtension
{
    public static IConfiguration AddStageConfig(this IConfigurationBuilder configurationBuilder, string currentRootPath)
    {
        var configuration = (IConfiguration)configurationBuilder;
        var stage = configuration.GetSection("Stage").Value;

        string jsonPath = Path.Combine(currentRootPath, "..", "Config", $"custom-setting-{stage}.json");
        configurationBuilder.AddJsonFile(jsonPath, optional: false, reloadOnChange: true);
        return configurationBuilder.Build();
    }
}