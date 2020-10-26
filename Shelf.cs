using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook
{
    [Serializable]
    public class Shelf
    {
        Dictionary<string, AddressBook> shelf = new Dictionary<string, AddressBook>();

        public void ShowBooks()
        {
            foreach (var item in shelf)
                Console.WriteLine(item.Key);
        }

        public void AddNewAddressBook(string addressBookName,AddressBook addressBook)
        {
            shelf.Add(addressBookName, addressBook);
        }

        public AddressBook GetAddressBook(string name)
        {
            return shelf[name];
        }
    }
}
