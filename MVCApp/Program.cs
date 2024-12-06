using Database.SQLDbContextModels;
using FastReport.Data;
using Microsoft.EntityFrameworkCore;
using Service.Class;
using Shared.DbServices;
using Shared.Models;
using Shared.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.AddStageConfig(builder.Environment.ContentRootPath);
builder.Services.Configure<CustomSettingModel>(configuration);

builder.Services.AddDbContext<SQLAppDbContext>(opt =>
	opt.UseSqlServer(builder.Configuration.GetSection("DbConnections:SQLConnection").Value)
	);

// Add services to the container.
builder.Services.AddScoped<QRService>();
builder.Services.AddScoped<ClassService>();
builder.Services.AddScoped<JsonService>();
builder.Services.AddScoped<DataSetService>();
builder.Services.AddControllersWithViews();
builder.Services.AddFastReport();


FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Report}/{action=FastReport}/{id?}");
app.UseFastReport();

app.Run();


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
