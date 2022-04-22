using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using MantisWebService;

namespace MantisTests
{
    public class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void CreateProjectIfDoesNotExists()
        {
            if (GetProjectsCount() == 0)
            {
                ProjectData project = new ProjectData()
                {
                    Name = "ProjectToRemove",
                    Description = "Some description"
                };

                CreateProject(project);
            }
        }

        public List<ProjectData> GetProjectsList()
        {
            manager.Navigation.GoToProjectManagementTab();
            List<ProjectData> projects = new List<ProjectData>();

            var rows = GetProjectsTable();
            foreach (var row in rows)
            {
                var cells = row.FindElements(By.CssSelector("td"));
                projects.Add(new ProjectData()
                {
                    Name = cells[0].Text,
                    Description = cells[4].Text
                });
            }

            return projects;
        }

        public int GetProjectsCount()
        {
            manager.Navigation.GoToProjectManagementTab();
            var rows = GetProjectsTable();

            return rows.Count;
        }

        public IReadOnlyCollection<IWebElement> GetProjectsTable()
        {
            var tables = driver.FindElements(By.ClassName("table"));
            var projectsTable = tables[0].FindElement(By.XPath($"//*/tbody"));
            var rows = projectsTable.FindElements(By.CssSelector("tr"));
            return rows;
        }

        public void CreateProject(ProjectData project)
        {
            manager.Navigation.GoToProjectCreationPage();
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
            driver.FindElement(By.Id("project-description")).SendKeys(project.Description);
            driver.FindElement(By.XPath("//input[@value='Добавить проект']")).Click();
        }

        public void SelectProjectByName(ProjectData project)
        {
            manager.Navigation.GoToProjectManagementTab();
            var projectEditUrls = driver.FindElements(By.XPath($"(//a[text()='{project.Name}'])"));
            driver.Navigate().GoToUrl(projectEditUrls[1].GetAttribute("href"));
        }

        public void RemoveProject(ProjectData project)
        {
            SelectProjectByName(project);
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@value='Удалить проект']")).Click();
        }

        public async Task<List<ProjectData>> GetProjectsListWithApi(AccountData account)
        {
            List<ProjectData> projectDatas = new List<ProjectData>();
            MantisConnectPortTypeClient client = new MantisConnectPortTypeClient();

            var projects = await client.mc_projects_get_user_accessibleAsync(account.Name, account.Password);
            foreach (var project in projects)
            {
                projectDatas.Add(new ProjectData()
                {
                    Name = project.name,
                    Description = project.description,
                    Id = project.id
                });
            }

            return projectDatas;
        }

        public async Task<int> GetProjectsCountWithApi(AccountData account)
        {
            MantisConnectPortTypeClient client = new MantisConnectPortTypeClient();
            var projects = await client.mc_projects_get_user_accessibleAsync(account.Name, account.Password);

            return projects.Length;
        }

        public async Task CreateProjectIfDoesNotExistsWithApi(AccountData account)
        {
            var projectsCount = await GetProjectsCountWithApi(account);
            if (projectsCount == 0)
            {
                ProjectData project = new ProjectData()
                {
                    Name = "ProjectToRemove",
                    Description = "Some description"
                };

                await CreateProjectWithApi(account, project);
            }
        }

        public async Task CreateProjectWithApi(AccountData account, ProjectData project)
        {
            MantisConnectPortTypeClient client = new MantisConnectPortTypeClient();

            MantisWebService.ProjectData projectToCreate = new MantisWebService.ProjectData();
            projectToCreate.name = project.Name;
            projectToCreate.description = project.Description;

            await client.mc_project_addAsync(account.Name, account.Password, projectToCreate);
        }

        public async Task RemoveProjectWithApi(AccountData account, ProjectData project)
        {
            MantisConnectPortTypeClient client = new MantisConnectPortTypeClient();

            await client.mc_project_deleteAsync(account.Name, account.Password, project.Id);
        }
    }
}
