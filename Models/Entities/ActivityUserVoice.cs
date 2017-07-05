﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace Models.Entities
{
    public class ActivityUserVoice
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Ссылка на пользователя, который голосует
        /// </summary>
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// Ссылка на активность, за которую голосуем
        /// </summary>
        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        /// <summary>
        /// Значение голоса: или -1, или +1
        /// </summary>
        public VoiceValue VoiceValue { get; set; }
    }

    public enum VoiceValue
    {
        Positive = 1,
        Negative = -1
    }
}
