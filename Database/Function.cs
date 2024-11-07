namespace Database;

public partial class Function
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? MenuName { get; set; }

    public int MenuOrder { get; set; }

    public bool IsMenu { get; set; }

    public bool IsMenuHeader { get; set; }

    public string? Icon { get; set; }

    public string? Url { get; set; }

    public int ParentId { get; set; }

    public bool HasChild { get; set; }

    public bool? DeleteFlag { get; set; }

    public int? CreatedUserId { get; set; }

    public int? ModifiedUserId { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public DateTime? CreatedDateTime { get; set; }
}
