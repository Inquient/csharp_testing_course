using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            app.Groups.CreateGroupIfDoesNotExists("groupToModify");

            List<GroupData> oldGroups = GroupData.GetAll();

            GroupData toBeModified = oldGroups[0];
            toBeModified.Name = "GroupWasModified";
            toBeModified.Header = null;
            toBeModified.Footer = null;

            app.Groups.Modify(toBeModified);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupsCount());

            List<GroupData> groups = GroupData.GetAll();
            oldGroups.Sort();
            groups.Sort();
            Assert.AreEqual(oldGroups, groups);

            foreach(GroupData group in groups)
            {
                if(group.Id == toBeModified.Id)
                {
                    Assert.AreEqual(toBeModified.Name, group.Name);
                }
            }
        }
    }
}
