using System;

namespace StammtischControl.Models.Attributes
{
    public class DescricaoEnumAttribute : Attribute
    {
        public string Descricao { get; private set; }

        public DescricaoEnumAttribute(string descricao)
        {
            Descricao = descricao;
        }
    }
}