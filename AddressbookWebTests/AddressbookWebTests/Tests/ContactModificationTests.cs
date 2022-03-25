using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.CreateContactIfDoesNotExists("contactToModifyFirstName", "contactToModifyLastName");

            ContactData contact = new ContactData("ModifiedUserFirstName", "ModifiedBaseUserLastName");
            contact.Email = "Modifiedemail@gmail.com";

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Modify(contact, 0);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts[0].FirstName = contact.FirstName;
            oldContacts[0].LastName = contact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
