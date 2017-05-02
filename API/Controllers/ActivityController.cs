using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using API.ViewModels;
using Camps.Tools;

namespace API.Controllers
{
    [RoutePrefix("api/activity")]
    public class ActivityController : ApiController
    {

        /// <summary>
        /// Ищет активность по ID
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        [ResponseType(typeof(ActivityViewModelGet))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET ACTIVITY", ex);
                throw;
            }
        }

        /// <summary>
        /// Ищет активности по параметрам
        /// </summary>
        /// <param name="word">Поисковое слово</param>
        /// <param name="age">Возраст</param>
        /// <param name="interestId">Интерес</param>
        /// <param name="cityId">Город</param>
        /// <param name="sobriety">Трезвые преподаватели</param>
        /// <param name="free">Бесплатно</param>
        /// <param name="page">Страница пагинации</param>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        [ResponseType(typeof(IEnumerable<ActivityViewModelGet>))]
        public async Task<IHttpActionResult> Search(String word = null,
                                                             int? age = null,
                                                             int? interestId = null,
                                                             int? cityId = null,
                                                             bool? sobriety = null,
                                                             bool? free = null,
                                                             int? page = null)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT SEARCH ACTIVITY", ex);
                throw;
            }
        }

        /// <summary>
        /// Обновление активности
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ResponseType(typeof(ActivityViewModelGet))]
        public async Task<IHttpActionResult> Put(int id, ActivityViewModelPost putViewModel)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT PUT ACTIVITY", ex);
                throw;
            }
        }

        /// <summary>
        /// Добавление активности
        /// </summary>
        /// <param name="postViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ActivityViewModelGet))]
        public async Task<IHttpActionResult> Post(ActivityViewModelPost postViewModel)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT POST ACTIVITY", ex);
                throw;
            }
        }

        /// <summary>
        /// Удаление активности
        /// </summary>
        [Route("{id}")]
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT DELETE ACTIVITY", ex);
                throw;
            }
        }
    }
}