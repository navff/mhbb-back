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

    /// <summary>
    /// Работа с организаторами активностей. 
    /// </summary>
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
                org.Phone = putViewModel.Phone;
                org.Email = putViewModel.Email;

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



        /// <summary>
        /// Ищет организаторов по параметрам
        /// </summary>
        /// <param name="cityId">Город</param>
        /// <param name="word">Поисковое слово</param>
        /// <param name="page">Номер страницы по 100. По умолчанию выдаётся первая страница</param>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<OrganizerViewModelGet>))]
        [Route("search")]
        public async Task<IHttpActionResult> Search(int? cityId=null, String word = "", int page=1)
        {
            try
            {
                var orgs = await _organizerOperations.SearchAsync(cityId, word, page);
                var result = Mapper.Map<List<OrganizerViewModelGet>>(orgs);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT SEARCH ORGANIZERS", ex);
                throw;
            }
        }

    }
}