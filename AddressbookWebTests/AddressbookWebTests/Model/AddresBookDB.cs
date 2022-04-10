using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace AddressbookWebTests
{
    public class AddresBookDB : LinqToDB.Data.DataConnection
    {
        public AddresBookDB() : base("AddressBook"){}

        public ITable<GroupData> groups
        {
            get { return GetTable<GroupData>(); }
        }

        public ITable<ContactData> Contacts
        {
            get { return GetTable<ContactData>(); }
        }

        public ITable<GroupContactRelation> GCR
        {
            get { return GetTable<GroupContactRelation>(); }
        }
    }
}
