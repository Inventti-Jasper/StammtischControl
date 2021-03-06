﻿using System.Linq;
using System.Web.Mvc;
using StammtischControl.Models.Entidades.CadastroGeral;
using StammtischControl.Models.Persistencia;

namespace StammtischControl.Controllers.CadastroGeral
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
            return CriarIndex();
        }

        private ActionResult CriarIndex()
        {
            var participantes = _repositorio.ObterTodos().OrderBy(participante => participante.Nome).ToList();
            return View("Index", participantes);
        }

        public ActionResult FrmCadastroParticipante()
        {
            return View("FrmCadastroParticipante");
        }

        [HttpPost]
        public ActionResult FrmCadastroParticipante(Participante participante)
        {
            _repositorio.Salvar(participante);

            return CriarIndex();
        }

        public ActionResult FrmEditarParticipante(int id)
        {
            var participante = _repositorio.Buscar(id);
            
            return View(participante);
        }

        [HttpPost]
        public ActionResult FrmEditarParticipante(int id, Participante participante)
        {
            _repositorio.Atualizar(participante);
            return CriarIndex();
        }
    }
}