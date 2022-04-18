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
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }

        [Test]
        public void TestAccountRegistration()
        {
	        AccountData account = new AccountData()
            {
                Name = "TestUser8",
                Password = "password",
                Email = "testuser8@localhost"
            };

	        List<AccountData> accounts = app.Admin.GetAllAccounts();
	        var existingAccount = accounts.Find(x => x.Name == account.Name);
	        if (existingAccount != null)
            {
	            app.Admin.DeleteAccount(existingAccount);
            }

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
        }
    }
}
