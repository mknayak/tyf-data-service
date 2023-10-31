namespace tyf.data.service.DbModels;

public partial class SchemaDatum
{
    public Guid SchemaDataId { get; set; }

    public Guid SchemaInstanceId { get; set; }

    public Guid SchemaFieldId { get; set; }

    public string? SchemeDataValue { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual SchemaField SchemaField { get; set; } = null!;

    public virtual SchemaInstance SchemaInstance { get; set; } = null!;
}
