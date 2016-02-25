using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using StammtischControl.Controllers;
using StammtischControl.Models.Entidades.CadastroGeral;
using StammtischControl.Models.Persistencia;
using StammtischControl.Tests.Builder;

namespace StammtischControl.Tests.Integracao.Controllers
{
    public class TestParticipanteController: RepositorioTesteBase
    {
        private ParticipanteController _participanteController;
        private Repositorio<Participante> _repositorio;

        [SetUp]
        public void SetUp()
        {
            _repositorio = new Repositorio<Participante>(new RepositorioContexto());
            _participanteController = new ParticipanteController(_repositorio);
        }

        [Test]
        public void carregando_view_com_todos_os_participantes()
        {
            var participante1 = new ParticipanteBuilderTest().Criar();
            var participante2 = new ParticipanteBuilderTest().Criar();
            _repositorio.Salvar(participante1);
            _repositorio.Salvar(participante2);
            var actionResult = _participanteController.Index();

            var participantes = (List<Participante>) ((System.Web.Mvc.ViewResultBase) (actionResult)).Model;
            participantes.Should().NotBeNullOrEmpty();
            participantes.Count.Should().Be(2);
            participantes.Should().Contain(participante1);
            participantes.Should().Contain(participante2);
        }

        [Test]
        public void salvando_participante()
        {
            var participante = new ParticipanteBuilderTest().Criar();

            _participanteController.FrmCadastroParticipante(participante);

            var participantesCadastrados = _repositorio.ObterTodos();

            participantesCadastrados.Should().NotBeEmpty();
            participantesCadastrados.Count.Should().Be(1);
            participantesCadastrados[0].ShouldBeEquivalentTo(participante);
        }
    }
}