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
    public class TestCategoriaItemController: RepositorioTesteBase
    {
        private CategoriaItemController _categoriaItemController;
        private Repositorio<CategoriaItem> _repositorio;

        [SetUp]
        public void Setup()
        {
            _repositorio = new Repositorio<CategoriaItem>(new RepositorioContexto());
            _categoriaItemController = new CategoriaItemController(_repositorio);
        }

        [Test]
        public void carregando_view_index_com_todas_categorias_item_ordenadas_pela_descricao()
        {
            var categoriaItem1 = new CategoriaItemBuilderTest().ComDescricao("Bebida Alcoolica").Criar();
            var categoriaItem2 = new CategoriaItemBuilderTest().ComDescricao("Alimentação").Criar();
            var categoriaItem3 = new CategoriaItemBuilderTest().ComDescricao("Limpeza").Criar();

            _repositorio.Salvar(categoriaItem1);
            _repositorio.Salvar(categoriaItem2);
            _repositorio.Salvar(categoriaItem3);

            ActionResult  actionResult = _categoriaItemController.Index();

            ((ViewResultBase)actionResult).ViewName.Should().Be("Index");

            var categoriasItem = (List<CategoriaItem>)((ViewResultBase)(actionResult)).Model;
            categoriasItem.Should().NotBeEmpty();
            categoriasItem.Count.Should().Be(3);
            categoriasItem[0].Descricao.Should().Be("Alimentação");
            categoriasItem[1].Descricao.Should().Be("Bebida Alcoolica");
            categoriasItem[2].Descricao.Should().Be("Limpeza");
        }

        [Test]
        public void retornando_view_para_cadastro_de_categoria_item()
        {
            var actionResult = _categoriaItemController.FrmCadastroCategoriaItem();

            ((ViewResultBase)actionResult).ViewName.Should().Be("FrmCadastroCategoriaItem");
        }

        [Test]
        public void salvando_categoria_item()
        {
            var categoriaItem = new CategoriaItemBuilderTest().Criar();

            _categoriaItemController.FrmCadastroCategoriaItem(categoriaItem);

            var categoriasItem = _repositorio.ObterTodos();

            categoriasItem.Should().NotBeEmpty();
            categoriasItem.Count.Should().Be(1);
            categoriasItem[0].ShouldBeEquivalentTo(categoriaItem);
        }

        [Test]
        public void retornando_view_index_com_as_categorias_item_ao_salvar_categoria_item()
        {
            var categoriaItem1 = new CategoriaItemBuilderTest().ComDescricao("Bbbbb").Criar();
            var categoriaItem2 = new CategoriaItemBuilderTest().ComDescricao("Aaaaa").Criar();
            var categoriaItem3 = new CategoriaItemBuilderTest().ComDescricao("Cccc").Criar();
            _repositorio.Salvar(categoriaItem1);
            _repositorio.Salvar(categoriaItem2);

            var actionResult = _categoriaItemController.FrmCadastroCategoriaItem(categoriaItem3);

            var categoriasItem = (List<CategoriaItem>)((ViewResultBase)(actionResult)).Model;
            ((ViewResultBase)actionResult).ViewName.Should().Be("Index");

            categoriasItem.Should().NotBeNullOrEmpty();
            categoriasItem.Count.Should().Be(3);
            categoriasItem[0].Descricao.Should().Be(categoriaItem2.Descricao);
            categoriasItem[1].Descricao.Should().Be(categoriaItem1.Descricao);
            categoriasItem[2].Descricao.Should().Be(categoriaItem3.Descricao);
        }
    }
}