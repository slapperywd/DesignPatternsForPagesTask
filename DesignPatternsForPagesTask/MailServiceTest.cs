using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using DesignPatternsForPagesTask;
using DesignPatternsForPagesTask.webdriver;
using DesignPatternsForPagesTask.WebDriver;
using DesignPatternsForPagesTask.Pages;
using System.Configuration;

namespace DesignPatternsForPagesTask
{
    [TestFixture]
    public class MailServiceTest:BaseTest
    {
        string mailLogin = ConfigurationManager.AppSettings["logname"];
        string mailPassword = ConfigurationManager.AppSettings["password"];

        //message will be sent to an e-mail account specified in the following line
        string messageRecipient = "maglish@mail.ru";
        string messageSubject = "Test subject";
        string messageBody = "Body body body test body test body bla bla ";

        [Test]
        public void Login()
        {
            LoginPage lp = new LoginPage().Open();
            lp.Login(mailLogin, mailPassword);
            MailPage mp = new MailPage();
            mp.FillMessageAndSaveInDraftsFolder(messageRecipient, messageSubject, messageBody);
            mp.SendEmail();
            mp.CheckIfMailExistsAndSent(messageRecipient, messageSubject, messageBody);
            mp.LogOut();
        }

    }
}
