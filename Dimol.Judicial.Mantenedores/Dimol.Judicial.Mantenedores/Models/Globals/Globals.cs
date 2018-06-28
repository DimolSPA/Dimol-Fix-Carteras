using Dimol.dto;
using System.Collections.Generic;

namespace Dimol.Judicial.Mantenedores.Models.Globals
{
    public static class ComboTipoRol {
        public static List<Combobox> CargarListaTipoRol(string TipoComp) {
            List<Combobox> lstTipo = new List<Combobox>();

            switch (TipoComp)
            {
                case "P":
                    //Combo de TipoRol para roles previsionales
                    lstTipo.Add(new Combobox { Text = "P", Value = "P" }); //P = Actual
                    lstTipo.Add(new Combobox { Text = "A", Value = "A" }); //A = Antiguo
                    break;
                default:
                    //Combo de TipoRol para roles civiles
                    lstTipo.Add(new Combobox { Text = "C", Value = "C" });
                    lstTipo.Add(new Combobox { Text = "V", Value = "V" });
                    lstTipo.Add(new Combobox { Text = "E", Value = "E" });
                    lstTipo.Add(new Combobox { Text = "A", Value = "A" });
                    lstTipo.Add(new Combobox { Text = "F", Value = "F" });
                    lstTipo.Add(new Combobox { Text = "I", Value = "I" });
                    break;
            }

            return lstTipo;
        }
    }
}