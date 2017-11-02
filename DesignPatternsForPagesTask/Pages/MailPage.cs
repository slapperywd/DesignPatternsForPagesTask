using DesignPatternsForPagesTask.WebDriver;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsForPagesTask.Pages
{
    public class MailPage
    {
        IWebDriver driver;
        WebDriverWait wait;

        [FindsBy(How = How.CssSelector, Using = "div.T-I-KE")]
        private IWebElement mailDialog;

        [FindsBy(How = How.ClassName, Using = "vO")]
        private IWebElement messageRecipientInput;

        [FindsBy(How = How.CssSelector, Using = "input[name='subjectbox']")]
        private IWebElement messageSubjectInput;

        [FindsBy(How = How.ClassName, Using = "LW-avf")]
        private IWebElement messageBodyInput;

        [FindsBy(How = How.CssSelector, Using = "img.Ha")]
        private IWebElement closeDialogBtn;

        [FindsBy(How = How.CssSelector, Using = "div.TK div.aim:nth-child(4) a")]
        private IWebElement draftsFolder;

        [FindsBy(How = How.CssSelector, Using = "div.TK div.aim:nth-child(3) a")]
        private IWebElement sentMailsFolder;

        [FindsBy(How = How.CssSelector, Using = "div.AO div.nH div.ae4.UI[gh='tl'] table.zt tr")]
        private IWebElement draftsMessagesList;

        [FindsBy(How = How.CssSelector, Using = "div.T-I.J-J5-Ji.aoO.T-I-atl.L3")]
        private IWebElement sendMailBtn;

        [FindsBy(How = How.CssSelector, Using = "a.gb_b.gb_gb.gb_R")]
        private IWebElement logoutDialog;

        [FindsBy(How = How.CssSelector, Using = "a#gb_71")]
        private IWebElement logoutBtn;

        [FindsBy(How = How.CssSelector, Using = "div.AO div.nH div.ae4.UI[gh='tl'] table.zt tr")]
        private IList<IWebElement> sentMails;

        public MailPage()
        {
            driver = Browser.GetDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Browser.TimeoutForElement));
            PageFactory.InitElements(driver, this);
        }

        public MailPage Open()
        {
            driver.Navigate().GoToUrl("https://mail.google.com/mail/#inbox");
            return this;
        }

        public void FillMessageAndSaveInDraftsFolder(string mesRecipient, string mesSubject, string mesBody)
        {
            //Open typing mail dialog
            wait.Until(ExpectedConditions.ElementToBeClickable(mailDialog)).Click();
            //Fill recipient input
            wait.Until(ExpectedConditions.ElementToBeClickable(messageRecipientInput)).SendKeys(mesRecipient);
            //Fill subject input
            wait.Until(ExpectedConditions.ElementToBeClickable(messageSubjectInput)).SendKeys(mesSubject);
            //Fill message body 
            wait.Until(ExpectedConditions.ElementToBeClickable(messageBodyInput)).SendKeys(mesBody);
            //Close dialog (your mail will be automatically saved in "Drafts" folder)
            wait.Until(ExpectedConditions.ElementToBeClickable(closeDialogBtn)).Click();

            //Go to "Drafts" folder
            wait.Until(ExpectedConditions.ElementToBeClickable(draftsFolder)).Click();

            //Assert, that the mail presents in ‘Drafts’ folder. 
            //and draft content (addressee, subject and body the same )
            wait.Until(ExpectedConditions.ElementToBeClickable(draftsMessagesList)).Click();

            //addresse
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("table.aoP.aoC input[name='subjectbox']")))
                .GetAttribute("value").Contains(mesSubject));
            // body 
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("table.aoP.aoC div.LW-avf")))
               .Text.Contains(mesBody));
            //to
            Assert.IsTrue(wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.oL.aDm.az9 span")))
              .Text.Contains(mesRecipient));
        }

        public void SendEmail()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(sendMailBtn)).Click();
        }

        public void LogOut()
        {
            //Open log out dialog
            wait.Until(ExpectedConditions.ElementToBeClickable(logoutDialog)).Click();
            //Log off.
            wait.Until(ExpectedConditions.ElementToBeClickable(logoutBtn)).Click();
        }

        public void CheckIfMailExistsAndSent(string mesRecipient, string mesSubject, string mesBody)
        {
            //Verify, that the mail disappeared from ‘Drafts’ folder.
            //Verify, that the mail is in ‘Sent’ folder.
            //Open "Sent mails" folder
            wait.Until(ExpectedConditions.ElementToBeClickable(sentMailsFolder)).Click();
            //this line is neccessary
            wait.Until(ExpectedConditions.TitleContains("Sent Mail"));
         
            bool isMailSend = false;
            foreach (var mail in sentMails)
            {
                string mailRecepient = mail.FindElement(By.CssSelector("td.yX.xY div.yW span.yP")).Text;
                string mailSubject = mail.FindElement(By.CssSelector("td.xY.a4W div.y6 span.bog")).Text;
                string mailBody = mail.FindElement(By.CssSelector("td.xY.a4W div.y6 span.y2")).Text.Remove(0, 3);

                if (mesRecipient.Contains(mailRecepient) && mesSubject.Contains(mailSubject) && mesBody.Contains(mailBody))
                {
                    isMailSend = true;
                    break;
                }
            }
            //Verify, that the mail is in ‘Sent’ folder.
            Assert.IsTrue(isMailSend);
        }
    }
}
