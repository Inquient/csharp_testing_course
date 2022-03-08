using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            // arrange
            app.Auth.Logout();

            // act
            AccountData credentials = new AccountData("admin", "secret");
            app.Auth.Login(credentials);

            // assert
            Assert.IsTrue(app.Auth.IsLoggedIn(credentials));
        }

        [Test]
        public void LoginWithInValidCredentials()
        {
            // arrange
            app.Auth.Logout();

            // act
            AccountData credentials = new AccountData("admin", "wrong");
            app.Auth.Login(credentials);

            // assert
            Assert.IsFalse(app.Auth.IsLoggedIn(credentials));
        }
    }
}
