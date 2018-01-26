using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Models.DTO
{
    public class PictureDTO
    {
            public int Id { get; set; }

            public string Filename { get; set; }

            public bool IsMain { get; set; }

            public int LinkedObjectId { get; set; }

            public LinkedObjectType LinkedObjectType { get; set; }

            public byte[] Data { get; set; }
    }

    public class PictureShortDTO
    {
        public int Id { get; set; }

        public string Filename { get; set; }

        public bool IsMain { get; set; }

        public int LinkedObjectId { get; set; }

        public LinkedObjectType LinkedObjectType { get; set; }
    }
}
