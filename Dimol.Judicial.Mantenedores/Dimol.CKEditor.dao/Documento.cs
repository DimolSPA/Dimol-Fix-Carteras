using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;

namespace Dimol.CKEditor.dao
{
    public class Documento
    {
        public static List<Combobox> ListarDocumentos(int codemp, string area, string first)
        {
            List<Combobox> lst = new List<Combobox>();

            try
            {
                if (!string.IsNullOrEmpty(first))
                {
                    lst.Add(new Combobox()
                    {
                        Text = first,
                        Value = ""
                    });
                }

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Borradores");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("area", area);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ID"].ToString()
                        });
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static Dimol.dto.Combobox TraeXSLTBorrador(int codemp, int idBorrador)
        {
            Dimol.dto.Combobox salida = new Combobox();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Ultimo_Template");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("id_borrador", idBorrador);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        salida.Text = ds.Tables[0].Rows[i]["template"].ToString();
                        salida.Value = ds.Tables[0].Rows[i]["tipo"].ToString();
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }
        }

        public static int InsertarRolBorrador(int codemp, int rolid, int idBorrador, string html, int usrid)
        {
            int error = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_rpt_Insertar_Rol_Borrador");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("id_borrador", idBorrador);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("html", html);
                sp.AgregarParametro("usrid", usrid);
                error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return error;
        }

        public static int InsertarBorradorDemandasMasivas(int codemp, int PanelDemandaId, int TipoBorradorId, string HtmlBorrador, int usrid)
        {
            int error = -1;

            try
            {
                StoredProcedure sp = new StoredProcedure("_rpt_Insertar_DemandaMasivaConfeccion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("id_borrador", TipoBorradorId);
                sp.AgregarParametro("panelMasivoId", PanelDemandaId);
                sp.AgregarParametro("html", HtmlBorrador);
                sp.AgregarParametro("usrid", usrid);

                error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return error;
        }

        public static int InsertarBorradorDemandasPrevisional(int codemp, int PanelDemandaId, int TipoBorradorId, string HtmlBorrador, int usrid)
        {
            int error = -1;

            try
            {
                StoredProcedure sp = new StoredProcedure("_rpt_Insertar_DemandaPrevisionalConfeccion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("id_borrador", TipoBorradorId);
                sp.AgregarParametro("panelPrevisionalId", PanelDemandaId);
                sp.AgregarParametro("html", HtmlBorrador);
                sp.AgregarParametro("usrid", usrid);

                error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return error;
        }

        public static string TraeHTLMUltimoBorrador(int codemp, int idBorrador, int rolid)
        {
            string salida = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Ultimo_Borrador");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("id_borrador", idBorrador);
                sp.AgregarParametro("rolid", rolid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        salida = ds.Tables[0].Rows[i]["html"].ToString();
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }
        }

        public static string TraeHTLMUltimoBorradorDemandaMasiva(int codemp, int idBorrador, int IdDM)
        {
            string salida = "";

            try
            {
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Ultimo_BorradorDemandaMasiva");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idBorrador", idBorrador);
                sp.AgregarParametro("idDM", IdDM);

                DataSet ds = new DataSet();

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        salida = ds.Tables[0].Rows[i]["html"].ToString();
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }
        }

        public static string TraeHTLMUltimoBorradorDemandaPrevisional(int codemp, int idBorrador, int IdDP)
        {
            string salida = "";

            try
            {
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Ultimo_BorradorDemandaPrevisional");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idBorrador", idBorrador);
                sp.AgregarParametro("idDP", IdDP);

                DataSet ds = new DataSet();

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        salida = ds.Tables[0].Rows[i]["html"].ToString();
                    }
                }

                return salida;
            }
            catch (Exception ex)
            {
                return salida;
            }
        }

        public static string NumeroEnletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;
            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }
            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }
            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private static string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";

            else if (value == 1) Num2Text = "UNO";

            else if (value == 2) Num2Text = "DOS";

            else if (value == 3) Num2Text = "TRES";

            else if (value == 4) Num2Text = "CUATRO";

            else if (value == 5) Num2Text = "CINCO";

            else if (value == 6) Num2Text = "SEIS";

            else if (value == 7) Num2Text = "SIETE";

            else if (value == 8) Num2Text = "OCHO";

            else if (value == 9) Num2Text = "NUEVE";

            else if (value == 10) Num2Text = "DIEZ";

            else if (value == 11) Num2Text = "ONCE";

            else if (value == 12) Num2Text = "DOCE";

            else if (value == 13) Num2Text = "TRECE";

            else if (value == 14) Num2Text = "CATORCE";

            else if (value == 15) Num2Text = "QUINCE";

            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);

            else if (value == 20) Num2Text = "VEINTE";

            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);

            else if (value == 30) Num2Text = "TREINTA";

            else if (value == 40) Num2Text = "CUARENTA";

            else if (value == 50) Num2Text = "CINCUENTA";

            else if (value == 60) Num2Text = "SESENTA";

            else if (value == 70) Num2Text = "SETENTA";

            else if (value == 80) Num2Text = "OCHENTA";

            else if (value == 90) Num2Text = "NOVENTA";

            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);

            else if (value == 100) Num2Text = "CIEN";

            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);

            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";

            else if (value == 500) Num2Text = "QUINIENTOS";

            else if (value == 700) Num2Text = "SETECIENTOS";

            else if (value == 900) Num2Text = "NOVECIENTOS";

            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);

            else if (value == 1000) Num2Text = "MIL";

            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);

            else if (value < 1000000)
            {

                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";

                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);

            }

            else if (value == 1000000) Num2Text = "UN MILLON";

            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);

            else if (value < 1000000000000)
            {

                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";

                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);

            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";

            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {

                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";

                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            }

            return Num2Text;

        }
    }
}
