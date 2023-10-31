namespace tyf.data.service.Requests
{
    public class CreateInstanceRequest
	{
		public Guid SchemaId { get; set; }
		public required string Namespace { get; set; }
		public required string Name { get; set; }
	
	}
}

