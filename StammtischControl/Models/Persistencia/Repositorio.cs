using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using StammtischControl.Models.Entidades;

namespace StammtischControl.Models.Persistencia
{
    public class Repositorio<TEntidade> : IRepositorio<TEntidade> where TEntidade : Entidade
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

        public void Atualizar(TEntidade entidade)
        {
            _repositorioContexto.Entry(entidade).State = EntityState.Modified;
            _repositorioContexto.SaveChanges();
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

        public List<TEntidade> ObterTodos()
        {
            return _dbSet.ToList();
        }
    }
}