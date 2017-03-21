using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using API.Common;
using API.Operations;

namespace API.Common
{
    public class RESTAuthorizeAttribute : AuthorizationFilterAttribute, IDisposable
    {
        private string[] _roles;
        private UserOperations _userOperations = new UserOperations();


        public RESTAuthorizeAttribute(params  string[] roles)
        {
            this._roles = roles;
        }
        

        /// <summary>
        /// Вызывается каждый раз при поступлении запроса
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.Authorization?.Parameter;
            var user = _userOperations.GetUserByToken(token);

            if (user==null) 
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("User with this token is not found")
                };
                return;
            }

            if (!this._roles.Contains(user.Role.ToString()))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("Your role is too small")
                };
                return;
            }

            var identity = new Identity {Name = user.Email, IsAuthenticated = true};
            actionContext.RequestContext.Principal = new GenericPrincipal(identity, new[] { user.Role.ToString()});
        }

        public void Dispose()
        {
            _userOperations.Dispose();
        }
    }
}