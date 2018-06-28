using System.Collections.Generic;
using Dimol.dto;
namespace Dimol.Carteras.bcp
{
    public class BienVehiculo
    {
        public static List<dto.BienVehiculo> ListarBienesVehiculosGrilla(int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.BienVehiculo.ListarBienesVehiculosGrilla(ctcid, where, sidx, sord, inicio, limite);
        }

        public static int ListarBienesVehiculosGrillaCount(int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.BienVehiculo.ListarBienesVehiculosGrillaCount(ctcid, where, sidx, sord, inicio, limite);
        }
        public static List<Combobox> ListarMarcasVehiculo()
        {
            return dao.BienVehiculo.ListarMarcasVehiculo();
        }
        public static List<Combobox> ListarModelosVehiculo(int marcaId)
        {
            return dao.BienVehiculo.ListarModelosVehiculo(marcaId);
        }

        public static int InsertUpdateBienVehiculo(int ctcid, dto.BienVehiculo model, int user)
        {
            int result = 0;
            int idCriterio = dao.BienVehiculo.ExisteRegistro(model.VehiculoId);
            if (idCriterio > 0)//Actualizar, existe
            {
                result = dao.BienVehiculo.ActualizarBienVehiculo(model, user);
                if (result > 0 && !string.IsNullOrEmpty(model.ArchivoComprobante))
                    result = dao.BienVehiculo.InsertarVehiculoArchivo(model.VehiculoId, model.ArchivoComprobante, user);

            }
            else
            {
                result = dao.BienVehiculo.InsertarBienVehiculo(model, ctcid,user);
                if (result > 0 && !string.IsNullOrEmpty(model.ArchivoComprobante))
                    result = dao.BienVehiculo.InsertarVehiculoArchivo(result, model.ArchivoComprobante, user);

            }

            return result;
        }
    }
}
