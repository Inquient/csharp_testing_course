using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [SetUp]
        public void CreateContactAndGroupIfDoesNotExists()
        {
            app.Contacts.CreateContactIfDoesNotExists();
            app.Groups.CreateGroupIfDoesNotExists();
            app.Navigator.GoToHomePage();
        }

        [Test]
        public void AddingContactToGroupTest()
        {
            // arrange
            var (group, contact) = FindOrCreateContactNotInGroup();

            List<ContactData> oldContacts = group.GetContacts();

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
            var group = FindOrCreateGroupWithContact();

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

        public (GroupData group, ContactData contact) FindOrCreateContactNotInGroup()
        {
            List<GroupData> groups = GroupData.GetAll();
            List<ContactData> contacts = ContactData.GetAll();

            foreach (var group in groups)
            {
                var contactsNotInGroup = contacts.Except(group.GetContacts()).ToList();
                if (contactsNotInGroup.Count() > 0)
                {
                    return (group, contactsNotInGroup[0]);
                }
            }

            app.Contacts.Create(new ContactData(GenerateRandomString(10), GenerateRandomString(10)));

            var newContacts = ContactData.GetAll();
            var newContact = newContacts.Except(contacts).ToList();

            return (groups[0], newContact.First());
        }

        public GroupData FindOrCreateGroupWithContact()
        {
            List<GroupData> groups = GroupData.GetAll();
            List<ContactData> contacts = ContactData.GetAll();

            foreach (var group in groups)
            {
                var contactsOfGroup = group.GetContacts();
                if (contactsOfGroup.Count > 0)
                {
                    return group;
                }
            }

            var groupWithContacts = groups[0];
            app.Contacts.AddContactToGroup(contacts[0], groupWithContacts);
            return groupWithContacts;
        }
    }
}
