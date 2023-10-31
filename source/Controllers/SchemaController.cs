using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Extensions;
using tyf.data.service.Models;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tyf.data.service.Controller
{
    [ApiController]
    [ValidateAPIAccess(Constants.Roles.DataManager)]
    [Route("api/schema")]
    public class SchemaController : ControllerBase
    {
        private readonly IDataRepository repository;

        
        private readonly ILogger<SchemaController> _logger;

        public SchemaController(ILogger<SchemaController> logger,IDataRepository repository)
        {
            _logger = logger;
            this.repository = repository;
        }
        [HttpPost]
        public SchemaModel CreateSchema(CreateSchemaRequest request) {

            var model = repository.CreateSchema(request);
            return model;
        }
        [HttpGet]
        [Route("id/{schemaId}")]
        public SchemaModel GetSchemaById(Guid schemaId) {

            var model = repository.GetSchemaById(schemaId);
            return model;
        }
        [HttpGet]
        [Route("name/{schemaFullName}")]
        public SchemaModel GetSchemaByName(string schemaFullName) {
            var model = repository.GetSchemaByName(schemaFullName);
            return model;
        }
        [HttpGet]
        [Route("search/{name}")]
        public SchemaListModel SearchSchema(string name) {
            var response = repository.SearchSchema(name);
            return response;
        }
        [HttpGet]
        [Route("list/{namespace}")]
        public SchemaListModel GetSchemasUnderNamespace(string nameSpace)
        {
            var response = repository.FilterSchema(nameSpace);
            return response;
        }
        [HttpGet]
        [Route("field/{fieldName}/{fieldValue}")]
        public SchemaListModel SearchSchemaByField(string fieldName, string fieldValue) {
            var response = repository.SearchSchemaByField(fieldName,fieldValue);
            return response;
        }
    }
}
