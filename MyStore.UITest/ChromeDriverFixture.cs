using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MyStore.UITest
{
    public sealed class ChromeDriverFixture
    {
        public IWebDriver Driver { get; private set; }

        public ChromeDriverFixture()
        {
            Driver = new ChromeDriver();
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
