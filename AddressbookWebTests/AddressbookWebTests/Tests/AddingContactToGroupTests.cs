using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroupTest()
        {
            // arrange
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldContacts = group.GetContacts();
            ContactData contact =  ContactData.GetAll().Except(oldContacts).First();

            // act
            app.Contacts.AddContactToGroup(contact, group);

            // assert
            List<ContactData> newContacts = group.GetContacts();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void RemoveContactFromGroupTest()
        {
            // arrange
            List<GroupData> allGroups = GroupData.GetAll();
            GroupData group = allGroups.First(g => g.GetContacts().Count > 0);

            List<ContactData> contactsOfGroup = group.GetContacts();
            ContactData contactToRemove = contactsOfGroup[0];

            // act
            app.Contacts.RemoveContactFromGroup(contactToRemove, group);

            // assert
            List<ContactData> newContactsOfGroup = group.GetContacts();
            contactsOfGroup.Remove(contactToRemove);
            contactsOfGroup.Sort();
            newContactsOfGroup.Sort();
            Assert.AreEqual(contactsOfGroup, newContactsOfGroup);
        }
    }
}
