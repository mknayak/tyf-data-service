using System;
using System.Collections.Generic;

namespace tyf.data.service.DbModels;

public partial class AccessToken
{
    public Guid AccessTokenId { get; set; }

    public string? AccessScope { get; set; }

    public string? AccessToken1 { get; set; }

    public string RefreshToken { get; set; } = null!;

    public DateTime AccessTokenExpiry { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid AccessAccountId { get; set; }

    public virtual AccessAccount AccessAccount { get; set; } = null!;
}
