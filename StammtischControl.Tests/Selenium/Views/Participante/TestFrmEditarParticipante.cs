using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using StammtischControl.Models.Persistencia;
using StammtischControl.Tests.Builder;
using StammtischControl.Tests.Integracao.Models;

namespace StammtischControl.Tests.Selenium.Views.Participante
{
    public class TestFrmEditarParticipante : SeleniumDriverBase
    {
        private const string host = "http://localhost:50295/";
        private Repositorio<Models.Entidades.CadastroGeral.Participante> _repositorio;
        private RepositorioContexto _repositorioContexto;

        [SetUp]
        public void SetUp()
        {
            _repositorioContexto = new RepositorioContexto();
            _repositorio = new Repositorio<Models.Entidades.CadastroGeral.Participante>(_repositorioContexto);
        }

        [Test]
        public void acessando_tela_de_edicao_do_participante()
        {
            var participante = new ParticipanteBuilderTest().Criar();
            _repositorio.Salvar(participante);

            _webDriver.Navigate().GoToUrl($"{host}Participante/Index");
            _webDriver.Navigate().GoToUrl($"{host}Participante/Index");
            _webDriver.Manage().Window.Maximize();
            _webDriver.FindElement(By.Id($"participante_{participante.Id}")).Click();
            Thread.Sleep(500);
            _webDriver.Url.Should().Be($"{host}Participante/FrmEditarParticipante/{participante.Id}");

        }

        [Test]
        public void atualizando_informacoes_do_participante()
        {
            var participante = new ParticipanteBuilderTest().Criar();
            _repositorio.Salvar(participante);
            _webDriver.Navigate().GoToUrl($"{host}Participante/Index");
            _webDriver.Manage().Window.Maximize();
            _webDriver.FindElement(By.Id($"participante_{participante.Id}")).Click();
            Thread.Sleep(500);

            _webDriver.FindElement(By.Name("Nome")).Clear();
            _webDriver.FindElement(By.Name("Nome")).SendKeys("Freund Maenns");

            _webDriver.FindElement(By.Name("CPF")).Clear();
            _webDriver.FindElement(By.Name("CPF")).SendKeys("12345678901");

            _webDriver.FindElement(By.Name("Email")).Clear();
            _webDriver.FindElement(By.Name("Email")).SendKeys("freund@outlook.com");

            _webDriver.FindElement(By.Name("Telefone")).Clear();
            _webDriver.FindElement(By.Name("Telefone")).SendKeys("(47)91119030");
            _webDriver.FindElement(By.Id("salvar")).Click();
            Thread.Sleep(500);

            _repositorioContexto.Entry(participante).Reload();

            participante.Should().NotBeNull();
            participante.Nome.Should().Be("Freund Maenns");
            participante.CPF.Should().Be("12345678901");
            participante.Email.Should().Be("freund@outlook.com");
            participante.Telefone.Should().Be("(47)91119030");
        }

        [Test]
        public void apresentando_mensagem_de_campos_obrigatorios()
        {
            var participante = new ParticipanteBuilderTest().Criar();
            _repositorio.Salvar(participante);

            _webDriver.Navigate().GoToUrl($"{host}Participante/Index");
            _webDriver.Manage().Window.Maximize();
            _webDriver.FindElement(By.Id($"participante_{participante.Id}")).Click();
            Thread.Sleep(500);

            _webDriver.FindElement(By.Name("Nome")).Clear();
            _webDriver.FindElement(By.Name("CPF")).Clear();
            _webDriver.FindElement(By.Name("Email")).Clear();
            _webDriver.FindElement(By.Name("Telefone")).Clear();

            _webDriver.FindElement(By.Id("salvar")).Click();
            Thread.Sleep(50);
            _webDriver.FindElement(By.XPath("//span[@for='Nome']")).Text.Should().Be("O nome é obrigatório");
            _webDriver.FindElement(By.XPath("//span[@for='CPF']")).Text.Should().Be("O CPF é obrigatório");
            _webDriver.FindElement(By.XPath("//span[@for='Email']")).Text.Should().Be("O e-mail é obrigatório");
            _webDriver.FindElement(By.XPath("//span[@for='Telefone']")).Text.Should().Be("O telefone é obrigatório");

        }
    }
}