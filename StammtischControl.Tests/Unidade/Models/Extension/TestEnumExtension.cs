using FluentAssertions;
using NUnit.Framework;
using StammtischControl.Models.Entidades.CadastroGeral;
using StammtischControl.Models.Extension;

namespace StammtischControl.Tests.Unidade.Models.Extension
{
    public class TestEnumExtension
    {
        [Test]
        public void obtendo_descricao_da_enum()
        {
            TipoRateio.ParticipanteConsumir.ObterDescricao().Should().Be("Participante que Consumir");
        }
    }
}