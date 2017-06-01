using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karaok.Models
{
    public class ArtistsModel
    {
        public int id { get; set; }

        public string name { get; set; }
        public int languageID { get; set; }

        public string artistShort { get; set; }

        public byte[] picture { get; set; }

        public bool sex { get; set; }

        public string sort { get; set; }

        public int rating { get; set; }
    }
}