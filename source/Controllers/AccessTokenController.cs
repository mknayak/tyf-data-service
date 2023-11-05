using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Managers;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

namespace tyf.data.service.Controllers
{
    /// <summary>
    /// Controller for managing access tokens and authentication.
    /// </summary>
    [Route("api/[controller]")]
    public class AccessTokenController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ISecurityManager securityManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessTokenController"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="securityManager">The security manager.</param>
        public AccessTokenController(IUserRepository userRepository, ISecurityManager securityManager)
        {
            this.userRepository = userRepository;
            this.securityManager = securityManager;
        }

        /// <summary>
        /// Authenticates a user and returns an access token.
        /// </summary>
        /// <param name="request">The authentication request.</param>
        /// <returns>The authentication token.</returns>
        [HttpPost("auth")]
        public Models.AuthToken Post([FromBody] AuthenticateUserRequest request)
        {
            var user = userRepository.ValidateUser(request);
            return securityManager.CreateToken(user);
        }
    }
}

