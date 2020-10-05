using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ManagementMenuHelper : HelperBase
    {

        public ManagementMenuHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void OpenFromMenu()
        {
            //*10.0
            //manager.Driver.Url = "http://localhost/mantisbt-2.24.2/manage_proj_page.php";
            driver.FindElement(By.CssSelector("i.menu-icon.fa.fa-gears")).Click();
        }

        public void OpenProjectsMenu()
        {
            driver.FindElement(By.LinkText("Manage Projects")).Click();
        }
    }
}
