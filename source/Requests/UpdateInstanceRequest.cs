namespace tyf.data.service.Requests
{
    public class UpdateInstanceRequest
	{
		public UpdateInstanceRequest()
		{
			Fields = new Dictionary<string, string>();
		}
		public required Guid InstanceId { get; set; }
		public string? Name { get; set; }
		public Dictionary<string,string> Fields { get; set; }
	}
	public class BulkCreateInstanceRequest
	{
		public BulkCreateInstanceRequest()
		{
			Instances = new List<BulkCreateInstanceRequestItem>();
		}
		public Guid SchemaId { get; set; }
		public string Namespace { get; set; }
		public List<BulkCreateInstanceRequestItem> Instances { get; set; }
	}
	public class BulkCreateInstanceRequestItem{
		public string Name { get; set; }
		public Dictionary<string,string> Fields { get; set; }
	}
}

