using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MantisTests
{
    [TestFixture]
    public class ProjectManagementWithApiTests : TestBase
    {
        public AccountData admin;

        [SetUp]
        public void SetUpAdmin()
        {
            admin = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };
        }

        [Test]
        public async Task CreateProjectWithMantisApiTest()
        {
            ProjectData project = new ProjectData()
            {
                Name = GenerateRandomString(12),
                Description = "Project with api"
            };

            List<ProjectData> projects = await app.Projects.GetProjectsListWithApi(admin);

            await app.Projects.CreateProjectWithApi(admin, project);

            List<ProjectData> newProjects = await app.Projects.GetProjectsListWithApi(admin);
            projects.Add(project);
            projects.Sort();
            newProjects.Sort();
            Assert.AreEqual(projects, newProjects);
        }

        [Test]
        public async Task ProjectRemovalWithApiTest()
        {
            await app.Projects.CreateProjectIfDoesNotExistsWithApi(admin);

            List<ProjectData> projects = await app.Projects.GetProjectsListWithApi(admin);
            ProjectData projectToRemove = projects[0];

            await app.Projects.RemoveProjectWithApi(admin, projectToRemove);

            List<ProjectData> newProjects = await app.Projects.GetProjectsListWithApi(admin);
            projects.Remove(projectToRemove);
            projects.Sort();
            newProjects.Sort();
            Assert.AreEqual(projects, newProjects);
        }
    }
}
