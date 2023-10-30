namespace tyf.data.service.Models
{
    public class SchemaFieldModel
    {
        public required string FieldName { get; set; }
        public required int FieldType { get; set; }
        public bool IsRequired { get; set; }
        public string? RegexCheck { get; set; }
    }
}

