using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.UITest.PageObjectModels
{
    class LifeMilesPage : Page
    {
        protected override string PageUrl => "https://www.avianca.com/otr/en/experience/lifemiles-program/";
        protected override string PageTitle => "LifeMiles frequent flyer program | Avianca";

        public LifeMilesPage(IWebDriver driver)
        {
            Driver = driver;
        }


    }
}
