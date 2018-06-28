using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.ArchivoCopec.dao
{
    public class ArchivoCopec
    {
        public static List<Dimol.ArchivoCopec.dto.ArchivoCopecGestiones> ListarGestionesCopec(int pclid)
        {
            List<Dimol.ArchivoCopec.dto.ArchivoCopecGestiones> lst = new List<Dimol.ArchivoCopec.dto.ArchivoCopecGestiones>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Copec_Gestiones");
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        lst.Add(new Dimol.ArchivoCopec.dto.ArchivoCopecGestiones()
                        {
                            RutCliente = ds.Tables[0].Rows[i]["Rut_Cliente"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["ROL"].ToString(),
                            Tribunal = ds.Tables[0].Rows[i]["TRIBUNAL"].ToString(),
                            GestionRealizada = ds.Tables[0].Rows[i]["Gestion_Realizada"].ToString(),
                            FechaGestion = ds.Tables[0].Rows[i]["Fecha_Gestion"].ToString(),
                            RutGestion = ds.Tables[0].Rows[i]["Rut_Gestion"].ToString()
                        });
                    }
                    catch (Exception e)
                    {
                        Dimol.dao.Funciones.InsertarError("ListarGestionesCopec " + e.Message, e.StackTrace, "Bot ListarGestionesCopec", 0);
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "ListarGestionesCopec", 0);
                return lst;
            }
        }

        public static List<Dimol.ArchivoCopec.dto.ArchivoCopec> ListarJuiciosCopec(int pclid)
        {
            List<Dimol.ArchivoCopec.dto.ArchivoCopec> lst = new List<Dimol.ArchivoCopec.dto.ArchivoCopec>();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Copec_Juicios");
                sp.AgregarParametro("pclid", pclid);
                ds = sp.EjecutarProcedimiento();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        lst.Add(new Dimol.ArchivoCopec.dto.ArchivoCopec()
                        {
                            CuentaInterna = ds.Tables[0].Rows[i]["Cuenta_Interna"].ToString(),
                            RutCliente = ds.Tables[0].Rows[i]["Rut_Cliente"].ToString(),
                            TipoPersona = ds.Tables[0].Rows[i]["Tipo_Persona"].ToString(),
                            RazonSocial = ds.Tables[0].Rows[i]["Razon_Social"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            ApellidoPaterno = ds.Tables[0].Rows[i]["Apellido_Paterno"].ToString(),
                            ApellidoMaterno = ds.Tables[0].Rows[i]["Apellido_Materno"].ToString(),
                            TipoCliente = ds.Tables[0].Rows[i]["Tipo_Cliente"].ToString(),
                            TotalDeuda = ds.Tables[0].Rows[i]["Total_Deuda"].ToString(),
                            MonedaTotalDeuda = ds.Tables[0].Rows[i]["Moneda_Total_Deuda"].ToString(),
                            FechaEnvio = ds.Tables[0].Rows[i]["Fecha_Envio"].ToString(),
                            TipoCobranzaDocumento = ds.Tables[0].Rows[i]["Tipo_Cobranza_Documento"].ToString(),
                            TipoJuicio = ds.Tables[0].Rows[i]["Tipo_Juicio"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["Rol"].ToString(),
                            Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString(),
                            EtapaJuicio = ds.Tables[0].Rows[i]["Etapa_Juicio"].ToString(),
                            EstadoJuicio = ds.Tables[0].Rows[i]["Estado_Juicio"].ToString(),
                            UltimaGestion = ds.Tables[0].Rows[i]["Ultima_Gestion_Judicial"].ToString(),
                            FechaUltimaGestion = ds.Tables[0].Rows[i]["Fecha_Ultima_Gestion_Judicial"].ToString(),
                            RutAbogado = ds.Tables[0].Rows[i]["Rut_Abogado"].ToString(),
                            NombreAbogado = ds.Tables[0].Rows[i]["Nombre_Abogado"].ToString(),
                            FechaInicioJuicio = ds.Tables[0].Rows[i]["Fecha_Inicio_Juicio"].ToString(),
                            FechaNotificacion = ds.Tables[0].Rows[i]["Fecha_Notificacion"].ToString(),
                            FechaEmbargo = ds.Tables[0].Rows[i]["Fecha_Embargo"].ToString(),
                            FechaRemate = ds.Tables[0].Rows[i]["Fecha_Remate"].ToString(),
                            FechaIncitacion = ds.Tables[0].Rows[i]["Fecha_Incitacion"].ToString(),
                            FechaFinJuicio = ds.Tables[0].Rows[i]["Fecha_Fin_Juicio"].ToString(),
                            RutEjecutivo = ds.Tables[0].Rows[i]["Rut_Ejecutivo"].ToString(),
                            NombreEjecutivo = ds.Tables[0].Rows[i]["Nombre_Ejecutivo"].ToString(),
                            ObservacionesCopec = ds.Tables[0].Rows[i]["Observaciones_Copec"].ToString(),
                            ObservacionesOficina = ds.Tables[0].Rows[i]["Observaciones_Oficina"].ToString(),
                            Localidad = ds.Tables[0].Rows[i]["Localidad"].ToString(),
                            NumeroCuota = ds.Tables[0].Rows[i]["Numero_Cuota"].ToString(),
                            Sociedad = ds.Tables[0].Rows[i]["Sociedad"].ToString(),
                            Anio = ds.Tables[0].Rows[i]["Anio"].ToString(),
                            NumeroDocumento = ds.Tables[0].Rows[i]["Numero_Documento"].ToString(),
                            RolPadre = ds.Tables[0].Rows[i]["Rol_Padre"].ToString(),
                            TribunalPadre = ds.Tables[0].Rows[i]["Tribunal_Padre"].ToString()
                        });
                    }
                    catch (Exception e)
                    {
                        Dimol.dao.Funciones.InsertarError("ListarJuiciosCopec " + e.Message, e.StackTrace, "Bot ListarJuiciosCopec", 0);
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "ListarJuiciosCopec", 0);
                return lst;
            }
        }
    }
}
