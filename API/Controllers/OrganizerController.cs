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
using AutoMapper;
using Camps.Tools;
using Models.Entities;
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
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var org = await _organizerOperations.GetAsync(id);
                if (org == null)
                {
                    return this.Result404("Organizer is not found");
                }

                var result = Mapper.Map<OrganizerViewModelGet>(org);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET ORGANIZER", ex);
                throw;
            }

            
        }

        /// <summary>
        /// Изменение организатора
        /// </summary>
        [HttpPut]
        [ResponseType(typeof(OrganizerViewModelGet))]
        [RESTAuthorize(Role.PortalAdmin, Role.PortalManager)]
        public async Task<IHttpActionResult> Put(int id, OrganizerViewModelPost putViewModel)
        {
            try
            {
                var org = await _organizerOperations.GetAsync(id);
                if (org == null)
                {
                    return this.Result404("Organizer is not found");
                }

                org.CityId = putViewModel.CityId;
                org.Name = putViewModel.Name;
                org.Sobriety = putViewModel.Sobriety;

                await _organizerOperations.UpdateAsync(org);
                return await Get(id);

            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT PUT ORGANIZER", ex);
                throw;
            }
        }

        /// <summary>
        /// Добавление организатора
        /// </summary>
        /// <param name="postViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(OrganizerViewModelGet))]
        public async Task<IHttpActionResult> Post(OrganizerViewModelPost postViewModel)
        {
            try
            {
                var org = Mapper.Map<Organizer>(postViewModel);
                var result = await _organizerOperations.AddAsync(org);
                return await Get(result.Id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT POST ORGANIZER", ex);
                throw;
            }
        }


        /// <summary>
        /// Удаление организатора
        /// </summary>
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                var org = await _organizerOperations.GetAsync(id);
                if (org == null)
                {
                    return this.Result404("Organizer is not found");
                }
                await _organizerOperations.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT DELETE ORGANIZER", ex);
                throw;
            }
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<OrganizerViewModelGet>))]
        [Route("search")]
        public async Task<IHttpActionResult> Search(string word)
        {
            try
            {
                var orgs = await _organizerOperations.SearchAsync(word);
                var result = Mapper.Map<List<OrganizerViewModelGet>>(orgs);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT SEARCH ORGANIZER", ex);
                throw;
            }

        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<OrganizerViewModelGet>))]
        [Route("getall")]
        public async Task<IHttpActionResult> GetAll(int page)
        {
            try
            {
                var orgs = await _organizerOperations.GetAllAsync(page);
                var result = Mapper.Map<List<OrganizerViewModelGet>>(orgs);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GETALL ORGANIZERS", ex);
                throw;
            }
        }
    }
}