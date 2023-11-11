using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Models;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;
using tyf.data.service.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tyf.data.service.Controllers
{
    /// <summary>
    /// Controller for managing schema field types.
    /// </summary>
    [Route("api/fieldtypes")]
    [ValidateAPIAccess(Constants.Roles.DataManager)]
    public class SchemaFieldsController : ControllerBase
    {
        private readonly ISchemaRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaFieldsController"/> class.
        /// </summary>
        /// <param name="repository">The schema repository.</param>
        public SchemaFieldsController(ISchemaRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets the list of available field types.
        /// </summary>
        /// <returns>The list of field types.</returns>
        [HttpGet]
        public FieldTypeListModel Get()
        {
            return repository.GetFieldTypes();
        }

        /// <summary>
        /// Gets the field type with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the field type.</param>
        /// <returns>The field type with the specified ID.</returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Creates a new field type.
        /// </summary>
        /// <param name="request">The request containing the details of the new field type.</param>
        /// <returns>The newly created field type.</returns>
        [HttpPost]
        public FieldTypeModel Post([FromBody]CreateFieldTypeRequest request)
        {
            var model =repository.CreateFieldType(request);
            return model;
        }

        /// <summary>
        /// Updates the field type with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the field type to update.</param>
        /// <param name="value">The new value of the field type.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        /// Deletes the field type with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the field type to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

