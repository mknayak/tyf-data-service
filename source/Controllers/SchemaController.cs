using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Extensions;
using tyf.data.service.Models;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tyf.data.service.Controller
{
    /// <summary>
    /// Controller for managing schemas.
    /// </summary>
    [ApiController]
    [ValidateAPIAccess(Constants.Roles.DataManager)]
    [Route("api/schema")]
    public class SchemaController : ControllerBase
    {
        private readonly IDataRepository repository;
        private readonly ILogger<SchemaController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The data repository.</param>
        public SchemaController(ILogger<SchemaController> logger, IDataRepository repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        /// <summary>
        /// Creates a new schema.
        /// </summary>
        /// <param name="request">The request containing the schema details.</param>
        /// <returns>The created schema.</returns>
        [HttpPost]
        public SchemaModel CreateSchema(CreateSchemaRequest request)
        {
            var model = repository.CreateSchema(request);
            return model;
        }

        /// <summary>
        /// Gets a schema by its ID.
        /// </summary>
        /// <param name="schemaId">The ID of the schema to get.</param>
        /// <returns>The schema with the specified ID.</returns>
        [HttpGet]
        [Route("id/{schemaId}")]
        public SchemaModel GetSchemaById(Guid schemaId)
        {
            var model = repository.GetSchemaById(schemaId);
            return model;
        }

        /// <summary>
        /// Gets a schema by its full name.
        /// </summary>
        /// <param name="schemaFullName">The full name of the schema to get.</param>
        /// <returns>The schema with the specified full name.</returns>
        [HttpGet]
        [Route("name/{schemaFullName}")]
        public SchemaModel GetSchemaByName(string schemaFullName)
        {
            var model = repository.GetSchemaByName(schemaFullName);
            return model;
        }

        /// <summary>
        /// Searches for schemas by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A list of schemas matching the search criteria.</returns>
        [HttpGet]
        [Route("search/{name}")]
        public SchemaListModel SearchSchema(string name)
        {
            var response = repository.SearchSchema(name);
            return response;
        }

        /// <summary>
        /// Gets a list of schemas under the specified namespace.
        /// </summary>
        /// <param name="nameSpace">The namespace to filter by.</param>
        /// <returns>A list of schemas under the specified namespace.</returns>
        [HttpGet]
        [Route("list/{namespace}")]
        public SchemaListModel GetSchemasUnderNamespace(string nameSpace)
        {
            var response = repository.FilterSchema(nameSpace);
            return response;
        }

        /// <summary>
        /// Searches for schemas by field.
        /// </summary>
        /// <param name="fieldName">The name of the field to search by.</param>
        /// <param name="fieldValue">The value of the field to search for.</param>
        /// <returns>A list of schemas matching the search criteria.</returns>
        [HttpGet]
        [Route("field/{fieldName}/{fieldValue}")]
        public SchemaListModel SearchSchemaByField(string fieldName, string fieldValue)
        {
            var response = repository.SearchSchemaByField(fieldName, fieldValue);
            return response;
        }
        
    }
}
