namespace tyf.data.service.Requests
{
    public class UploadCsvRequest
	{
		public string Separator { get; set; } = ",";
		public IFormFile File { get; set; } = null!;
        public Guid SchemaId { get; set; }
		public string TargetNamespace { get; set; } = null!;
        public List<KeyValuePair<string,string>>? Mapping { get; set; }
	}
}

