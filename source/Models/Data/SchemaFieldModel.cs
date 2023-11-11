namespace tyf.data.service.Models
{
    /// <summary>
    /// Model for a schema field.
    /// </summary>
    public class SchemaFieldModel
    {
        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        public required string FieldName { get; set; }
        /// <summary>
        /// Gets or sets the type of the field.
        /// </summary>
        public required int FieldType { get; set; }
        /// <summary>
        /// Gets or sets if the field is required.
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// Gets or sets the regex validation for the field.
        /// </summary>
        public string? RegexCheck { get; set; }
    }
}

