using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Addressbook
{
    public class NameComparer : IComparer<Contact>
    {
        public int Compare(Contact x, Contact y)
        {
            return string.Compare(x.firstName + x.lastName, y.firstName + y.lastName);
        }
    }

    public class CityComparer : IComparer<Contact>
    {
        public int Compare(Contact x, Contact y)
        {
            return string.Compare(x.city, y.city);
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
            return y.zip-x.zip;
        }
    }


    public class Contact
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public double phoneNo { get; set; }
        public string email { get; set; }

        public Contact()
        {

        }

        public Contact(string firstName, string lastName, string address, string city, string state, int zip, double phoneNo, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zip = zip;
            this.phoneNo = phoneNo;
            this.email = email;
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
            return firstName + "\t\t" + lastName + "\t\t" + address + "\t" + city + "\t" + state + "\t" + zip + "\t\t" + phoneNo + "\t" + email;
        }

        public void DisplayContact()
        {
            Console.WriteLine(ToString());
        }
    }
}
