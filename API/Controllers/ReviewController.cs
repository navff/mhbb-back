using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class ReviewController : ApiController
    {
        public async Task<IHttpActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IHttpActionResult> Put(int id, object putViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<IHttpActionResult> Post(object postViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IHttpActionResult> SetChecked(int id, bool isChecked = true)
        {
            throw new NotImplementedException();
        }
    }
}