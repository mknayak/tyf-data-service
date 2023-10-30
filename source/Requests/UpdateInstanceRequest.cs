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
}

