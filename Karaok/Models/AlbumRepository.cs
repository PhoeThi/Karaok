using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Karaok.Models
{
    public class AlbumRepository : IAlbumsRepository
    {
        private static List<AlbumModel> albumList = new List<AlbumModel>();
        public AlbumModel GetAlbumByUniqueID(int id)
        {
            DataTable dt = SQLHelper.GetDataTable(QueryStrings.AlbumByUniqueID(id));
            AlbumModel album = new AlbumModel();
            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                album = GetAlbumList(dr);
            }
            return album;
        }

        internal static AlbumModel GetAlbumList(DataRow row)
        {
            AlbumModel album = new AlbumModel();
            album.id = Convert.ToInt32(row["id"]);
            album.name = row["name"].ToString();
            album.languageID = Convert.ToInt32(row["languageid"]);
            album.picture = Encoding.ASCII.GetBytes(row["Picture"].ToString());
            album.Sort = row["sort"].ToString();
            album.Rating = Convert.ToInt32(string.IsNullOrEmpty(row["rating"].ToString()) ? 0 : row["rating"]);            
            albumList.Add(album);
            return album;
        }
    }
}