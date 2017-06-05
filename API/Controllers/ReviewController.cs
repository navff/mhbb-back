using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
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
    /// Работа с отзывами
    /// </summary>
    [RoutePrefix("api/review")]
    public class ReviewController : ApiController
    {
        private ReviewOperations _reviewOperations;

        public ReviewController(ReviewOperations reviewOperations)
        {
            _reviewOperations = reviewOperations;
        }

        /// <summary>
        /// Получение отзыва
        /// </summary>
        /// <param name="id">ID отзыва</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ReviewViewModelGet))]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var review = await _reviewOperations.GetAsync(id);
                if (review == null) return this.Result404("This review is not found");
                var result = Mapper.Map<ReviewViewModelGet>(review);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET REVIEW", ex);
                throw;
            }
        }

        /// <summary>
        /// Получение списка отзывов, которые опубликовал пользователь
        /// </summary>
        /// <param name="email">Почта пользователя</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byuser")]
        [ResponseType(typeof(IEnumerable<ReviewViewModelGet>))]
        public async Task<IHttpActionResult> GetByUser(string email)
        {
            try
            {
                var reviews = await _reviewOperations.GetByUserEmailAsync(email);
                var result = Mapper.Map<IEnumerable<ReviewViewModelGet>>(reviews);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET REVIEWS", ex);
                throw;
            }
        }

        /// <summary>
        /// Получить отзывы по активности. 
        /// </summary>
        /// <param name="activityId">ID активности</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byactivity")]
        [ResponseType(typeof(IEnumerable<ReviewViewModelGet>))]
        public async Task<IHttpActionResult> GetByActivity(int activityId)
        {
            try
            {
                var reviews = await _reviewOperations.GetByActivityAsync(activityId);
                var result = Mapper.Map<IEnumerable<ReviewViewModelGet>>(reviews);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET REVIEWS", ex);
                throw;
            }
        }

        /// <summary>
        /// Изменение отзыва
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [RESTAuthorize]
        [ResponseType(typeof(ReviewViewModelGet))]
        public async Task<IHttpActionResult> Put(int id, ReviewViewModelPost viewModel)
        {
            try
            {
                var review = await _reviewOperations.GetAsync(id);
                if (review == null) return this.Result404("This review is not found");
                var checkRightsResult = CheckPermission(review);
                if (checkRightsResult != null) return checkRightsResult;

                review = Mapper.Map<Review>(viewModel);
                review.Id = id;
                await _reviewOperations.UpdateAsync(review);
                return await Get(id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT PUT REVIEW", ex);
                throw;
            }
        }

        /// <summary>
        /// Добавление отзыва
        /// </summary>
        [HttpPost]
        [ResponseType(typeof(ReviewViewModelGet))]
        [RESTAuthorize]
        public async Task<IHttpActionResult> Post(ReviewViewModelPost postViewModel)
        {
            try
            {
                var review = Mapper.Map<Review>(postViewModel);
                review.UserEmail = User.Identity.Name;
                var result = await _reviewOperations.AddAsync(review);
                return await Get(result.Id);

            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT POST REVIEW", ex);
                throw;
            }
        }

        /// <summary>
        /// Удаление отзыва.
        /// </summary>
        [HttpDelete]
        [RESTAuthorize]
        public async Task<IHttpActionResult> Delete(int id)
        {
            
            try
            {
                var review = await _reviewOperations.GetAsync(id);
                if (review == null) return this.Result404("This review is not found");
                var checkRightsResult = CheckPermission(review);
                if (checkRightsResult != null) return checkRightsResult;

                await _reviewOperations.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT DELETE REVIEW", ex);
                throw;
            }
        }

        /// <summary>
        /// Установить признак проверки. Админ проверяет отзыв и устанавливает, что отзыв нормальный. 
        /// </summary>
        /// <param name="reviewId">ID отзыва</param>
        /// <param name="isChecked">Результат проверки. True, если всё нормально</param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ReviewViewModelGet))]
        [RESTAuthorize(Role.PortalAdmin, Role.PortalManager)]
        [Route("setchecked")]
        public async Task<IHttpActionResult> SetChecked(int reviewId, bool isChecked)
        {
            try
            {
                var review = await _reviewOperations.GetAsync(reviewId);
                if (review == null) return this.Result404("This review is not found");
                review.IsChecked = isChecked;
                await _reviewOperations.UpdateAsync(review);
                return await Get(reviewId);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT SET CHECKED ON REVIEW", ex);
                throw;
            }
        }

        /// <summary>
        /// Проверка прав пользователя на отзыв
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        private IHttpActionResult CheckPermission(Review review)
        {
            if ((!User.IsInRole("PortalAdmin"))
                && (!User.IsInRole("PortalManager"))
                && (User.Identity.Name != review.UserEmail))
            {
                return new ResponseMessageResult(
                    new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent("You haven't rights to edit this review.")
                    });
            }
            else return null;
        }
    }
}