using Dimol.Carteras.dto;
using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dao
{
    public class DeudorSII
    {

        public static void ListarDatosSII(int ctcid, dto.DeudorSII d)
        {
            List<DeudorDocTimbrados> lst = new List<DeudorDocTimbrados>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("SII_Listar_Datos_Contribuyente");
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                            d.RazonSocial = ds.Tables[0].Rows[0]["NOMBRE_RAZON_SOCIAL"].ToString();
                            d.RutContribuyente = ds.Tables[0].Rows[0]["RUT"].ToString();
                            d.FechaConsulta = DateTime.Parse(string.IsNullOrEmpty(ds.Tables[0].Rows[0]["FECHA_CONSULTA"].ToString()) ? DateTime.MinValue.ToString(): ds.Tables[0].Rows[0]["FECHA_CONSULTA"].ToString());
                            d.InicioActividades = ds.Tables[0].Rows[0]["INICIO_ACTIVIDADES"].ToString();
                            d.FechaInicioAct = DateTime.Parse(string.IsNullOrEmpty(ds.Tables[0].Rows[0]["FECHA_INICIO_ACTVIDADES"].ToString()) ? DateTime.MinValue.ToString() : ds.Tables[0].Rows[0]["FECHA_INICIO_ACTVIDADES"].ToString());
                            d.ContribuyenteAutorizado = ds.Tables[0].Rows[0]["IMPUESTO_MONEDA_EXTRANJERA"].ToString();
                            d.ContribuyenteProPyme = ds.Tables[0].Rows[0]["MENOR_PRO_PYME"].ToString();
                            d.Comentario = ds.Tables[0].Rows[0]["EMISION"].ToString();
                            d.Observacion = ds.Tables[0].Rows[0]["OBSERVACION"].ToString();
                            d.Registrado = ds.Tables[0].Rows[0]["REGISTRADO"].ToString();

                        if (ds.Tables[0].Rows[0]["REGISTRADO"].ToString() == "S" && ds.Tables[0].Rows[0]["INICIO_ACTIVIDADES"].ToString() == "SI")
                        {
                            d.lstTimbrados = ListarTimbrajeSII(ctcid);
                            d.lstActividades = ListarActividadesEconomicas(ctcid);
                        }
                     
                    }
                }

               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static List<DeudorDocTimbrados> ListarTimbrajeSII(int ctcid)
        {
            List<DeudorDocTimbrados> lst = new List<DeudorDocTimbrados>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("SII_Listar_Tipo_Documento");
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DeudorDocTimbrados()
                        {
                            Documento = ds.Tables[0].Rows[i]["DOCUMENTO"].ToString(),
                            Anio = Int32.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString())
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

        public static List<DeudorActividades> ListarActividadesEconomicas(int ctcid)
        {
            List<DeudorActividades> lst = new List<DeudorActividades>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("SII_Listar_Actividades_Economicas");
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DeudorActividades()
                        {
                            Actividades = ds.Tables[0].Rows[i]["ACTIVIDAD"].ToString(),
                            Codigo = Int32.Parse(ds.Tables[0].Rows[i]["CODIGO_ACTIVIDAD"].ToString()),
                            Categoria = ds.Tables[0].Rows[i]["CATEGORIA"].ToString(),
                            AfectaIVA = ds.Tables[0].Rows[i]["AFECTO_IVA"].ToString()
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
    }
}
