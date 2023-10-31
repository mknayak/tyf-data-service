namespace tyf.data.service.DbModels;

public partial class AccessAccountRole
{
    public Guid AccessAccountRoleId { get; set; }

    public Guid AccessAccountId { get; set; }

    public Guid AccessRoleId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public virtual AccessAccount AccessAccount { get; set; } = null!;

    public virtual AccessRole AccessRole { get; set; } = null!;
}
