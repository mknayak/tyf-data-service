namespace tyf.data.service.Requests
{
    public class AuthenticateUserRequest
    {
        public required string UserEmail { get; set; }
        public required string Password { get; set; }
    }
}

