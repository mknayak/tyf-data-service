using System.Text.Json;
using tyf.data.service.DbModels;
using tyf.data.service.Models;

namespace tyf.data.service.Repositories
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly TyfDataContext dataContext;

        public ConfigurationRepository(TyfDataContext dataContext)
        {
            this.dataContext = dataContext;
            
        }

        public KeyValuePair<string, T> AddConfiguration<T>(ConfigType configType, string key, T value)
        {
            dataContext.Configurations.Add(new Configuration
            {
                ConfigurationKey = key,
                ConfigurationType = (short)configType,
                ConfigurationValue = JsonSerializer.Serialize(value),
                ConfigurationVersion = 1,
                CreatedBy = "System",
                UpdatedBy = "System",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now

            });
            dataContext.SaveChanges();
            return new KeyValuePair<string, T>(key,value);
        }

        public Dictionary<string, object> GetAllConfigurations(ConfigType configType)
        {
            var configItems = dataContext.Configurations.Skip(0);
            if(configType!= ConfigType.All)
            {
                configItems = configItems.Where(c => c.ConfigurationType == (short)configType);
            }
            var result = configItems.Select(x => new { x.ConfigurationKey, x.ConfigurationValue }).ToList();
            var configDict = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var item in result)
            {
                var dictValue = JsonSerializer.Deserialize<object>(item.ConfigurationValue ?? string.Empty) ?? new { };
                if (configDict.ContainsKey(item.ConfigurationKey))
                    configDict[item.ConfigurationKey] = dictValue;
                else
                    configDict.Add(item.ConfigurationKey, dictValue);
            }
            return configDict;
        }

        public T? GetConfiguration<T>(string key)
        {
            var keyExist = KeyExists(key);
            if (keyExist)
            {
                var value = dataContext.Configurations.FirstOrDefault(x => x.ConfigurationKey == key)?.ConfigurationValue??string.Empty;
                return JsonSerializer.Deserialize<T>(value);
            }
            return default(T);
        }

        public bool KeyExists(string key)
        {
            return dataContext.Configurations.Any(x => x.ConfigurationKey == key);
        }
    }
}

