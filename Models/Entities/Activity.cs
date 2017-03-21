using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

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

        public StringInfo Address { get; set; }

        public string Prices { get; set; }

        public bool Sobriety { get; set; }

        public string Mentor { get; set; }

        public string Description { get; set; }

        //========================================
        public int GetRaiting()
        {
            //TODO: для рейтинга использовать отдельную табличку. ActivityUsersVoices
            throw new NotImplementedException();
        }


    }
}
