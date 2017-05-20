using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using API.Common;
using API.Models;
using API.ViewModels;
using Camps.Tools;

namespace API.Controllers
{
    [RoutePrefix("api/review")]
    public class ReviewController : ApiController
    {

        [HttpGet]
        [ResponseType(typeof(ReviewViewModelGet))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET REVIEW", ex);
                throw;
            }
        }

        [HttpGet]
        [Route("byuser")]
        [ResponseType(typeof(IEnumerable<ReviewViewModelGet>))]
        public async Task<IHttpActionResult> GetByUser(string email)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET REVIEW", ex);
                throw;
            }
        }

        [HttpGet]
        [Route("byactivity")]
        [ResponseType(typeof(IEnumerable<ReviewViewModelGet>))]
        public async Task<IHttpActionResult> GetByActivity(int activityId)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET REVIEWS", ex);
                throw;
            }
        }

        [HttpPut]
        [RESTAuthorize]
        [ResponseType(typeof(ReviewViewModelGet))]
        public async Task<IHttpActionResult> Put(int id, object putViewModel)
        {
            //TODO: проверить, есть ли права на отзыв
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT PUT REVIEW", ex);
                throw;
            }
        }

        [HttpPost]
        [ResponseType(typeof(ReviewViewModelGet))]
        [RESTAuthorize]
        public async Task<IHttpActionResult> Post(object postViewModel)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT POST REVIEW", ex);
                throw;
            }
        }

        [HttpDelete]
        [RESTAuthorize]
        public async Task<IHttpActionResult> Delete(int id)
        {
            //TODO: проверить, есть ли права на отзыв
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT DELETE REVIEW", ex);
                throw;
            }
        }

        [HttpPut]
        [ResponseType(typeof(ReviewViewModelGet))]
        [RESTAuthorize(Role.PortalAdmin, Role.PortalManager)]
        [Route("setchecked")]
        public async Task<IHttpActionResult> SetChecked(int reviewId, bool isChecked)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT SET CHECKED ON REVIEW", ex);
                throw;
            }
        }
    }
}