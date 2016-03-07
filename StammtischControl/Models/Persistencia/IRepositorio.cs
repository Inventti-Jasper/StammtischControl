using System.Collections.Generic;
using StammtischControl.Models.Entidades;

namespace StammtischControl.Models.Persistencia
{
    public interface IRepositorio<TEntidade> where TEntidade : Entidade
    {
        void Salvar(TEntidade entidade);
        void Atualizar(TEntidade entidade);
        TEntidade Buscar(int id);
        List<TEntidade> ObterTodos();
        void Excluir(int id);
    }
}