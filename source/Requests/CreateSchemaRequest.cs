using tyf.data.service.Models;

namespace tyf.data.service.Requests
{
	/// <summary>
	/// Request for creating a new schema.
	/// </summary>
    public class CreateSchemaRequest
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CreateSchemaRequest"/> class.
		/// </summary>
		public CreateSchemaRequest()
		{
			Fields = new Dictionary<string, SchemaFieldModel>();
		}
		/// <summary>
		/// Gets or sets the name of the schema.
		/// </summary>
		public required string Name { get; set; }
		/// <summary>
		/// Gets or sets the namespace of the schema.
		/// </summary>
        public required string Namespace { get; set; }
		/// <summary>
		/// Gets or sets the fields of the schema.
		/// </summary>
        public required IDictionary<string,SchemaFieldModel> Fields { get; set; }
	}
}

