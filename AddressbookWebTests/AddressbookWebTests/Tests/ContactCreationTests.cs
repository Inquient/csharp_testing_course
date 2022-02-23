using System;
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
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.GoToContactCrationPage();

            ContactData contact = new ContactData("UserFirstName", "UserLastName");
            contact.Email = "beautifullemail@gmail.com";
            app.Contacts.FillContactForm(contact);

            app.Contacts.SubmitContactCreation();
            app.Contacts.ReturnToHomePage();

            app.Auth.Logout();
        }
    }
}
