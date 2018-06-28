using Dimol.dao;
using Dimol.dto;
using Dimol.Judicial.Mantenedores.dto;
using System;
using System.Data;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class PanelDemandaMasiva
    {
        #region "Borradores"
        public static Dimol.dto.Combobox HistoriaBorradorDemandasMasivas(int codemp, int IdDM, int TipoBorrador)
        {
            Dimol.dto.Combobox salida = new Combobox();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Historia_BorradoresDemandaMasiva");
                sp.AgregarParametro("codEmp", codemp);
                sp.AgregarParametro("idDM", IdDM);
                sp.AgregarParametro("idBorrador", TipoBorrador);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    salida.Text = ds.Tables[0].Rows[0]["Creacion"].ToString();
                    salida.Value = ds.Tables[0].Rows[0]["Ultimo"].ToString();
                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }
        }
        #endregion

        public static PanelDemandaMasivaDetalle ObtenerPanelDemandaMasivaDetalle(int IdDM)
        {
            PanelDemandaMasivaDetalle salida = new PanelDemandaMasivaDetalle();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Obtener_Panel_Demanda_Masiva_Detalle_PorIdPanel");
                sp.AgregarParametro("IdPanel", IdDM);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    salida.PanelId = int.Parse(ds.Tables[0].Rows[0]["ID_PANEL_MASIVO"].ToString());
                    salida.UsrEncargado = int.Parse(ds.Tables[0].Rows[0]["USRID_ENCARGADO"].ToString());
                    salida.FechaEnvio = DateTime.Parse(ds.Tables[0].Rows[0]["FEC_ENVIO"].ToString());

                    if (ds.Tables[0].Rows[0]["FEC_ENTREGA"].ToString() != "")
                    {
                        salida.FechaEntrega = DateTime.Parse(ds.Tables[0].Rows[0]["FEC_ENTREGA"].ToString());
                    }

                    if (ds.Tables[0].Rows[0]["FEC_INGRESO_TRIBUNAL"].ToString() != "")
                    {
                        salida.FechaIngresoTribunal = DateTime.Parse(ds.Tables[0].Rows[0]["FEC_INGRESO_TRIBUNAL"].ToString());
                    }

                    salida.RolAdjudicado = ds.Tables[0].Rows[0]["ROL_ADJUDICADO"].ToString();
                    salida.RolId = int.Parse(ds.Tables[0].Rows[0]["ROLID"].ToString());
                    salida.Comentarios = ds.Tables[0].Rows[0]["COMENTARIOS"].ToString();
                    salida.UsrIdRegistro = int.Parse(ds.Tables[0].Rows[0]["USRID_REGISTRO"].ToString());

                    if (ds.Tables[0].Rows[0]["FEC_REGISTRO"].ToString() != "")
                    {
                        salida.FechaRegistro = DateTime.Parse(ds.Tables[0].Rows[0]["FEC_REGISTRO"].ToString());
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }
        }
    }
}