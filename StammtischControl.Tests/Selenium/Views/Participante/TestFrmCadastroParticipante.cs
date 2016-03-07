
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StammtischControl.Models.Persistencia;
using StammtischControl.Tests.Integracao;
using System.Threading;

namespace StammtischControl.Tests.Selenium.Views.Participante
{
    class TestFrmCadastroParticipante: RepositorioTesteBase
    {
        private const string host = "http://localhost:50295/";
        private Repositorio<Models.Entidades.CadastroGeral.Participante> _repositorio;

        [SetUp]
        public void SetUp()
        {
            _repositorio = new Repositorio<Models.Entidades.CadastroGeral.Participante>(new RepositorioContexto());
        }

        [Test]
        public void acessando_tela_de_cadastro_de_participante()
        {
            using(IWebDriver webDriver = new ChromeDriver())
            {
                webDriver.Navigate().GoToUrl($"{host}Participante/Index");
                webDriver.Manage().Window.Maximize();
                webDriver.FindElement(By.Id("NovoParticipante")).Click();
                Thread.Sleep(500);
                webDriver.Url.Should().Be($"{host}Participante/FrmCadastroParticipante");
                webDriver.Close();
            }
        }

        [Test]
        public void cadastrando_participante()
        {
            using (IWebDriver webDriver = new ChromeDriver())
            {
                webDriver.Navigate().GoToUrl($"{host}Participante/FrmCadastroParticipante");
                webDriver.Manage().Window.Maximize();
                webDriver.FindElement(By.Name("Nome")).SendKeys("Freund Maenns");
                webDriver.FindElement(By.Name("CPF")).SendKeys("12345678901");
                webDriver.FindElement(By.Name("Email")).SendKeys("freund@outlook.com");
                webDriver.FindElement(By.Name("Telefone")).SendKeys("(47)91119030");
                webDriver.FindElement(By.Id("salvar")).Click();
                Thread.Sleep(500);
                
                webDriver.Close();
            }

            var participantes = _repositorio.ObterTodos();

            participantes.Should().NotBeEmpty();
            participantes.Count.Should().Be(1);
            participantes[0].Nome.Should().Be("Freund Maenns");
            participantes[0].CPF.Should().Be("12345678901");
            participantes[0].Email.Should().Be("freund@outlook.com");
            participantes[0].Telefone.Should().Be("(47)91119030");
        }

        [Test]
        public void apresentando_mensagem_de_campos_obrigatorios()
        {
            using (IWebDriver webDriver = new ChromeDriver())
            {
                webDriver.Navigate().GoToUrl($"{host}Participante/FrmCadastroParticipante");
                webDriver.Manage().Window.Maximize();               
                webDriver.FindElement(By.Id("salvar")).Click();
                Thread.Sleep(50);
                webDriver.FindElement(By.XPath("//span[@for='Nome']")).Text.Should().Be("O nome é obrigatório");
                webDriver.FindElement(By.XPath("//span[@for='CPF']")).Text.Should().Be("O CPF é obrigatório");
                webDriver.FindElement(By.XPath("//span[@for='Email']")).Text.Should().Be("O e-mail é obrigatório");
                webDriver.FindElement(By.XPath("//span[@for='Telefone']")).Text.Should().Be("O telefone é obrigatório");

                webDriver.Close();
            }

            var participantes = _repositorio.ObterTodos();

            participantes.Should().BeEmpty();
        }
    }
}
