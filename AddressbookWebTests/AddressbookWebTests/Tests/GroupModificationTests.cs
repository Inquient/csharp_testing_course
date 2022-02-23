using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData group = new GroupData("ModifiedGroupName");
            group.Header = "ModifiedGroupHeader";
            group.Footer = "ModifiedGroupFooter";

            app.Groups.Modify(1, group);
        }
    }
}
