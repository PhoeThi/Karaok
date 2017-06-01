using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karaok.Models
{
    public interface IArtistsRepository
    {
        IEnumerable<ArtistsModel> GetAllArtists();
        ArtistsModel FindArtist(int id);

        ArtistsModel GetArtistByCode(string code);

     
    }
}