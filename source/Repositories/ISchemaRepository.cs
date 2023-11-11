using tyf.data.service.Requests;
using tyf.data.service.Models;
using Models.Stats;

namespace tyf.data.service.Repositories
{
/// <summary>
/// Represents a repository for managing schema and field type data.
/// </summary>
public interface ISchemaRepository
{
	/// <summary>
	/// Creates a new schema with the specified details.
	/// </summary>
	/// <param name="request">The details of the schema to create.</param>
	/// <returns>The newly created schema.</returns>
	public SchemaModel CreateSchema(CreateSchemaRequest request);

	/// <summary>
	/// Retrieves the schema with the specified ID.
	/// </summary>
	/// <param name="schemaId">The ID of the schema to retrieve.</param>
	/// <returns>The schema with the specified ID.</returns>
	public SchemaModel GetSchemaById(Guid schemaId);

	/// <summary>
	/// Retrieves the schema with the specified full name.
	/// </summary>
	/// <param name="schemaFullName">The full name of the schema to retrieve.</param>
	/// <returns>The schema with the specified full name.</returns>
	public SchemaModel GetSchemaByName(string schemaFullName);

	/// <summary>
	/// Searches for schemas that match the specified criteria.
	/// </summary>
	/// <param name="request">The criteria to use when searching for schemas.</param>
	/// <returns>A list of schemas that match the specified criteria.</returns>
	public SchemaListModel SearchSchema(SearchSchemaRequest request);

	/// <summary>
	/// Creates a new field type with the specified details.
	/// </summary>
	/// <param name="request">The details of the field type to create.</param>
	/// <returns>The newly created field type.</returns>
	public FieldTypeModel CreateFieldType(CreateFieldTypeRequest request);

	/// <summary>
	/// Retrieves a list of all available field types.
	/// </summary>
	/// <returns>A list of all available field types.</returns>
	public FieldTypeListModel GetFieldTypes();
	
	/// <summary>
	/// Retrieves statistics for the schema.
	/// </summary>
	/// <param name="schemaId"></param>
	/// <returns></returns>
	public SchemaStatisticsModel GetSchemaStatistics(Guid schemaId);
    }
}

