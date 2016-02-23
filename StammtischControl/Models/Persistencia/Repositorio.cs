using System;
using System.Data.Entity;
using StammtischControl.Models.Entidades;

namespace StammtischControl.Models.Persistencia
{
    public class Repositorio<TEntidade> where TEntidade : Entidade
    {
        private readonly RepositorioContexto _repositorioContexto;
        private readonly DbSet<TEntidade> _dbSet;

        public Repositorio(RepositorioContexto repositorioContexto)
        {
            this._repositorioContexto = repositorioContexto;
            _dbSet = repositorioContexto.Set<TEntidade>();
        }

        public void Salvar(TEntidade entidade)
        {
            try
            {
                _dbSet.Add(entidade);
                _repositorioContexto.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Falha ao salvar o registro. Erro: {0}", exception.Message), exception);
            }
        }

        public TEntidade Buscar(int id)
        {
            return _dbSet.Find(id);
        }

        public void Excluir(int id)
        {
            var entidade = Buscar(id);
            _dbSet.Remove(entidade);
            _repositorioContexto.SaveChanges();
        }
    }
}