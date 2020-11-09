using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

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

        public static bool AddNewContacts(List<Contact> contacts)
        {
            try
            {
                foreach (Contact contact in contacts)
                {
                    RestRequest request = new RestRequest("/addressBook", Method.POST);
                    JObject JsonContact = new JObject();
                    JsonContact.Add("FirstName", contact.FirstName);
                    JsonContact.Add("LastName", contact.LastName);
                    JsonContact.Add("Address",contact.Address);
                    JsonContact.Add("City", contact.City);
                    JsonContact.Add("state", contact.state);
                    JsonContact.Add("Zip", contact.Zip);
                    JsonContact.Add("PhoneNumber", contact.PhoneNo);
                    JsonContact.Add("Email", contact.Email);

                    request.AddParameter("application/json", JsonContact, ParameterType.RequestBody);

                    IRestResponse response = client.Execute(request);

                    Console.WriteLine(response.StatusCode);
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
