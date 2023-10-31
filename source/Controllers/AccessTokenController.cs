using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Managers;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tyf.data.service.Controllers
{
    [Route("api/[controller]")]
    public class AccessTokenController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ISecurityManager securityManager;

        public AccessTokenController(IUserRepository userRepository, ISecurityManager securityManager)
        {
            this.userRepository = userRepository;
            this.securityManager = securityManager;
        }
   
        [HttpPost("auth")]
        public Models.AuthToken Post([FromBody] AuthenticateUserRequest request)
        {
            var user = userRepository.ValidateUser(request);
            return securityManager.CreateToken(user);
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

