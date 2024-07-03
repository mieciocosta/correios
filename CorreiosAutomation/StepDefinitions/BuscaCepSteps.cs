using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using CorreiosAutomation.Pages;
using CorreiosAutomation.Config;

namespace CorreiosAutomation.StepDefinitions
{
    [Binding]
    public class BuscaCepSteps
    {
        private static IWebDriver driver;
        private BuscaCepPage buscaCepPage;

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

        [Given(@"que estou na página inicial dos correios")]
        public void GivenQueEstouNaPaginaInicialDosCorreios()
        {
            buscaCepPage = new BuscaCepPage(driver);
            buscaCepPage.NavigateTo(Urls.BuscaCepUrl);
            TestContext.WriteLine("Navegou para a página inicial dos correios");
        }

        [When(@"procuro pelo CEP ""(.*)""")]
        public void WhenProcuroPeloCEP(string cep)
        {
            buscaCepPage.SearchCep(cep);
            TestContext.WriteLine($"Procurou pelo CEP: {cep}");
        }

        [Then(@"o resultado deve ser ""(.*)""")]
        public void ThenOResultadoDeveSer(string resultado)
        {
            var resultText = buscaCepPage.GetResultText();
            TestContext.WriteLine($"Resultado encontrado: {resultText}");
            Assert.IsTrue(resultText.Contains(resultado), $"Expected result to contain '{resultado}' but was '{resultText}'");
        }

        [Then(@"o resultado de erro deve ser ""(.*)""")]
        public void ThenOResultadoDeErroDeveSer(string mensagemErro)
        {
            var resultText = buscaCepPage.GetErrorText();
            TestContext.WriteLine($"Mensagem de erro encontrada: {resultText}");
            Assert.IsTrue(resultText.Contains(mensagemErro), $"Expected error message to contain '{mensagemErro}' but was '{resultText}'");
        }

        [When(@"volto para a página inicial dos correios")]
        public void WhenVoltoParaAPaginaInicialDosCorreios()
        {
            buscaCepPage.NavigateTo(Urls.BuscaCepUrl);
            TestContext.WriteLine("Voltou para a página inicial dos correios");
        }

        [Then(@"realizo verificações utilizando diferentes métodos de localização")]
        public void ThenRealizoVerificacoesUtilizandoDiferentesMetodosDeLocalizacao()
        {
            // Verificação por ID (não aplicável aqui, exemplo alternativo)
            var searchBoxById = driver.FindElement(By.Name("relaxation"));
            Assert.IsNotNull(searchBoxById, "Elemento não encontrado por ID");

            // Verificação por XPath
            var searchBoxByXPath = driver.FindElement(By.XPath("//*[@id=\"Geral\"]/div/div/span[2]/label/input"));
            Assert.IsNotNull(searchBoxByXPath, "Elemento não encontrado por XPath");

            // Verificação por CSS Selector
            var searchButtonByCss = driver.FindElement(By.CssSelector(".btn2.float-right"));
            Assert.IsNotNull(searchButtonByCss, "Elemento não encontrado por CSS Selector");
        }
    }
}
