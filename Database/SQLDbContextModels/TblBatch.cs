using System;
using System.Collections.Generic;

namespace Database.SQLDbContextModels;

public partial class TblBatch
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? ClassId { get; set; }

    public string? Description { get; set; }
}
