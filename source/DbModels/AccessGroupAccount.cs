namespace tyf.data.service.DbModels;

public partial class AccessGroupAccount
{
    public Guid AccessGroupAccountId { get; set; }

    public Guid AccessAccountId { get; set; }

    public Guid AccessGroupId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public virtual AccessAccount AccessAccount { get; set; } = null!;

    public virtual AccessGroup AccessGroup { get; set; } = null!;
}
