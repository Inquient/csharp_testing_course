using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MantisTests
{
    public class MenuNavigationHelper : HelperBase
    {
        public static string ManagementOverviewPage = "http://localhost/mantisbt-2.25.3/manage_overview_page.php";
        public static string ProjectManagementTab = "http://localhost/mantisbt-2.25.3/manage_proj_page.php";
        public static string ProjectCreationPage = "http://localhost/mantisbt-2.25.3/manage_proj_create_page.php";

        public static string LoginPage = "http://localhost/mantisbt-2.25.3/login_page.php";

        public MenuNavigationHelper(ApplicationManager manager) : base(manager)
        {
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
