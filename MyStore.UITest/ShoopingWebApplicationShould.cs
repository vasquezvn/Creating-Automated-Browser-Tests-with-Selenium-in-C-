using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit.Abstractions;
using System.Collections.ObjectModel;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using System.IO;
using ApprovalTests;
using MyStore.UITest.PageObjectModels;

namespace MyStore.UITest
{
    [Trait("Category", "Applications")]
    public class ShoopingWebApplicationShould : IClassFixture<ChromeDriverFixture>
    {
        private const string HomeUrl = "https://www.avianca.com/otr/en/";
        private const string FlyWorrieFreeAgainUrl = "https://www.avianca.com/otr/en/experience/avianca-biocare/";

        private const string DemoQAAlerts = "https://demoqa.com/alerts";

        public readonly ChromeDriverFixture ChromeDriverFixture;

        public ShoopingWebApplicationShould(ChromeDriverFixture chromeDriverFixture)
        {
            ChromeDriverFixture = chromeDriverFixture;
            ChromeDriverFixture.Driver.Manage().Cookies.DeleteAllCookies();
            ChromeDriverFixture.Driver.Navigate().GoToUrl("about:blank");
        }

        [Fact]
        public void BeInitiatedFromHomePage_LifeMiles()
        {
            var homePage = new AviancaPage(ChromeDriverFixture.Driver);
            homePage.NavigateTo();

            var lifeMilesPage = homePage.LifeMilesClick();

            DemoHelper.Pause();

            lifeMilesPage.EnsurePageLoaded();
        }


        [Fact]
        public void BeInitiatedFromHomePage_WaitCarouselPage()
        {
            var aviancaPage = new AviancaPage(ChromeDriverFixture.Driver);
            aviancaPage.NavigateTo();

            aviancaPage.WaitForCarouselPage();

            FindOutMorePage findOutMorePage = aviancaPage.ClickFindOutMmoreLink();

            findOutMorePage.EnsurePageLoaded();
        }


        [Fact]
        public void BeInitiatedFromHomePage_GoToAviancaExperience()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to {HomeUrl}");
                driver.Navigate().GoToUrl(HomeUrl);
                driver.Manage().Window.Maximize();


                //IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                //carouselNext.Click();
                //DemoHelper.Pause(1000);

                //carouselNext.Click();
                //DemoHelper.Pause(1000);

                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element using explicit wait");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));


                //Func<IWebDriver, IWebElement> findEnabledAndVisible = delegate (IWebDriver d)
                //{
                //    var e = d.FindElement(By.CssSelector("[aria-label='Go to Avianca Experiences']"));

                //    if (e is null)
                //    {
                //        throw new NotFoundException();
                //    }

                //    if (e.Enabled && e.Displayed)
                //    {
                //        return e;
                //    }

                //    throw new NotFoundException();
                //};

                IWebElement iWantToJoin = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[aria-label='Go to Avianca Experiences']")));

                //IWebElement iWantToJoin = wait.Until(findEnabledAndVisible);

                //IWebElement iWantToJoin = wait.Until(d => d.FindElement(By.CssSelector("[aria-label='Go to Avianca Experiences']")));

                //driver.FindElement(By.CssSelector("[aria-label='Go to Avianca Experiences']"));

                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element Displayed={iWantToJoin.Displayed} Enabled={iWantToJoin.Enabled}");

                //output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                iWantToJoin.Click();

                DemoHelper.Pause(1000);

                Assert.Equal("Enjoy Avianca at home : Concerts, cooking and fun for the whole family", driver.Title);
                //Assert.Equal(FlyWorrieFreeAgainUrl, driver.Url);

            }
        }


        [Fact]
        public void BeSubmittedWhenValid()
        {
            ContactUsPage contactUsPage = new ContactUsPage(ChromeDriverFixture.Driver);
            contactUsPage.NavigateTo();

            contactUsPage.ChooseSubjectHeading();
            contactUsPage.EnterEmail();
            contactUsPage.EnterOrderReference();
            contactUsPage.EnterMessage();
            contactUsPage.ClickSendButton();

        }


        [Fact]
        public void BeSubmittedWhenValidErrorsCorrected()
        {
            var contactUsPage = new ContactUsPage(ChromeDriverFixture.Driver);
            contactUsPage.NavigateTo();

            contactUsPage.ChooseSubjectHeading();
            contactUsPage.EnterOrderReference();
            contactUsPage.EnterMessage();
            contactUsPage.ClickSendButton();

            // ASSERT THAT VALIDATION FAILED
            Assert.Equal(1, contactUsPage.ValidationErrorMessages.Count);
            Assert.Contains("Invalid email address.", contactUsPage.ValidationErrorMessages);

            //Fix Errors
            contactUsPage.EnterEmail();

            // Resubmit form
            contactUsPage.ClickSendButton();
        }


        [Fact]
        public void OpenBookingStatusLinkInNewTab()
        {
            var aviancaPage = new AviancaPage(ChromeDriverFixture.Driver);
            aviancaPage.NavigateTo();

            aviancaPage.ClickCheckOutLink();

            // NOS PERMITE OBTENER TODOS LAS PESTAÑAS
            ReadOnlyCollection<string> allTabs = ChromeDriverFixture.Driver.WindowHandles;
            string homeTab = allTabs[0];
            string bookingStateTab = allTabs[1];

            ChromeDriverFixture.Driver.SwitchTo().Window(bookingStateTab);
            DemoHelper.Pause();

            Assert.Equal("Estado Boleto", ChromeDriverFixture.Driver.Title);
        }

        [Fact]
        public void AlertButtonClick()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(DemoQAAlerts);
                driver.Manage().Window.Maximize();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                driver.FindElement(By.Id("alertButton")).Click();

                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

                Assert.Equal("You clicked a button", alert.Text);

                alert.Accept();

                DemoHelper.Pause();
            }
        }

        [Fact]
        public void ConfirmationButtonClick()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(DemoQAAlerts);
                driver.Manage().Window.Maximize();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                driver.FindElement(By.Id("confirmButton")).Click();

                IAlert alertBox = wait.Until(ExpectedConditions.AlertIsPresent());

                Assert.Equal("Do you confirm action?", alertBox.Text);

                alertBox.Dismiss();

                DemoHelper.Pause();
            }
        }

        [Fact]
        public void ValidateCookies()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                driver.Manage().Window.Maximize();

                var AviancaCookie = driver.Manage().Cookies.AllCookies;

                if (AviancaCookie.Count != 0)
                {
                    driver.Manage().Cookies.AddCookie(new Cookie("AviancaCookie", "true"));
                }

                Cookie CookieValue = driver.Manage().Cookies.GetCookieNamed("AviancaCookie");

                Assert.Equal("true", CookieValue.Value);
            }
        }

        [Fact]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void TakeScreenshotTest()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                driver.Manage().Window.Maximize();

                ITakesScreenshot screenShotDriver = (ITakesScreenshot)driver;

                Screenshot screenshot = screenShotDriver.GetScreenshot();

                screenshot.SaveAsFile("capture.bmp", ScreenshotImageFormat.Bmp);

                FileInfo file = new FileInfo("capture.bmp");

                Approvals.Verify(file);
            }
        }
    }
}
