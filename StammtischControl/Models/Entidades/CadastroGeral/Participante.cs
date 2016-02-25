
namespace StammtischControl.Models.Entidades.CadastroGeral
{
    public class Participante : Entidade
    {
        public string Nome { get;  set; }
        public string CPF { get;  set; }
        public string Email { get;  set; }
        public string Telefone { get;  set; }

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