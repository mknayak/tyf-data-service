using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Extensions;
using tyf.data.service.Models;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

namespace tyf.data.service.Controllers
{
    /// <summary>
    /// Controller for managing instances of schemas.
    /// </summary>
    [Route("api/instance")]
    [ValidateAPIAccess(Constants.Roles.DataManager)]
    public class InstancesController : ControllerBase
    {
        private readonly IInstanceRepository instanceRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstancesController"/> class.
        /// </summary>
        /// <param name="instanceRepository">The instance repository.</param>
        public InstancesController(IInstanceRepository instanceRepository)
        {
            this.instanceRepository = instanceRepository;
        }

        /// <summary>
        /// Gets the schema instance with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the schema instance to retrieve.</param>
        /// <returns>The schema instance with the specified ID.</returns>
        [HttpGet("{id}")]
        public SchemaInstanceModel Get(Guid id)
        {
            var instanceModel = instanceRepository.GetInstance(id);
            return instanceModel;
        }

        /// <summary>
        /// Filters the schema instances based on the specified filter criteria.
        /// </summary>
        /// <param name="request">The filter criteria.</param>
        /// <returns>The filtered list of schema instances.</returns>
        [HttpPost("filter")]
        public InstanceListModel Filter([FromBody] FilterInstanceRequest request)
        {
            var listModel = instanceRepository.FilterInstances(request);
            return listModel;
        }

        /// <summary>
        /// Creates a new schema instance.
        /// </summary>
        /// <param name="request">The request containing the details of the new schema instance.</param>
        /// <returns>The newly created schema instance.</returns>
        [HttpPost]
        public SchemaInstanceModel Post([FromBody] CreateInstanceRequest request)
        {
            var instanceModel = instanceRepository.CreateInstance(request);
            return instanceModel;
        }

        /// <summary>
        /// Updates the schema instance with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the schema instance to update.</param>
        /// <param name="request">The request containing the updated details of the schema instance.</param>
        /// <returns>The updated schema instance.</returns>
        [HttpPut("{id}")]
        public SchemaInstanceModel Put(Guid id, [FromBody] UpdateInstanceRequest request)
        {
            request.InstanceId = id;
            var responseModel = instanceRepository.UpdateInstance(request);
            return responseModel;
        }

        /// <summary>
        /// Deletes the schema instance with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the schema instance to delete.</param>
        [HttpDelete("{id}")]
        public StatusResponseModel Delete(Guid id)
        {
            instanceRepository.DeleteInstance(id);
            return new StatusResponseModel { Success = true };
        }
    }
}

