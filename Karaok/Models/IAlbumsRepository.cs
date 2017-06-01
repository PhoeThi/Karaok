using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karaok.Models
{
    public interface IAlbumsRepository
    {
        AlbumModel GetAlbumByUniqueID(int albumID);
    }
}