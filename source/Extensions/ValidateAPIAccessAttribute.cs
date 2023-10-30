using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using tyf.data.service.Managers;

namespace tyf.data.service.Extensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ValidateAPIAccessAttribute:Attribute,IAuthorizationFilter
	{
        private readonly AccessLevel accessLevel;
        private readonly string[] roles;

        public ValidateAPIAccessAttribute(params string[] roles)
		{
            this.accessLevel = AccessLevel.Basic;
            this.roles = roles;
        }
        public ValidateAPIAccessAttribute(AccessLevel accessLevel, params string[] roles)
        {
            this.accessLevel = accessLevel;
            this.roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (accessLevel == AccessLevel.Open)
                return;
            var accountIdHeader = context.HttpContext.Request.Headers["x-tyf-groupid"];

            var accountToken = context.HttpContext.Request.Headers["x-tyf-accessToken"];
            var securityManager = context.HttpContext.RequestServices.GetService<ISecurityManager>();

            var token = securityManager.ValidateToken(new Models.AuthToken { Token = accountToken });
            if (null==token)
            {
                context.Result = new ContentResult { StatusCode = StatusCodes.Status401Unauthorized };
                return;
            }

            if (token.Roles.Any(c => roles.Contains(c)))
                return;

            
        }
    }

    public enum AccessLevel
    {
        Open,
        Basic,
        Restricted
    }
}

