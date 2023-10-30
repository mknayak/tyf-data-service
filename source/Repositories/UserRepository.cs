using System;
using tyf.data.service.DbModels;
using tyf.data.service.Requests;
using tyf.data.service.Models;
using tyf.data.service.Managers;
using System.Security.Principal;
using tyf.data.service.Exeptions;
using tyf.data.service.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using tyf.data.service.Models.Account;
using System.Text.RegularExpressions;

namespace tyf.data.service.Repositories
{
	public class UserRepository:IUserRepository
	{
        private readonly TyfDataContext dbContext;
        private readonly ISecurityManager securityManager;
        private readonly ErrorMessages errorMessages;

        public UserRepository(TyfDataContext dbContext,ISecurityManager securityManager, IOptions<ErrorMessages> messageOptions)
		{
            this.dbContext = dbContext;
            this.securityManager = securityManager;
            this.errorMessages = messageOptions.Value;
        }

        public void AddRolesToUser(Guid userId, List<Guid> roleIds)
        {
            foreach (var roleId in roleIds)
            {
                dbContext.AccessAccountRoles.Add(new AccessAccountRole
                {
                    AccessAccountRoleId=Guid.NewGuid(),
                    AccessAccountId=userId,
                    AccessRoleId=roleId,
                    CreatedBy = "System",
                    UpdatedBy = "System",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                });
            }
            dbContext.SaveChanges();
        }

        public void AddUsersToGroup(Guid accessGroupId, List<Guid> userIds)
        {
            var group = dbContext.AccessGroups.FirstOrDefault(c => c.AccessGroupId == accessGroupId);
            if(null!=group)
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
                        AccessGroupAccountId=Guid.NewGuid()
                    });

                }
                dbContext.SaveChanges();
            }
        }

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

        public UserModel CreateUser(CreateUserRequest userRequest)
        {
            var newUserId = Guid.NewGuid();
            var salt = newUserId.ToString("N");
            AccessAccount account = new AccessAccount
            {
                AccessAccountId = newUserId,
                AccessAccountEmail = userRequest.Email,
                AccessAccountName = userRequest.Name,
                AccessAccountIsLocked=false,
                AccessAccountSalt= salt,
                AccessAccountPasswordhash= securityManager.ComputeHash(salt,userRequest.Password),
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

        public AccessRoleModelLst GetRoles()
        {
            var count = dbContext.AccessRoles.Count();
            var dbModels = dbContext.AccessRoles.OrderBy(c => c.CreatedDate).Take(20).ToList();
            var model = new AccessRoleModelLst();
            model.Results = dbModels.Select(x => new AccessRoleModel { Name = x.AccessRoleName, RoleId = x.AccessRoleId });
            model.TotalResults = count;
            return model;
        }

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

        public void GetUsersInGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        public void RemoveRolesFromUser(Guid userId, List<Guid> roleIds)
        {
            throw new NotImplementedException();
        }

        public void RemoveUsersFromGroup(Guid accessGroupId, List<Guid> userIds)
        {
            throw new NotImplementedException();
        }

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

        public UserModelList SearchUsers(string name)
        {
            var users = dbContext.AccessAccounts.
                Include(c => c.AccessAccountRoles).ThenInclude(x => x.AccessRole).
                Include(c => c.AccessGroupAccounts).ThenInclude(x => x.AccessGroup).
                Where(c => c.AccessAccountName.Contains(name)).OrderBy(x => x.CreatedDate).Take(10).ToList();
            var userModels = users.Select(x => new UserModel
            {
                UserId = x.AccessAccountId,
                Name = x.AccessAccountName,
                Email = x.AccessAccountEmail,
                Namespaces = x.AccessGroupAccounts.Select(c => c.AccessGroup.AccessGroupNamespace),
                Roles = x.AccessAccountRoles.Select(c => c.AccessRole.AccessRoleName)
            });
            return new UserModelList(userModels);
        }

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

