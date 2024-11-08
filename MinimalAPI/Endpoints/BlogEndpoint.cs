

using Service.Blog;

namespace MinimalAPI.Endpoints;

public class BlogEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var blogEndpoint = app.MapGroup("/api/blogs").WithOpenApi();

        blogEndpoint.MapGet("/", GetAllBlogsAsync).WithName("Get All Blogs");
        blogEndpoint.MapPost("/save", SaveBlogAsync).WithName("Save Blog");
    }

    private async Task<IResult> GetAllBlogsAsync(IBlogService blogService)
    {
        var blogList = await blogService.GetListAsync();
        return Results.Ok(blogList);
    }

    private async Task<IResult> SaveBlogAsync(IBlogService blogService)
    {
        await blogService.SaveAsync();
        return Results.Ok("Saved successfully.");
    }
}
