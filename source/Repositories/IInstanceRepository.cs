using tyf.data.service.Requests;
using tyf.data.service.Models;

namespace tyf.data.service.Repositories
{
    public interface IInstanceRepository
	{
		public SchemaInstanceModel CreateInstance(CreateInstanceRequest request);

        public InstanceListModel FilterInstances(FilterInstanceRequest request);

        public SchemaInstanceModel UpdateInstance(UpdateInstanceRequest request);
        public SchemaInstanceModel GetInstance(Guid instanceId);

    }
}

