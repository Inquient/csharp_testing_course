using NUnit.Framework;

namespace AddressbookTestsWhite
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            // arrange
            var contacts = app.Contacts.GetContactsList();
            ContactData contactToCreate = new ContactData("contactFirstName", "contactLastName");

            // act
            app.Contacts.CreateContact(contactToCreate);

            // assert
            var newContacts = app.Contacts.GetContactsList();
            contacts.Add(contactToCreate);
            contacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(contacts, newContacts);
        }
    }
}
