using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using API.Models;
using Models.Entities;

namespace API.ViewModels
{
    public class ActivityUserVoiceViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Ссылка на пользователя, который голосует
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Ссылка на активность, за которую голосуем
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// Значение голоса: или -1, или +1
        /// </summary>
        public VoiceValue VoiceValue { get; set; }

    }
}