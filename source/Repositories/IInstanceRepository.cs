using tyf.data.service.Requests;
using tyf.data.service.Models;

namespace tyf.data.service.Repositories
{
    /// <summary>
    /// Interface for managing instances of schema models.
    /// </summary>
    public interface IInstanceRepository
    {
        /// <summary>
        /// Creates a new instance of a schema model.
        /// </summary>
        /// <param name="request">The request containing the data for the new instance.</param>
        /// <returns>The newly created instance.</returns>
        public SchemaInstanceModel CreateInstance(CreateInstanceRequest request);

        /// <summary>
        /// Filters instances of a schema model based on the provided criteria.
        /// </summary>
        /// <param name="request">The request containing the filter criteria.</param>
        /// <returns>A list of instances that match the filter criteria.</returns>
        public InstanceListModel FilterInstances(FilterInstanceRequest request);

        /// <summary>
        /// Updates an existing instance of a schema model.
        /// </summary>
        /// <param name="request">The request containing the updated data for the instance.</param>
        /// <returns>The updated instance.</returns>
        public SchemaInstanceModel UpdateInstance(UpdateInstanceRequest request);

        /// <summary>
        /// Retrieves an instance of a schema model by its ID.
        /// </summary>
        /// <param name="instanceId">The ID of the instance to retrieve.</param>
        /// <returns>The instance with the specified ID.</returns>
        public SchemaInstanceModel GetInstance(Guid instanceId);

        /// <summary>
        /// Creates multiple instances of a schema model in bulk.
        /// </summary>
        /// <param name="request">The request containing the data for the new instances.</param>
        /// <returns>True if all instances were created successfully, false otherwise.</returns>
        public bool BulkCreateInstances(BulkCreateInstanceRequest request);

        /// <summary>
        /// Deletes an instance of a schema model by its ID.
        /// </summary>
        /// <param name="id">The ID of the instance to delete.</param>
        public void DeleteInstance(Guid id);
    }
}

