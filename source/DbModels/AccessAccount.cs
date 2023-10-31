namespace tyf.data.service.DbModels;

public partial class AccessAccount
{
    public Guid AccessAccountId { get; set; }

    public string AccessAccountName { get; set; } = null!;

    public string AccessAccountEmail { get; set; } = null!;

    public string AccessAccountSalt { get; set; } = null!;

    public string AccessAccountPasswordhash { get; set; } = null!;

    public bool AccessAccountIsLocked { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;

    public virtual ICollection<AccessAccountRole> AccessAccountRoles { get; set; } = new List<AccessAccountRole>();

    public virtual ICollection<AccessGroupAccount> AccessGroupAccounts { get; set; } = new List<AccessGroupAccount>();

    public virtual ICollection<AccessToken> AccessTokens { get; set; } = new List<AccessToken>();
}
