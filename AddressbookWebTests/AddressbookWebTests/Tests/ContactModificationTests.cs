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
            ContactData contact = new ContactData("ModifiedUserFirstName", "ModifiedBaseUserLastName");
            contact.Email = "Modifiedemail@gmail.com";

            app.Contacts.Modify(contact);
        }
    }
}
