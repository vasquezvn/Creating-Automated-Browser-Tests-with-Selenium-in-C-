using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.UITest.PageObjectModels
{
    class Page
    {
        protected IWebDriver Driver;
        protected virtual string PageUrl { get; }
        protected virtual string PageTitle { get; }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
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
    }
}
