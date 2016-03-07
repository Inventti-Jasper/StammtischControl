using StammtischControl.Models.Attributes;

namespace StammtischControl.Models.Entidades.CadastroGeral
{
    public enum TipoRateio
    {
        [DescricaoEnum("Todos os Participantes")]
        TodosParticipantes = 0,

        [DescricaoEnum("Participante que Consumir")]
        ParticipanteConsumir = 1
    }
}