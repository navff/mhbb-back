using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API.ViewModels;
using AutoMapper;
using Camps.Tools;
using Models.Entities;
using Models.Operations;

namespace API.Controllers
{
    /// <summary>
    /// Управление городами
    /// </summary>
    
    [RoutePrefix("api/city")]
    public class CityController : ApiController
    {
        private CityOperations _cityOperations;

        public CityController(CityOperations cityOperations)
        {
            _cityOperations = cityOperations;
        }

        /// <summary>
        /// Получает город по его Id
        /// </summary>
        /// <param name="id">Id города</param>
        [ResponseType(typeof(CityViewModelGet))]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var city = await _cityOperations.GetAsync(id);
                var result =  Mapper.Map<CityViewModelGet>(city);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.ThrowAndLog("CANNOT GET CITY", ex);
                return null;
            }
        }

        /// <summary>
        /// Изменяет название города
        /// </summary>
        [ResponseType(typeof(CityViewModelGet))]
        public async Task<IHttpActionResult> Put(int id, CityViewModelPost putViewModel)
        {
            try
            {
                var city = new City
                {
                    Name = putViewModel.Name,
                    Id = id
                };
                await _cityOperations.AddAsync(city);
                return await Get(id);

            }
            catch (Exception ex)
            {
                ErrorLogger.ThrowAndLog("CANNOT PUT CITY", ex);
                return null;
            }
        }

        /// <summary>
        /// Добавление города
        /// </summary>
        [ResponseType(typeof(CityViewModelGet))]
        public async  Task<IHttpActionResult> Post(CityViewModelPost postViewModel)
        {

            var city = new City()
            {
                Name = postViewModel.Name
            };

            try
            {
                var addedCity = await _cityOperations.AddAsync(city);
                return await Get(addedCity.Id);
            }
            catch (Exception ex)
            {
                ErrorLogger.ThrowAndLog("CANNOT POST CITY", ex);
                return null;
            }
        }

        /// <summary>
        /// Удаление города
        /// </summary>
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                await _cityOperations.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                ErrorLogger.ThrowAndLog("CANNOT DELETE CITY", ex);
                return null;
            }
        }

        /// <summary>
        /// Поиск городов по названию. 
        /// </summary>
        /// <param name="word">Часть названия города</param>
        public async Task<IHttpActionResult> Search(string word)
        {
            try
            {
                var entities = await _cityOperations.SearchAsync(word);
                var result = Mapper.Map<CityViewModelGet>(entities);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.ThrowAndLog("CANNOT SEARCH CITIES", ex);
                return null;
            }

        }
    }
}