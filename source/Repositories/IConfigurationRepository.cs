using tyf.data.service.Models;

namespace tyf.data.service.Repositories
{
    public interface IConfigurationRepository
	{
		public bool KeyExists(string key);
		public T? GetConfiguration<T>(string key);
		public Dictionary<string,object> GetAllConfigurations(ConfigType configType);
		public KeyValuePair<string,T> AddConfiguration<T>(ConfigType configType,string key,T value);
    }
}

