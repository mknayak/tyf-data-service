using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Extensions;
using tyf.data.service.Models;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

namespace tyf.data.service.Controllers
{
    
    /// <summary>
    /// Controller for managing user access accounts.
    /// </summary>
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
        public UserInfoList Search(string name)
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

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="request">The update user request.</param>
        /// <returns>The updated user model.</returns>
        [HttpPut("{id}")]
        public UserModel Put(Guid id, [FromBody] UpdateUserRequest request)
        {
            var model = userRepository.UpdateUser(id, request);
            return model;
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            userRepository.DeleteUser(id);
        }

        /// <summary>
        /// Locks a user.
        /// </summary>
        /// <param name="id">The ID of the user to lock.</param>
        [HttpPut("lock/{id}")]
        public void Lock(Guid id)
        {
            userRepository.LockUser(id);
        }

        /// <summary>
        /// Unlocks a user.
        /// </summary>
        /// <param name="id">The ID of the user to unlock.</param>
        [HttpPut("unlock/{id}")]
        public void Unlock(Guid id)
        {
            userRepository.UnlockUser(id);
        }



    }
}

