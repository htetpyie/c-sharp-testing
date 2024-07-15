using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Threading.Tasks.Dataflow;

//https://www.google.com/search?sca_esv=f78427f5824e21a0&sxsrf=ADLYWIKGp7UcI8v6rSVpks8y5EFyFTHy_w:1721033929194&q=memory+caching+aps.net+core&tbm=vid&source=lnms&fbs=AEQNm0Aa4sjWe7Rqy32pFwRj0UkWfbQph1uib-VfD_izZO2Y5sC3UdQE5x8XNnxUO1qJLaRfKvQK6KSkFrWZdGNeSbyPYLbGwYpXwowDysIKFZ-15Za86EOyW0mw36V7RoBuNtxUud7n95sZMDqL32cREfnu6ywPFDZoBr_1xEBH7wIzdaswHhx09FHtRMcq-imyJCfmxo8J&sa=X&ved=2ahUKEwjlzvv_1qiHAxVzSGcHHStpBvQQ0pQJegQIFBAB&biw=1536&bih=695&dpr=1.25#fpstate=ive&vld=cid:90df7c99,vid:MSUTojuUEX4,st:0

namespace MVCApp.Caching
{
    public class CacheController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        private List<Blog> BlogList = new();

        private void CreateBlogList()
        {
            if (!BlogList.Any())
            {
                foreach (var i in Enumerable.Range(0, 1000))
                {
                    BlogList.Add(new Blog { Title = "Title", Author = "Author" });
                }
            }
        }

        private string key = "blogListCache";
        public CacheController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            CreateBlogList();
        }

        public IActionResult Index()
        {
            var sw = Stopwatch.StartNew();

            if (_memoryCache.TryGetValue(key, out IEnumerable<Blog> blogs))
            {
                Console.WriteLine("Product is found in cache.");
            }
            else
            {
                blogs = BlogList;
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(10))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(20))
                    .SetPriority(CacheItemPriority.Normal);

                _memoryCache.Set(key, blogs, cacheOptions);

                Console.WriteLine("Product is not found in cache.");
            }
            sw.Stop();
            var time = sw.ElapsedMilliseconds;

            return Ok(time);
        }

        public IActionResult ClearCache()
        {
            _memoryCache.Remove(key);

            return Ok("Cache is cleared.");
        }
    }

    public class Blog
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
    }

}
