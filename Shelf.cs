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
        public Dictionary<string, AddressBook> shelf = new Dictionary<string, AddressBook>();

        public void ListBooks()
        {
            if (shelf.Count > 0)
            {
                Console.WriteLine("Books Availaible:-");
                foreach (var item in shelf)
                    Console.Write("|" + item.Key);
                Console.WriteLine();
            }
        }

        public void AddNewAddressBook(string addressBookName,AddressBook addressBook)
        {
            shelf.Add(addressBookName, addressBook);
        }

        public void ReplaceAddressBook(string addressBookName,AddressBook addressBook)
        {
            shelf[addressBookName] = addressBook;
        }

        public AddressBook GetAddressBook(string name)
        {
            return shelf[name];
        }
    }
}
