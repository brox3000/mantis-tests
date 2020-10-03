﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;


namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationForm();
            FillRegistrationForm(account);
            SubmitRegistration();
            // mail
            String url = GetConfirmationUrl(account);
            FillPasswordForm(url, account);
            SubmitPasswordForm();
        }

        public void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-2.24.2/login_page.php";
        }

        public void OpenRegistrationForm()
        {
            //driver.FindElement(By.ClassName("back-to-login-link pull-left")).Click();
            driver.FindElement(By.LinkText("Signup for a new account")).Click();
        }

        public void FillRegistrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        public void SubmitRegistration()
        {
            driver.FindElement(By.CssSelector("input.width-40.pull-right.btn.btn-success.btn-inverse.bigger-110")).Click();
            //driver.FindElement(By.CssSelector("input.button")).Click();
        }

        private string GetConfirmationUrl(AccountData account)
        {
            String message = manager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }

        private void FillPasswordForm(string url, AccountData account) // Адрес для подтверждения регистрации: Ввод пароля
        {
            driver.Url = url;
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("pasword_confirm")).SendKeys(account.Password);
        }

        private void SubmitPasswordForm() 
        {
            driver.FindElement(By.CssSelector("input.button")).Click();
        }
    }
}
