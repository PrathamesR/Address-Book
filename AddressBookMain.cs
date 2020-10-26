﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.Text.RegularExpressions;

namespace Addressbook
{

    class AddressBookMain
    {
        public static Shelf shelf;
        static string savePath = @"D:\Capgemini\BridgeLabs Lectures\Week2\Addressbook\Shelf.txt";
        static string csvPath = @"D:\Capgemini\BridgeLabs Lectures\Week2\Addressbook\Address Book.csv";

        public static void UseAddressBook(string Name,AddressBook addressbook)
        {
            Logger nlog = LogManager.GetCurrentClassLogger();

            bool flag = true;
            int choice;
            while (flag)
            {
                try
                {
                    Console.WriteLine("\n1. Display All Contacts\n2. Add New Contact\n3. Edit a Contact\n4. Delete a Contact" +
                        "\n5. Order this Address Book\n6. Save To CSV \n7. Load from CSV\n8. Close Address Book");
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
                        Console.WriteLine("\n1.Name\n2.City\n3.State\n4.ZIP");
                        int orderChoice = int.Parse(Console.ReadLine());
                        switch(orderChoice)
                        {
                            case 1: addressbook.addressBook.Sort(new NameComparer());
                                Console.WriteLine("Sorted Successfully By Name");
                                break;
                            case 2:
                                addressbook.addressBook.Sort(new CityComparer());
                                Console.WriteLine("Sorted Successfully By City");
                                break;
                            case 3:
                                addressbook.addressBook.Sort(new StateComparer());
                                Console.WriteLine("Sorted Successfully By State");
                                break;
                            case 4:
                                addressbook.addressBook.Sort(new ZipComparer());
                                Console.WriteLine("Sorted Successfully By ZIP");
                                break;
                            default:
                                Console.WriteLine("Invalid Input");
                                break;
                        }
                    }
                    else if (choice == 6)
                    {
                        FileIO.SaveToCSV(addressbook,csvPath);
                        Console.WriteLine("Stored Data at "+csvPath);
                    }
                    else if (choice == 7)
                    {
                        AddressBook Obj = new AddressBook();
                        Obj.addressBook = FileIO.LoadFromCSV(csvPath);
                        shelf.ReplaceAddressBook(Name, Obj);
                    }
                    else if (choice == 8)
                    {
                        nlog.Info("Changing Address Book");
                        flag = false;
                    }
                    else
                    {
                        nlog.Warn("Invalid Input");
                        Console.WriteLine("Invalid Input");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid data entered. Error: " + e.Message +"\n" + e.StackTrace);
                    nlog.Error(e.StackTrace + e.Message);
                }
            }

        }

        static void Main(string[] args)
        {
            FileIO.LoadFromTxt(savePath);

            Logger nlog = LogManager.GetCurrentClassLogger();

            if (shelf == null)
                shelf = new Shelf();
            shelf.ListBooks();

            bool flag = true;
            int choice;

            while (flag)
            {
                try
                {

                    Console.WriteLine("\n1. Create New Address Book \n2. Use Address Book\n3. Search Contact by City Name\n4. Search Contact by State Name\n5. Exit");
                    choice = int.Parse(Console.ReadLine());
                    if (choice == 1)
                    {
                        AddressBook addressBook = new AddressBook();
                        Console.Write("\nEnter New Address Book's Name: ");
                        string addressBookName = Console.ReadLine();
                        shelf.AddNewAddressBook(addressBookName, addressBook);
                        Console.WriteLine("Successfully created " + addressBookName + "\tUsing Address Book " + addressBookName + "...");
                        UseAddressBook(addressBookName, addressBook);
                    }
                    else if (choice == 2)
                    {

                        shelf.ListBooks();
                        Console.Write("\nEnter Address Book's Name: ");
                        string addressBookName = Console.ReadLine();
                        AddressBook addressBook = shelf.GetAddressBook(addressBookName);
                        if (addressBook != null)
                        {
                            Console.WriteLine("Using Address Book " + addressBookName + "...");
                            UseAddressBook(addressBookName, addressBook);
                        }
                        else
                            Console.WriteLine("There is no Book with name " + addressBookName);
                    }
                    else if (choice == 3)
                    {
                        Console.Write("\nEnter City Name: ");
                        Info.GetPeopleByState(Console.ReadLine());
                    }
                    else if (choice == 4)
                    {
                        Console.Write("\nEnter State Name: ");
                        Info.GetPeopleByState(Console.ReadLine());
                    }
                    else if (choice == 5)
                    {
                        FileIO.SaveToText(shelf, savePath);
                        nlog.Info("Exiting Program");
                        flag = false;
                    }
                    else
                    {
                        nlog.Warn("Invalid Input");
                        Console.WriteLine("Invalid Input");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("Invalid data entered. Error: " + e.Message + "\n" + e.StackTrace);
                }
            }

        }
    }
}
