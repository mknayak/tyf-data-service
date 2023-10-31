namespace tyf.data.service.DbModels;

public partial class AccessGroupRole
{
    public Guid AccessGroupRoleId { get; set; }

    public Guid AccessGroupId { get; set; }

    public Guid AccessRoleId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public virtual AccessGroup AccessGroup { get; set; } = null!;

    public virtual AccessRole AccessRole { get; set; } = null!;
}
