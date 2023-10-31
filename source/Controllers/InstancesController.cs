using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Extensions;
using tyf.data.service.Models;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tyf.data.service.Controllers
{
    [Route("api/instance")]
    [ValidateAPIAccess(Constants.Roles.DataManager)]
    public class InstancesController : ControllerBase
    {
        private readonly IInstanceRepository instanceRepository;

        public InstancesController(IInstanceRepository instanceRepository)
        {
            this.instanceRepository = instanceRepository;
        }
        // GET: api/values
        [HttpGet("{id}")]
        public SchemaInstanceModel Get(Guid id)
        {
            var instanceModel = instanceRepository.GetInstance(id);
            return instanceModel;
        }

        // GET api/values/5
        [HttpPost("filter")]
        public InstanceListModel Filter([FromBody] FilterInstanceRequest request)
        {
            var listModel = instanceRepository.FilterInstances(request);
            return listModel;
        }

        // POST api/values
        [HttpPost]
        public SchemaInstanceModel Post([FromBody] CreateInstanceRequest request)
        {
            var instanceModel =instanceRepository.CreateInstance(request);
            return instanceModel;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public SchemaInstanceModel Put(Guid id, [FromBody] UpdateInstanceRequest request)
        {
            request.InstanceId = id;
            var responseModel = instanceRepository.UpdateInstance(request);
            return responseModel;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

