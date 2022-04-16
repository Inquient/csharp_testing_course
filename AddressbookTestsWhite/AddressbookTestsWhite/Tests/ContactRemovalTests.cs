using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookTestsWhite
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            // arrange
            app.Contacts.CreateContactIfDoesNotExists();

            var contacts = app.Contacts.GetContactsList();
            ContactData contactToRemove = contacts[0];

            // act
            app.Contacts.RemoveContact(contactToRemove);

            // assert
            var newContacts = app.Contacts.GetContactsList();
            contacts.Remove(contactToRemove);
            contacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(contacts, newContacts);
        }
    }
}
