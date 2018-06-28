using System;
using System.ComponentModel;
using System.Linq;

namespace Dimol.Judicial.Mantenedores.Models.Enums
{
    public static class Enums
    {
        public static string Descripcion<TEnum>(this TEnum enumValue) where TEnum : struct
        {
            return ObteneDescripcion((Enum)(object)enumValue);
        }

        public static string ObteneDescripcion(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Any() ? attributes[0].Description : value.ToString();
        }
    }

    public enum Competencia
    {
        CorteSuprema = 1,
        CorteApelaciones = 2,
        Civíl = 3,
        Laboral = 4,
        Penal = 5,
        Cobranza = 6,
        Familia = 7
    }
}