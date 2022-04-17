using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MantisTests
{
    [TestFixture]
    public class ProjectCreationTests : AuthBase
    {
        [Test]
        public void CreateProjectTest()
        {
            ProjectData project = new ProjectData()
            {
                Name = GenerateRandomString(12),
                Description = "Good project to start"
            };

            List<ProjectData> projects = app.Projects.GetProjectsList();
            
            app.Projects.CreateProject(project);

            List<ProjectData> newProjects = app.Projects.GetProjectsList();
            projects.Add(project);
            projects.Sort();
            newProjects.Sort();
            Assert.AreEqual(projects, newProjects);
        }

        [Test]
        public void CreateProjectThatAlreadyExistsTest()
        {
            List<ProjectData> projects = app.Projects.GetProjectsList();
            var existingProject = projects[0];

            app.Projects.CreateProject(existingProject);

            List<ProjectData> newProjects = app.Projects.GetProjectsList();
            projects.Sort();
            newProjects.Sort();
            Assert.AreEqual(projects, newProjects);
        }
    }
}
