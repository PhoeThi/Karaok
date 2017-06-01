using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Karaok.Models;


namespace Karaok.Controllers
{
    public class ArtistsController : ApiController
    {
        static IArtistsRepository repository = new ArtistRepository();

        [AllowAnonymous]
        [HttpGet]
        //[Route("")]

        public IEnumerable<ArtistsModel> GetAllArtists()
        {
            var artists = repository.GetAllArtists();
            return artists;
        }
        public ArtistsModel Get(int id)
        {
            ArtistsModel artist = new ArtistsModel();
            artist = repository.FindArtist(id);
            return artist;
        }

        public ArtistsModel GetAritstsByCode(string code)
        {
            ArtistsModel artist = new ArtistsModel();
            artist = repository.GetArtistByCode(code);
            return artist;
        }

    }
}
