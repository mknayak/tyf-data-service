using tyf.data.service.DbModels;
using tyf.data.service.Requests;
using tyf.data.service.Models;
using tyf.data.service.Managers;
using tyf.data.service.Exeptions;
using tyf.data.service.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using tyf.data.service.Models.Account;

namespace tyf.data.service.Repositories
{
    /// <summary>
    /// Repository class for managing user-related data in the database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly TyfDataContext dbContext;
        private readonly ISecurityManager securityManager;
        private readonly ErrorMessages errorMessages;
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="securityManager"></param>
        /// <param name="messageOptions"></param>
        public UserRepository(TyfDataContext dbContext, ISecurityManager securityManager, IOptions<ErrorMessages> messageOptions)
        {
            this.dbContext = dbContext;
            this.securityManager = securityManager;
            this.errorMessages = messageOptions.Value;
        }

        /// <summary>
        /// Adds the specified roles to the user with the given ID.
        /// </summary>
        /// <param name="userId">The ID of the user to add roles to.</param>
        /// <param name="roleIds">The IDs of the roles to add to the user.</param>
        public void AddRolesToUser(Guid userId, List<Guid> roleIds)
        {
            foreach (var roleId in roleIds)
            {
                dbContext.AccessAccountRoles.Add(new AccessAccountRole
                {
                    AccessAccountRoleId = Guid.NewGuid(),
                    AccessAccountId = userId,
                    AccessRoleId = roleId,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                });
            }
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Adds the specified users to the group with the given ID.
        /// </summary>
        /// <param name="accessGroupId">The ID of the group to add users to.</param>
        /// <param name="userIds">The IDs of the users to add to the group.</param>
        public void AddUsersToGroup(Guid accessGroupId, List<Guid> userIds)
        {
            var group = dbContext.AccessGroups.FirstOrDefault(c => c.AccessGroupId == accessGroupId);
            if (null != group)
            {
                foreach (var uid in userIds)
                {
                    dbContext.AccessGroupAccounts.Add(new AccessGroupAccount
                    {
                        AccessAccountId = uid,
                        AccessGroup = group,
                        CreatedBy = "System",
                        UpdatedBy = "System",
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        AccessGroupAccountId = Guid.NewGuid()
                    });

                }
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Adds the specified users to the role with the given ID.
        /// </summary>
        /// <param name="roleId">The ID of the role to add users to.</param>
        /// <param name="userIds">The IDs of the users to add to the role.</param>
        public void AddUsersToRole(Guid roleId, List<Guid> userIds)
        {
            foreach (var userId in userIds)
            {
                dbContext.AccessAccountRoles.Add(new AccessAccountRole
                {
                    AccessAccountRoleId = Guid.NewGuid(),
                    AccessAccountId = userId,
                    AccessRoleId = roleId,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                });
            }
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Creates a new access group with the specified name and namespace.
        /// </summary>
        /// <param name="createAccessGroupRequest">The request object containing the name and namespace of the new group.</param>
        /// <returns>A model object representing the newly created access group.</returns>
        public AccessGroupModel CreateAccessGroup(CreateAccessGroupRequest createAccessGroupRequest)
        {
            AccessGroup entity = new AccessGroup
            {
                AccessGroupId = Guid.NewGuid(),
                AccessGroupName = createAccessGroupRequest.Name,
                AccessGroupNamespace = createAccessGroupRequest.Namespace,
                CreatedBy = "System",
                UpdatedBy = "System",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            dbContext.AccessGroups.Add(entity);
            dbContext.SaveChanges();
            return new AccessGroupModel
            {
                GroupId = entity.AccessGroupId,
                Name = entity.AccessGroupName,
                Namespace = entity.AccessGroupNamespace
            };
        }

        /// <summary>
        /// Creates a new access role with the specified name.
        /// </summary>
        /// <param name="createAccessRoleRequest">The request object containing the name of the new role.</param>
        /// <returns>A model object representing the newly created access role.</returns>
        public AccessRoleModel CreateRole(CreateAccessRoleRequest createAccessRoleRequest)
        {
            AccessRole entity = new AccessRole
            {
                AccessRoleName = createAccessRoleRequest.Name,
                AccessRoleId = Guid.NewGuid(),
                CreatedBy = "System",
                UpdatedBy = "System",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            dbContext.AccessRoles.Add(entity);
            dbContext.SaveChanges();
            return new AccessRoleModel { Name = entity.AccessRoleName, RoleId = entity.AccessRoleId };
        }

        /// <summary>
        /// Creates a new user with the specified name, email, and password.
        /// </summary>
        /// <param name="userRequest">The request object containing the name, email, and password of the new user.</param>
        /// <returns>A model object representing the newly created user.</returns>
        public UserModel CreateUser(CreateUserRequest userRequest)
        {
            var newUserId = Guid.NewGuid();
            var salt = newUserId.ToString("N");
            AccessAccount account = new AccessAccount
            {
                AccessAccountId = newUserId,
                AccessAccountEmail = userRequest.Email,
                AccessAccountName = userRequest.Name,
                AccessAccountIsLocked = false,
                AccessAccountSalt = salt,
                AccessAccountPasswordhash = securityManager.ComputeHash(salt, userRequest.Password),
                CreatedBy = "System",
                UpdatedBy = "System",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            dbContext.AccessAccounts.Add(account);
            dbContext.SaveChanges();

            return new UserModel
            {
                UserId = account.AccessAccountId,
                Name = account.AccessAccountName,
                Email = account.AccessAccountEmail,
            };
        }

        /// <summary>
        /// Deletes the user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        public void DeleteUser(Guid id)
        {
            var account = dbContext.AccessAccounts.FirstOrDefault(c => c.AccessAccountId == id);
            if (null != account)
            {
                dbContext.AccessAccounts.Remove(account);
                dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// Get Access Group by name
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        /// <exception cref="TechnicalException"></exception>
        public AccessGroupModel GetAccessGroup(string groupName)
        {
            var entity = dbContext.AccessGroups.FirstOrDefault(c => c.AccessGroupName == groupName);
            if (null == entity)
                throw new TechnicalException(errorMessages.Format("CER-102", $"Group ({groupName})"));
            return new AccessGroupModel
            {
                GroupId = entity.AccessGroupId,
                Name = entity.AccessGroupName,
                Namespace = entity.AccessGroupNamespace
            };
        }
        /// <summary>
        /// Get Access Group by Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        /// <exception cref="TechnicalException"></exception>
        public AccessGroupModel GetAccessGroup(Guid groupId)
        {
            var entity = dbContext.AccessGroups.FirstOrDefault(c => c.AccessGroupId == groupId);
            if (null == entity)
                throw new TechnicalException(errorMessages.Format("CER-102", $"Group ({groupId})"));
            return new AccessGroupModel
            {
                GroupId = entity.AccessGroupId,
                Name = entity.AccessGroupName,
                Namespace = entity.AccessGroupNamespace
            };
        }
        /// <summary>
        /// Get Access Groups
        /// </summary>
        /// <returns></returns>
        public AccessRoleModelLst GetRoles()
        {
            var count = dbContext.AccessRoles.Count();
            var dbModels = dbContext.AccessRoles.OrderBy(c => c.CreatedDate).Take(20).ToList();
            var model = new AccessRoleModelLst();
            model.Results = dbModels.Select(x => new AccessRoleModel { Name = x.AccessRoleName, RoleId = x.AccessRoleId });
            model.TotalResults = count;
            return model;
        }
        /// <summary>
        /// Get Access Role by name
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="TechnicalException"></exception>
        public UserModel GetUser(Guid userId)
        {
            var account = dbContext.AccessAccounts.
                Include(c => c.AccessAccountRoles).ThenInclude(x => x.AccessRole).
                Include(c => c.AccessGroupAccounts).ThenInclude(x => x.AccessGroup).FirstOrDefault(c => c.AccessAccountId == userId);
            if (null == account)
                throw new TechnicalException(errorMessages.Format("CER-102", $"User ({userId})"));
            return new UserModel
            {
                UserId = account.AccessAccountId,
                Name = account.AccessAccountName,
                Email = account.AccessAccountEmail,
                Namespaces = account.AccessGroupAccounts.Select(c => c.AccessGroup.AccessGroupNamespace),
                Roles = account.AccessAccountRoles.Select(c => c.AccessRole.AccessRoleName)
            };
        }
        /// <summary>
        /// Get Users in an Access Group
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public UserInfoList GetUsersInGroup(Guid groupId)
        {
            var usersInGroup = dbContext.AccessGroupAccounts.Where(c => c.AccessGroupId == groupId);
            var users = usersInGroup.Select(x => new UserInfo
            {
                UserId = x.AccessAccountId,
                Name = x.AccessAccount.AccessAccountName,
                Email = x.AccessAccount.AccessAccountEmail
            });
            return new UserInfoList(users);
        }
        /// <summary>
        /// Get Users in a Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public UserInfoList GetUsersInRole(Guid roleId)
        {
            var usersInRole = dbContext.AccessAccountRoles.Where(c => c.AccessRoleId == roleId);
            var users = usersInRole.Select(x => new UserInfo
            {
                UserId = x.AccessAccountId,
                Name = x.AccessAccount.AccessAccountName,
                Email = x.AccessAccount.AccessAccountEmail
            });
            return new UserInfoList(users);
        }

        /// <summary>
        /// Lock User
        /// </summary>
        /// <param name="id"></param>
        public void LockUser(Guid id)
        {
            var userAccount = dbContext.AccessAccounts.FirstOrDefault(c => c.AccessAccountId == id);
            if (null != userAccount)
            {
                userAccount.AccessAccountIsLocked = true;
                dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// Remove Roles from User
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RemoveRolesFromUser(Guid userId, List<Guid> roleIds)
        {
            var rolesInUser = dbContext.AccessAccountRoles.Where(c => c.AccessAccountId == userId && roleIds.Contains(c.AccessRoleId));
            dbContext.AccessAccountRoles.RemoveRange(rolesInUser);
            dbContext.SaveChanges();
        }
        /// <summary>
        /// Remove Users from Role
        /// </summary>
        /// <param name="accessGroupId"></param>
        /// <param name="userIds"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void RemoveUsersFromGroup(Guid accessGroupId, List<Guid> userIds)
        {
           var usersInAccessGroup= dbContext.AccessGroupAccounts.Where(c => c.AccessGroupId == accessGroupId && userIds.Contains(c.AccessAccountId));
            dbContext.AccessGroupAccounts.RemoveRange(usersInAccessGroup);
            dbContext.SaveChanges();
        }
        /// <summary>
        /// Remove Users from Role
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        public void RemoveUsersFromRole(Guid roleId, List<Guid> userIds)
        {
            var usersInRole = dbContext.AccessAccountRoles.Where(c => c.AccessRoleId == roleId && userIds.Contains(c.AccessAccountId));
            dbContext.AccessAccountRoles.RemoveRange(usersInRole);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Remove Users from Role
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AccessGroupModelList SearchAccessgroup(string name)
        {
            var entities = dbContext.AccessGroups.Where(c => c.AccessGroupName.Contains(name) || c.AccessGroupNamespace.Contains(name)).OrderBy(c=>c.CreatedDate).Take(20);
             
            return new AccessGroupModelList
            {
                Results = entities.Select(entity => new AccessGroupModel
                {
                    GroupId = entity.AccessGroupId,
                    Name = entity.AccessGroupName,
                    Namespace = entity.AccessGroupNamespace
                })
            };
        }
        /// <summary>
        /// Search Users
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public UserInfoList SearchUsers(string name)
        {
            var users = dbContext.AccessAccounts.
                Where(c => c.AccessAccountName.Contains(name)).OrderBy(x => x.CreatedDate).Take(10).ToList();
            var userModels = users.Select(x => new UserInfo
            {
                UserId = x.AccessAccountId,
                Name = x.AccessAccountName,
                Email = x.AccessAccountEmail
            });
            return new UserInfoList(userModels);
        }
        /// <summary>
        /// Unlock User
        /// </summary>
        /// <param name="id"></param>
        public void UnlockUser(Guid id)
        {
            var userAccount= dbContext.AccessAccounts.FirstOrDefault(c => c.AccessAccountId == id);
            if(null!=userAccount)
            {
                userAccount.AccessAccountIsLocked = false;
                dbContext.SaveChanges();
            }
        }
        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="TechnicalException"></exception>
        public UserModel UpdateUser(Guid id, UpdateUserRequest request)
        {
            if(null==request)
                throw new TechnicalException(errorMessages.Format("CER-105", "updateRequest"));
            var userAccount = dbContext.AccessAccounts.FirstOrDefault(c => c.AccessAccountId == id);
            if (null != userAccount)
            {
                userAccount.AccessAccountName = request.Name;
                userAccount.AccessAccountEmail = request.Email;
                userAccount.AccessAccountPasswordhash = securityManager.ComputeHash(userAccount.AccessAccountSalt, request.Password);
                dbContext.SaveChanges();
                return new UserModel
                {
                    UserId = userAccount.AccessAccountId,
                    Name = userAccount.AccessAccountName,
                    Email = userAccount.AccessAccountEmail,
                    Namespaces = userAccount.AccessGroupAccounts.Select(c => c.AccessGroup.AccessGroupNamespace),
                    Roles = userAccount.AccessAccountRoles.Select(c => c.AccessRole.AccessRoleName)
                };
            }
            throw new TechnicalException(errorMessages.Format("CER-102", "UserAccount"));
        }
        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="authenticateUserRequest"></param>
        /// <returns></returns>
        /// <exception cref="TechnicalException"></exception>
        public UserModel ValidateUser(AuthenticateUserRequest authenticateUserRequest)
        {
            var userAccount = dbContext.AccessAccounts.
                Include(c=>c.AccessAccountRoles).ThenInclude(x=>x.AccessRole).
                Include(c=>c.AccessGroupAccounts).ThenInclude(x=>x.AccessGroup).
                FirstOrDefault(c => c.AccessAccountEmail == authenticateUserRequest.UserEmail);
            if (null != userAccount)
            {
                var salt = userAccount.AccessAccountSalt;
                var passwordHash = userAccount.AccessAccountPasswordhash;
                var userPassword = authenticateUserRequest.Password; 
                if (securityManager.Compare(salt,userPassword,passwordHash))
                {
                    var user= new UserModel
                    {
                        UserId = userAccount.AccessAccountId,
                        Name = userAccount.AccessAccountName,
                        Email = userAccount.AccessAccountEmail,
                        Namespaces= userAccount.AccessGroupAccounts.Select(c=>c.AccessGroup.AccessGroupNamespace),
                        Roles=userAccount.AccessAccountRoles.Select(c=>c.AccessRole.AccessRoleName)
                    };

                    dbContext.AccessAccountRoles.Where(c => c.AccessAccountId == userAccount.AccessAccountId);

                    return user;
                }
            }
            throw new TechnicalException(errorMessages.Format("CER-102", "UserAccount"));
        }
    }
}

