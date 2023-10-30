using tyf.data.service.Models.Data;

namespace tyf.data.service.Repositories
{
    public interface ISettingItemsRepository
    {
        public bool KeyExists(string nameSpace, string key);
        public SettingModel GetValue(string nameSpace, string key);
        public SettingModel[] GetAllConfigurations(string nameSpace);
    }
}

