using System;
using tyf.data.service.Models;

namespace tyf.data.service.Requests
{
    public class CreateSchemaRequest
	{
		public CreateSchemaRequest()
		{
			Fields = new Dictionary<string, SchemaFieldModel>();
		}
		public required string Name { get; set; }
        public required string Namespace { get; set; }
        public required IDictionary<string,SchemaFieldModel> Fields { get; set; }
	}
}

