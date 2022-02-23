﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("BaseUserFirstName", "BaseUserLastName");
            contact.Email = "baseemail@gmail.com";

            app.Contacts.Create(contact);
        }

        [Test]
        public void ContactWithAdditionalFieldsCreationTest()
        {
            ContactData contact = new ContactData("ExtendedUserFirstName", "ExtendedUserLastName");
            contact.Email = "extendedemail@gmail.com";

            app.Contacts.CreateContactWithAdditionalFields(contact);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");
            contact.Email = "";

            app.Contacts.Create(contact);
        }
    }
}
