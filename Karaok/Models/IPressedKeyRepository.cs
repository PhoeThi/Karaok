using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace Karaok.Models
{
    public interface IPressedKeyRepository
    {
        IEnumerable<PressedKeyValueModel> GetAllPressedKeyValue();
        HttpResponseMessage PutPressedKey(int id, PressedKeyValueModel emp);
        void Put(string keyvalue);
    }
}