using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addressbook
{
    class AddressBook
    {
        SortedSet<Contact> addressBook = new SortedSet<Contact>(new ContactComparer());
    }
}
