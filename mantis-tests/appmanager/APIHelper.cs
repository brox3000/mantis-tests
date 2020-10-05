using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;


namespace mantis_tests
{
    //10.0 6:59
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

          public void CreateNewProjectNew(AccountData account, ProjectData project)
        //public void CreateNewIssue(AccountData account, ProjectDataIssue project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            //Mantis.IssueData issue = new Mantis.IssueData();
            //issue.summary = issueData.Summary;
            //issue.description = issueData.Description;
            //issue.category = issueData.Category;
            //issue.project = new Mantis.ObjectRef();
            //issue.project.id = project.Id;
            //client.mc_issue_add(account.Name, account.Password, issue);
            Mantis.ProjectData project_new = new Mantis.ProjectData();
            project_new.name = project.Pjdata_name;
            project_new.description = project.Pjdata_desc;
            client.mc_project_add(account.Name, account.Password, project_new);
        }
    }
}
