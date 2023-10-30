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

        public AccessAccountController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpPost()]
        public UserModel Post([FromBody] CreateUserRequest request)
        {
            var model = userRepository.CreateUser(request);
            return model;
        }


        [HttpGet("search/{name}")]
        public UserModelList Search(string name)
        {
            var model = userRepository.SearchUsers(name);
            return model;
        }

       
        [HttpGet("{id}")]
        public UserModel Get(Guid id)
        {
            var model = userRepository.GetUser(id);
            return model;
        }
    }
}

