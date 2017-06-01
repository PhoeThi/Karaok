using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Karaok.Models
{
    public interface ISelectedSongsRepository
    {
        IEnumerable<SelectedSongModel> GetSelectedSongs();
        HttpResponseMessage PutSelectedSongs(SelectedSongModel jSon);

        HttpResponseMessage PostSelectedSongs(SelectedSongsModel songsCollection);
    }
}
