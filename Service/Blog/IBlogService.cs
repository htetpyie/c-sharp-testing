using Database.PostgreDbContextModels;

namespace Service.Blog
{
    public interface IBlogService
    {
        Task<List<TblBlog>> GetListAsync();
        Task SaveAsync();
    }
}