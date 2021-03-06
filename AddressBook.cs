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
    [Serializable]
    public class AddressBook
    {
        public List<Contact> addressBook = new List<Contact>();
        
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
                contact.FirstName = firstName;

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            if (!Regex.IsMatch(lastName, namePattern))
                throw new Exception("Name should only contain alphabets");
            else
                contact.LastName = lastName;

            Console.Write("Enter Address:");
            contact.Address = Console.ReadLine();

            Console.Write("Enter City: ");
            string city = Console.ReadLine();
            if (!Regex.IsMatch(city, namePattern))
                throw new Exception("City Name should only contain alphabets");
            else
                contact.City = city;

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
                contact.Zip = (zip);

            string pnoPattern = "[0-9]{10}";
            Console.Write("Enter Phone Number: ");
            string pNo = Console.ReadLine();
            if (!Regex.IsMatch(pNo, pnoPattern))
                throw new Exception("Phone number should be a 10 digit number");
            else
                contact.PhoneNo = (pNo);

            string mailPattern = @"[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
            Console.Write("Enter Email Id: ");
            string mail = Console.ReadLine();
            if (!Regex.IsMatch(mail, mailPattern))
                throw new Exception("Check Mail address");
            else
                contact.Email = mail;

            addressBook.Add(contact);

            if (Info.cityInfo.ContainsKey(city))
                Info.cityInfo[city].Add(contact);
            else
                Info.cityInfo.Add(city, new List<Contact>() { contact });


            if (Info.stateInfo.ContainsKey(state))
                Info.stateInfo[state].Add(contact);
            else
                Info.stateInfo.Add(state, new List<Contact>() { contact });

            Console.WriteLine("New Contact added successfully");
        }

        /// <summary>
        /// Edits the contact.
        /// </summary>
        public void EditContact()
        {
            Console.Write("\nEnter the name of the contact to edit: ");
            string name = Console.ReadLine();

            Contact contact = null;
            IEnumerator<Contact> itr = addressBook.GetEnumerator();
            while (itr.MoveNext())
            {
                contact = itr.Current as Contact;
                if (contact.FirstName == name)
                    break;
            }


            if (name == contact.FirstName)
            {
                Console.WriteLine("Select The property to edit");
                Console.WriteLine("1.First Name\n2.Last Name\n3.Address\n4.City\n5.State\n6.ZIP Code\n7.Phone Number\n8.Email Address");
                string[] properties = { "firstName", "lastName", "address", "city", "state", "zip", "phoneNo", "email" };

                int choice = int.Parse(Console.ReadLine());
                string exitingVal = contact[properties[choice - 1]].ToString();
                Console.Write("Existing value : " + exitingVal + "\t Enter New Value: ");
                string newValue = Console.ReadLine();
                contact[properties[choice]] = TypeDescriptor.GetConverter(contact[properties[choice - 1]].GetType()).ConvertFrom(newValue);

                if (choice == 4)
                    Info.cityInfo[exitingVal][Info.cityInfo[exitingVal].IndexOf(contact)].City = newValue;
                else if (choice == 5) 
                    Info.stateInfo[exitingVal][Info.stateInfo[exitingVal].IndexOf(contact)].state = newValue;

                Console.WriteLine(properties[choice] + " edited succesfully");
            }
            else
            {
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
                if (contact.FirstName == name)
                    break;
            }

            if (name == contact.FirstName)
            {
                addressBook.Remove(contact);
                Console.WriteLine("Contact Deleted Successfully");
            }
            else
            {
                Console.WriteLine("Name not Found");
            }

        }
    }
}