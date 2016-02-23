using System;
using System.Data.Entity;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using StammtischControl.Models.Entidades.CadastroGeral;
using StammtischControl.Models.Persistencia;
using StammtischControl.Tests.Builder;
using StammtischControl.Tests.Integracao.Models.Persistencia;

namespace StammtischControl.Tests.Unidade.Models.Persistencia
{
    public class TestRepositorio
    {
        private Repositorio<Participante> _repositorio;
        private Mock<RepositorioContexto> _mockRepositorioContexto;

        [SetUp]
        public void SetUp()
        {
            _mockRepositorioContexto = new Mock<RepositorioContexto>();
            var _repositorioContexto = new RepositorioContexto();

            _mockRepositorioContexto.Setup(contexto => contexto.Set<Participante>()).Returns(_repositorioContexto.Set<Participante>());
            _repositorio = new Repositorio<Participante>(_mockRepositorioContexto.Object);
        }

        [Test]
        public void lancando_excecao_quando_nao_conseguir_salvar()
        {
            var participante = new ParticipanteBuilderTest().Criar();
            _mockRepositorioContexto.Setup(contexto => contexto.SaveChanges()).Throws(new Exception("Coluna xamps não encontrada"));

            _repositorio.Invoking(repositorio => repositorio.Salvar(participante)).ShouldThrow<Exception>().And.Message.Should().Be("Falha ao salvar o registro. Erro: Coluna xamps não encontrada");
        }
    }
}