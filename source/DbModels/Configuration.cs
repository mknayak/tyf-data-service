namespace tyf.data.service.DbModels;

public partial class Configuration
{
    public string ConfigurationKey { get; set; } = null!;

    public short? ConfigurationType { get; set; }

    public decimal ConfigurationVersion { get; set; }

    public string? ConfigurationValue { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string UpdatedBy { get; set; } = null!;
}
