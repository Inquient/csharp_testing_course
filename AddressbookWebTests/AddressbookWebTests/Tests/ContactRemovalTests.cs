using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.CreateContactIfDoesNotExists();

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Remove(0);

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void AllContactsRemovalTest()
        {
            app.Contacts.CreateContactIfDoesNotExists();

            app.Contacts.RemoveAll();

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            Assert.IsEmpty(newContacts);
        }
    }
}
