using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Extensions;
using tyf.data.service.Models;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tyf.data.service.Controllers
{
    [Route("api/[controller]")]
    [ValidateAPIAccess(Constants.Roles.UserManager)]
    public class AccessAccountController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessAccountController"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public AccessAccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="request">The create user request.</param>
        /// <returns>The newly created user model.</returns>
        [HttpPost()]
        public UserModel Post([FromBody] CreateUserRequest request)
        {
            var model = userRepository.CreateUser(request);
            return model;
        }

        /// <summary>
        /// Searches for users by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A list of user models that match the search criteria.</returns>
        [HttpGet("search/{name}")]
        public UserModelList Search(string name)
        {
            var model = userRepository.SearchUsers(name);
            return model;
        }

        /// <summary>
        /// Gets a user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to get.</param>
        /// <returns>The user model.</returns>
        [HttpGet("{id}")]
        public UserModel Get(Guid id)
        {
            var model = userRepository.GetUser(id);
            return model;
        }
    }
}

