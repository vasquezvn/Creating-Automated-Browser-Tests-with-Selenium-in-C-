using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.UITest.PageObjectModels
{
    class FindOutMorePage: Page
    {
        protected override string PageUrl => "https://www.avianca.com/otr/en/experience/avianca-biocare/";
        protected override string PageTitle => "Biosecurity measures for you to fly with confidence";

        public FindOutMorePage(IWebDriver driver)
        {
            Driver = driver;
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
