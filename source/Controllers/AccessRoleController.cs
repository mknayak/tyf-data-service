using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Extensions;
using tyf.data.service.Models;
using tyf.data.service.Models.Account;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tyf.data.service.Controllers
{
    [Route("api/[controller]")]
    [ValidateAPIAccess(Constants.Roles.UserManager)]
    public class AccessRoleController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AccessRoleController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpPost()]
        public StatusResponseModel Post([FromBody] CreateAccessRoleRequest createAccessRoleRequest)
        {
            this.userRepository.CreateRole(createAccessRoleRequest);
            return new StatusResponseModel { Success = true };
        }


        [HttpGet()]
        public AccessRoleModelLst Get()
        {
            var roles = this.userRepository.GetRoles();
            return roles;
        }

        [HttpPost("{roleId}")]
        public StatusResponseModel Get(Guid roleId, [FromBody] Guid[] userIds)
        {
            this.userRepository.AddUsersToRole(roleId,userIds.ToList());
            return new StatusResponseModel { Success = true };
        }

    }
}

