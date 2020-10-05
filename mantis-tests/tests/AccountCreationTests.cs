﻿using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using System.IO;

namespace mantis_tests
{
    [TestFixture]

    public class AccountCreationTests : TestBase
    {
        [TestFixtureSetUp]
        public void setupXonfig()
        {
            app.Ftp.BackupFile("/config_inc.php");

            using (Stream localFile = File.Open(TestContext.CurrentContext.TestDirectory + "\\config_inc.php", FileMode.Open))
            //using (Stream localFile = File.Open("/config_inc.php", FileMode.Open))

            {
                app.Ftp.Upload("/config_inc.php", localFile);
            }
        }
        
        [Test]

        public void TestAccountRegistration()
        {
            AccountData account = new AccountData("user8", "password")
            {
                Email = "user9@localhost.localdomainone"
            };

            List<AccountData> accounts = app.Admin.GetAllaccounts();
            AccountData existingAccount = accounts.Find(x => x.Name == account.Name);

            if (existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }
            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);
        }

        [TestFixtureTearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config_inc.php");
        }
    }
}