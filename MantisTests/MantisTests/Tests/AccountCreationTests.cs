using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MantisTests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [SetUp]
        public void SetUpConfig()
        {
            app.Ftp.BackupFiles("/config_inc.php");
            using (Stream localFile = File.Open(@"C:\Users\User\source\repos\csharp_testing_course\MantisTests\MantisTests\config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
        }

        [TearDown]
        public void RestoreCongig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "TestUser",
                Password = "password",
                Email = "testuser@localhost.localdomail"
            };

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
        }
    }
}
