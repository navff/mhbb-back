using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using API.Common;
using API.ViewModels;
using AutoMapper;
using Camps.Tools;
using Models.Entities;
using Models.Operations;

namespace API.Controllers
{
    [RoutePrefix("api/activity")]
    public class ActivityController : ApiController
    {
        private ActivityOperations _activityOperations;
        private PictureOperations _pictureOperations;
        private VoiceOperations _voiceOperations;

        public ActivityController(ActivityOperations activityOperations, PictureOperations pictureOperations, VoiceOperations voiceOperations)
        {
            _activityOperations = activityOperations;
            _pictureOperations = pictureOperations;
            _voiceOperations = voiceOperations;
        }

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
                var activity = await _activityOperations.GetAsync(id);
                if (activity == null)
                {
                    return this.Result404("This activity is not found");

                }
                var result = Mapper.Map<ActivityViewModelGet>(activity);
                var pictures = await _pictureOperations.GetByLinkedObject(LinkedObjectType.Activity, id);
                var picturesVirewModels = new List<PictureViewModelShortGet>(); 
                foreach (var picture in pictures)
                {
                    picturesVirewModels.Add(new PictureViewModelShortGet
                    {
                        Id = picture.Id,
                        Url = Url.Content($"~/api/picture/{picture.Id}"),
                        IsMain = picture.IsMain
                });
                }
                result.Pictures = picturesVirewModels;
                result.Voices = await _voiceOperations.GetActivityVoices(result.Id);
                return Ok(result);
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
                                                             int page = 1)
        {
            try
            {
                var activities = await _activityOperations.SearchAsync(word, age, interestId,
                                                                       cityId, sobriety, free, page);
                var result = Mapper.Map<IEnumerable<ActivityViewModelShortGet>>(activities).ToList();

                foreach (var viewModel in result)
                {
                    var picture = (await _pictureOperations.GetMainByLinkedObject(LinkedObjectType.Activity, viewModel.Id));
                    var pictureViewModel = Mapper.Map<PictureViewModelShortGet>(picture);
                    if (pictureViewModel != null)
                    {
                        pictureViewModel.Url = Url.Content($"~/api/picture/{picture.Id}");
                    }
                    viewModel.MainPicture = pictureViewModel;
                    viewModel.Voices = await _voiceOperations.GetActivityVoices(viewModel.Id);
                }

                return Ok(result);
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
                var activity = Mapper.Map<Activity>(putViewModel);
                activity.Id = id;
                await _activityOperations.UpdateAsync(activity);
                return await Get(id);
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
                var activity = Mapper.Map<Activity>(postViewModel);
                var result = await _activityOperations.AddAsync(activity);
                await _pictureOperations.SaveByFormIdAsync(postViewModel.FormId, result.Id, LinkedObjectType.Activity);
                return await Get(result.Id);
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
                await _activityOperations.DeleteAsync(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT DELETE ACTIVITY", ex);
                throw;
            }
        }
    }
}