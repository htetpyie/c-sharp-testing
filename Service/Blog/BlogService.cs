﻿using Database.PostgreDbContextModels;
using Microsoft.EntityFrameworkCore;

namespace Service.Blog;

public class BlogService : IBlogService
{
    private readonly AppDbContext _postgreContext;

    public BlogService(AppDbContext postgreContext)
    {
        _postgreContext = postgreContext;
    }

    public async Task<List<TblBlog>> GetListAsync()
    {
        var list = await _postgreContext.TblBlogs.AsNoTracking().ToListAsync();
        return list;
    }

    public void SaveAsync()
    {
        var tblBlog = new TblBlog
        {
            BlogTitle = "Blog Title",
            BlogAuthor = "Blog Author",
            BlogContent = "Blog Content"
        };
        _postgreContext.TblBlogs.AddAsync(tblBlog);
        _postgreContext.SaveChangesAsync();
    }

}
