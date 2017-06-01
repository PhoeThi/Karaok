using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Karaok.Models;

namespace Karaok.Controllers
{
    public class PressKeyValueController : ApiController
    {
        static IPressedKeyRepository repository = new PressedKeyRepository();

        //api/PressKeyValue?keypressvalue=key
        //http://192.168.0.173:4704/api/PressKeyValue?keypressvalue=r
        public IEnumerable<PressedKeyValueModel> GetAllKeyPressedButton()
        {
            var keyvalues = repository.GetAllPressedKeyValue();
            return keyvalues;
        }

        public HttpResponseMessage PutPressedKeyValue(PressedKeyValueModel keyvalues)
        {

            var retMessage = new HttpResponseMessage(HttpStatusCode.OK);
            string key = keyvalues.pressedkey;
            int id = keyvalues.id;
            retMessage = repository.PutPressedKey(id,keyvalues);
            var response = Request.CreateResponse<PressedKeyValueModel>(HttpStatusCode.UpgradeRequired, keyvalues);
            string uri = Url.Link("UpdateApi", new { id = keyvalues.pressedkey });
            response.Headers.Location = new Uri(uri);
            return response;
            
        }

        public string Put(string keypressvalue)
        {
 
            repository.Put(keypressvalue);
            return keypressvalue;

        }
    }
}
