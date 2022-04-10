using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.CreateContactIfDoesNotExists();

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];

            app.Contacts.Remove(toBeRemoved);

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts.Remove(toBeRemoved);
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void AllContactsRemovalTest()
        {
            app.Contacts.CreateContactIfDoesNotExists();

            app.Contacts.RemoveAll();

            Assert.AreEqual(0, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = ContactData.GetAll();
            Assert.IsEmpty(newContacts);
        }
    }
}
