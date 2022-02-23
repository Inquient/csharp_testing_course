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
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToContactCrationPage();

            ContactData contact = new ContactData("UserFirstName", "UserLastName");
            contact.Email = "beautifullemail@gmail.com";
            FillContactForm(contact);

            SubmitContactCreation();
            ReturnToHomePageAndLogout();
        }
    }
}
