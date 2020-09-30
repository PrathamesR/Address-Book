using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook
{
    public class ContactComparer : IComparer<Contact>
    {
        public int Compare(Contact x, Contact y)
        {
            return string.Compare(x.firstName+x.lastName, y.firstName+y.lastName);
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
    }
}
