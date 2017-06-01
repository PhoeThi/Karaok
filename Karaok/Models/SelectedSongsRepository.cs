using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Karaok.Models;
using System.IO;
using System.Text;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using Newtonsoft.Json;
using Karaok.Controllers;
using Newtonsoft.Json.Linq;

namespace Karaok.Models
{
    public class SelectedSongsRepository : ISelectedSongsRepository
    {
        private List<SelectedSongModel> songList = new List<SelectedSongModel>();
        public IEnumerable<SelectedSongModel> GetSelectedSongs()
        {
            List<SelectedSongModel> songList = null;
            DataTable dt = new DataTable();
            dt = SQLHelper.GetDataTable(QueryStrings.GetSelectedSongs());
            if (dt.Rows.Count > 0)
            {
                songList = new List<SelectedSongModel>();
                foreach (DataRow row in dt.Rows)
                {
                    SelectedSongModel selectedSong = new SelectedSongModel();
                    selectedSong = GetSelectedSongList(row);
                    songList.Add(selectedSong);
                }
            }
            return songList;
        }

        private SelectedSongModel GetSelectedSongList(DataRow row)
        {
            SelectedSongModel selectedSongs = new SelectedSongModel();
            selectedSongs.ID = (Int32)row["ID"];
            selectedSongs.SongID = (Int32)row["SongID"];
            selectedSongs.SongName = row["SongName"].ToString();

            return selectedSongs;
        }


        #region<Selected Songs>
        public HttpResponseMessage PutSelectedSongs([FromBody] SelectedSongModel jSon)
        {
            #region<just tested old codes>
            //DataTable dt = new DataTable();
            //dt = (DataTable)JsonConvert.DeserializeObject(jSon, (typeof(DataTable)));
            #endregion

            var message = new HttpResponseMessage(HttpStatusCode.OK);

            #region<just tested Old Codes>
            /*
            int dtCount = dt.Rows.Count;
            if (dtCount>0)
            {
                SQLHelper.ExecuteSQLQuery(QueryStrings.TruncateSelectedSongs());
                foreach (DataRow row in dt.Rows)
                {
                    SQLHelper.ExecuteSQLQuery(QueryStrings.UpdateSelectedSongs((int)row["ID"], (int)row["SongID"]));
                }
            SQLHelper.ExecuteSQLQuery(QueryStrings.UpdateSelectedSongs(jSon.ID, jSon.SongID));
            */
            #endregion


            SQLHelper.ExecuteSQLQuery(QueryStrings.InsertSelectedSong(jSon.ID, jSon.SongID));
            return message;
        }

        public HttpResponseMessage PostSelectedSongs(SelectedSongsModel objectData)
        {
            List<SelectedSongsModel> songsModel = new List<SelectedSongsModel>();
            dynamic jsonData = objectData;

            #region<Comment not required old codes>
            //JArray jsonArr = jsonData.SelectedSongsCollection;
            //SQLHelper.ExecuteSQLQuery(QueryStrings.TruncateSelectedSongs());
            #endregion

            SQLHelper.ExecuteSQLQuery(QueryStrings.TruncateSelectedSongs());
            foreach (var item in jsonData.SelectedSongsCollection)
            {
                #region<Just Old Codes>
                /*
                int id = (int)item["ID"];
                int SongID = (int)item["SongID"];
                int ID = item.ID;
                int SongID = item.SongID;
                 * 
                 * bool isExitID = Convert.ToInt32(SQLHelper.GetBoolValue(QueryStrings.IsExitSong(item.ID))) == 0 ? false : true;
                if (!isExitID)
                {
                    SQLHelper.ExecuteSQLQuery(QueryStrings.InsertSelectedSong(item.ID, item.SongID));
                }
                else
                {
                    SQLHelper.ExecuteSQLQuery(QueryStrings.UpdateSelectedSongs(item.ID, item.SongID));
                }
                 * */
                #endregion

                SQLHelper.ExecuteSQLQuery(QueryStrings.InsertSelectedSong(item.ID, item.SongID));
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        #endregion
    }
}