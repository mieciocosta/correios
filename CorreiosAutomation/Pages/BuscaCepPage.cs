using OpenQA.Selenium;

namespace CorreiosAutomation.Pages
{
    public class BuscaCepPage : BasePage
    {
        public BuscaCepPage(IWebDriver driver) : base(driver) { }

        public void SearchCep(string cep)
        {
            var searchBox = Driver.FindElement(By.Name("relaxation"));
            searchBox.Clear();
            searchBox.SendKeys(cep);
            Driver.FindElement(By.CssSelector(".btn2")).Click();
        }

        public string GetResultText()
        {
            return Driver.FindElement(By.CssSelector("body > div.back > div.tabs > div:nth-child(2) > div > div > div.column2 > div.content > div.ctrlcontent > table > tbody > tr:nth-child(2) > td:nth-child(1)")).Text;
        }

        public string GetErrorText()
        {
            return Driver.FindElement(By.CssSelector("body > div.back > div.tabs > div:nth-child(2) > div > div > div.column2 > div.content > div.ctrlcontent > p")).Text;
        }
    }
}
