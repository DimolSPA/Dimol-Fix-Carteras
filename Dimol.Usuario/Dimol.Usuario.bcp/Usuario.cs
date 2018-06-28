using System.Collections.Generic;
using Dimol.dto;

namespace Dimol.Usuario.bcp
{
    public class Usuario
    {
        public static List<dto.Usuario> ListarUsuariosGrilla(int codemp, string nombre, string usuario, string where, string sidx, string sord, int inicio, int limite)
        {
            //List<dto.Insumo> lst = new List<dto.Insumo>();
            return dao.Usuario.ListarUsuariosGrilla(codemp, nombre, usuario, where, sidx, sord, inicio, limite);
            //return lst;
        }

        public static int ListarUsuariosGrillaCount(int codemp, string nombre, string usuario, string where, string sidx, string sord)
        {
            //int total = 0;
            return dao.Usuario.ListarUsuariosGrillaCount(codemp, nombre, usuario, where, sidx, sord);
            //return total;
        }

        public static List<Combobox> ListarPreguntasSecretas(int idioma, string first)
        {
            return dao.Usuario.ListarPreguntasSecretas(idioma, first);
        }

        public static List<Combobox> ListarEstados(int idioma, string first)
        {
            return dao.Usuario.ListarEstados(idioma, first);
        }

        public static List<Combobox> ListarPermisos(int idioma, string first)
        {
            return dao.Usuario.ListarPermisos(idioma, first);
        }

        public static List<Combobox> ListarPerfiles(int idioma, string first)
        {
            return dao.Usuario.ListarPerfiles(idioma, first);
        }

        public static List<dto.Sucursal> ListarSucursalesSinAsignar(int codemp, int id, string where, string sidx, string sord)
        {
            return dao.Usuario.ListarSucursalesSinAsignar(codemp, id, where, sidx, sord);
        }

        public static List<dto.Sucursal> ListarSucursalesAsignadas(int codemp, int id, string where, string sidx, string sord)
        {
            return dao.Usuario.ListarSucursalesAsignadas(codemp, id, where, sidx, sord);
        }

        public static int Insertar(dto.Usuario objAccion, int codemp, int suc)
        {
            return dao.Usuario.Insertar(objAccion, codemp, suc);
        }

        public static int Actualizar(dto.Usuario objAccion)
        {
            return dao.Usuario.Actualizar(objAccion);
        }

        public static dto.Usuario BuscarUsuarioPorIdUsuario(int IdUsuario)
        {
            return dao.Usuario.BuscarUsuarioPorIdUsuario(IdUsuario);
        }
    }
}
