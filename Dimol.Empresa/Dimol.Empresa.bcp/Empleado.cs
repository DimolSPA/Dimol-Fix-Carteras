using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Empresa.bcp
{
    public class Empleado:dto.Empleado
    {
        dao.Empleado daoEmpleado = new dao.Empleado();

        public static List<Combobox> ListarEstadosEmpleado(int codemp)
        {
            return dao.Empleado.ListarEstadosEmpleado(codemp);
        }
        
        public List<dto.Empleado> ListarEmpleadoGrilla(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite,
                                                        string nombre, string paterno, string materno, string rut, string estado)
        {
            List<dto.Empleado> lstEmpleado = new List<dto.Empleado>();
            lstEmpleado = daoEmpleado.ListarEmpleadoGrilla(codemp, idioma, where, sidx, sord, inicio, limite, nombre, paterno, materno, rut, estado);
            return lstEmpleado;
        }

        public static int ListarEmpleadoGrillaCount(int codemp, int idioma, string where, string sidx, string sord, int inicio, int limite,
                                                        string nombre, string paterno, string materno, string rut, string estado)
        {
            return dao.Empleado.ListarEmpleadoGrillaCount(codemp, idioma, where, sidx, sord, inicio, limite, nombre, paterno, materno, rut, estado);

        }
    }
}
