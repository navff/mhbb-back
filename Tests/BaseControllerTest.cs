using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Routing;
using API.Common;

namespace Tests
{
   public abstract class BaseControllerTest : BaseTest
    {
        protected void PrepareController(ApiController controller, string userName = null, string role=null)
        {
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost");
            var route = config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;

            if (role != null)
            {
                Identity identity = new Identity();
                identity.Name = userName;
                identity.IsAuthenticated = true;

                controller.ControllerContext.RequestContext.Principal = 
                    new GenericPrincipal(identity, new string[] { role } );
            }

            controller.Url = new UrlHelper(new HttpRequestMessage());
            controller.Url.Request = request;

            
            
            
        }
    }
}
