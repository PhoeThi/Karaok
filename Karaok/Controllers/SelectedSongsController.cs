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
    public class SelectedSongsController : ApiController
    {
        static ISelectedSongsRepository repository = new SelectedSongsRepository();
        public IEnumerable<SelectedSongModel> GetSelectedSongs()
        {
            var artists = repository.GetSelectedSongs();
            return artists;
        }

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
    }
}
