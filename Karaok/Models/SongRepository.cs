using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using System.Web.Configuration;
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
    public class SongRepository : ISongsRepository
    {
        #region<List Group>

        static ConcurrentDictionary<int, SongsModel> songs = new ConcurrentDictionary<int, SongsModel>();
        private List<SongsModel> songList = new List<SongsModel>();
        private List<AlbumModel> albumList = new List<AlbumModel>();
        private List<ArtistsModel> artistList = new List<ArtistsModel>();
        private ArtistsModel[] artistObj = new ArtistsModel[1];
        private AlbumModel[] albumObj = null;

        #endregion

        #region <Get Songs >
        public IEnumerable<SongsModel> GetAllSongs()
        {
            List<SongsModel> songList = null;
            DataTable dt = new DataTable();
            dt = SQLHelper.GetDataTable(QueryStrings.SongsAll());
            if (dt.Rows.Count>0)
            {
                songList = new List<SongsModel>();
                foreach (DataRow row in dt.Rows)
                {
                    SongsModel song = new SongsModel();
                    song = GetSongList(row);
                    songList.Add(song);
                }
            }
            return songList;
        }

        public SongsModel GetSongByID(int id)
        {
            SongsModel song = new SongsModel();
            DataTable dt = new DataTable();
            dt = SQLHelper.GetDataTable(QueryStrings.SongByUniqueID(id));
            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                song = GetSongList(row);
            }
            return song;
        }

        public SongsModel GetSongByCode(string songCode) 
        {
            SongsModel song = new SongsModel();
            DataTable dt = new DataTable();
            dt = SQLHelper.GetDataTable(QueryStrings.SongByCode(songCode));
            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                song = GetSongList(row);
            }
            return song;
        }

        public IEnumerable<SongsModel> GetSongByCodes(string songCodes)
        {
            List<SongsModel> songList = null;
            DataTable dt = new DataTable();
            dt = SQLHelper.GetDataTable(QueryStrings.SongByCodes(songCodes));
            songList = new List<SongsModel>();
            foreach (DataRow row in dt.Rows)
            {
                SongsModel song = new SongsModel();
                song = GetSongList(row);
                songList.Add(song);
            }

            return songList;
        }

        public HttpResponseMessage AddSelectedSongList(SongsModel songs)
        {
            var message = new HttpResponseMessage(HttpStatusCode.OK);

            return message;
        }
        private SongsModel GetSongList(DataRow row)
        {
            
            SongsModel song = new SongsModel();
            song.id = Convert.ToInt32(row["id"]);
            song.code = row["code"].ToString();
            song.name = row["name"].ToString();
            song.Short = row["short"].ToString();
            song.path = row["path"].ToString();
            song.fileName = row["filename"].ToString();
            song.languageid = Convert.ToInt32(string.IsNullOrEmpty(row["languageid"].ToString()) ? 0 : row["languageid"]);
            song.albumid = Convert.ToInt32(string.IsNullOrEmpty(row["albumid"].ToString()) ? 0 : row["albumid"]);           
            return song;
        }

        #endregion

        #region< To Get Ugraded Songs >
        /*To Get Upgrade Songs*/

        public IEnumerable<SongsModel> GetUpgradedSongs(int latestID, bool UpgradeSongs)
        {
            List<SongsModel> updatedSongList = new List<SongsModel>();
            DataTable dtSongs = new DataTable();
            DataTable dtAlbum = new DataTable();
            DataTable dtArtist = new DataTable();
            dtSongs = SQLHelper.GetDataTable(QueryStrings.UpgradeSongs(latestID));
            int dtCount = dtSongs.Rows.Count;
            albumObj = new AlbumModel[dtCount];
            artistObj = new ArtistsModel[dtCount];
            int i = 0;
            foreach (DataRow row in dtSongs.Rows)
            {

                SongsModel song = new SongsModel();
                song = GetUpdradedSongList(row, i);
                updatedSongList.Add(song);
                i++;
            }
            return updatedSongList;

        }
        private SongsModel GetUpdradedSongList(DataRow row,int line)
        {
            SongsModel song = new SongsModel();
            song.id = Convert.ToInt32(row["id"]);
            song.code = row["code"].ToString();
            song.name = row["name"].ToString();
            song.Short = row["short"].ToString();
            song.path = row["path"].ToString();
            song.fileName = row["filename"].ToString();
            song.languageid = Convert.ToInt32(string.IsNullOrEmpty(row["languageid"].ToString()) ? 0 : row["languageid"]);
            song.albumid = Convert.ToInt32(string.IsNullOrEmpty(row["albumid"].ToString()) ? 0 : row["albumid"]);
            song.artistid = Convert.ToInt32(string.IsNullOrEmpty(row["artistid"].ToString()) ? 0 : row["artistid"]);

            #region<For Album Information>
            //song.AlbumInformation = null;
            song.AlbumInformation = GetAlbumInformation(song.albumid,line);
            #endregion

            //song.ArtistInformation = null;
            #region <For Artists Information>
            song.ArtistInformation = GetArtistInformation(song.artistid,line);
            #endregion
            return song;
 
        }

        private AlbumModel[] GetAlbumInformation(int albumID,int line)
        {
            AlbumModel[] albumObjRes = new AlbumModel[1];
            DataTable dtAlbum = new DataTable();
            dtAlbum = SQLHelper.GetDataTable(QueryStrings.AlbumByUniqueID(albumID));
            albumList = new List<AlbumModel>();
            AlbumModel album = new AlbumModel();
            foreach (DataRow alRow in dtAlbum.Rows)
            {
                album = AlbumRepository.GetAlbumList(alRow);
                albumObj = new AlbumModel[line+1];
                for (int i = 0; i < line+1; i++)
                {
                    if(i==line)
                    {
                        albumObj[line] = album;
                        albumObjRes[0] = albumObj[line];
                    }
                }
                

                #region<Useless Old Code>

                /*
                albumModel.id = Convert.ToInt32(album.id);
                albumModel.name = album.name.ToString();
                albumModel.picture = album.picture;
                albumModel.languageID = album.languageID;
                albumModel.Sort = album.Sort;
                albumModel.Rating = album.Rating;
                albumObj[0] = albumModel;
                 */

                #endregion


            }
            //return albumObj;
            return albumObjRes;
        }

        private ArtistsModel[] GetArtistInformation(int artistID,int line)
        {
            ArtistsModel[] artistObjRes = new ArtistsModel[1];
            DataTable dtArtist = new DataTable();
            dtArtist = SQLHelper.GetDataTable(QueryStrings.ArtistByUniqueID(artistID));
            artistList = new List<ArtistsModel>();
            ArtistsModel artist = new ArtistsModel();
            foreach (DataRow arRow in dtArtist.Rows)
            {
                artist = ArtistRepository.GetArtistList(arRow);
                artistObj = new  ArtistsModel[line+1]; // add later Not Ok to delete Same on Album
                for (int i = 0; i < line+1; i++)
                {
                    artistObj[line] = artist;
                    artistObjRes[0] = artistObj[line];
                }
                
            }
            //return artistObj;
            return artistObjRes;
        }

        #endregion

        /*
        #region<Selected Songs>
        public HttpResponseMessage PutSelectedSongs([FromBody] SelectedSongModel jSon)
        {
            #region<just tested old codes>
            //DataTable dt = new DataTable();
            //dt = (DataTable)JsonConvert.DeserializeObject(jSon, (typeof(DataTable)));
            #endregion

            var message = new HttpResponseMessage(HttpStatusCode.OK);

            #region<just tested Old Codes>
            //int dtCount = dt.Rows.Count;
            //if (dtCount>0)
            //{
            //    SQLHelper.ExecuteSQLQuery(QueryStrings.TruncateSelectedSongs());
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        SQLHelper.ExecuteSQLQuery(QueryStrings.UpdateSelectedSongs((int)row["ID"], (int)row["SongID"]));
            //    }

            #endregion

            SQLHelper.ExecuteSQLQuery(QueryStrings.UpdateSelectedSongs(jSon.ID, jSon.SongID));
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

            foreach (var item in jsonData.SelectedSongsCollection)
            {
                #region<Just Old Codes>
                //int id = (int)item["ID"];
                //int SongID =(int)item["SongID"];
                //int ID = item.ID;
                //int SongID = item.SongID;
                #endregion

                bool isExitID = Convert.ToInt32(SQLHelper.GetBoolValue(QueryStrings.IsExitSong(item.ID))) == 0 ? false : true;
                if (!isExitID)
                {
                    SQLHelper.ExecuteSQLQuery(QueryStrings.InsertSelectedSong(item.ID, item.SongID));
                }
                else
                {
                    SQLHelper.ExecuteSQLQuery(QueryStrings.UpdateSelectedSongs(item.ID, item.SongID));
                }
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        #endregion
         * */

    }
}