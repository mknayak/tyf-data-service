using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Models;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;
using tyf.data.service.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tyf.data.service.Controllers
{
    [Route("api/fieldtypes")]
    [ValidateAPIAccess(Constants.Roles.DataManager)]
    public class SchemaFieldsController : ControllerBase
    {
        private readonly IDataRepository repository;

        public SchemaFieldsController(IDataRepository repository)
        {
            this.repository = repository;
        }
        // GET: api/values
        [HttpGet]
        public FieldTypeListModel Get()
        {
            return repository.GetFieldTypes();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public FieldTypeModel Post([FromBody]CreateFieldTypeRequest request)
        {
            var model =repository.CreateFieldType(request);
            return model;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

