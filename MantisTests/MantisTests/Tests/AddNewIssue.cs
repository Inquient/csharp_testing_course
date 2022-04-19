using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MantisTests
{
	[TestFixture]
	public class AddNewIssue : TestBase
	{
		[Test]
		public void AddNewIssueTest()
		{
			AccountData account = new AccountData()
			{
				Name = "administrator",
				Password = "root"
			};
			ProjectData project = new ProjectData()
			{
				Id = "6"
			};
			IssueData issueData = new IssueData()
			{
				Summary = "some short text",
				Description = "some long text",
				Category = "General"
			};

			app.Api.CreateNewIssue(account, project, issueData);
		}
	}
}
