using System;
namespace tyf.data.service.Requests
{
	public class InstallApplicationModel
	{
		public string ApplicationName { get; set; }
		public CreateUserRequest RootUser { get; set; }
	}
}

