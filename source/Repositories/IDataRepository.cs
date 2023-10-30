using tyf.data.service.Requests;
using tyf.data.service.Models;

namespace tyf.data.service.Repositories
{
    public interface IDataRepository
	{
		public SchemaModel CreateSchema(CreateSchemaRequest request);
		public SchemaModel GetSchemaById(Guid schemId);
		public SchemaModel GetSchemaByName(string schemaFullName);
		public SchemaListModel	SearchSchema(string name);
        public SchemaListModel FilterSchema(string nameSpace);
        public SchemaListModel SearchSchemaByField(string fieldName, string fieldValue);

		public SchemaInstanceModel CreateNewInstance(CreateSchemaInstanceRequest schemaInstanceRequest);

		public FieldTypeModel CreateFieldType(CreateFieldTypeRequest request);
		public FieldTypeListModel GetFieldTypes();
    }
}

