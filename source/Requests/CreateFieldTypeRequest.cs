namespace tyf.data.service.Requests
{
    public class CreateFieldTypeRequest
	{
		public required string Name { get; set; }
		public string? DefaultValue { get; set; }
	}
}

