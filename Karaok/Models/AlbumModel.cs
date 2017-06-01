using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karaok.Models
{
    public class AlbumModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public byte[] picture { get; set; }
        public int languageID { get; set; }
        public string Sort { get; set; }
        public int Rating { get; set; }
    }
}