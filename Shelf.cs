using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook
{
    class Shelf
    {
        Dictionary<string, AddressBook> shelf = new Dictionary<string, AddressBook>();

        public void AddNewAddressBook(string addressBookName,AddressBook addressBook)
        {
            shelf.Add(addressBookName, addressBook);
        }

        public AddressBook GetAddressBook(string name)
        {
            foreach(var item in shelf)
            {
                if (item.Key == name)
                    return item.Value;
            }
            return null;
        }

        internal enum SearchBy { city,state}
        public void SearchPeopleBy(string value,SearchBy searchBy)
        {
            bool isFound = false;
            Console.WriteLine("\nDisplaying Contacts - \nFirst Name\tLast Name\tAddress\tCity\tState\tZIP Code\tPhone Number\tEmailId");
            foreach (var book in shelf.Values)
            {
                foreach(Contact contact in book.addressBook)
                {
                    if (contact.city.Equals(value) && SearchBy.city == searchBy)
                    {
                        contact.DisplayContact();
                        isFound = true;
                    }

                    if (contact.state.Equals(value) && SearchBy.state == searchBy)
                    {
                        contact.DisplayContact();
                        isFound = true;
                    }
                }
            }

            if(!isFound)
            {
                Console.WriteLine("No Contact found from " + value);
            }
        }
    }
}
