using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Extensions;
using tyf.data.service.Models;
using tyf.data.service.Models.Account;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tyf.data.service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    namespace YourNamespace.Controllers
    {
        /// <summary>
        /// Controller for managing access groups.
        /// </summary>
        [Route("api/[controller]")]
        [ValidateAPIAccess(Constants.Roles.UserManager)]
        public class AccessGroupController : ControllerBase
        {
            private readonly IUserRepository userRepository;

            /// <summary>
            /// Initializes a new instance of the <see cref="AccessGroupController"/> class.
            /// </summary>
            /// <param name="userRepository">The user repository.</param>
            public AccessGroupController(IUserRepository userRepository)
            {
                this.userRepository = userRepository;
            }

            /// <summary>
            /// Creates a new access group.
            /// </summary>
            /// <param name="request">The request containing the access group details.</param>
            /// <returns>The newly created access group.</returns>
            [HttpPost()]
            [ProducesResponseType(typeof(AccessGroupModel), 200)]
            public AccessGroupModel Post([FromBody] CreateAccessGroupRequest request)
            {
                var model = userRepository.CreateAccessGroup(request);
                return model;
            }

            /// <summary>
            /// Gets an access group by ID.
            /// </summary>
            /// <param name="id">The ID of the access group to get.</param>
            /// <returns>The access group with the specified ID.</returns>
            [HttpGet("{id}")]
            [ProducesResponseType(typeof(AccessGroupModel), 200)]
            public AccessGroupModel Get(Guid id)
            {
                var model = userRepository.GetAccessGroup(id);
                return model;
            }

            /// <summary>
            /// Gets an access group by namespace.
            /// </summary>
            /// <param name="nameSpace">The namespace of the access group to get.</param>
            /// <returns>The access group with the specified namespace.</returns>
            [HttpGet("ns/{nameSpace}")]
            [ProducesResponseType(typeof(AccessGroupModel), 200)]
            public AccessGroupModel Get(string nameSpace)
            {
                var model = userRepository.GetAccessGroup(nameSpace);
                return model;
            }

            /// <summary>
            /// Searches for access groups by name.
            /// </summary>
            /// <param name="name">The name to search for.</param>
            /// <returns>A list of access groups matching the search criteria.</returns>
            [HttpGet("search/{name}")]
            [ProducesResponseType(typeof(AccessGroupModelList), 200)]
            public AccessGroupModelList Search(string name)
            {
                var model = userRepository.SearchAccessgroup(name);
                return model;
            }

            /// <summary>
            /// Adds users to an access group.
            /// </summary>
            /// <param name="groupId">The ID of the access group to add users to.</param>
            /// <param name="userIds">The IDs of the users to add to the access group.</param>
            /// <returns>A status response indicating whether the operation was successful.</returns>
            [HttpPost("{groupId}/users")]
            [ProducesResponseType(typeof(StatusResponseModel), 200)]
            public StatusResponseModel AddUsers(Guid groupId, [FromBody] Guid[] userIds)
            {
                userRepository.AddUsersToGroup(groupId, userIds.ToList());
                return new StatusResponseModel { Success = true };
            }
        }
    }
}

