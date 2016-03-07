using System;
using StammtischControl.Models.Attributes;

namespace StammtischControl.Models.Extension
{
    public static class EnumExtension
    {
        public static string ObterDescricao(this Enum valorEnum)
        {
            var type = valorEnum.GetType();
            var fieldInfo = type.GetField(valorEnum.ToString());

            var customAttributes = fieldInfo.GetCustomAttributes(typeof(DescricaoEnumAttribute), false);
            var descricao = ((DescricaoEnumAttribute)customAttributes[0]).Descricao;
            return descricao;
        }
    }
}