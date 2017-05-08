using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Models.Entities;

namespace API.ViewModels
{
    public class ActivityViewModelGet
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual OrganizerViewModelGet Organizer { get; set; }

        public int AgeFrom { get; set; }

        public int AgeTo { get; set; }

        public string Phones { get; set; }

        public string Address { get; set; }

        public string Prices { get; set; }

        public string Mentor { get; set; }

        public string Description { get; set; }

        public virtual InterestViewModelGet Interest { get; set; }

        public bool IsChecked { get; set; }
        public IEnumerable<PictureViewModelShortGet> Pictures { get; set; }
    }

    public class ActivityViewModelShortGet
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual OrganizerViewModelGet Organizer { get; set; }

        public int AgeFrom { get; set; }

        public int AgeTo { get; set; }

        public string Phones { get; set; }

        public string Address { get; set; }

        public string Prices { get; set; }

        public string Mentor { get; set; }

        public string Description { get; set; }

        public virtual InterestViewModelGet Interest { get; set; }

        public bool IsChecked { get; set; }

        public PictureViewModelShortGet MainPicture { get; set; }
    }

    public class ActivityViewModelPost
    {
        public string Name { get; set; }

        public int OrganizerId { get; set; }
        public virtual OrganizerViewModelPost Organizer { get; set; }

        public int AgeFrom { get; set; }

        public int AgeTo { get; set; }

        public string Phones { get; set; }

        public string Address { get; set; }

        public string Prices { get; set; }

        public string Mentor { get; set; }

        public string Description { get; set; }

        public int? InterestId { get; set; }

        public bool IsChecked { get; set; }


        /// <summary>
        /// ID формы. По этому идентификатору связываются временные файлы
        /// с активностями. После сохранения активности, все TempFile с указанным FormId
        /// превратятся в Picture и привяжутся в данной активности.
        /// </summary>
        public string FormId { get; set; }


    }
}