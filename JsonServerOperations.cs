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
                    JsonContact.Add("Address", contact.Address);
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
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool UpdateContact()
        {
            Console.Write("\nEnter the name of the contact to edit: ");
            string name = Console.ReadLine();
            List<Contact> contacts = ReadEntries();

            int i = 0;
            foreach (var contact in contacts)
            {
                i++;
                if (contact.FirstName.Equals(name))
                    break;
            }

            Console.WriteLine("Select The property to edit");
            Console.WriteLine("1.First Name\n2.Last Name\n3.Address\n4.City\n5.State\n6.ZIP Code\n7.Phone Number\n8.Email Address");
            string[] properties = { "FirstName", "LastName", "Address", "City", "state", "Zip", "PhoneNumber", "Email" };
            int choice = int.Parse(Console.ReadLine());
            Console.Write("Enter New Value: ");
            string value = Console.ReadLine();

            try
            {
                RestRequest request = new RestRequest("/addressBook/" + i, Method.PATCH);
                JObject JsonContact = new JObject();
                JsonContact.Add(properties[choice - 1], value);

                request.AddParameter("application/json", JsonContact, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                Console.WriteLine(response.StatusCode);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool DeleteContact()
        {
            Console.Write("\nEnter the name of the contact to delete: ");
            string name = Console.ReadLine();
            List<Contact> contacts = ReadEntries();

            int i = 0;
            foreach (var contact in contacts)
            {
                i++;
                if (contact.FirstName.Equals(name))
                    break;
            }

            try
            {
                RestRequest request = new RestRequest("/addressBook/" + i, Method.DELETE);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.StatusCode);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
