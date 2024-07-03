using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using CorreiosAutomation.Pages;
using CorreiosAutomation.Config;

namespace CorreiosAutomation.StepDefinitions
{
    [Binding]
    public class RastreioSteps
    {
        private static IWebDriver driver;
        private RastreioPage rastreioPage;

        [BeforeFeature]
        public static void BeforeFeature()
        {
            driver = new ChromeDriver();
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            if (driver != null)
            {
                driver.Quit();
                TestContext.WriteLine("Fechou o navegador");
            }
        }

        [Given(@"que estou na página inicial de rastreamento")]
        public void GivenQueEstouNaPaginaInicialDeRastreamento()
        {
            rastreioPage = new RastreioPage(driver);
            rastreioPage.NavigateTo(Urls.RastreioUrl);
            TestContext.WriteLine("Navegou para a página inicial de rastreamento dos correios");
        }

        [When(@"procuro pelo código de rastreamento ""(.*)""")]
        public void WhenProcuroPeloCodigoDeRastreamento(string codigo)
        {
            rastreioPage.SearchTrackingCode(codigo);
            TestContext.WriteLine($"Procurou pelo código de rastreamento: {codigo}");
        }

        [Then(@"o resultado do rastreamento deve ser ""(.*)""")]
        public void ThenOResultadoDoRastreamentoDeveSer(string resultado)
        {
            var resultText = rastreioPage.GetResultText();
            TestContext.WriteLine($"Resultado do rastreamento: {resultText}");
            Assert.IsTrue(resultText.Contains(resultado), $"Expected result to contain '{resultado}' but was '{resultText}'");
        }

        [Then(@"realizo verificações utilizando diferentes métodos de localização para rastreamento")]
        public void ThenRealizoVerificacoesUtilizandoDiferentesMetodosDeLocalizacaoParaRastreamento()
        {
            // Verificação por ID
            var searchBoxById = driver.FindElement(By.Id("id"));
            Assert.IsNotNull(searchBoxById, "Elemento não encontrado por ID");

            // Verificação por XPath
            var searchBoxByXPath = driver.FindElement(By.XPath("//header[@class='header menu_fixed ']"));
            Assert.IsNotNull(searchBoxByXPath, "Elemento não encontrado por XPath");

            // Verificação por CSS Selector
            var searchButtonByCss = driver.FindElement(By.CssSelector(".col-lg-2 input[type='submit']"));
            Assert.IsNotNull(searchButtonByCss, "Elemento não encontrado por CSS Selector");
        }
    }
}
