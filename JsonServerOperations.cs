using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace Addressbook
{
    class JsonServerOperations
    {
        static RestClient client = new RestClient("http://localhost:3000");

        public static List<Contact> ReadEntries()
        {
            RestRequest request = new RestRequest("/addressBook", Method.GET);
            IRestResponse response = client.Execute(request);
            List <Contact>  contacts= JsonConvert.DeserializeObject<List<Contact>>(response.Content);

            return contacts;
        }           
    }
}
