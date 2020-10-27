using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Addressbook
{
    [Serializable]
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string state { get; set; }
        public string Zip { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }

        public Contact()
        {

        }

        public Contact(string firstName, string lastName, string address, string city, string state, string zip, string phoneNo, string email)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.City = city;
            this.state = state;
            this.Zip = zip;
            this.PhoneNo = phoneNo;
            this.Email = email;
        }

        public object this[string propName]
        {
            get
            {
                Type type = GetType();
                PropertyInfo propertyInfo = type.GetProperty(propName);
                return propertyInfo.GetValue(this, null);
            }
            set
            {
                Type type = GetType();
                PropertyInfo propertyInfo = type.GetProperty(propName);
                propertyInfo.SetValue(this, value, null);
            }
        }

        public override string ToString()
        {
            return FirstName + "\t\t" + LastName + "\t\t" + Address + "\t" + City + "\t" + state + "\t" + Zip + "\t\t" + PhoneNo + "\t" + Email;
        }

        public void DisplayContact()
        {
            Console.WriteLine(ToString());
        }
    }


    public class NameComparer : IComparer<Contact>
    {
        public int Compare(Contact x, Contact y)
        {
            return string.Compare(x.FirstName + x.LastName, y.FirstName + y.LastName);
        }
    }

    public class CityComparer : IComparer<Contact>
    {
        public int Compare(Contact x, Contact y)
        {
            return string.Compare(x.City, y.City);
        }
    }

    public class StateComparer : IComparer<Contact>
    {
        public int Compare(Contact x, Contact y)
        {
            return string.Compare(x.state, y.state);
        }
    }

    public class ZipComparer : IComparer<Contact>
    {
        public int Compare(Contact x, Contact y)
        {
            return string.Compare(x.Zip,y.Zip);
        }
    }
}
