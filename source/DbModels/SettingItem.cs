namespace tyf.data.service.DbModels;

public partial class SettingItem
{
    public Guid SettingItemId { get; set; }

    public string SettingItemKey { get; set; } = null!;

    public string SettingItemNamespace { get; set; } = null!;

    public decimal SettingItemVersion { get; set; }

    public string? SettingItemValue { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;
}
