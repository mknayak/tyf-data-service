namespace tyf.data.service.DbModels;

public partial class DataSchema
{
    public Guid SchemaId { get; set; }

    public string SchemaName { get; set; } = null!;

    public string SchemaNameSpace { get; set; } = null!;

    public bool? IsPublic { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public virtual ICollection<SchemaField> SchemaFields { get; set; } = new List<SchemaField>();

    public virtual ICollection<SchemaInstance> SchemaInstances { get; set; } = new List<SchemaInstance>();
}
