using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MantisTests
{
    public class JamesLoginTests : TestBase
    {
        [Test]
        public void TestLogin()
        {
            var account = new AccountData()
            {
                Name = "sss",
                Password = "yyy"
            };
            Assert.IsFalse(app.James.Verify(account));

            app.James.Add(account);
            Assert.IsTrue(app.James.Verify(account));

            app.James.Delete(account);
            Assert.IsFalse(app.James.Verify(account));
        }
    }
}
