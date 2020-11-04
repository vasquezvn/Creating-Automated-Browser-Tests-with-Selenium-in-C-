using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.UITest.PageObjectModels
{
    class ContactUsPage : Page
    {
        protected override string PageUrl => "http://automationpractice.com/index.php?controller=contact";
        protected override string PageTitle => "Contact us - My Store";

        public void EnterEmail() => Driver.FindElement(By.Id("email")).SendKeys("test@test.com");
        public void EnterOrderReference() => Driver.FindElement(By.Id("id_order")).SendKeys("Order Reference test");
        public void EnterMessage() => Driver.FindElement(By.Id("message")).SendKeys("testMessage");
        public void ClickSendButton() => Driver.FindElement(By.Id("submitMessage")).Click();

        public void ChooseSubjectHeading()
        {
            SelectElement SubjectHeadingSource = new SelectElement(Driver.FindElement(By.Id("id_contact")));
            SubjectHeadingSource.SelectByText("Customer service");
        }

        public ContactUsPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public IReadOnlyCollection<string> ValidationErrorMessages
        {
            get
            {
                return Driver.FindElements(By.CssSelector(".alert.alert-danger > ol > li"))
                             .Select(x => x.Text)
                             .ToList()
                             .AsReadOnly();
            }
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

    }
}
