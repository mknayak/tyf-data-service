using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Extensions;
using tyf.data.service.Models;
using tyf.data.service.Models.Account;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

namespace tyf.data.service.Controllers
{
    /// <summary>
    /// Controller for managing access roles.
    /// </summary>
    [Route("api/[controller]")]
    [ValidateAPIAccess(Constants.Roles.UserManager)]
    public class AccessRoleController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessRoleController"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public AccessRoleController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Creates a new access role.
        /// </summary>
        /// <param name="createAccessRoleRequest">The request to create an access role.</param>
        /// <returns>A <see cref="StatusResponseModel"/> indicating whether the operation was successful.</returns>
        [HttpPost()]
        public StatusResponseModel Post([FromBody] CreateAccessRoleRequest createAccessRoleRequest)
        {
            this.userRepository.CreateRole(createAccessRoleRequest);
            return new StatusResponseModel { Success = true };
        }

        /// <summary>
        /// Gets a list of all access roles.
        /// </summary>
        /// <returns>A <see cref="AccessRoleModelLst"/> containing all access roles.</returns>
        [HttpGet()]
        public AccessRoleModelLst Get()
        {
            var roles = this.userRepository.GetRoles();
            return roles;
        }

        /// <summary>
        /// Adds users to an access role.
        /// </summary>
        /// <param name="roleId">The ID of the access role to add users to.</param>
        /// <param name="userIds">The IDs of the users to add to the access role.</param>
        /// <returns>A <see cref="StatusResponseModel"/> indicating whether the operation was successful.</returns>
        [HttpPost("{roleId}")]
        public StatusResponseModel AddUsersInRole(Guid roleId, [FromBody] Guid[] userIds)
        {
            this.userRepository.AddUsersToRole(roleId,userIds.ToList());
            return new StatusResponseModel { Success = true };
        }

        /// <summary>
        /// Removes users from an access role.
        /// </summary>
        /// <param name="roleId">The ID of the access role to remove users from.</param>
        /// <param name="userIds">The IDs of the users to remove from the access role.</param>
        /// <returns>A <see cref="StatusResponseModel"/> indicating whether the operation was successful.</returns>
        [HttpDelete("{roleId}")]
        public StatusResponseModel RemoveUsersInRole(Guid roleId, [FromBody] Guid[] userIds)
        {
            this.userRepository.RemoveUsersFromRole(roleId, userIds.ToList());
            return new StatusResponseModel { Success = true };
        }

        /// <summary>
        /// Gets a list of users in an access role.
        /// </summary>
        /// <param name="roleId">The ID of the access role to get users for.</param>
        /// <returns>A <see cref="UserInfoList"/> containing the users in the access role.</returns>
        [HttpGet("{roleId}")]
        public UserInfoList GetUsersInRole(Guid roleId)
        {
            var users = this.userRepository.GetUsersInRole(roleId);
            return users;
        }
    }
}