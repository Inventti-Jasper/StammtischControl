using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using StammtischControl.Models.Persistencia;
using StammtischControl.Tests.Integracao.Models;

namespace StammtischControl.Tests.Selenium.Views.Participante
{
    class TestFrmCadastroParticipante : SeleniumDriverBase
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
            _webDriver.Navigate().GoToUrl($"{host}Participante/Index");
            _webDriver.Manage().Window.Maximize();
            _webDriver.FindElement(By.Id("NovoParticipante")).Click();
            Thread.Sleep(500);
            _webDriver.Url.Should().Be($"{host}Participante/FrmCadastroParticipante");
        }

        [Test]
        public void cadastrando_participante()
        {
            _webDriver.Navigate().GoToUrl($"{host}Participante/FrmCadastroParticipante");
            _webDriver.Manage().Window.Maximize();
            _webDriver.FindElement(By.Name("Nome")).SendKeys("Freund Maenns");
            _webDriver.FindElement(By.Name("CPF")).SendKeys("12345678901");
            _webDriver.FindElement(By.Name("Email")).SendKeys("freund@outlook.com");
            _webDriver.FindElement(By.Name("Telefone")).SendKeys("(47)91119030");
            _webDriver.FindElement(By.Id("salvar")).Click();
            Thread.Sleep(500);


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
            _webDriver.Navigate().GoToUrl($"{host}Participante/FrmCadastroParticipante");
            _webDriver.Manage().Window.Maximize();
            _webDriver.FindElement(By.Id("salvar")).Click();
            Thread.Sleep(50);
            _webDriver.FindElement(By.XPath("//span[@for='Nome']")).Text.Should().Be("O nome é obrigatório");
            _webDriver.FindElement(By.XPath("//span[@for='CPF']")).Text.Should().Be("O CPF é obrigatório");
            _webDriver.FindElement(By.XPath("//span[@for='Email']")).Text.Should().Be("O e-mail é obrigatório");
            _webDriver.FindElement(By.XPath("//span[@for='Telefone']")).Text.Should().Be("O telefone é obrigatório");

            var participantes = _repositorio.ObterTodos();

            participantes.Should().BeEmpty();
        }

        [Test]
        public void apresentando_mensagem_de_email_invalido()
        {
            _webDriver.Navigate().GoToUrl($"{host}Participante/FrmCadastroParticipante");
            _webDriver.Manage().Window.Maximize();
            _webDriver.FindElement(By.Name("Email")).SendKeys("emailinvalido");
            _webDriver.FindElement(By.Name("Telefone")).SendKeys("453453456");

            Thread.Sleep(50);
            _webDriver.FindElement(By.XPath("//span[@for='Email']")).Text.Should().Be("Não é um E-mail válido");

            var participantes = _repositorio.ObterTodos();

            participantes.Should().BeEmpty();
        }

        [Test]
        public void apresentando_mensagem_da_quantidade_de_digitos_do_cpf()
        {
            _webDriver.Navigate().GoToUrl($"{host}Participante/FrmCadastroParticipante");
            _webDriver.Manage().Window.Maximize();
            _webDriver.FindElement(By.Name("CPF")).SendKeys("342432");
            _webDriver.FindElement(By.Name("Telefone")).SendKeys("453453456");

            Thread.Sleep(50);
            _webDriver.FindElement(By.XPath("//span[@for='CPF']")).Text.Should().Be("O CPF deve conter 11 digitos");

            var participantes = _repositorio.ObterTodos();

            participantes.Should().BeEmpty();
        }
    }
}
