namespace tyf.data.service.Requests
{
    public class CreateSchemaInstanceRequest
    {
		public Guid SchemaId { get; set; }
        public required string Name { get; set; }
        public required IDictionary<string, SchemaFieldRequest> Fields { get; set; }
    }
}

