using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using StammtischControl.Models.Extension;

namespace StammtischControl.Models.Entidades.CadastroGeral
{
    public class CategoriaItem : Entidade
    {
        [StringLength(maximumLength: 50)]
        [Required(ErrorMessage = "O campo descrição é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O tipo de rateio é obrigatório")]
        public TipoRateio TipoRateio { get; set; }

        public static IEnumerable<SelectListItem> TiposRateio
        {
            get
            {
                var values = Enum.GetValues(typeof(TipoRateio));

                var tiposRateio = new List<SelectListItem>();
                foreach (TipoRateio tipoRateio in values)
                {
                    tiposRateio.Add(new SelectListItem { Value = ((int)tipoRateio).ToString(), Text = tipoRateio.ObterDescricao() });
                }

                return tiposRateio;
            }
        }

        public CategoriaItem()
        {
        }

        public CategoriaItem(string descricao, TipoRateio tipoRateio)
        {
            Descricao = descricao;
            TipoRateio = tipoRateio;
        }
    }
}