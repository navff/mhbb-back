using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using API.Common;
using API.Models;
using API.ViewModels;
using AutoMapper;
using Camps.Tools;
using Models.Entities;
using Models.Operations;

namespace API.Controllers
{
    [RoutePrefix("api/interest")]
    public class InterestController : ApiController
    {
        private InterestOperations _interestOperations;

        public InterestController(InterestOperations interestOperations)
        {
            _interestOperations = interestOperations;
        }

        /// <summary>
        /// Получает интерес по ID
        /// </summary>
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var interest = await _interestOperations.GetAsync(id);
                if (interest == null)
                {
                    return this.Result404("Interest is not found");
                }

                var result = Mapper.Map<InterestViewModelGet>(interest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET INTEREST", ex);
                throw;
            }
        }

        /// <summary>
        /// Обновление интереса
        /// </summary>
        [HttpPut]
        [RESTAuthorize(Role.PortalAdmin)]
        public async Task<IHttpActionResult> Put(int id, InterestViewModelPost putViewModel)
        {
            try
            {
                var interest = await _interestOperations.GetAsync(id);
                if (interest == null)
                {
                    return this.Result404("Interest is not found");
                }
                interest.Name = putViewModel.Name;
                await _interestOperations.UpdateAsync(interest);
                return await Get(id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT PUT INTEREST", ex);
                throw;
            }
        }

        /// <summary>
        /// Добавление интереса
        /// </summary>
        [HttpPost]
        [RESTAuthorize(Role.PortalAdmin)]
        public async Task<IHttpActionResult> Post(InterestViewModelPost postViewModel)
        {
            try
            {
                var interest = Mapper.Map<Interest>(postViewModel);
                var result = await _interestOperations.AddAsync(interest);
                return await Get(result.Id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT POST INTEREST", ex);
                throw;
            }
        }

        /// <summary>
        /// Удаление интереса
        /// </summary>
        [HttpDelete]
        [RESTAuthorize(Role.PortalAdmin)]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                await _interestOperations.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT DELETE INTEREST", ex);
                throw;
            }
        }

        /// <summary>
        /// Получение всех возможных интересов
        /// </summary>
        [HttpGet]
        [Route("getall")]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var interests = await _interestOperations.GetAllAsync();
                var result = Mapper.Map<IEnumerable<InterestViewModelGet>>(interests);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GETALL INTERESTS", ex);
                throw;
            }
        }
    }
}