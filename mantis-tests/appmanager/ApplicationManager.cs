using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;


namespace mantis_tests
{

    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        public RegistrationHelper Registration { get; set; }
        public FtpHelper Ftp { get; set; }
        public JamesHelper James { get; set; }
        public MailHelper Mail { get; set; }
        public AdminHelper Admin { get; private set; }
        public APIHelper API { get; set; }

        protected LoginHelper loginHelper;
        protected ManagementMenuHelper managementMenuHelper;
        protected ProjectManagementHelper projectManagementHelper;
        protected APIHelper apiHelper;



        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
            baseURL = "http://localhost/mantisbt-2.24.2";

            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);
            Admin = new AdminHelper(this, baseURL);
            API = new APIHelper(this);

            // 9.0
            loginHelper = new LoginHelper(this);
            managementMenuHelper = new ManagementMenuHelper(this);
            projectManagementHelper = new ProjectManagementHelper(this);
            apiHelper = new APIHelper(this);
        }

        ~ApplicationManager()  // Диструктор // Stop Test
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {

            }
        }

        public static ApplicationManager GetInstance()
        {

            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = newInstance.baseURL + "/login_page.php";
                app.Value = newInstance;
            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get { return driver; }

        }

        public LoginHelper Auth
        {
            get { return loginHelper; }
        }

        public ManagementMenuHelper Navigator
        {
            get { return managementMenuHelper; }
        }

        public ProjectManagementHelper prMH
        {
            get { return projectManagementHelper; }

        }

        public APIHelper apiH
        {
            get { return apiHelper; }

        }

    }
}
