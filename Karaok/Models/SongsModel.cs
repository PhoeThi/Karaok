using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karaok.Models
{
    public class SongsModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string Short {get;set;}
        public string path { get; set; }
        public string fileName { get; set; }
        public int languageid { get; set; }
        public int albumid { get; set; }

        #region<For Update Songs>

        public int artistid { get; set; }
        public ArtistsModel[] ArtistInformation { get; set; }

        public AlbumModel[] AlbumInformation { get; set; }

        #endregion
    }
}