using System.Data.Entity;
using StammtischControl.Models.Entidades.CadastroGeral;

namespace StammtischControl.Models.Persistencia
{
    public class RepositorioContexto: DbContext
    {
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<CategoriaItem> CategoriasItem { get; set; }
        public DbSet<Item> Itens { get; set; }
    }
}