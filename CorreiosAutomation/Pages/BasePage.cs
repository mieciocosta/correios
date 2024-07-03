using OpenQA.Selenium;

namespace CorreiosAutomation.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; }

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
            Driver.Manage().Window.Maximize();
        }

        public void Quit()
        {
            Driver.Quit();
        }
    }
}
