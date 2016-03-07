using StammtischControl.Models.Entidades.CadastroGeral;

namespace StammtischControl.Tests.Builder
{
    public class CategoriaItemBuilderTest
    {
        private string descricao = "Bebida Alcoolica";

        public CategoriaItemBuilderTest ComDescricao(string descricao)
        {
            this.descricao = descricao;
            return this;
        }

        public CategoriaItem Criar()
        {
            return new CategoriaItem(descricao, TipoRateio.ParticipanteConsumir);
        }
    }
}