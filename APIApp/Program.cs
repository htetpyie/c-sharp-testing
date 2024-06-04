using APIApp.Controllers.FilterTesting.ActionFilter;
using APIApp.GraphQl;
using Asp.Versioning;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Add filter globally
builder.Services.AddControllers(option =>
{
    option.Filters.Add<ActionFilterSample>();
});
#endregion

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<ProductType>()
    .AddType<ProductInputType>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

    // Enable the Playground middleware
    app.UsePlayground(new PlaygroundOptions
    {
        Path = "/graphql-playground",
        QueryPath = "/graphql"
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Map the GraphQL endpoint
app.MapGraphQL("/graphql");

app.Run();
