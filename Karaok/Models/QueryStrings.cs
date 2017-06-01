using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karaok.Models
{
    public class QueryStrings
    {

        #region < Qurey String >

        private static string qryArtists = "select * from Artist";
        private static string qryAlbums = "select * from Album";
        private static string qrySong = "select * From Song";
        private static string qryUpdateKeyPressedButtonTrue = " update KeyPressedButton set CurrentKey = 1 where KeyPressedValue = ";
        private static string qryUpdateKeyPressedButtonFalse = " update KeyPressedButton set CurrentKey = 0 where KeyPressedValue <> ";
        private static string qryKeyValue = "select * From KeyPressedButton";
        private static string qrySongsInfo = "select s.Name Song,a.Name Artist,al.Name Album From Song s join SingerList sl on sl.SongID = s.ID join Artist a on a.id = sl.ArtistID join album al on al.id = s.AlbumID ";
        private static string qryUpgradeSongs = "select s.*,ArtistID From Song s join SingerList sl on sl.SongID = s.ID where s.id >";
        private static string qryAlbum = "select * From Album";
        
        #endregion

        #region <Song Information>
        public static string SongInformationOnSong(object para)
        {
            para = 1;
            string songInfo = qrySongsInfo + "where";
            return songInfo;
        }
        #endregion

        #region < Query For Songs >

        public static string SongsAll()
        {
            return qrySong;
        }
        public static string SongByCode(string songCode)
        {
            string qrySongByCode = qrySong + " where code = '" + songCode + "'";
            return qrySongByCode;
        }
        public static string SongByCodes(string songCodes)
        {
            string qrySongByCode = qrySong + " where code like N'" + songCodes + "'";
            return qrySongByCode;
        }
        public static string SongByName(string songName)
        {
            string qrySongByName = qrySong + " where name = '" + songName + "'";
            return qrySongByName;
        }
        public static string SongByUniqueID(int id)
        {
            string qrySongByUniqueID = qrySong + " where id = " + id.ToString();
            return qrySongByUniqueID;
        }
        public static string UpgradeSongs(int id)
        {
            string qryUpgradeSong = qryUpgradeSongs + " " + id.ToString();
            return qryUpgradeSong;
        }

        #endregion

        #region< Query For Artists >
        public static string ArtistAll()
        {
            return qryArtists;
        }
        public string ArtistByName(string artistName)
        {
            string qryArtistByName = qryArtists + " where name ='" + artistName + "'";
            return qryArtistByName;
        }
        public string ArtistByNames(string artistName)
        {
            string qryArtistByName = qryArtists + " where name  like N'" + artistName + "%'";
            return qryArtistByName;
        }
        public static string ArtistByShort(string artistShort)
        {
            string qryArtistByShort = qryArtists + " where short='"+ artistShort +"'";
            return qryArtistByShort;
        }
        public string ArtistByShorts(string artistShort)
        {
            string qryArtistByShort = qryArtists + " where short like N'" + artistShort + "%'";
            return qryArtistByShort;
        }
        public static string ArtistByUniqueID(int id)
        {
            string qryArtistByUniqueID = qryArtists + " where id = " + id.ToString();
            return qryArtistByUniqueID;
        }

        #endregion


        #region<Query For Album>
        public static string AlbumByUniqueID(int ID)
        {
            string qryAlbumByUniqueID = qryAlbum + " Where id = " + ID.ToString();
            return qryAlbumByUniqueID;
        }
        #endregion

        #region <Query For KeyPressButton>
        public static string KeyValueAll()
        {
            return qryKeyValue;
        }
        public static string UpdateKeyPressedButtonQuery(string value)
        {
            string Query = qryUpdateKeyPressedButtonTrue + "'" + value + "';" + qryUpdateKeyPressedButtonFalse + "'" + value + "';";
            return Query;
        }

        #endregion

        #region< Query For Update Selected Songs>
        public static string TruncateSelectedSongs()
        {
            string Query = "truncate table SelectedSong";
            return Query;
        }
        public static string IsExitSong(int id)
        {
            string Query = "select isExit = count(*)  From SelectedSong where id =" + id;
            return Query;
        }
        public static string UpdateSelectedSongs(int id, int songID)
        {
            string Query = "Update SelectedSong set SongID =" + songID + " Where id = " + id;
            return Query;
        }
        public static string InsertSelectedSong(int id, int SongID)
        {
            string Query = "insert into SelectedSong(ID,SongID) Values(" + id + "," + SongID + ")";
            return Query;
        }

        #endregion

        #region<Get Selected Songs>

        public static string GetSelectedSongs()
        {
           string qrySelectedSongs = "select sl.ID,s.ID SongID,s.Name SongName From song s join SelectedSong sl on s.ID = sl.SongID";
           return qrySelectedSongs;
        }

        #endregion

        #region<Useful Links>
        //http://www.dotnetcurry.com/aspnet/1278/aspnet-webapi-pass-multiple-parameters-action-method
        #endregion

        public static string SQLDataGet(string tableName)
        {
            string qryString = "select * from " + tableName;
            return qryString;
        }

        public static string SQLDataGet(string tableName, string condition)
        {
            string qryString = "select * From " + tableName + " Where " + condition;
            return qryString;
        }

        public static string SQLDataGet(string tableName, string condition, List<string> fieldNames)
        {
            string qryString = "select " + fieldNames + " From " + tableName + " where " + condition;
            return qryString;
        }
    }

}