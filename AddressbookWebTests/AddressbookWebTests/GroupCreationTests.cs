﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace AddressbookWebTests 
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToGroupsPage();
            InitGroupCreation();

            GroupData group = new GroupData("GroupName");
            group.Header = "GroupHeader";
            group.Footer = "GroupFooter";
            FillGroupForm(group);

            SubmitGroupCreation();
            ReturnToGroupsPage();
        }
    }
}