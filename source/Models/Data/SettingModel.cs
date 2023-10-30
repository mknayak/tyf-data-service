using System;
namespace tyf.data.service.Models.Data
{
	public class SettingModel
	{
        public string SettingItemKey { get; set; } = null!;

        public string SettingItemNamespace { get; set; } = null!;

        public decimal SettingItemVersion { get; set; }

        public string? SettingItemValue { get; set; }
    }
}

