using System;
using NUnit.Framework;
using StammtischControl.Models.Persistencia;

namespace StammtischControl.Tests.Integracao
{
    public class RepositorioTesteBase
    {
        [SetUp]
        public void SetUp()
        {
            var listaTabelas = new[]
            {
                "Items",
                "CategoriaItems",
                "Participantes"
            };
            var repositorioContexto = new RepositorioContexto();

            Array.ForEach(listaTabelas, nomeTabela => repositorioContexto.Database.ExecuteSqlCommand(string.Format("delete from {0}", nomeTabela)));
            repositorioContexto.SaveChanges();
        }
    }
}