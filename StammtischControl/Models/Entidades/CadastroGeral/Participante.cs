
namespace StammtischControl.Models.Entidades.CadastroGeral
{
    public class Participante : Entidade
    {
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }

        public Participante()
        {
        }

        public Participante(string nome, string cpf, string email, string telefone)
        {
            Nome = nome;
            CPF = cpf;
            Email = email;
            Telefone = telefone;
        }
    }
}