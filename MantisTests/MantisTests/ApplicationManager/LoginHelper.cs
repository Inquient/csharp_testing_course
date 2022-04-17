using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace MantisTests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (!CheckIsLogin())
            {
                manager.Navigation.GoToLoginPage();
                driver.FindElement(By.Name("username")).SendKeys(account.Name);
                driver.FindElement(By.ClassName("width-40")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.Name("password")).SendKeys(account.Password);
                driver.FindElement(By.ClassName("width-40")).Click();
            }
        }

        public void Logout()
        {
            if (CheckIsLogin())
            {
                driver.FindElement(By.ClassName("user-info")).Click();
                var userMenu = driver.FindElement(By.ClassName("user-menu"));
                var menuLinks = userMenu.FindElements(By.CssSelector("li"));
                menuLinks[3].Click();
            }
        }

        public bool CheckIsLogin()
        {
            var isNavbarPresent = IsElementPresent(By.ClassName("navbar-brand"));
            return isNavbarPresent;
        }
    }
}
