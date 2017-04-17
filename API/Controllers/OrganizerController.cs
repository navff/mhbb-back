using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;
using API.Common;
using API.Models;
using API.ViewModels;
using Models.Operations;

namespace API.Controllers
{
    [RoutePrefix("api/organizer")]
    public class OrganizerController : ApiController
    {
        private OrganizerOperations _organizerOperations;

        public OrganizerController(OrganizerOperations organizerOperations)
        {
            _organizerOperations = organizerOperations;
        }

        /// <summary>
        /// Получат организатора
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(OrganizerViewModelGet))]
        public Task<IHttpActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Изменение организатора
        /// </summary>
        [HttpPut]
        [ResponseType(typeof(OrganizerViewModelGet))]
        [RESTAuthorize(Role.PortalAdmin, Role.PortalManager)]
        public Task<IHttpActionResult> Put(int id, object putViewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавление организатора
        /// </summary>
        /// <param name="postViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(OrganizerViewModelGet))]
        public Task<IHttpActionResult> Post(object postViewModel)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Удаление организатора
        /// </summary>
        [HttpDelete]
        public Task<IHttpActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<OrganizerViewModelGet>))]
        [Route("search")]
        public async Task<IHttpActionResult> Search(string word)
        {
            throw new NotImplementedException();

        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<OrganizerViewModelGet>))]
        [Route("getall")]
        public async Task<IHttpActionResult> GetAll(int page)
        {
            throw new NotImplementedException();
        }
    }
}