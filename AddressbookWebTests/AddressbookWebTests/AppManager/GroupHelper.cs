using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace AddressbookWebTests
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public List<GroupData> GetGroupsList()
        {
            List <GroupData> groups = new List<GroupData>();

            manager.Navigator.GoToGroupsPage();

            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group")); // Выбирает все элементы span с классом group
            foreach(IWebElement element in elements)
            {
                groups.Add(new GroupData(element.Text));
            }

            return groups;
        }

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();

            return this;
        }

        public void CreateGroupIfDoesNotExists(string groupName = "groupToRemove")
        {
            if (!DoesAnyGroupExist())
            {
                Create(new GroupData(groupName));
            }
        }

        public GroupHelper Modify(int index, GroupData newGroup)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(index);
            InitGroupModification();
            FillGroupForm(newGroup);
            SubmitGroupModification();
            ReturnToGroupsPage();

            return this;
        }

        public GroupHelper Remove(int index)
        {
            manager.Navigator.GoToGroupsPage();

            SelectGroup(index);
            RemoveGroup();
            ReturnToGroupsPage();

            return this;
        }

        public bool DoesAnyGroupExist()
        {
            return IsElementPresent(By.ClassName("group"));
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper SelectGroup(int groupIndex)
        {
            driver.FindElement(By.XPath($"//div[@id='content']/form/span[{groupIndex}]/input")).Click();
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }
    }
}
