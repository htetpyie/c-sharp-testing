using APIApp.Controllers.FilterTesting.ActionFilter;
using APIApp.GraphQl;
using Asp.Versioning;
using Serilog;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Rate Limitting
https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-8.0https://learn.microsoft.com/en-us/aspnet/core/performance/rate-limit?view=aspnetcore-8.0
//fixed
builder.Services.AddRateLimiter(_ => _
	.AddFixedWindowLimiter(policyName: "fixed", options =>
	{
		options.PermitLimit = 2;
		options.Window = TimeSpan.FromSeconds(12);
		options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
		options.QueueLimit = 5;
	}));

//concurrency

builder.Services.AddRateLimiter(_ => _
	.AddConcurrencyLimiter(policyName: "concurrency", options =>
	{
		options.PermitLimit = 2;
		options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
		options.QueueLimit = 3;
	}));
#endregion

#region Logging By Asp.Net
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();
#endregion

#region Serilog
//https://github.com/serilog/serilog-aspnetcore
builder
	.Configuration
	.AddJsonFile("appsettings.json")
	.AddJsonFile("serilogConfiguration.json")
	.Build();

builder.Services.AddSerilog((services, lc) => lc
	.ReadFrom.Configuration(builder.Configuration)
	.ReadFrom.Services(services)
	.Enrich.FromLogContext()
	.WriteTo.Console());

#endregion

#region Add filter globally
builder.Services.AddControllers(option =>
{
	option.Filters.Add<ActionFilterSample>();
});
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Graphql
builder.Services
	.AddRouting()
	.AddGraphQLServer()
	.AddQueryType<Query>()
	.AddMutationType<Mutation>()
	.AddType<ProductType>()
	.AddType<ProductInputType>();
#endregion

#region API Versioning
var apiVersioningBuilder = builder.Services.AddApiVersioning(o =>
{
	o.AssumeDefaultVersionWhenUnspecified = true;
	o.DefaultApiVersion = new ApiVersion(2, 0);
	o.ReportApiVersions = true;
	o.ApiVersionReader = ApiVersionReader.Combine(
		new QueryStringApiVersionReader("api-version"),
		new HeaderApiVersionReader("X-Version"),
		new MediaTypeApiVersionReader("ver"));
});

apiVersioningBuilder.AddApiExplorer(
	options =>
	{
		options.GroupNameFormat = "'v'VVV";
		options.SubstituteApiVersionInUrl = true;
	});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();

	// Enable the Playground middleware
	//app.UsePlayground();
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseStaticFiles();
// app.UseCookiePolicy();

app.UseRouting();
// app.UseRateLimiter();
// app.UseRequestLocalization();
// app.UseCors();

// app.UseSession();
// app.UseResponseCompression();
// app.UseResponseCaching();

//Configure for Serilog
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Map the GraphQL endpoint
app.MapGraphQL("/graphql");

//Middleware

app.Use(async (context, next) =>
{
	// Do work that can write to the Response.
	await next.Invoke();
	//await context.Response.WriteAsync("Just writing from middleware.");
	// Do logging or other work that doesn't write to the Response.
});

//app.Run(async context =>
//{
//	await context.Response.WriteAsync("Hello from 2nd delegate.");
//});
app.UseRateLimiter();


app.Run();
