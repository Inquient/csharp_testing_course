using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace AddressbookWebTests 
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("GroupName");
            group.Header = "GroupHeader";
            group.Footer = "GroupFooter";

            List<GroupData> oldGroups = app.Groups.GetGroupsList();

            app.Groups.Create(group);

            List<GroupData> groups = app.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count + 1, groups.Count);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupsList();

            app.Groups.Create(group);

            List<GroupData> groups = app.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count + 1, groups.Count);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("a'a");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Groups.GetGroupsList();

            app.Groups.Create(group);

            List<GroupData> groups = app.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count + 1, groups.Count);
        }
    }
}
