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
    public class AccessGroupController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AccessGroupController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost()]
        public AccessGroupModel Post([FromBody] CreateAccessGroupRequest request)
        {
            var model = userRepository.CreateAccessGroup(request);
            return model;
        }


        [HttpGet("{id}")]
        public AccessGroupModel Get(Guid id)
        {
            var model = userRepository.GetAccessGroup(id);
            return model;
        }
        [HttpGet("ns/{nameSpace}")]
        public AccessGroupModel Get(string nameSpace)
        {
            var model = userRepository.GetAccessGroup(nameSpace);
            return model;
        }
        [HttpGet("search/{name}")]
        public AccessGroupModelList Search(string name)
        {
            var model = userRepository.SearchAccessgroup(name);
            return model;
        }

        // DELETE api/values/5
        [HttpPost("{groupId}/users")]
        public StatusResponseModel AddUsers(Guid groupId, [FromBody] Guid[] userIds)
        {
            userRepository.AddUsersToGroup(groupId, userIds.ToList());
            return new StatusResponseModel { Success = true };
        }
    }
}

