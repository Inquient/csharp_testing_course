using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantisTests
{
    public class MenuNavigationHelper : HelperBase
    {
        public static string ManagementOverviewPage;
        public static string ProjectManagementTab;
        public static string ProjectCreationPage;

        public static string LoginPage;

        private string BaseUrl;

        public MenuNavigationHelper(ApplicationManager manager, string baseUrl) : base(manager)
        {
            this.BaseUrl = baseUrl;
            ManagementOverviewPage = baseUrl + "/manage_overview_page.php";
            ProjectManagementTab = baseUrl + "/manage_proj_page.php";
            ProjectCreationPage = baseUrl + "/manage_proj_create_page.php";

            LoginPage = baseUrl + "/login_page.php";
        }

        public void GoToManagementOverviewPage()
        {
            driver.Navigate().GoToUrl(ManagementOverviewPage);
        }

        public void GoToProjectManagementTab()
        {
            driver.Navigate().GoToUrl(ProjectManagementTab);
        }

        public void GoToProjectCreationPage()
        {
            driver.Navigate().GoToUrl(ProjectCreationPage);
        }

        public void GoToLoginPage()
        {
            driver.Navigate().GoToUrl(LoginPage);
        }
    }
}
