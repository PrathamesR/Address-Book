﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Addressbook
{

    public class AddressBook
    {
        public SortedSet<Contact> addressBook = new SortedSet<Contact>(new ContactComparer());
        Logger logger = LogManager.GetCurrentClassLogger();



        /// <summary>
        /// Displays All Contacts in the AdressBook
        /// </summary>
        public void DisplayAddressBook()
        {
            if (addressBook.Count == 0)
                Console.WriteLine("No Contacts to display");
            else
            {
                Console.WriteLine("\nDisplaying Contacts - \nFirst Name\tLast Name\tAddress\tCity\tState\tZIP Code\tPhone Number\tEmailId");
                foreach (Contact contact in addressBook)
                {
                    contact.DisplayContact();
                }
                logger.Info("Displayed Contacts Successfully");
            }
        }

        /// <summary>
        /// Adds New Contact to the Address Book
        /// </summary>
        public void AddNewContact()
        {
            string namePattern = "^[a-zA-Z ]+$";
            Contact contact = new Contact();

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            if (!Regex.IsMatch(firstName, namePattern))
                throw new Exception("Name should only contain alphabets");
            else
                contact.firstName = firstName;

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            if (!Regex.IsMatch(lastName, namePattern))
                throw new Exception("Name should only contain alphabets");
            else
                contact.lastName = lastName;

            Console.Write("Enter Address:");
            contact.address = Console.ReadLine();

            Console.Write("Enter City: ");
            string city = Console.ReadLine();
            if (!Regex.IsMatch(city, namePattern))
                throw new Exception("City Name should only contain alphabets");
            else
                contact.city = city;

            Console.Write("Enter State: ");
            string state = Console.ReadLine();
            if (!Regex.IsMatch(state, namePattern))
                throw new Exception("City Name should only contain alphabets");
            else
                contact.state = state;

            string zipPattern = "[0-9]{6}";
            Console.Write("Enter ZIP Code: ");
            string zip = Console.ReadLine();
            if (!Regex.IsMatch(zip, zipPattern))
                throw new Exception("ZIP Code should be a 6 digit number");
            else
                contact.zip = int.Parse(zip);

            string pnoPattern = "[0-9]{10}";
            Console.Write("Enter Phone Number: ");
            string pNo = Console.ReadLine();
            if (!Regex.IsMatch(pNo, pnoPattern)) 
                throw new Exception("Phone number should be a 10 digit number");
            else
                contact.phoneNo = double.Parse(pNo);

            string mailPattern = @"[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            Console.Write("Enter Email Id: ");
            string mail = Console.ReadLine();
            if (!Regex.IsMatch(mail, mailPattern))
                throw new Exception("Check Mail address");
            else
                contact.email = mail;

            addressBook.Add(contact);
            Console.WriteLine("New Contact added successfully");
            logger.Info("Added New contact "+contact.firstName+" "+contact.lastName);
        }

        /// <summary>
        /// Edits the contact.
        /// </summary>
        public void EditContact()
        {
            Console.Write("\nEnter the name of the contact to edit: ");
            string name = Console.ReadLine();

            Contact contact=null;
            IEnumerator<Contact> itr = addressBook.GetEnumerator();
            while(itr.MoveNext())
            {
                contact = itr.Current as Contact;
                if (contact.firstName == name)
                    break;
            }


            if(name==contact.firstName)
            {
                Console.WriteLine("Select The property to edit");
                Console.WriteLine("1.First Name\n2.Last Name\n3.Address\n4.City\n5.State\n6.ZIP Code\n7.Phone Number\n8.Email Address");
                string[] properties = { "firstName", "lastName", "address", "city", "state", "zip", "phoneNo", "email" };
                
                int choice = int.Parse(Console.ReadLine());
                Console.Write("Existing value : " + contact[properties[choice-1]]+"\t Enter New Value: ");
                contact[properties[choice]] = TypeDescriptor.GetConverter(contact[properties[choice-1]].GetType()).ConvertFrom(Console.ReadLine());


                Console.WriteLine(properties[choice] + " edited succesfully");
            }
            else
            {
                logger.Warn("Name " + name + " not present in addressbook");
                Console.WriteLine("Name not Found");
            }
        }

        /// <summary>
        /// Deletes the contact.
        /// </summary>
        public void DeleteContact()
        {
            Console.Write("\nEnter the name of the contact to delete: ");
            string name = Console.ReadLine();

            Contact contact = null;
            IEnumerator<Contact> itr = addressBook.GetEnumerator();
            while (itr.MoveNext())
            {
                contact = itr.Current as Contact;
                if (contact.firstName == name)
                    break;
            }

            if (name == contact.firstName)
            {
                addressBook.Remove(contact);
                Console.WriteLine("Contact Deleted Successfully");
                logger.Info("Deleted Contact");
            }
            else
            {
                logger.Warn("Name " + name + " not present in addressbook");
                Console.WriteLine("Name not Found");
            }

        }
    }
}
