using System;
using System.Collections.Generic;

namespace tyf.data.service.DbModels;

public partial class SchemaInstance
{
    public Guid SchemaInstanceId { get; set; }

    public Guid SchemaId { get; set; }

    public string? SchemaInstanceName { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string SchemaInstanceNamespace { get; set; } = null!;

    public virtual DataSchema Schema { get; set; } = null!;

    public virtual ICollection<SchemaDatum> SchemaData { get; set; } = new List<SchemaDatum>();
}
