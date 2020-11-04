using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace MyStore.UITest
{
    public class JavaScriptsExamples
    {
        [Fact]
        public void ClickOverLayedLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("");
                DemoHelper.Pause();

                string script = "document.getElementById('HiddenLink').click();";

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                js.ExecuteScript(script);

                //driver.FindElement(By.Id("")).Click();

                Assert.Equal("", driver.Title);
            }
        }

        [Fact]
        public void GetOverlayedLinkText()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("");
                DemoHelper.Pause();

                string script = "return document.getElementById('HiddenLink').innerHTML;";

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                string linkText = (string)js.ExecuteScript(script);

                Assert.Equal("Go to Pluralsigh", linkText);

            }
        }

        [Fact]
        public void AdvancedInteractions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("");
                IWebElement contactLink = driver.FindElement(By.Id("contact-link"));

                Actions actions = new Actions(driver);
                actions.MoveToElement(contactLink);
                actions.Click();
                actions.Perform();


            }
        }
    }
}
