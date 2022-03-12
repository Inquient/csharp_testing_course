﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.CreateContactIfDoesNotExists();

            app.Contacts.Remove(1);
        }

        [Test]
        public void AllContactsRemovalTest()
        {
            app.Contacts.CreateContactIfDoesNotExists();

            app.Contacts.RemoveAll();
        }
    }
}
