using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Caja.bcp
{
    public class Reporte
    {
        public static List<dto.ReporteImputacion> ListarReporteImputacion(int codemp, int conciliacionId)
        {
            return dao.Reporte.ListarReporteImputacion(codemp, conciliacionId);
        }
        public static dto.ReporteCabecera obtenerCabecera(int codemp, int conciliacionId)
        {
            return dao.Reporte.obtenerCabecera(codemp, conciliacionId);
        }
        public static List<dto.ReporteImputacionDetail> ListarReporteImputacionDetail(int codemp, int conciliacionId)
        {
            return dao.Reporte.ListarReporteImputacionDetail(codemp, conciliacionId);
        }
        public static List<dto.DocumentoCustodiaReporte> ObtenerDocumentoCustodiaDetail(int codemp, int conciliacionId)
        {
            return dao.Reporte.ObtenerDocumentoCustodiaDetail(codemp, conciliacionId);
        }
        /// <summary>
        /// Le da formato a un rut, concatenándole puntos y guión.
        /// </summary>
        /// <param name="rut">Rut a formatear.</param>
        /// <returns>Un nuevo String, con el rut formateado</returns>
        public static string getRutFormato(string rut)
        {
            int cont = 0;
            
            string format;
            if (rut.Length == 0)
            {
                return "";
            }
            else
            {
                rut = rut.Trim();
                string rutTemporal = rut.Substring(0, rut.Length - 1);
                string dv = rut.Substring(rut.Length - 1, 1);

                Int64 rutc;
                if (!Int64.TryParse(rutTemporal, out rutc))
                {
                    rutc = 0;
                }
                //este comando formtaea con separadores de miles
                format = rutc.ToString("N0");

                if (format.Equals("0"))
                {
                    format = string.Empty;
                }
                else
                {
                    format += "-" + dv;
                    format = format.Replace(",", ".");
                }

                return format;
            }
        }
    }
}
