using FluentAssertions;
using NUnit.Framework;
using StammtischControl.Models.Entidades.CadastroGeral;
using StammtischControl.Models.Persistencia;
using StammtischControl.Tests.Builder;

namespace StammtischControl.Tests.Integracao.Models.Persistencia
{
    public class TestRepositorio: RepositorioTesteBase
    {
        private Repositorio<Participante> _repositorio;

        [SetUp]
        public void SetUp()
        {
            _repositorio = new Repositorio<Participante>(new RepositorioContexto());
        }

        [Test]
        public void salvando_participante()
        {
            var participante = new ParticipanteBuilderTest().Criar();

            _repositorio.Salvar(participante);

            participante.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void buscando_participante()
        {
            var participante = new ParticipanteBuilderTest().Criar();
            _repositorio.Salvar(participante);

            Participante participanteRecuperado = _repositorio.Buscar(participante.Id);
            participanteRecuperado.Should().NotBeNull();
            participanteRecuperado.ShouldBeEquivalentTo(participante);
        }

        [Test]
        public void excluindo_participante()
        {
            var participante = new ParticipanteBuilderTest().Criar();
             _repositorio.Salvar(participante);

            _repositorio.Excluir(participante.Id);

            var participanteRecuperado = _repositorio.Buscar(participante.Id);

            participanteRecuperado.Should().BeNull();
        }
    }
}