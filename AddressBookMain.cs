using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook
{
    class AddressBookMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Address Book Problem");

            //UseCase2
            AddressBook addressbook = new AddressBook();
            addressbook.AddNewContact();
            addressbook.DisplayAddressBook();


            //Temp Read to view Console
            Console.Read();
        }
    }
}
