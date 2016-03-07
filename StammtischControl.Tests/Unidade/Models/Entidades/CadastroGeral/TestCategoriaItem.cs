using FluentAssertions;
using NUnit.Framework;
using StammtischControl.Models.Entidades.CadastroGeral;
using StammtischControl.Models.Extension;

namespace StammtischControl.Tests.Unidade.Models.Entidades.CadastroGeral
{
    public class TestCategoriaItem
    {
        [Test]
        public void obtendo_tipos_de_rateio()
        {
            var tiposRateio = CategoriaItem.TiposRateio;
            tiposRateio.Should().NotBeEmpty();
            tiposRateio.Should().Contain(x => x.Text == TipoRateio.TodosParticipantes.ObterDescricao());
            tiposRateio.Should().Contain(x => x.Value == ((int)TipoRateio.TodosParticipantes).ToString());
        }
    }
}