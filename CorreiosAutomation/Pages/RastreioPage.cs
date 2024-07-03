using OpenQA.Selenium;

namespace CorreiosAutomation.Pages
{
    public class RastreioPage : BasePage
    {
        public RastreioPage(IWebDriver driver) : base(driver) { }

        public void SearchTrackingCode(string code)
        {
            var trackingBox = Driver.FindElement(By.Id("id"));
            trackingBox.Clear();
            trackingBox.SendKeys(code);
            Driver.FindElement(By.CssSelector(".col-lg-2 > [type='submit']")).Click();
        }

        public string GetResultText()
        {
            return Driver.FindElement(By.CssSelector("div.col-lg-8 > p")).Text;
        }
    }
}
