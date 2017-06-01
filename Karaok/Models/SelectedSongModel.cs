using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Karaok.Models;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Karaok.Models
{
    public class SelectedSongModel
    {
        [Key]
        public int ID { get; set; }
        public int SongID { get; set; }
        public string SongName { get; set; }
    }
}