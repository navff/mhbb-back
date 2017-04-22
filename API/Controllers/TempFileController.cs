using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class TempFileController: ApiController
    {
        public Task<IHttpActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Put(int id, object putViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Post(object postViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> GetBySessionId(int sessionId)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> GetByFormId(int id)
        {
            throw new NotImplementedException();
        }


    }
}