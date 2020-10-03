using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;


namespace mantis_tests
{
    public class ProjectManagementHelper : HelperBase
    {
        public By Project_MH = By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr/td/a");

        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ProjectManagementHelper Add(ProjectData projectData)
        {
            ProjectCreation();
            FillField(projectData);
            SumbitNewProject();
            return this;
        }

        public ProjectManagementHelper RemoveProject()
        {
            SelectProject();
            Removing();
            ConfirmRemoving();
            return this;
        }


        public ProjectManagementHelper ConfirmRemoving()
        {
            driver.FindElement(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/form/input[4]")).Click();
            ProjectCach = null;
            return this;
        }



        public ProjectManagementHelper Removing()
        {
            driver.FindElement(By.XPath("//form[@id='project-delete-form']/fieldset/input[3]")).Click();
            return this;
        }

        public ProjectManagementHelper SelectProject()
        {
            driver.FindElement(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr/td/a")).Click();
            return this;
        }

        public ProjectManagementHelper SumbitNewProject()
        {
            driver.FindElement(By.XPath("//input[@value='Add Project']")).Click();
            ProjectCach = null;
            return this;
        }

        public ProjectManagementHelper FillField(ProjectData projectData)
        {
            Type(By.Name("name"), projectData.Pjdata_name);
            Type(By.Name("description"), projectData.Pjdata_desc);
            return this;
        }

        public ProjectManagementHelper ProjectCreation()
        {
            driver.FindElement(By.CssSelector("button.btn.btn-primary.btn-white.btn-round")).Click();
            return this;
        }


        private List<ProjectData> ProjectCach = null;

        internal List<ProjectData> GetProjectsList()
        {
            if (ProjectCach == null)
            {
                ProjectCach = new List<ProjectData>();
                manager.Navigator.OpenFromMenu();
                manager.Navigator.OpenProjectsMenu();

                IWebElement NTable = driver.FindElement(By.CssSelector("tbody"));
                ICollection<IWebElement> elements = NTable.FindElements(By.TagName("tr"));
                
                foreach (IWebElement element in elements)
                {
                    IList<IWebElement> cells = element.FindElements(By.TagName("td"));
                    ProjectCach.Add(new ProjectData(cells[0].Text, cells[4].Text));
                }
            }

            return new List<ProjectData>(ProjectCach);
        }
    }
}