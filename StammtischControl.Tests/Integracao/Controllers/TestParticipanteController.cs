using System.Collections.Generic;
using System.Web.Mvc;
using FluentAssertions;
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
        public void carregando_view_index_com_todos_os_participantes_ordenados_pelo_nome()
        {
            var participante1 = new ParticipanteBuilderTest().ComNome("Bbbbb").Criar();
            var participante2 = new ParticipanteBuilderTest().ComNome("Aaaaa").Criar();
            var participante3 = new ParticipanteBuilderTest().ComNome("Cccc").Criar();
            _repositorio.Salvar(participante1);
            _repositorio.Salvar(participante2);
            _repositorio.Salvar(participante3);
            var actionResult = _participanteController.Index();

            var participantes = (List<Participante>) ((ViewResultBase) (actionResult)).Model;
            ((ViewResultBase) actionResult).ViewName.Should().Be("Index");
            participantes.Should().NotBeNullOrEmpty();
            participantes.Count.Should().Be(3);
            participantes[0].Nome.Should().Be(participante2.Nome);
            participantes[1].Nome.Should().Be(participante1.Nome);
            participantes[2].Nome.Should().Be(participante3.Nome);
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

        [Test]
        public void retornando_view_index_com_os_participantes_ao_salvar_participante()
        {
            var participante1 = new ParticipanteBuilderTest().ComNome("Bbbbb").Criar();
            var participante2 = new ParticipanteBuilderTest().ComNome("Aaaaa").Criar();
            var participante3 = new ParticipanteBuilderTest().ComNome("Cccc").Criar();
            _repositorio.Salvar(participante1);
            _repositorio.Salvar(participante2);
            
            var actionResult = _participanteController.FrmCadastroParticipante(participante3);

            var participantes = (List<Participante>)((ViewResultBase)(actionResult)).Model;
            ((ViewResultBase)actionResult).ViewName.Should().Be("Index");

            participantes.Should().NotBeNullOrEmpty();
            participantes.Count.Should().Be(3);
            participantes[0].Nome.Should().Be(participante2.Nome);
            participantes[1].Nome.Should().Be(participante1.Nome);
            participantes[2].Nome.Should().Be(participante3.Nome);
        }
    }
}