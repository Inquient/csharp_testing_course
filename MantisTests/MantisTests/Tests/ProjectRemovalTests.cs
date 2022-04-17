using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MantisTests
{
    [TestFixture]
    public class ProjectRemovalTests : AuthBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            app.Projects.CreateProjectIfDoesNotExists();

            List<ProjectData> projects = app.Projects.GetProjectsList();
            ProjectData projectToRemove = projects[0];

            app.Projects.RemoveProject(projectToRemove);

            List<ProjectData> newProjects = app.Projects.GetProjectsList();
            projects.Remove(projectToRemove);
            projects.Sort();
            newProjects.Sort();
            Assert.AreEqual(projects, newProjects);
        }
    }
}
