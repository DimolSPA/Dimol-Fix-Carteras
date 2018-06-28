using System.Collections.Generic;
using Dimol.dto;
namespace Dimol.Carteras.bcp
{
    public class BienPropiedad
    {
        public static List<dto.BienPropiedad> ListarBienesRaicesGrilla(int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.BienPropiedad.ListarBienesRaicesGrilla(ctcid, where, sidx, sord, inicio, limite);
        }

        public static int ListarBienesRaicesGrillaCount(int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.BienPropiedad.ListarBienesRaicesGrillaCount(ctcid, where, sidx, sord, inicio, limite);
        }
        public static dto.BienDetalle DetalleBienes(int ctcid)
        {
            return dao.BienPropiedad.DetelleBienes(ctcid);
        }
        public static List<Combobox> ListarConservadorBienes()
        {
            return dao.BienPropiedad.ListarConservadorBienes();
        }
        public static int InsertUpdateBienPropiedad(int ctcid, dto.BienPropiedad model, int user)
        {
            int result = 0;
            int idCriterio = dao.BienPropiedad.ExisteRegistro(model.BienesRaicesId);
            if (idCriterio > 0)//Actualizar, existe
            {
                result = dao.BienPropiedad.ActualizarBienPropiedad(model, user);
                if (result > 0 && !string.IsNullOrEmpty(model.ArchivoCertificado))
                    result = dao.BienPropiedad.InsertarPropiedadArchivo(model.BienesRaicesId, model.ArchivoCertificado, user);

            }
            else
            {
                result = dao.BienPropiedad.InsertarBienPropiedad(model, ctcid, user);
                if (result > 0 && !string.IsNullOrEmpty(model.ArchivoCertificado))
                    result = dao.BienPropiedad.InsertarPropiedadArchivo(result, model.ArchivoCertificado, user);

            }

            return result;
        }
    }
}
