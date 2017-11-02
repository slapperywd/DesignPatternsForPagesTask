using DesignPatternsForPagesTask.webdriver;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsForPagesTask.WebDriver
{
    public class Browser
    {
        private static Browser _currentInstane;
        private static IWebDriver _driver;
        public static BrowserFactory.BrowserType CurrentBrowser;
        public static int TimeoutForElement;
        private static string _browser;

        //Default constructor that uses ChromeDriver as default
        //Also sets 15 seconds timeout to wait for each command.
        public Browser()
        {
            InitParamas();
            _driver = BrowserFactory.GetDriver(CurrentBrowser, TimeoutForElement);
        }

        private static void InitParamas()
        {
            TimeoutForElement = Convert.ToInt32(Configuration.ElementTimeout);
            _browser = Configuration.Browser;
            Enum.TryParse(_browser, out CurrentBrowser);
        }

        //Singleton
        public static Browser Instance => _currentInstane ?? (_currentInstane = new Browser());

        public static void WindowMaximise()
        {
            _driver.Manage().Window.Maximize();
        }

        public static void NavigateTo(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public static IWebDriver GetDriver()
        {
            return _driver;
        }

        public static void Quit()
        {
            _driver.Quit();
            _currentInstane = null;
            _driver = null;
            _browser = null;
        }
    }
}
