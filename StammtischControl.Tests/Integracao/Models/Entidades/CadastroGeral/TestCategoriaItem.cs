using FluentAssertions;
using NUnit.Framework;
using StammtischControl.Models.Entidades.CadastroGeral;
using StammtischControl.Models.Persistencia;
using StammtischControl.Tests.Builder;

namespace StammtischControl.Tests.Integracao.Models.Entidades.CadastroGeral
{
    public class TestCategoriaItem: RepositorioTesteBase
    {
        private Repositorio<CategoriaItem> _repositorio;

        [SetUp]
        public void Setup()
        {
            _repositorio = new Repositorio<CategoriaItem>(new RepositorioContexto());
        }

        [Test]
        public void salvando_categoria_item()
        {
            var categoriaItem = new CategoriaItemBuilderTest().Criar();
            _repositorio.Salvar(categoriaItem);

            var categoriaItemSalva = _repositorio.Buscar(categoriaItem.Id);

            categoriaItemSalva.Should().NotBeNull();
            categoriaItemSalva.ShouldBeEquivalentTo(categoriaItemSalva);
        }
    }
}