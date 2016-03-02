
using System.ComponentModel.DataAnnotations;

namespace StammtischControl.Models.Entidades.CadastroGeral
{
    public class Participante : Entidade
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Nome { get;  set; }
        
        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string CPF { get;  set; }
        
        [Required(ErrorMessage = "O E-amil é obrigatório")]
        [EmailAddress(ErrorMessage = "Não é um E-mail válido")]
        public string Email { get;  set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
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