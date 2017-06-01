using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Collections.Concurrent;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using Karaok.Models;
using System.IO;
using System.Text;

namespace Karaok.Models
{
    public class PressedKeyRepository : IPressedKeyRepository
    {
        static ConcurrentDictionary<int, PressedKeyValueModel> artists = new ConcurrentDictionary<int, PressedKeyValueModel>();
        private List<PressedKeyValueModel> keyValueList = new List<PressedKeyValueModel>();
        public IEnumerable<PressedKeyValueModel> GetAllPressedKeyValue()
        {
            List<PressedKeyValueModel> keyValueList = null;

            DataTable dt = new DataTable();
            dt = SQLHelper.GetDataTable(QueryStrings.KeyValueAll());
            if (dt.Rows.Count > 0)
            {
                keyValueList = new List<PressedKeyValueModel>();
                foreach (DataRow row in dt.Rows)
                {
                    PressedKeyValueModel keyvalue = new PressedKeyValueModel();
                    keyvalue = GetPressedKeyValueList(row);
                    keyValueList.Add(keyvalue);
                }
            }
            return keyValueList;
        }

        public HttpResponseMessage PutPressedKey(int id,PressedKeyValueModel emp)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            string keyvalue = emp.pressedkey;
            try
            {
                SQLHelper.ExecuteSQLQuery(QueryStrings.UpdateKeyPressedButtonQuery(keyvalue));
            }
            catch (Exception)
            {

                throw new NotImplementedException();
            }

            return result;
        }

        private PressedKeyValueModel GetPressedKeyValueList(DataRow row)
        {
            PressedKeyValueModel keyvalue = new PressedKeyValueModel();
            keyvalue.id = Convert.ToInt32(row["id"]);
            keyvalue.pressedkey = row["KeyPressedValue"].ToString();
            return keyvalue;
        }

        public void Put(string keyvalue)
        {
            SQLHelper.ExecuteSQLQuery(QueryStrings.UpdateKeyPressedButtonQuery(keyvalue));
        }
    }
}