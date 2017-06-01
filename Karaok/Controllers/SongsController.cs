using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Karaok.Models;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Karaok.Controllers
{
    //[RoutePrefix("api/Song")]
    /*Links to research are in the query string*/

    #region<url>
    
    //Content-Type: application/json
    //Post : http://localhost:4704/api/Songs
    //{"SelectedSongsCollection":[{"ID":1,"SongID":11},{"ID":2,"SongID":12},{"ID":3,"SongID":13}]}
   
    #endregion
  
    public class SongsController : ApiController
    {
        
        static ISongsRepository repository = new SongRepository();
        
        public IEnumerable<SongsModel> GetAllSongs()
        {
            var artists = repository.GetAllSongs();
            return artists;
        }

        public SongsModel Get(int id)
        {
            SongsModel song = new SongsModel();
            song = repository.GetSongByID(id);
            return song;
        }

        public SongsModel GetSongByCode(string songCode)
        {
            SongsModel song = new SongsModel();
            song = repository.GetSongByCode(songCode);
            return song;
        }

        public IEnumerable<SongsModel> GetSongByCodes(string songCodes)
        {
            var songs = repository.GetSongByCodes(songCodes);
            return songs;
        }

        public IEnumerable<SongsModel> GetUpgradedSongs(int ID ,bool upgradeSong)
        {
            var songs = repository.GetUpgradedSongs(ID, upgradeSong);
            return songs;
        }

        /*
        [HttpPut]
        public HttpResponseMessage PutSelectedSongs([FromBody] SelectedSongModel selectedSongs)
        {
            if (ModelState.IsValid)
            {
                repository.PutSelectedSongs(selectedSongs);
                return new HttpResponseMessage(HttpStatusCode.OK);
                //Console.WriteLine( HttpStatusCode.OK.ToString());
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public HttpResponseMessage PostSelectedSongs([FromBody] SelectedSongsModel songsCollection)
        {
            
            if (ModelState.IsValid)
            {
                repository.PostSelectedSongs(songsCollection);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }


        */
        #region
    //    // DELETE /api/notatwitterapi/5
    //    public void Delete(int id)
    //    {
    //        var notatweet = notatweetRepository.Find(id);
    //        if (notatweet == null)
    //            throw new HttpResponseException(HttpStatusCode.NotFound);

    //        notatweetRepository.Delete(id);
        //    }
        #endregion
    }
}
