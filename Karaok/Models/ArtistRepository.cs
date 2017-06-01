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

namespace Karaok.Models
{
    public class ArtistRepository : IArtistsRepository
    {
        static ConcurrentDictionary<int, ArtistsModel> artists = new ConcurrentDictionary<int, ArtistsModel>();
        private static List<ArtistsModel> artistList = new List<ArtistsModel>();
        public IEnumerable<ArtistsModel> GetAllArtists()
        {
            List<ArtistsModel> artistList = null;

            DataTable dt = new DataTable();
            dt = SQLHelper.GetDataTable(QueryStrings.ArtistAll());
            if (dt.Rows.Count > 0)
            {
                artistList = new List<ArtistsModel>();
                foreach (DataRow row in dt.Rows)
                {
                    ArtistsModel artist = new ArtistsModel();
                    artist = GetArtistList(row);
                    artistList.Add(artist);
                }
            }
            return artistList;
        }

        public ArtistsModel FindArtist(int id)
        {
            DataTable dt = SQLHelper.GetDataTable(QueryStrings.ArtistByUniqueID(id));
            ArtistsModel artist = new ArtistsModel();
            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                artist = GetArtistList(dr);
            }
            return artist;
        }

        public ArtistsModel GetArtistByCode(string code)
        {
            DataTable dt = SQLHelper.GetDataTable(QueryStrings.ArtistByShort(code));
            ArtistsModel artist = new ArtistsModel();
            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                artist = GetArtistList(row);
            }
            return artist;
        }
        internal static ArtistsModel GetArtistList(DataRow row)
        {
            ArtistsModel artist = new ArtistsModel();
            artist.id = Convert.ToInt32(row["id"]);
            artist.name = row["name"].ToString();
            artist.languageID = Convert.ToInt32(row["languageid"]);
            artist.picture = Encoding.ASCII.GetBytes(row["picture"].ToString());
            artist.rating = Convert.ToInt32(string.IsNullOrEmpty(row["rating"].ToString()) ? 0 : row["rating"]);
            artist.sex = Convert.ToBoolean(row["sex"]);
            artist.sort = row["sort"].ToString();
            artistList.Add(artist);
            return artist;
        }
    }
}