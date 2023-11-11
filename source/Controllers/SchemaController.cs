using Microsoft.AspNetCore.Mvc;
using Models.Stats;
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
        private readonly ISchemaRepository repository;
        private readonly ILogger<SchemaController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repository">The schema repository.</param>
        public SchemaController(ILogger<SchemaController> logger, ISchemaRepository repository)
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
        [Route("{schemaId}")]
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
        /// <param name="request">The search request.</param>
        /// <returns>A list of schemas matching the search criteria.</returns>
        [HttpPost]
        [Route("search")]
        public SchemaListModel SearchSchema(SearchSchemaRequest request)
        {
            request.Page = request.Page <= 0 ? 1 : request.Page;
            var response = repository.SearchSchema(request);
            return response;
        }     

        /// <summary>
        /// Retrieves statistics for the schema.
        /// </summary>
        /// <param name="schemaId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{schemaId}/stats")]
        public SchemaStatisticsModel GetSchemaStatistics(Guid schemaId)
        {
            var model = repository.GetSchemaStatistics(schemaId);
            return model;
        }
           
    }

    
}
