using Database.SQLDbContextModels;
using Microsoft.EntityFrameworkCore;
using Service.Class;
using Shared.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<SQLAppDbContext>(opt =>
	opt.UseSqlServer(builder.Configuration.GetSection("DbConnections:SQLConnection").Value)
	);

// Add services to the container.
builder.Services.AddScoped<QRService>();
builder.Services.AddScoped<ClassService>();
builder.Services.AddControllersWithViews();

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
	pattern: "{controller=Report}/{action=Index}/{id?}");

app.Run();
