using System.Collections.Generic;
using System.Threading;
using System.Web.Http;
using API.Common;

namespace API.Controllers
{
    [RESTAuthorize("PortalAdmin", "PortalManager")]
    public class TestController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            
            if (this.User.IsInRole("PortalAdmin"))
                return Ok(this.User.Identity.Name);
            else return this.Unauthorized();
        }

        [HttpPost]
        public IHttpActionResult Post()
        {
            if (this.User.IsInRole("PortalAdmin"))
                return Ok(this.User.Identity.Name);
            else return this.Unauthorized();
        }

    }
}
