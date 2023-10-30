using System;
using System.Collections.Generic;

namespace tyf.data.service.DbModels;

public partial class AccessGroup
{
    public Guid AccessGroupId { get; set; }

    public string AccessGroupName { get; set; } = null!;

    public string AccessGroupNamespace { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<AccessGroupAccount> AccessGroupAccounts { get; set; } = new List<AccessGroupAccount>();

    public virtual ICollection<AccessGroupRole> AccessGroupRoles { get; set; } = new List<AccessGroupRole>();
}
