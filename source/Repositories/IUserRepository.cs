using tyf.data.service.Requests;
using tyf.data.service.Models;
using tyf.data.service.Models.Account;

namespace tyf.data.service.Repositories
{
    /// <summary>
    /// Interface for managing user data in the system.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Creates a new user in the system.
        /// </summary>
        /// <param name="userRequest">The user information to create.</param>
        /// <returns>The newly created user.</returns>
        public UserModel CreateUser(CreateUserRequest userRequest);

        /// <summary>
        /// Validates a user's credentials and returns a user token if successful.
        /// </summary>
        /// <param name="authenticateUserRequest">The user's authentication information.</param>
        /// <returns>The authenticated user.</returns>
        public UserModel ValidateUser(AuthenticateUserRequest authenticateUserRequest);

        /// <summary>
        /// Searches for users in the system by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A list of users matching the search criteria.</returns>
        public UserInfoList SearchUsers(string name);

        /// <summary>
        /// Gets a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The user's unique identifier.</param>
        /// <returns>The user with the specified identifier.</returns>
        public UserModel GetUser(Guid userId);

        /// <summary>
        /// Creates a new access role in the system.
        /// </summary>
        /// <param name="createAccessRoleRequest">The access role information to create.</param>
        /// <returns>The newly created access role.</returns>
        public AccessRoleModel CreateRole(CreateAccessRoleRequest createAccessRoleRequest);

        /// <summary>
        /// Gets a list of all access roles in the system.
        /// </summary>
        /// <returns>A list of all access roles in the system.</returns>
        public AccessRoleModelLst GetRoles();

        /// <summary>
        /// Creates a new access group in the system.
        /// </summary>
        /// <param name="createAccessGroupRequest">The access group information to create.</param>
        /// <returns>The newly created access group.</returns>
        public AccessGroupModel CreateAccessGroup(CreateAccessGroupRequest createAccessGroupRequest);

        /// <summary>
        /// Gets an access group by its name.
        /// </summary>
        /// <param name="groupName">The name of the access group to get.</param>
        /// <returns>The access group with the specified name.</returns>
        public AccessGroupModel GetAccessGroup(string groupName);

        /// <summary>
        /// Gets an access group by its unique identifier.
        /// </summary>
        /// <param name="groupId">The unique identifier of the access group to get.</param>
        /// <returns>The access group with the specified identifier.</returns>
        public AccessGroupModel GetAccessGroup(Guid groupId);

        /// <summary>
        /// Adds users to an access group.
        /// </summary>
        /// <param name="accessGroupId">The unique identifier of the access group to add users to.</param>
        /// <param name="userIds">The unique identifiers of the users to add to the access group.</param>
        public void AddUsersToGroup(Guid accessGroupId, List<Guid> userIds);

        /// <summary>
        /// Removes users from an access group.
        /// </summary>
        /// <param name="accessGroupId">The unique identifier of the access group to remove users from.</param>
        /// <param name="userIds">The unique identifiers of the users to remove from the access group.</param>
        public void RemoveUsersFromGroup(Guid accessGroupId, List<Guid> userIds);

        /// <summary>
        /// Adds access roles to a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to add access roles to.</param>
        /// <param name="roleIds">The unique identifiers of the access roles to add to the user.</param>
        public void AddRolesToUser(Guid userId, List<Guid> roleIds);

        /// <summary>
        /// Removes access roles from a user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to remove access roles from.</param>
        /// <param name="roleIds">The unique identifiers of the access roles to remove from the user.</param>
        public void RemoveRolesFromUser(Guid userId, List<Guid> roleIds);

        /// <summary>
        /// Adds users to an access role.
        /// </summary>
        /// <param name="roleId">The unique identifier of the access role to add users to.</param>
        /// <param name="userIds">The unique identifiers of the users to add to the access role.</param>
        public void AddUsersToRole(Guid roleId, List<Guid> userIds);
        /// <summary>
        /// Removes users from an access role.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        public void RemoveUsersFromRole(Guid roleId, List<Guid> userIds);
        /// <summary>
        /// Gets a list of all users in the access role.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public UserInfoList GetUsersInRole(Guid roleId);

        /// <summary>
        /// Gets a list of all users in the access group.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public UserInfoList GetUsersInGroup(Guid groupId);

        /// <summary>
        /// Searches for access groups in the system by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A list of access groups matching the search criteria.</returns>
        public AccessGroupModelList SearchAccessgroup(string name);

        /// <summary>
        /// Updates a user's information.
        /// </summary>
        /// <param name="id">The unique identifier of the user to update.</param>
        /// <param name="request">The updated user information.</param>
        /// <returns>The updated user.</returns>
        public UserModel UpdateUser(Guid id, UpdateUserRequest request);

        /// <summary>
        /// Deletes a user from the system.
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete.</param>
        public void DeleteUser(Guid id);

        /// <summary>
        /// Locks a user's account.
        /// </summary>
        /// <param name="id">The unique identifier of the user to lock.</param>
        public void LockUser(Guid id);

        /// <summary>
        /// Unlocks a user's account.
        /// </summary>
        /// <param name="id">The unique identifier of the user to unlock.</param>
        public void UnlockUser(Guid id);
    }
}