using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookTestsWhite.Tests
{
    [TestFixture]
    class GroupRemovalTests : TestBase
    {
        [Test]
        public void TestGroupRemoval()
        {
            // arrange
            app.Groups.CreateGroupIfDoesNotExists();

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            GroupData groupToRemove = oldGroups[0];

            // act
            app.Groups.RemoveGroup(groupToRemove);

            // assert
            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Remove(groupToRemove);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestLastGroupRemoval()
        {
            // arrange
            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData lastGroup = oldGroups[0];
            oldGroups.Remove(lastGroup);

            if (oldGroups.Count > 1)
            {
                foreach (var group in oldGroups)
                {
                    app.Groups.RemoveGroup(group);
                }
            }

            // act
            app.Groups.RemoveLastGroup(lastGroup);

            // assert
            List<GroupData> newGroups = app.Groups.GetGroupList();
            List<GroupData> remainingGroups = new List<GroupData>(){lastGroup};
            remainingGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(remainingGroups, newGroups);

            RestoreRemovedGroups(oldGroups);
        }

        public void RestoreRemovedGroups(List<GroupData> groups)
        {
            foreach (var group in groups)
            {
                app.Groups.CreateGroup(group);
            }
        }
    }
}
