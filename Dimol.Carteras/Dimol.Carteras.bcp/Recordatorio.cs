using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.bcp
{
    public class Recordatorio
    {
        public static string GrabarRecordatorio(dto.Recordatorio obj)
        {
            string mensaje = "";
            int error = 0;
            if (obj.Tipo == "SMS")
            {
                error=dao.Recordatorio.GrabarSMS(obj);
            }
            if (obj.Tipo == "E")
            {
                error=dao.Recordatorio.GrabarEmail(obj);
            }
            if (error <= 0)
            {
                mensaje = "Error al eliminar.";
            }
            return mensaje;
        }

        public static string EliminarRecordatorio(dto.Recordatorio obj)
        {
            string mensaje = "";
            int error = 0;
            if (obj.Tipo == "SMS")
            {
                error = dao.Recordatorio.EliminarSMS(obj);
            }
            if (obj.Tipo == "E")
            {
                error = dao.Recordatorio.EliminarEmail(obj);
            }
            if (error <= 0)
            {
                mensaje = "Error al eliminar.";
            }
            return mensaje;
        }

        public static List<dto.Recordatorio> ListarSMSPreDeudor(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Recordatorio.ListarSMSPreDeudor(codemp, ctcid, where, sidx, sord, inicio, limite);
        }

        public static int ListarSMSPreDeudorCount(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Recordatorio.ListarSMSPreDeudorCount(codemp, ctcid, where, sidx, sord, inicio, limite);
        }

        public static List<dto.Recordatorio> ListarEmailPreDeudor(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Recordatorio.ListarEmailPreDeudor(codemp, ctcid, where, sidx, sord, inicio, limite);
        }

        public static int ListarEmailPreDeudorCount(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Recordatorio.ListarEmailPreDeudorCount(codemp, ctcid, where, sidx, sord, inicio, limite);
        }
    }
}
