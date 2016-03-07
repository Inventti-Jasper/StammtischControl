using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using StammtischControl.Models.Persistencia;
using StammtischControl.Tests.Builder;
using StammtischControl.Tests.Integracao;

namespace StammtischControl.Tests.Selenium.Views.Participante
{
    public class TestFrmEditarParticipante : RepositorioTesteBase
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
            using (IWebDriver webDriver = new ChromeDriver())
            {
                webDriver.Navigate().GoToUrl($"{host}Participante/Index");
                webDriver.Manage().Window.Maximize();
                webDriver.FindElement(By.Id($"participante_{participante.Id}")).Click();
                Thread.Sleep(500);
                webDriver.Url.Should().Be($"{host}Participante/FrmEditarParticipante/{participante.Id}");
                webDriver.Close();
            }
        }

        [Test]
        public void atualizando_informacoes_do_participante()
        {
            var participante = new ParticipanteBuilderTest().Criar();
            _repositorio.Salvar(participante);

            using (IWebDriver webDriver = new ChromeDriver())
            {
                webDriver.Navigate().GoToUrl($"{host}Participante/Index");
                webDriver.Manage().Window.Maximize();
                webDriver.FindElement(By.Id($"participante_{participante.Id}")).Click();
                Thread.Sleep(500);

                webDriver.FindElement(By.Name("Nome")).Clear();
                webDriver.FindElement(By.Name("Nome")).SendKeys("Freund Maenns");

                webDriver.FindElement(By.Name("CPF")).Clear();
                webDriver.FindElement(By.Name("CPF")).SendKeys("12345678901");

                webDriver.FindElement(By.Name("Email")).Clear();
                webDriver.FindElement(By.Name("Email")).SendKeys("freund@outlook.com");

                webDriver.FindElement(By.Name("Telefone")).Clear();
                webDriver.FindElement(By.Name("Telefone")).SendKeys("(47)91119030");
                webDriver.FindElement(By.Id("salvar")).Click();
                Thread.Sleep(500);

                webDriver.Close();
            }

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

            using (IWebDriver webDriver = new ChromeDriver())
            {
                webDriver.Navigate().GoToUrl($"{host}Participante/Index");
                webDriver.Manage().Window.Maximize();
                webDriver.FindElement(By.Id($"participante_{participante.Id}")).Click();
                Thread.Sleep(500);

                webDriver.FindElement(By.Name("Nome")).Clear();
                webDriver.FindElement(By.Name("CPF")).Clear();
                webDriver.FindElement(By.Name("Email")).Clear();
                webDriver.FindElement(By.Name("Telefone")).Clear();

                webDriver.FindElement(By.Id("salvar")).Click();
                Thread.Sleep(50);
                webDriver.FindElement(By.XPath("//span[@for='Nome']")).Text.Should().Be("O nome é obrigatório");
                webDriver.FindElement(By.XPath("//span[@for='CPF']")).Text.Should().Be("O CPF é obrigatório");
                webDriver.FindElement(By.XPath("//span[@for='Email']")).Text.Should().Be("O e-mail é obrigatório");
                webDriver.FindElement(By.XPath("//span[@for='Telefone']")).Text.Should().Be("O telefone é obrigatório");

                webDriver.Close();
            }
        }
    }
}