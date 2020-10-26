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
    }
}
