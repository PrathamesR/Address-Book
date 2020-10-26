using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook
{

    class InfoException : Exception
    {
        public InfoException(string message) : base(message)
        {

        }
    }

    class Info
    {
        public static Dictionary<string, List<Contact>> cityInfo = new Dictionary<string, List<Contact>>();
        public static Dictionary<string, List<Contact>> stateInfo = new Dictionary<string, List<Contact>>();

        public static int GetCountByCity(string city)
        {
            try
            {
                if (cityInfo.ContainsKey(city))
                {
                    return cityInfo[city].Count;
                }
                else
                { throw new InfoException("City Does Not Exist"); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public static int GetCountByState(string state)
        {
            try
            {
                if (stateInfo.ContainsKey(state))
                {
                    return stateInfo[state].Count;
                }
                else
                { throw new InfoException("State Does Not Exist"); }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public static void GetPeopleByCity(string city)
        {
            Console.WriteLine("There are " + GetCountByCity(city) + " people in " + city);
            if (cityInfo.ContainsKey(city))
                Console.WriteLine("\nDisplaying Contacts - \nFirst Name\tLast Name\tAddress\tCity\tState\tZIP Code\tPhone Number\tEmailId");

            foreach (Contact contact in cityInfo[city])
            {
                contact.DisplayContact();
            }
        }

        public static void GetPeopleByState(string state)
        {
            Console.WriteLine("There are " + GetCountByState(state) + " people in " + state);

            if (stateInfo.ContainsKey(state))
                Console.WriteLine("\nDisplaying Contacts - \nFirst Name\tLast Name\tAddress\tCity\tState\tZIP Code\tPhone Number\tEmailId");

            foreach (Contact contact in stateInfo[state])
            {
                contact.DisplayContact();
            }
        }
    }
}
