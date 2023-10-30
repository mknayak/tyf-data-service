using System;

namespace tyf.data.service.Models
{
    public class SchemaModel
    {
        public SchemaModel()
        {
            Fields = new Dictionary<string, SchemaFieldModel>();
        }
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public IDictionary<string, SchemaFieldModel> Fields { get; set; }
    }
}

