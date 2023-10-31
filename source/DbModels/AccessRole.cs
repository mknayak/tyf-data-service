namespace tyf.data.service.DbModels;

public partial class AccessRole
{
    public Guid AccessRoleId { get; set; }

    public string AccessRoleName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<AccessAccountRole> AccessAccountRoles { get; set; } = new List<AccessAccountRole>();

    public virtual ICollection<AccessGroupRole> AccessGroupRoles { get; set; } = new List<AccessGroupRole>();
}
