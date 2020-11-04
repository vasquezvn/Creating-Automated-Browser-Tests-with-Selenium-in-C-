using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.UITest.PageObjectModels
{
    class AviancaPage : Page
    {
        protected override string PageUrl => "https://www.avianca.com/otr/en/";
        protected override string PageTitle => "Avianca.com international, flights at the best price | Avianca";

        public void ClickCheckOutLink() => Driver.FindElement(By.CssSelector("[aria-label='Check it out here']")).Click();
        

        public AviancaPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            Driver.Manage().Window.Maximize();
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded(bool onlyCheckUrlStartWithExpectedText = true)
        {
            bool urlIsCorrect;

            if (onlyCheckUrlStartWithExpectedText)
            {
                urlIsCorrect = Driver.Url.StartsWith(PageUrl);
            }
            else
            {
                urlIsCorrect = Driver.Url == PageUrl;
            }


            bool pageHasLoaded = urlIsCorrect && (Driver.Title == PageTitle);

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load Page. Page URL = {Driver.Url} Page Source = \r\n {Driver.PageSource}");
            }
        }

        public LifeMilesPage LifeMilesClick()
        {
            Driver.FindElement(By.LinkText("LifeMiles")).Click();
            
            return new LifeMilesPage(Driver);
        }

        internal void WaitForCarouselPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(21));
            IWebElement contactUsLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Find out more")));
        }

        internal FindOutMorePage ClickFindOutMmoreLink()
        {
            Driver.FindElement(By.LinkText("Find out more")).Click();

            return new FindOutMorePage(Driver);
        }
    }
}
