using System;
namespace tyf.data.service.Requests
{
    public class CreateUserRequest
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
}

