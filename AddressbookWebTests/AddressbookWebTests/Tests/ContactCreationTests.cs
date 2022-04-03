using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();

            for (int i = 0; i < 3; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30), GenerateRandomString(30)));
            }

            return contacts;
        }

        public static IEnumerable<ContactData> GroupDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void ContactCreationTest(ContactData contact)
        {
            //ContactData contact = new ContactData("BaseUserFirstName", "BaseUserLastName");
            //contact.Email = "baseemail@gmail.com";

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void ContactWithAdditionalFieldsCreationTest()
        {
            ContactData contact = new ContactData("ExtendedUserFirstName", "ExtendedUserLastName");
            contact.Email = "extendedemail@gmail.com";

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.CreateContactWithAdditionalFields(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");
            contact.Email = "";

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
