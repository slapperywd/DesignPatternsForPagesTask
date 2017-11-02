using DesignPatternsForPagesTask.WebDriver;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsForPagesTask.Pages
{
    public class LoginPage
    {
        IWebDriver driver;
        WebDriverWait wait;

        [FindsBy(How = How.CssSelector, Using = "input[type='email']")]
        private IWebElement emailInput;

        [FindsBy(How = How.CssSelector, Using = "input[name='password']")]
        private IWebElement passwordInput;

        [FindsBy(How = How.Id, Using = "identifierNext")]
        private IWebElement submitEmailBtn;

        [FindsBy(How = How.Id, Using = "passwordNext")]
        private IWebElement submitPassowordBtn;

        public LoginPage()
        {
            driver = Browser.GetDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Browser.TimeoutForElement));
            PageFactory.InitElements(driver, this);
        }
        
        public LoginPage Open()
        {
            driver.Navigate().GoToUrl("https://www.google.com/gmail/");
            return this;
        }

        //Login to the mail box
        public void Login(string logName, string password)
        {
            //Find email input and enter login
            // driver.FindElement(By.CssSelector("input[type='email']")).SendKeys(logName);
            emailInput.SendKeys(logName);
            wait.Until(ExpectedConditions.ElementToBeClickable(submitEmailBtn)).Click();
            //Find password input and enter password
            wait.Until(ExpectedConditions.ElementToBeClickable(passwordInput)).SendKeys(password);
            //Submit entered credentials
            wait.Until(ExpectedConditions.ElementToBeClickable(submitPassowordBtn)).Click();
 
            //Assert, that the login is successful
            Assert.IsTrue(wait.Until(ExpectedConditions.TitleContains("Inbox")));
        }
    }
}
