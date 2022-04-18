using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SimpleBrowser.WebDriver;

namespace MantisTests
{
	public class AdminHelper : HelperBase
	{
		private string BaseUrl;
		public AdminHelper(ApplicationManager manager, string BaseUrl) : base(manager)
		{
			this.BaseUrl = BaseUrl;
		}

		public List<AccountData> GetAllAccounts()
		{
			IWebDriver driver = OpenAppAndLogin();
			List<AccountData> accounts = new List<AccountData>();
			driver.Url = BaseUrl + "/manage_user_page.php";
			IList<IWebElement> rows = driver.FindElements(By.CssSelector("table tr.row-1, table tr.row-2"));
			foreach (IWebElement row in rows)
			{
				IWebElement link = row.FindElement(By.TagName("a"));
				string name = link.Text;
				string href = link.GetAttribute("href");
				string id = Regex.Match(href, @"\d+$").Value;

				accounts.Add(new AccountData()
				{
					Name = name,
					Id = id
				});
			}

			return accounts;
		}

		public void DeleteAccount(AccountData account)
		{
			IWebDriver driver = OpenAppAndLogin();
			driver.Url = BaseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
			driver.FindElement(By.CssSelector("input[value='Delete User']")).Click();
			driver.FindElement(By.CssSelector("input[value='Delete Account']")).Click();
		}

		private IWebDriver OpenAppAndLogin()
		{
			IWebDriver driver = new FirefoxDriver();
			driver.Url = BaseUrl + "/login_page.php";
			driver.FindElement(By.Name("username")).SendKeys("administrator");
			driver.FindElement(By.Name("password")).SendKeys("root");
			driver.FindElement(By.ClassName("width-40")).Click();
			return driver;
		}
	}
}
