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
            AddressBook addressbook = new AddressBook();
            bool flag = true;
            int choice;
            while(flag)
            {
                Console.WriteLine("\n1. Display All Contacts\n2. Add New Contact\n3. Edit a Contact\n4. Delete a Contact\n5. Exit");
                choice=int.Parse(Console.ReadLine());
                if(choice==1)
                {
                    addressbook.DisplayAddressBook();
                }
                else if(choice==2)
                {
                    addressbook.AddNewContact();
                }
                else if(choice==3)
                {
                    addressbook.EditContact();
                }
                else if(choice==4)
                {
                    addressbook.DeleteContact();
                }
                else if(choice==5)
                {
                    flag = false;
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                }
            }
        }
    }
}
