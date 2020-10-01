using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Addressbook
{
    class AddressBookMain
    {
        static void Main(string[] args)
        {
            Logger nlog = LogManager.GetCurrentClassLogger();

            try
            {
                Console.WriteLine("Welcome To Address Book Problem");
                AddressBook addressbook = new AddressBook();
                bool flag = true;
                int choice;
                while (flag)
                {
                    Console.WriteLine("\n1. Display All Contacts\n2. Add New Contact\n3. Edit a Contact\n4. Delete a Contact\n5. Exit");
                    choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        nlog.Info("Displaying contacts");
                        addressbook.DisplayAddressBook();
                    }
                    else if (choice == 2)
                    {
                        nlog.Info("Adding new contact");
                        addressbook.AddNewContact();
                    }
                    else if (choice == 3)
                    {
                        nlog.Info("Editing contacts");
                        addressbook.EditContact();
                    }
                    else if (choice == 4)
                    {
                        nlog.Info("Deleting contacts");
                        addressbook.DeleteContact();
                    }
                    else if (choice == 5)
                    {
                        nlog.Info("Exiting Program");
                        flag = false;
                    }
                    else
                    {
                        nlog.Warn("Invalid Input");
                        Console.WriteLine("Invalid Input");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace+e.Message);
                nlog.Error(e.StackTrace+e.Message);
            }
        }
    }
}
