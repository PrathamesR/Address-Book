using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
namespace Addressbook
{

    public class AddressBook
    {
        SortedSet<Contact> addressBook = new SortedSet<Contact>(new ContactComparer());
        Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Displays All Contact in the AdressBook
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
                    Console.WriteLine(contact.firstName + "\t\t" + contact.lastName + "\t\t" + contact.address + "\t" + contact.city + "\t" + contact.state + "\t" + contact.zip + "\t\t" + contact.phoneNo + "\t\t" + contact.email);
                }
                logger.Info("Displayed Contacts Successfully");
            }
        }

        /// <summary>
        /// Adds New Contact to the Address Book
        /// </summary>
        public void AddNewContact()
        {
            Contact contact = new Contact();
            Console.Write("Enter First Name: ");
            contact.firstName = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            contact.lastName = Console.ReadLine();
            Console.Write("Enter Address:");
            contact.address = Console.ReadLine();
            Console.Write("Enter City: ");
            contact.city = Console.ReadLine();
            Console.Write("Enter State: ");
            contact.state = Console.ReadLine();
            Console.Write("Enter ZIP Code: ");
            contact.zip = int.Parse(Console.ReadLine());
            Console.Write("Enter Phone Number: ");
            contact.phoneNo = double.Parse(Console.ReadLine());
            Console.Write("Enter Email Id: ");
            contact.email = Console.ReadLine();

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
                Console.Write("Existing value : " + contact[properties[choice]]+"\t Enter New Value: ");
                contact[properties[choice]] = (Console.ReadLine());
                Console.WriteLine(properties[choice] + " edited succesfully");
            }
            else
            {
                logger.Warn("Name " + name + " not present in addressbook");
                Console.WriteLine("Name not Found");
            }
        }


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
