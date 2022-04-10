using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.CreateContactIfDoesNotExists("contactToModifyFirstName", "contactToModifyLastName");

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeModified = oldContacts[0];
            toBeModified.FirstName = "ModifiedUserFirstName";
            toBeModified.LastName = "ModifiedBaseUserLastName";
            toBeModified.Email = "Modifiedemail@gmail.com";

            app.Contacts.Modify(toBeModified);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].FirstName = toBeModified.FirstName;
            oldContacts[0].LastName = toBeModified.LastName;
            oldContacts[0].Email = toBeModified.Email;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
