using Dimol.Carteras.dto;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.bcp
{
    public class Gestor
    {
        public static List<dto.RestriccionGestor> ListarRestriccionesGestor(int codemp, int codsuc, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Gestor.ListarRestriccionesGestor(codemp, codsuc, where, sidx, sord, inicio, limite);
        }

        public static int ListarRestriccionesGestorCount(int codemp, int codsuc, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Gestor.ListarRestriccionesGestorCount(codemp, codsuc, where, sidx, sord, inicio, limite);
        }

        public static int InsertarRestriccionesGestor(RestriccionGestor obj)
        {
            return dao.Gestor.InsertarRestriccionesGestor(obj);
        }

        public static int ModificarRestriccionesGestor(RestriccionGestor obj)
        {
            return dao.Gestor.ModificarRestriccionesGestor(obj);
        }

        public static int BorrarRestriccionesGestor(RestriccionGestor obj)
        {
            return dao.Gestor.BorrarRestriccionesGestor(obj);
        }

        public static string ListarUsuarios(int codemp)
        {
            return dao.Gestor.ListarUsuarios( codemp);
        }

        public static string ListarGestores(int codemp, int codsuc)
        {
            return dao.Gestor.ListarGestores(codemp,codsuc);
        }

        public static void OperAnularRestriccion(RestriccionGestor obj, string oper, string id)
        {
            switch (oper)
            {
                case "add":
                    dao.Gestor.InsertarRestriccionesGestor(obj);
                    break;
                case "edit":
                    dao.Gestor.ModificarRestriccionesGestor(obj);
                    break;
                case "del":
                    dao.Gestor.BorrarRestriccionesGestor(obj);
                    break;
            }
        }

        public static List<Dimol.dto.Combobox> ListarGestoresCombo(int codemp, int codsuc)
        {
            return dao.Gestor.ListarGestoresCombo(codemp, codsuc);
        }

        public static List<Dimol.dto.Combobox> ListarGestorCombo(int codemp, int codsuc, string email)
        {
            return dao.Gestor.ListarGestorCombo(codemp, codsuc, email);
        }

        public static List<dto.Gestor> ListarGestorGrilla(int codemp, int codsucursal, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Gestor.ListarGestorGrilla(codemp, codsucursal, where, sidx, sord, inicio, limite);
        }

        public static int ListarGestorGrillaCount(int codemp, int codsucursal, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Gestor.ListarGestorGrillaCount(codemp, codsucursal, where, sidx, sord, inicio, limite);
        }
        public List<Combobox> TraeListaDe(string EtiClave, int idioma)
        {
            return dao.Gestor.TraeListaDe(EtiClave, idioma);
        }
        public List<Combobox> ListarGrupoCobranza(int codemp, int codsucursal)
        {
            return dao.Gestor.ListarGrupoCobranza(codemp, codsucursal);
        }
        public List<Combobox> ListarEmpleados(int codemp)
        {
            return dao.Gestor.ListarEmpleados(codemp);
        }
        public List<Combobox> ListarGestoresVisitaTerreno(int codemp, int codsucursal)
        {
            return dao.Gestor.ListarGestoresVisitaTerreno(codemp, codsucursal);
        }
        public static int GuardarGestor(dto.Gestor obj, int codemp, int codsucursal)
        {
            return dao.Gestor.GuardarGestor(obj, codemp, codsucursal);
        }
        public List<Combobox> ListarVisitaTerrenoGestores(int codemp, int codsucursal)
        {
            return dao.Gestor.ListarVisitaTerrenoGestores(codemp, codsucursal);
        }
    }
}
