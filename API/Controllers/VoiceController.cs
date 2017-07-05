using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using API.Common;
using API.Operations;
using Models.Entities;
using Models.Operations;

namespace API.Controllers
{
    /// <summary>
    ///Голосование пользователей за активности. 
    /// </summary>
    [RoutePrefix("api/voice")]
    public class VoiceController : ApiController
    {
        private VoiceOperations _voiceOperations;
        private UserOperations _userOperations;

        public VoiceController(VoiceOperations voiceOperations, UserOperations userOperations)
        {
            _voiceOperations = voiceOperations;
            _userOperations = userOperations;
        }

        /// <summary>
        /// Проголосовать позитивно. Активность понравилась
        /// </summary>
        [Route("positive/{activityId}")]
        [RESTAuthorize]
        [HttpPost]
        public async Task<IHttpActionResult> Positive(int activityId)
        {
            var user = await _userOperations.GetAsync(User.Identity.Name);
            var result = await _voiceOperations.AddVoice(user.Id, 
                                                         VoiceValue.Positive, 
                                                         activityId);
            return Ok(result);
        }

        /// <summary>
        /// Проголосовать негативно. Активность не понравилась
        /// </summary>
        [Route("negative/{activityId}")]
        [RESTAuthorize]
        [HttpPost]
        public async Task<IHttpActionResult> Negative(int activityId)
        {
            var user = await _userOperations.GetAsync(User.Identity.Name);
            var result = await _voiceOperations.AddVoice(user.Id, 
                                                         VoiceValue.Negative, 
                                                         activityId);
            return Ok(result);
        }

    }
}