using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyStore.UITest.PageObjectModels
{
    class HomePage : Page
    {
        protected override string PageUrl => "http://automationpractice.com/index.php";
        protected override string PageTitle => "My Store";

        public IWebElement ContactLink => Driver.FindElement(By.Id("contact-link"));
        

        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            Driver.Manage().Window.Maximize();
            EnsurePageLoaded();
        }

        public ReadOnlyCollection<(string name, string interestRate)> Products 
        { 
            get
            {
                var products = new List<(string name, string interestRate)>();

                var productCells = Driver.FindElements(By.TagName("td"));

                for (int i = 0; i < productCells.Count; i += 2)
                {
                    string name = productCells[i].Text;
                    string interestRate = productCells[i + 1].Text;
                    products.Add((name, interestRate));
                }

                return products.AsReadOnly();
            }
        
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


    }
}
