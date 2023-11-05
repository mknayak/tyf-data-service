using Microsoft.AspNetCore.Mvc;
using tyf.data.service.Exeptions;
using tyf.data.service.Extensions;
using tyf.data.service.Models;
using tyf.data.service.Models.Configs;
using tyf.data.service.Repositories;
using tyf.data.service.Requests;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace tyf.data.service.Controllers
{
    /// <summary>
    /// Controller for managing system and user-defined configurations.
    /// </summary>
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationRepository configurationRepository;
        private readonly IUserRepository userRepository;
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationController"/> class.
        /// </summary>
        /// <param name="configurationRepository"></param>
        /// <param name="userRepository"></param>
        public ConfigurationController(IConfigurationRepository configurationRepository,IUserRepository userRepository)
        {
            this.configurationRepository = configurationRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Gets the basic system information.
        /// </summary>
        /// <returns>The basic system information.</returns>
        [HttpGet("system")]
        public BasicSystemInfo System()
        {
            return configurationRepository.GetConfiguration<BasicSystemInfo>(Constants.ConfigKeys.SystemInformation)??new BasicSystemInfo { Installed=false };
        }

        /// <summary>
        /// Installs the application with the specified configuration.
        /// </summary>
        /// <param name="request">The installation request.</param>
        /// <returns>The key-value pair of the configuration.</returns>
        [HttpPost("install")]
        public KeyValuePair<string,SystemInfo> Install([FromBody]InstallApplicationModel request)
        {
            var key = Constants.ConfigKeys.SystemInformation;
            var systemBasicInfo = configurationRepository.GetConfiguration<BasicSystemInfo>(key);
            if (systemBasicInfo!=null && systemBasicInfo.Installed)
                throw new TechnicalException("SYS101","Application already installed.");
            var systemInfo = new SystemInfo
            {
                AdminUser = new UserInfo
                {
                    Name = request.RootUser.Name,
                    Email = request.RootUser.Email
                },
                ApplicationName= request.ApplicationName,
                Installed=true,
                InstalledOn=DateTime.Now,
                Namespace=$"tyf.{Helpers.ValidNamespace( request.ApplicationName)}"
            };
            var adminUser = userRepository.CreateUser(request.RootUser);
            var adminRole = userRepository.CreateRole(new CreateAccessRoleRequest { Name = Constants.Roles.Administrator });
            var dataRole = userRepository.CreateRole(new CreateAccessRoleRequest { Name = Constants.Roles.DataManager });
            var userRole = userRepository.CreateRole(new CreateAccessRoleRequest { Name = Constants.Roles.UserManager });
            userRepository.AddRolesToUser(adminUser.UserId, new List<Guid> {adminRole.RoleId, dataRole.RoleId,userRole.RoleId });

            var response = configurationRepository.AddConfiguration(Models.ConfigType.System, key, systemInfo);
            return response;
        }

        /// <summary>
        /// Gets the configuration value for the specified key.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <returns>The configuration value.</returns>
        [HttpGet("{key}")]
        [ValidateAPIAccess(Constants.Roles.Administrator)]
        public object Get(string key)
        {
            var value= configurationRepository.GetConfiguration<object>(key);
            if(null== value)
                throw new TechnicalException("SYS201", "Configuration Not Found.Key:"+key);
            return value;
        }

        /// <summary>
        /// Gets all configurations of the specified type.
        /// </summary>
        /// <param name="type">The configuration type.</param>
        /// <returns>The dictionary of configurations.</returns>
        [HttpGet("all/{type}")]
        [ValidateAPIAccess(Constants.Roles.Administrator)]
        public Dictionary<string,object> Get(ConfigType type)
        {
            var response = configurationRepository.GetAllConfigurations(type);
            return response;
        }

        /// <summary>
        /// Adds or updates the configuration value for the specified key.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <param name="value">The configuration value.</param>
        /// <returns>The updated configuration value.</returns>
        [HttpPost("{key}")]
        [ValidateAPIAccess(Constants.Roles.Administrator)]
        public object Get(string key, [FromBody] string value)
        {
            var response = configurationRepository.AddConfiguration(ConfigType.UserDefined, key, value);
            return response;
        }
    }
}

