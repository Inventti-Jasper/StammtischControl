namespace StammtischControl.Models.Entidades.CadastroGeral
{
    public class Item: Entidade
    {
        public string Nome { get; set; }
        public decimal Quantidade { get; set; }
        public string UnidadeMedida { get; set; }
        public TipoNecessidade TipoNecessidade { get; set; }
        public TipoDisposicaoItem TipoDisposicaoItem { get; set; }
    }
}