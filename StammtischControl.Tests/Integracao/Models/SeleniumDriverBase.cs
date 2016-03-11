using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace StammtischControl.Tests.Integracao.Models
{
    public class SeleniumDriverBase : RepositorioTesteBase
    {
        protected IWebDriver _webDriver;

        [SetUp]
        public void Setup()
        {
            _webDriver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            if (_webDriver != null)
            {
                _webDriver.Close();
                _webDriver.Dispose();
            }
        }
    }
}