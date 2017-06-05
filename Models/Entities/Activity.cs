using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using DelegateDecompiler;

namespace Models.Entities
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Organizer")]
        public int OrganizerId { get; set; }
        public virtual Organizer Organizer { get; set; }

        public int AgeFrom { get; set; }

        public int AgeTo { get; set; }

        public string Phones { get; set; }

        public string Address { get; set; }

        public string Prices { get; set; }

        public string Mentor { get; set; }

        public string Description { get; set; }

        [ForeignKey("Interest")]
        public int? InterestId { get; set; }
        public virtual Interest Interest { get; set; }

        public bool IsChecked { get; set; }

        public bool Free { get; set; }

        public virtual ICollection<ActivityUserVoice> ActivityUserVoices { get; set; }

        [Computed]
        public int Voices {
            get { return ActivityUserVoices.Sum(v => (int)v.VoiceValue); }
        }

    }
}
