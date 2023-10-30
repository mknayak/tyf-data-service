using System;
using tyf.data.service.Requests;
using tyf.data.service.Models;
using tyf.data.service.Models.Account;

namespace tyf.data.service.Repositories
{
	public interface IUserRepository
	{
		public UserModel CreateUser(CreateUserRequest userRequest);
		public UserModel ValidateUser(AuthenticateUserRequest authenticateUserRequest);//return usertoken
		public UserModelList SearchUsers(string name);
		public UserModel GetUser(Guid userId);

        public AccessRoleModel CreateRole(CreateAccessRoleRequest createAccessRoleRequest);
		public AccessRoleModelLst GetRoles();

		public AccessGroupModel CreateAccessGroup(CreateAccessGroupRequest createAccessGroupRequest);
        public AccessGroupModel GetAccessGroup(string groupName);
        public AccessGroupModel GetAccessGroup(Guid groupId);

        public void AddUsersToGroup(Guid accessGroupId,List<Guid> userIds);
		public void RemoveUsersFromGroup(Guid accessGroupId,List<Guid> userIds);

        public void AddRolesToUser(Guid userId, List<Guid> roleIds);
        public void RemoveRolesFromUser(Guid userId, List<Guid> roleIds);

        public void AddUsersToRole(Guid roleId, List<Guid> userIds);



        public void GetUsersInGroup(string groupName);
        AccessGroupModelList SearchAccessgroup(string name);
    }
}