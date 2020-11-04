using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using MyStore.UITest.PageObjectModels;

namespace MyStore.UITest
{
    public class ShoopingWebAppShould
    {
        private const string HomeUrl = "http://automationpractice.com/index.php";
        private const string AboutUrl = "http://automationpractice.com/index.php?id_cms=4&controller=cms";
        private const string HomeTitle = "My Store";

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                DemoHelper.Pause();

                driver.Navigate().Refresh();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                IWebElement contactLink = homePage.ContactLink;

                string contactLinkTxt = contactLink.Text;

                DemoHelper.Pause();

                driver.Navigate().GoToUrl(AboutUrl);
                driver.Navigate().Back();

                string reloadedContactLink = homePage.ContactLink.Text;
                Assert.Equal(contactLinkTxt, reloadedContactLink);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();

                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();

                Assert.Equal(HomeTitle, driver.Title);
                Assert.Equal(HomeUrl, driver.Url);

                // TODO: assert that page was reloaded
            }
        }



    }
}
