using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace DesignPatternsForPagesTask.WebDriver
{
    public class BaseTest
    {
        protected static Browser Browser = Browser.Instance;

        [SetUp]
        public virtual void SetUp()
        {
            Browser = Browser.Instance;
            Browser.WindowMaximise();
            //Browser.NavigateTo(Configuration.StartUrl);
        }

        [TearDown]
        public virtual void TearDown()
        {
            Console.Out.WriteLine("Test has been finished");
            //Browser.Quit();
        }
    }
}
