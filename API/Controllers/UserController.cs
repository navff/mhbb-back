using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using API.Common;
using API.Models;
using API.Operations;
using API.ViewModels;
using AutoMapper;
using Models;

namespace API.Controllers
{
    /// <summary>
    /// Управление пользователями
    /// </summary>
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly UserOperations _userOperations;

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
            try
            {
                var entity = await _userOperations.GetAsync(email);
                var result = Mapper.Map<UserViewModelGet>(entity);
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        /// <summary>
        /// Обновляет данные пользователя
        /// </summary>
        /// <param name="email"></param>
        /// <param name="putViewModel"></param>
        [HttpPut]
        [ResponseType(typeof(UserViewModelGet))]
        [RESTAuthorize]
        public async Task<IHttpActionResult> Put([FromUri] string email, [FromBody] UserViewModelPut putViewModel)
        {
            if ((!User.IsInRole("PortalAdmin"))  
                  && (User.Identity.Name!=email))
            {
                return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("You cannot edit another users")
                });

            }
            var userEntity = Mapper.Map<Models.User>(putViewModel);
            await _userOperations.UpdateAsync(userEntity);
            return await Get(email);
        }


        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        /// <param name="email"></param>
        [HttpDelete]
        [RESTAuthorize]
        [Route("delete")]
        public async Task<IHttpActionResult> Delete(string email)
        {
            if (!User.IsInRole("PortalAdmin") 
                && !User.IsInRole("PortalManager")
                && User.Identity.Name!=email)
            {
                return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("You cannot delete another users")
                });
            }
            await _userOperations.DeleteAsync(email);
            return Ok("Deleted "+email);
        }

        /// <summary>
        /// Регистрирует пользователя
        /// </summary>
        /// <param name="viewModel"></param>
        [HttpPost]
        public async Task<IHttpActionResult> Register(UserRegisterViewModel viewModel)
        {
            var existingUser = await _userOperations.GetAsync(viewModel.Email);

            if (existingUser == null)
            {
                return Ok(await _userOperations.RegisterAsync(viewModel.Email));
            }
            else
            {
                return Ok(existingUser);
            }
        }

        /// <summary>
        /// Получает данные пользователя по его токену в заголовке
        /// </summary>
        /// <returns></returns>
        [RESTAuthorize]
        [ResponseType(typeof(UserViewModelGet))]
        [HttpGet]
        public IHttpActionResult GetUser()
        {
            return Ok(User.Identity.Name);
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
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            return Ok("Logout");
        }

    }
}
