using System;
using System.Collections.Generic;

namespace Database.PostgreDbContextModels;

public partial class TblBlog
{
    public long BlogId { get; set; }

    public string? BlogTitle { get; set; }

    public string? BlogAuthor { get; set; }

    public string? BlogContent { get; set; }
}
