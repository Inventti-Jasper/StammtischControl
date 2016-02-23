using StammtischControl.Models.Entidades.CadastroGeral;

namespace StammtischControl.Tests.Builder
{
    public class ParticipanteBuilderTest
    {
        public Participante Criar()
        {
            return new Participante("Carlos Xamps", "12345678901", "xamps@gmail.com", "4532315654");
        }
    }
}