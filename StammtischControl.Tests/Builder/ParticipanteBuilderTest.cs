using StammtischControl.Models.Entidades.CadastroGeral;

namespace StammtischControl.Tests.Builder
{
    public class ParticipanteBuilderTest
    {
        private string nome = "Carlos Xamps";

        public ParticipanteBuilderTest ComNome(string nome)
        {
            this.nome = nome;
            return this;
        }

        public Participante Criar()
        {
            return new Participante(nome, "12345678901", "xamps@gmail.com", "4532315654");
        }
    }
}