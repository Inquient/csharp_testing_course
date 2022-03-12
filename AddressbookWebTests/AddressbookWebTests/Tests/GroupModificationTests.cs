using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            app.Groups.CreateGroupIfDoesNotExists("groupToModify");

            GroupData group = new GroupData("ModifiedGroupName");
            group.Header = null;
            group.Footer = null;

            List<GroupData> oldGroups = app.Groups.GetGroupsList();

            app.Groups.Modify(0, group);

            List<GroupData> groups = app.Groups.GetGroupsList();
            oldGroups[0].Name = group.Name;
            oldGroups.Sort();
            groups.Sort();
            Assert.AreEqual(oldGroups, groups);
        }
    }
}
