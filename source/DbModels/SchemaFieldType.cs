using System;
using System.Collections.Generic;

namespace tyf.data.service.DbModels;

public partial class SchemaFieldType
{
    public int FieldTypeId { get; set; }

    public string FieldTypeName { get; set; } = null!;

    public string? FieldTypeDefaultValue { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<SchemaField> SchemaFields { get; set; } = new List<SchemaField>();
}
