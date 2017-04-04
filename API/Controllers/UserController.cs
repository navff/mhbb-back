using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API.Common;
using API.Models;
using API.Operations;
using API.ViewModels;

namespace API.Controllers
{
    /// <summary>
    /// Управление пользователями
    /// </summary>
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private UserOperations _userOperations;

        public UserController(UserOperations userOperations)
        {
            _userOperations = userOperations;
        }

        /// <summary>
        /// Получает пользователя по его email
        /// </summary>
        /// <param name="email"></param>
        [HttpGet]
        [ResponseType(typeof(UserViewModelGet))]
        public async Task<IHttpActionResult> Get(string email)
        {
            var result = await _userOperations.GetAsync(email);
            return Ok(result);
        }

        /// <summary>
        /// Обновляет данные пользователя
        /// </summary>
        /// <param name="email"></param>
        /// <param name="putViewModel"></param>
        [HttpPut]
        [ResponseType(typeof(UserViewModelGet))]
        public IHttpActionResult Put([FromUri] string email, [FromBody] object putViewModel)
        {
            //TODO: проверить, сам себя редактирует, или вредничает
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавляет нового пользователя
        /// </summary>
        /// <param name="postViewModel"></param>
        [HttpPost]
        [ResponseType(typeof(UserViewModelGet))]
        public IHttpActionResult Post(object postViewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        /// <param name="email"></param>
        [HttpDelete]
        [RESTAuthorize]
        public IHttpActionResult Delete(string email)
        {
            //TODO: проверить, сам себя удаляет или вредничает
            throw new NotImplementedException();
        }

        /// <summary>
        /// Регистрирует пользователя
        /// </summary>
        /// <param name="viewModel"></param>
        [HttpPost]
        public IHttpActionResult Register(UserRegisterViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получает данные пользователя по его токену в заголовке
        /// </summary>
        /// <returns></returns>
        [RESTAuthorize()]
        [ResponseType(typeof(UserViewModelGet))]
        public IHttpActionResult GetUser()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ищет пользователей по ключу
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        [HttpGet]
        [RESTAuthorize(Role.PortalAdmin, Role.PortalManager)]
        public IHttpActionResult Search(string word)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [RESTAuthorize]
        public IHttpActionResult Logout()
        {
            throw new NotImplementedException();
        }
    }
}
