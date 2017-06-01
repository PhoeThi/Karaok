using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Karaok.Models
{
    public interface ISongsRepository
    {
        IEnumerable<SongsModel> GetAllSongs();

        SongsModel GetSongByID(int id);

        SongsModel GetSongByCode(string songCode);

        IEnumerable<SongsModel> GetSongByCodes(string songCodes);

        #region< Divided to Selected Song Layer>

        /*

        HttpResponseMessage PutSelectedSongs(SelectedSongModel jSon);

        HttpResponseMessage PostSelectedSongs(SelectedSongsModel songsCollection);
         * */

        #endregion

        IEnumerable<SongsModel> GetUpgradedSongs(int latestID,bool upgradeSongs);
    }
}