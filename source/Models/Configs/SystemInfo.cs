namespace tyf.data.service.Models.Configs
{
    public class BasicSystemInfo
    {
        public bool Installed { get; set; }
        public string? ApplicationName { get; set; }
        public string? Namespace { get; set; }
        public DateTime InstalledOn { get; set; }

    }
    public class SystemInfo: BasicSystemInfo
    {
		public required UserInfo AdminUser { get; set; }

	}
	
}

