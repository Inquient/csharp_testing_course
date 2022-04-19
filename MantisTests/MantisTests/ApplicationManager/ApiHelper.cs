using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceReference;

namespace MantisTests
{
    public class ApiHelper : HelperBase
    {
        public ApiHelper(ApplicationManager manager) : base(manager)
        {
        }


        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
	        MantisConnectPortTypeClient client = new MantisConnectPortTypeClient();
            ServiceReference.IssueData issue = new ServiceReference.IssueData();
	        issue.summary = issueData.Summary;
	        issue.description = issueData.Description;
	        issue.category = issueData.Category;
	        issue.project = new ServiceReference.ObjectRef();
	        issue.project.id = project.Id;
	        client.mc_issue_addAsync(account.Name, account.Password, issue);
        }
    }
}
