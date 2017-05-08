using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    public class TempFileViewModelGet
    {
        /// <summary>
        /// ID файла
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id формы, на которой закачали файл
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// Имя файла с расширением
        /// </summary>
        [Required]
        public string Filename { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }
    }

    public class TempFileViewModelPost
    {
        /// <summary>
        /// Id формы, на которой закачали файл
        /// </summary>
        public string FormId { get; set; }

        /// <summary>
        /// Имя файла с расширением
        /// </summary>
        [Required]
        public string Filename { get; set; }

        /// <summary>
        /// Двоичные данные файла
        /// </summary>
        [Required]
        public byte[] Data { get; set; }

        public bool IsMain { get; set; }
    }
}