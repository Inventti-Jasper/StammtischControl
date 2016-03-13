using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StammtischControl.Models.Entidades.CadastroGeral;
using StammtischControl.Models.Persistencia;

namespace StammtischControl.Controllers.CadastroGeral
{
    public class CategoriaItemController: Controller
    {
        private readonly IRepositorio<CategoriaItem> _repositorio;

        public CategoriaItemController(IRepositorio<CategoriaItem> repositorio)
        {
            this._repositorio = repositorio;
        }

        public ActionResult Index()
        {
            return CriarIndex();
        }

        public ActionResult FrmCadastroCategoriaItem()
        {
            return View("FrmCadastroCategoriaItem");
        }

        [HttpPost]
        public ActionResult FrmCadastroCategoriaItem(CategoriaItem categoriaItem)
        {
            _repositorio.Salvar(categoriaItem);
            return CriarIndex();
        }

        private ActionResult CriarIndex()
        {
            var categoriasItem = _repositorio.ObterTodos();

            return View("Index", categoriasItem.OrderBy(categoria => categoria.Descricao).ToList());
        }
    }
}