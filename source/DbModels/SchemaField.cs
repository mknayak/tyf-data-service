namespace tyf.data.service.DbModels;

public partial class SchemaField
{
    public Guid SchemaFieldId { get; set; }

    public Guid SchemaId { get; set; }

    public string SchemaFieldName { get; set; } = null!;

    public int SchemaFieldTypeKey { get; set; }

    public bool SchemaFieldIsRequired { get; set; }

    public string? SchemaFieldRegex { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual DataSchema Schema { get; set; } = null!;

    public virtual ICollection<SchemaDatum> SchemaData { get; set; } = new List<SchemaDatum>();

    public virtual SchemaFieldType SchemaFieldTypeKeyNavigation { get; set; } = null!;
}
