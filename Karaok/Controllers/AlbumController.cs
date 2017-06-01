using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Karaok.Models;

namespace Karaok.Controllers
{
    public class AlbumController : ApiController
    {
        static IAlbumsRepository repository = new AlbumRepository();
        public AlbumModel Get(int id)
        {
            AlbumModel albumList = new AlbumModel();
            albumList = repository.GetAlbumByUniqueID(id);
            return albumList;
        }
    }
}
