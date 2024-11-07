using System;
using System.Collections.Generic;

namespace Database.PostgreDbContextModels;

public partial class TblBook
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public long? CreatedUserId { get; set; }

    public DateTimeOffset? CreatedDate { get; set; }

    public int? ModifiedUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}
