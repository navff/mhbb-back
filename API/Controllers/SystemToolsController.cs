using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Models.Operations;

namespace API.Controllers
{
    [RoutePrefix("api/systemtools")]
    public class SystemToolsController : ApiController
    {
        private SystemToolsOperations _systemToolsOperations;

        public SystemToolsController(SystemToolsOperations systemToolsOperations)
        {
            _systemToolsOperations = systemToolsOperations;
        }

        [HttpGet]
        [Route("reformatpictures")]
        public async Task<IHttpActionResult> ReformatAllPictures()
        {
            try
            {
                await _systemToolsOperations.ReformatAllPictures();
                return Ok("done!");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("RemoveTempFiles")]
        public async Task<IHttpActionResult> RemoveAllTempFiles()
        {
            await _systemToolsOperations.RemoveAllTempFiles();
            return Ok("done!");
        }
    }
}