using System.Web.Mvc;
using StammtischControl.Models.Entidades.CadastroGeral;
using StammtischControl.Models.Persistencia;

namespace StammtischControl.Controllers
{
    public class ParticipanteController : Controller
    {
        private readonly IRepositorio<Participante> _repositorio;

        public ParticipanteController(IRepositorio<Participante> repositorio)
        {
            this._repositorio = repositorio;
        }

        // GET: Participante
        public ActionResult Index()
        {
            var participantes = _repositorio.ObterTodos();
            return View(participantes);
        }
    }
}