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

            GroupData newGroupData = new GroupData("ModifiedGroupName");
            newGroupData.Header = null;
            newGroupData.Footer = null;

            List<GroupData> oldGroups = app.Groups.GetGroupsList();
            GroupData oldData = oldGroups[0];

            app.Groups.Modify(0, newGroupData);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupsCount());

            List<GroupData> groups = app.Groups.GetGroupsList();
            oldGroups[0].Name = newGroupData.Name;
            oldGroups.Sort();
            groups.Sort();
            Assert.AreEqual(oldGroups, groups);

            foreach(GroupData group in groups)
            {
                if(group.Id == oldData.Id)
                {
                    Assert.AreEqual(newGroupData.Name, group.Name);
                }
            }
        }
    }
}
