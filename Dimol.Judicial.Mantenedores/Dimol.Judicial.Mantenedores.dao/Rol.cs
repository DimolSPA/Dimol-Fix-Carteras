using Dimol.dao;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;

namespace Dimol.Judicial.Mantenedores.dao
{
    public class Rol
    {
        public static List<dto.Rol> ListarRolesGrilla(int codemp, int idioma, int idCompetencia, string rol_numero, int? rol_trbid, int? rol_tcaid, int? rol_pclid, string ctc_rut, string ctc_nomfant, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Rol> lst = new List<dto.Rol>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Roles_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("idCompetencia", idCompetencia);
                sp.AgregarParametro("rol_numero", rol_numero);
                sp.AgregarParametro("rol_trbid", rol_trbid);
                sp.AgregarParametro("rol_tcaid", rol_tcaid);
                sp.AgregarParametro("rol_pclid", rol_pclid);
                sp.AgregarParametro("ctc_rut", ctc_rut);
                sp.AgregarParametro("ctc_nomfant", ctc_nomfant);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Rol()
                        {
                            rol_rolid = Int32.Parse(ds.Tables[0].Rows[i]["rol_rolid"].ToString()),
                            rol_numero = ds.Tables[0].Rows[i]["rol_numero"].ToString(),
                            trb_nombre = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                            pcl_nomfant = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString(),
                            ctc_rut = ds.Tables[0].Rows[i]["ctc_rut"].ToString(),
                            ctc_nomfant = ds.Tables[0].Rows[i]["ctc_nomfant"].ToString()
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

        public static int ListarRolesGrillaCount(int codemp, int idioma, int idCompetencia, string rol_numero, int? rol_trbid, int? rol_tcaid, int? rol_pclid, string ctc_rut, string ctc_nomfant, string where, string sidx, string sord)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Roles_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("idCompetencia", idCompetencia);
                sp.AgregarParametro("rol_numero", rol_numero);
                sp.AgregarParametro("rol_trbid", rol_trbid);
                sp.AgregarParametro("rol_tcaid", rol_tcaid);
                sp.AgregarParametro("rol_pclid", rol_pclid);
                sp.AgregarParametro("ctc_rut", ctc_rut.Trim());
                sp.AgregarParametro("ctc_nomfant", ctc_nomfant.Trim());
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static List<Combobox> ListarTiposCausa(int codemp, int idioma, string first)
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
                StoredProcedure sp = new StoredProcedure("_Listar_TiposCausa");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["tci_nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["tci_tcaid"].ToString()
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

        public static List<Combobox> ListarTribunales(int codemp, int idCompetencia, string first)
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
                StoredProcedure sp = new StoredProcedure("_Listar_TribunalesPorIdCompetencia");
                sp.AgregarParametro("codEmp", codemp);
                sp.AgregarParametro("idCompetencia", idCompetencia);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["TRB_NOMBRE"].ToString(),
                            Value = ds.Tables[0].Rows[i]["TRB_TRBID"].ToString()
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

        public static List<Combobox> ListarTribunales(int codemp, string first)
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
                StoredProcedure sp = new StoredProcedure("_Listar_Tribunales");
                sp.AgregarParametro("codEmp", codemp);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["TRB_NOMBRE"].ToString(),
                            Value = ds.Tables[0].Rows[i]["TRB_TRBID"].ToString()
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

        public static List<Combobox> ListarClientes(int codemp, int idioma, string first)
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
                StoredProcedure sp = new StoredProcedure("_Listar_Clientes");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString(),
                            Value = ds.Tables[0].Rows[i]["rol_pclid"].ToString()
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

        public static List<Combobox> ListarProvCli(int codemp, string first)
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
                StoredProcedure sp = new StoredProcedure("_Listar_ProvCli");
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString(),
                            Value = ds.Tables[0].Rows[i]["etj_etjid"].ToString()
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

        public static List<Combobox> ListarDeudores(int codemp, string first)
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
                StoredProcedure sp = new StoredProcedure("_Listar_Deudores");
                sp.AgregarParametro("codemp", codemp);
                //sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Dimol.dto.Combobox()
                        {
                            Text = ds.Tables[0].Rows[i]["ctc_nombre"].ToString(),
                            Value = ds.Tables[0].Rows[i]["ctc_numero"].ToString()
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

        public static int InsertarRol(dto.Rol objRol, Dimol.dto.UserSession objSession)
        {
            int id = -1;

            try {
                Funciones func = new Funciones();
                int estid = func.ConfiguracionEmpNum(objSession.CodigoEmpresa, 25);
                int matjud = func.ConfiguracionEmpNum(objSession.CodigoEmpresa, 24);

                StoredProcedure sp = new StoredProcedure("_Insertar_Rol");
                sp.AgregarParametro("rol_codemp", objSession.CodigoEmpresa);
                sp.AgregarParametro("rol_pclid", objRol.rol_pclid); //Cliente
                sp.AgregarParametro("rol_ctcid", objRol.rol_ctcid); //Deudor
                sp.AgregarParametro("rol_trbid", objRol.rol_trbid); //Tribunal
                sp.AgregarParametro("rol_tcaid", objRol.rol_tcaid); //Tipo causa
                sp.AgregarParametro("rol_estid", estid); //Estado

                //sp.AgregarParametro("rol_fecdem", DBNull.Value);
                if (objRol.rol_fecrol != null)
                {
                    sp.AgregarParametro("rol_fecrol", DateTime.Parse( objRol.rol_fecrol));
                }
                //else {
                //    sp.AgregarParametro("rol_fecrol", DBNull.Value);
                //}

                //sp.AgregarParametro("rol_comentario", (object)objRol.rol_comentario ?? DBNull.Value);
                if ((object)objRol.rol_comentario != null)
                {
                    sp.AgregarParametro("rol_comentario", objRol.rol_comentario);
                }
                
                sp.AgregarParametro("rol_esjid", matjud); //Materia judicial

                if (objRol.rol_fecjud != null)
                {
                    sp.AgregarParametro("rol_fecjud",  DateTime.Parse(objRol.rol_fecjud));
                }
                //else {
                //    sp.AgregarParametro("rol_fecjud", DBNull.Value);
                //}
                sp.AgregarParametro("rol_tipo_rol", objRol.rol_tipo_rol);
                sp.AgregarParametro("rol_numero", objRol.rol_numero);
                sp.AgregarParametro("rle_usrid", objSession.UserId);
                sp.AgregarParametro("rle_ipred", objSession.IpRed);
                sp.AgregarParametro("rle_ipmaquina", objSession.IpPc);
                sp.AgregarParametro("rle_comentario", "Ingreso ROL");

                DataSet ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    id = Int32.Parse(ds.Tables[0].Rows[0]["rolid"].ToString());
                }
            } catch (Exception ex) {
                return id;
            }

            return id;
        }

        public static int EditarRol(dto.Rol objRol, Dimol.dto.UserSession objSession)
        {
            int id = -1;
            try
            {
                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("_Update_Rol");
                sp.AgregarParametro("rol_codemp", objSession.CodigoEmpresa);
                sp.AgregarParametro("rol_rolid", objRol.rol_rolid);//proveedor cliente
                sp.AgregarParametro("rol_pclid", objRol.rol_pclid);//proveedor cliente
                sp.AgregarParametro("rol_ctcid", objRol.rol_ctcid);//deudor cliente
                sp.AgregarParametro("rol_numero", objRol.rol_numero);
                sp.AgregarParametro("rol_trbid", objRol.rol_trbid);//tribunal
                sp.AgregarParametro("rol_tcaid", objRol.rol_tcaid);//tipo causa
                sp.AgregarParametro("rol_fecdem", string.IsNullOrEmpty(objRol.rol_fecdem)?DBNull.Value:(object)DateTime.Parse(objRol.rol_fecdem ));
                sp.AgregarParametro("rol_fecrol", string.IsNullOrEmpty(objRol.rol_fecrol)?DBNull.Value:(object)DateTime.Parse(objRol.rol_fecrol ));
                sp.AgregarParametro("rol_comentario", (object)objRol.rol_comentario ?? DBNull.Value);
                sp.AgregarParametro("rol_bloqueo", objRol.rol_bloqueo);
                sp.AgregarParametro("rol_prequiebra",  objRol.rol_prequiebra);
                sp.AgregarParametro("rol_tipo_rol", objRol.rol_tipo_rol);

                id = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return id;
            }
            return id;
        }

        public static int ActualilzarQuiebraDeudor(int codemp, int ctcid, string quiebra)
        {
            int id = -1;
            try
            {
                Funciones func = new Funciones();
                StoredProcedure sp = new StoredProcedure("Update_Deudores_Quiebra");
                sp.AgregarParametro("ctc_codemp", codemp);
                sp.AgregarParametro("ctc_ctcid", ctcid);//deudor cliente
                sp.AgregarParametro("ctc_quiebra",quiebra);
                id = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return id;
            }
            return id;
        }

        public static dto.RolCarga BuscarRol(int codemp, int rolid, int idioma)
        {
            dto.RolCarga obj = new dto.RolCarga();
            DateTime fechaDemanda = new DateTime();
            DateTime fechaIngreso = new DateTime();
            DateTime FechaRol = new DateTime();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("idioma", idioma);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime.TryParse(ds.Tables[0].Rows[i]["rol_fecdem"].ToString(), out fechaDemanda);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["rol_fecemi"].ToString(), out fechaIngreso);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["rol_fecrol"].ToString(), out FechaRol);
                        obj.Cliente = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString();
                        obj.RutCliente = ds.Tables[0].Rows[i]["pcl_rut"].ToString();
                        obj.Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["rol_ctcid"].ToString());
                        obj.RutDeudor = ds.Tables[0].Rows[i]["ctc_rut"].ToString();
                        obj.BloquearRol = ds.Tables[0].Rows[i]["rol_bloqueo"].ToString() == "S" ? true : false;
                        obj.Comentario = ds.Tables[0].Rows[i]["rol_comentario"].ToString();
                        obj.NombreDeudor = ds.Tables[0].Rows[i]["ctc_nomfant"].ToString();
                        obj.Estado = ds.Tables[0].Rows[i]["eci_nombre"].ToString();
                        obj.FechaDemanda = fechaDemanda;
                        obj.FechaIngreso = fechaIngreso;
                        obj.FechaRol = FechaRol;
                        obj.MateriaJudicial = ds.Tables[0].Rows[i]["mji_nombre"].ToString();
                        obj.Pclid = Int32.Parse(ds.Tables[0].Rows[i]["rol_pclid"].ToString());
                        obj.IdEstado = Int32.Parse(ds.Tables[0].Rows[i]["rol_estid"].ToString());
                        obj.IdMateriaJudicial = Int32.Parse(ds.Tables[0].Rows[i]["rol_esjid"].ToString());
                        obj.IdTipoCausa = Int32.Parse(ds.Tables[0].Rows[i]["rol_tcaid"].ToString());
                        obj.IdTribunal = Int32.Parse(ds.Tables[0].Rows[i]["rol_trbid"].ToString());
                        obj.ProcesoQuiebra = ds.Tables[0].Rows[i]["rol_prequiebra"].ToString() == "S" ? true : false;
                        obj.Rol = ds.Tables[0].Rows[i]["rol_numero"].ToString();
                        obj.TipoCausa = ds.Tables[0].Rows[i]["tci_nombre"].ToString();
                        obj.TipoRol = ds.Tables[0].Rows[i]["rol_tipo_rol"].ToString();
                        obj.Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString();
                        obj.Rolid = Int32.Parse(ds.Tables[0].Rows[i]["rol_rolid"].ToString());
                        obj.Quiebra = ds.Tables[0].Rows[i]["ctc_quiebra"].ToString() == "S" ? true : false;
                        obj.IdCompetencia = Int32.Parse(ds.Tables[0].Rows[i]["ID_COMPETENCIA"].ToString());
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                return obj;
            }
        }

        public static void BuscarRolDemandaAvenimiento(int codemp, int rolid, dto.DemandaAvenimiento Demanda, dto.DemandaAvenimiento Avenimiento)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            DateTime fechaAvenimiento = new DateTime();
            DateTime fechaAvenimientoPrimeraCuota = new DateTime();
            DateTime fechaAvenimientoUltimaCuota = new DateTime();

            DateTime fechaDemanda = new DateTime();
            DateTime fechaDemandaPrimeraCuota = new DateTime();
            DateTime fechaDemandaUltimaCuota = new DateTime();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Buscar_Rol_Demanda_Avenimiento");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int i = 0;
                        DateTime.TryParse(ds.Tables[0].Rows[i]["RAD_FECAVE"].ToString(), out fechaAvenimiento);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["RAD_FECPCOUAVE"].ToString(), out fechaAvenimientoPrimeraCuota);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["RAD_FECUCOUAVE"].ToString(), out fechaAvenimientoUltimaCuota);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["RAD_FECDEM"].ToString(), out fechaDemanda);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["RAD_FECPCOUDEM"].ToString(), out fechaDemandaPrimeraCuota);
                        DateTime.TryParse(ds.Tables[0].Rows[i]["RAD_FECUCOUDEM"].ToString(), out fechaDemandaUltimaCuota);
                        Avenimiento.Fecha = fechaAvenimiento;
                        Avenimiento.FechaPrimeraCuota = fechaAvenimientoPrimeraCuota;
                        Avenimiento.FechaUltimaCuota = fechaAvenimientoUltimaCuota;
                        Avenimiento.Monto = decimal.Parse(ds.Tables[0].Rows[i]["RAD_MONAVE"].ToString());
                        Avenimiento.Cuotas = Int32.Parse(ds.Tables[0].Rows[i]["RAD_CUOAVE"].ToString());
                        Avenimiento.MontoCuota = decimal.Parse(ds.Tables[0].Rows[i]["RAD_MONPCOUAVE"].ToString());
                        Avenimiento.MontoUltimaCuota = decimal.Parse(ds.Tables[0].Rows[i]["RAD_MONUCOUAVE"].ToString());
                        Avenimiento.Interes = decimal.Parse(ds.Tables[0].Rows[i]["RAD_INTAVE"].ToString());

                        Demanda.Fecha = fechaDemanda;
                        Demanda.FechaPrimeraCuota = fechaDemandaPrimeraCuota;
                        Demanda.FechaUltimaCuota = fechaDemandaUltimaCuota;
                        Demanda.Monto = decimal.Parse(ds.Tables[0].Rows[i]["RAD_MONDEM"].ToString());
                        Demanda.Cuotas = Int32.Parse(ds.Tables[0].Rows[i]["RAD_CUODEM"].ToString());
                        Demanda.MontoCuota = decimal.Parse(ds.Tables[0].Rows[i]["RAD_MONPCUODEM"].ToString());
                        Demanda.MontoUltimaCuota = decimal.Parse(ds.Tables[0].Rows[i]["RAD_MONUCOUDEM"].ToString());
                        Demanda.Interes = decimal.Parse(ds.Tables[0].Rows[i]["RAD_INTDEM"].ToString());

                    }
                }


            }
            catch (Exception ex)
            {

            }
        }

        public static int InsertarDemandaAvenimientoRol(int codemp, int rolid, dto.DemandaAvenimiento Demanda, dto.DemandaAvenimiento Avenimiento)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Insertar_Rol_AveDem");
                sp.AgregarParametro("rad_codemp", codemp);
                sp.AgregarParametro("rad_rolid", rolid);

                sp.AgregarParametro("rad_fecdem", Demanda.Fecha == new DateTime() ? DBNull.Value : (object)Demanda.Fecha);
                sp.AgregarParametro("rad_cuodem", Demanda.Cuotas);
                sp.AgregarParametro("rad_mondem", Demanda.Monto);
                sp.AgregarParametro("rad_monpcuodem", Demanda.MontoCuota);
                sp.AgregarParametro("rad_monucoudem", Demanda.MontoUltimaCuota);
                sp.AgregarParametro("rad_fecpcoudem", Demanda.FechaPrimeraCuota == new DateTime() ? DBNull.Value : (object)Demanda.FechaPrimeraCuota);
                sp.AgregarParametro("rad_fecucoudem", Demanda.FechaUltimaCuota == new DateTime() ? DBNull.Value : (object)Demanda.FechaUltimaCuota);
                sp.AgregarParametro("rad_intdem", Demanda.Interes);
                sp.AgregarParametro("rad_fecave", Avenimiento.Fecha == new DateTime() ? DBNull.Value : (object)Avenimiento.Fecha);
                sp.AgregarParametro("rad_cuoave", Avenimiento.Cuotas);
                sp.AgregarParametro("rad_monave", Avenimiento.Monto);
                sp.AgregarParametro("rad_monpcouave", Avenimiento.MontoCuota);
                sp.AgregarParametro("rad_monucouave", Avenimiento.MontoUltimaCuota);
                sp.AgregarParametro("rad_fecpcouave", Avenimiento.FechaPrimeraCuota == new DateTime() ? DBNull.Value : (object)Avenimiento.FechaPrimeraCuota);
                sp.AgregarParametro("rad_fecucouave", Avenimiento.FechaUltimaCuota == new DateTime() ? DBNull.Value : (object)Avenimiento.FechaUltimaCuota);
                sp.AgregarParametro("rad_intave", Avenimiento.Interes);

                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ActualizarDemandaAvenimientoRol(int codemp, int rolid, dto.DemandaAvenimiento demanda, dto.DemandaAvenimiento avenimiento)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Update_Rol_AveDem");
                sp.AgregarParametro("rad_codemp", codemp);
                sp.AgregarParametro("rad_rolid", rolid);

                sp.AgregarParametro("rad_fecdem", demanda.Fecha == new DateTime() ? DBNull.Value : (object)demanda.Fecha);
                sp.AgregarParametro("rad_cuodem", demanda.Cuotas);
                sp.AgregarParametro("rad_mondem", demanda.Monto);
                sp.AgregarParametro("rad_monpcuodem", demanda.MontoCuota);
                sp.AgregarParametro("rad_monucoudem", demanda.MontoUltimaCuota);
                sp.AgregarParametro("rad_fecpcoudem", demanda.FechaPrimeraCuota == new DateTime() ? DBNull.Value : (object)demanda.FechaPrimeraCuota);
                sp.AgregarParametro("rad_fecucoudem", demanda.FechaUltimaCuota == new DateTime() ? DBNull.Value : (object)demanda.FechaUltimaCuota);
                sp.AgregarParametro("rad_intdem", demanda.Interes);
                sp.AgregarParametro("rad_fecave", avenimiento.Fecha == new DateTime() ? DBNull.Value : (object)avenimiento.Fecha);
                sp.AgregarParametro("rad_cuoave", avenimiento.Cuotas);
                sp.AgregarParametro("rad_monave", avenimiento.Monto);
                sp.AgregarParametro("rad_monpcouave", avenimiento.MontoCuota);
                sp.AgregarParametro("rad_monucouave", avenimiento.MontoUltimaCuota);
                sp.AgregarParametro("rad_fecpcouave", avenimiento.FechaPrimeraCuota == new DateTime() ? DBNull.Value : (object)avenimiento.FechaPrimeraCuota);
                sp.AgregarParametro("rad_fecucouave", avenimiento.FechaUltimaCuota == new DateTime() ? DBNull.Value : (object)avenimiento.FechaUltimaCuota);
                sp.AgregarParametro("rad_intave", avenimiento.Interes);

                int error = sp.EjecutarProcedimientoTrans();

                return error;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ExisteDemandaAvenimientoRol(int codemp, int rolid)
        {
            int id = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Existe_Aven_Dem_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    id = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                }

                return id;
            }
            catch (Exception ex)
            {
                return id;
            }
        }

        public static List<Autocomplete> ListarRutNombreDeudor(string nombre)
        {
            List<Autocomplete> lst = new List<Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rut_Nombre_Deudor_Rol");
                sp.AgregarParametro("texto", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }

        public static List<dto.DocumentoRol> ListarDocumentosAsignadosGrilla(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentoRol> lst = new List<dto.DocumentoRol>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Doc_Asig_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DocumentoRol()
                        {
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["estcpbt"].ToString()
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

        public static List<dto.DocumentoRol> ListarDocumentosPorNumeroResolucion(string NumeroResolucion)
        {
            List<dto.DocumentoRol> lst = new List<dto.DocumentoRol>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Cartera_Clientes_Cpbt_Documento_Por_Numero_Resolucion");
                sp.AgregarParametro("numResolucion", NumeroResolucion);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DocumentoRol()
                        {
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            //FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            //Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            //Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString())
                            //Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString(),
                            //Estado = ds.Tables[0].Rows[i]["estcpbt"].ToString()
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

        public static List<dto.DocumentoRol> ListarDocumentosAsignadosGrillaPrevisional(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentoRol> lst = new List<dto.DocumentoRol>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Doc_Asig_Grilla_GroupByResolucion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DocumentoRol()
                        {
                            Resolucion = ds.Tables[0].Rows[i]["Resolucion"].ToString(),
                            FechaResolucion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaResolucion"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString())
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

        public static List<dto.DocumentoEstampe> ListarDocumentosEstampesGrilla(int codemp, int pclid, int ctcid, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentoEstampe> lst = new List<dto.DocumentoEstampe>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Doc_Estampes_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DocumentoEstampe
                        {
                            Ddeid = Int32.Parse(ds.Tables[0].Rows[i]["ID"].ToString()),
                            Codemp = codemp,
                            Pclid = pclid,
                            Ctcid = ctcid,
                            Rolid = rolid,
                            FechaJudicial = ds.Tables[0].Rows[i]["FECHAJUD"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["NOMBRE"].ToString(),
                            NombreInsumo = ds.Tables[0].Rows[i]["INSUMO"].ToString(),
                            Usuario = ds.Tables[0].Rows[i]["USUARIO"].ToString()
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

        public static int ListarDocumentosAsignadosGrillaCount(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Doc_Asig_Grilla_count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static int ListarDocumentosAsignadosGrillaCountPrevisional(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Doc_Asig_Grilla_Count_GroupByResolucion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);

                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static int ListarDocumentosEstampesGrillaCount(int codemp, int pclid, int ctcid, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Doc_Estampes_Grilla_count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static List<dto.DocumentoRol> ListarDocumentosPorAsignarGrilla(int codemp, int idioma, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentoRol> lst = new List<dto.DocumentoRol>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Doc_No_Asig_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DocumentoRol()
                        {
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["FechaVencimiento"].ToString()),
                            Moneda = ds.Tables[0].Rows[i]["Moneda"].ToString(),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString(),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString()),
                            Tipo = ds.Tables[0].Rows[i]["Tipo"].ToString()
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

        public static List<dto.DocumentoRol> ListarDocumentosPorAsignarGrillaPrevisonal(int codemp, int idioma, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.DocumentoRol> lst = new List<dto.DocumentoRol>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Doc_No_Asig_Grilla_GroupByResolucion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.DocumentoRol()
                        {
                            Resolucion = ds.Tables[0].Rows[i]["Resolucion"].ToString(),
                            FechaResolucion = DateTime.Parse(ds.Tables[0].Rows[i]["FechaResolucion"].ToString()),
                            Monto = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString()),
                            Saldo = decimal.Parse(ds.Tables[0].Rows[i]["Saldo"].ToString())
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

        public static int ListarDocumentosPorAsignarGrillaCount(int codemp, int idioma, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Doc_No_Asig_Grilla_count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static int ListarDocumentosPorAsignarGrillaCountPrevisional(int codemp, int idioma, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Doc_No_Asig_Grilla_Count_GroupByResolucion");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static List<dto.EstadosRol> ListarEstadosRolGrilla(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.EstadosRol> lst = new List<dto.EstadosRol>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Estados_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.EstadosRol()
                        {
                            Id = Int32.Parse(ds.Tables[0].Rows[i]["row"].ToString()),
                            Archivo = ds.Tables[0].Rows[i]["Archivo"].ToString(),
                            Comentario = ds.Tables[0].Rows[i]["Comentario"].ToString(),
                            Cuaderno = ds.Tables[0].Rows[i]["Cuaderno"].ToString(),
                            Estado = ds.Tables[0].Rows[i]["Estado"].ToString(),
                            FechaJudicial = DateTime.Parse(ds.Tables[0].Rows[i]["FechaJudicial"].ToString()),
                            Materia = ds.Tables[0].Rows[i]["Materia"].ToString(),
                            Usuario = ds.Tables[0].Rows[i]["Usuario"].ToString(),
                            IdEstado = Int32.Parse(ds.Tables[0].Rows[i]["IdEstado"].ToString()),
                            IdMateria= Int32.Parse(ds.Tables[0].Rows[i]["IdMateria"].ToString()),
                            Fecha= Convert.ToDateTime(ds.Tables[0].Rows[i]["Fecha"]),//DateTime.Parse(ds.Tables[0].Rows[i]["Fecha"].ToString()),
                            Rolid = Int32.Parse(ds.Tables[0].Rows[i]["Rolid"].ToString())
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

        public static int ListarEstadosRolGrillaCount(int codemp, int idioma, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Estados_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idid", idioma);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static int ActualizarParaPoderJudicial(int codemp, int rolId, bool enActualizarPoderJudicial)
        {
            int result = -1;
            try
            {

                StoredProcedure sp = new StoredProcedure("_Insertar_Rol_Actualiza_PoderJudicial");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolId);
                sp.AgregarParametro("actualizarPoderJudicial", (enActualizarPoderJudicial) ? "S" : "N");
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

        public static int ActualizarParaPoderJudicialHistorial(int codemp, int rolId, bool enActualizarPoderJudicial, int userId)
        {
            int result = -1;
            try
            {

                StoredProcedure sp = new StoredProcedure("_Insertar_Rol_Actualiza_PoderJudicial_Historial");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolId);
                sp.AgregarParametro("actualizarPoderJudicial", (enActualizarPoderJudicial) ? "S" : "N");
                sp.AgregarParametro("userId", userId);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

        public static bool TraeActualizarPoderJudicial(int codemp, int rolId)
        {
            bool flag = false;
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Rol_Actualiza_PoderJudicial");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    flag = (ds.Tables[0].Rows[0]["actualizaPoderJudicial"].ToString() == "S" ? true : false);
                }

                return flag;
            }
            catch (Exception ex)
            {
                return flag;
            }
        }

        public static int GuardarDocumentoEstampe(int codemp, int pclid, int ctcid, int rolid, string path, string nombre, string ext, string fecjud, int usrid)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Insertar_Deudores_Estampes");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("path", (object)path ?? DBNull.Value);
                sp.AgregarParametro("nombre", (object)nombre ?? DBNull.Value);
                sp.AgregarParametro("ext", (object)ext ?? DBNull.Value);
                sp.AgregarParametro("fecjud", (object)fecjud ?? DBNull.Value);
                sp.AgregarParametro("usrid", (object)usrid ?? 0);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    return Int32.Parse(ds.Tables[0].Rows[0]["DDE_DDEID"].ToString());
                }
                else
                {
                    return -1;
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        #region "Rol Estados"

        public static int InsertarEstadoRol(int codemp, int rolid, int estid, int esjid, int usrid, string ipred, string ipmaquina, string comentario, DateTime fecjud, int codsuc, int gesid)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo; 
            StoredProcedure sp = new StoredProcedure("_Insertar_Rol_Estados");
            int error = 1;
            try
            {
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("esjid", esjid);
                sp.AgregarParametro("usrid", usrid);
                sp.AgregarParametro("ipred", ipred);
                sp.AgregarParametro("ipmaquina", ipmaquina);
                sp.AgregarParametro("comentario", comentario);
                sp.AgregarParametro("fecjud", fecjud);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("gesid", gesid);

                error= sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + sp.NombreProcedimiento, usrid);
                error = -1;
            }

            return error;
        }

        public static int InsertarEstadoRolDoctos(int codemp, int rolid, int estid, int esjid, int usrid, string ipred, string ipmaquina, string comentario, DateTime fecjud, int codsuc, int gesid)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            StoredProcedure sp = new StoredProcedure("_Insertar_Rol_Estados_2");
            int error = 1;
            try
            {

                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("estid", estid);
                sp.AgregarParametro("esjid", esjid);
                sp.AgregarParametro("usrid", usrid);
                sp.AgregarParametro("ipred", ipred);
                sp.AgregarParametro("ipmaquina", ipmaquina);
                sp.AgregarParametro("comentario", comentario);
                sp.AgregarParametro("fecjud", fecjud);
                sp.AgregarParametro("codsuc", codsuc);
                sp.AgregarParametro("gesid", gesid);

                error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + sp.NombreProcedimiento, usrid);
                error = -1;
                //throw ex;
            }
            return error;
        }

        public static int EliminarEstadoRol(int codemp, int rolid, int estid, int esjid,  DateTime fecjud)
        {
            int error = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Rol_Estados");
                sp.AgregarParametro("rle_codemp", codemp);
                sp.AgregarParametro("rle_rolid", rolid);
                sp.AgregarParametro("rle_estid", estid);
                sp.AgregarParametro("rle_esjid", esjid);
                sp.AgregarParametro("rle_fecha", fecjud);
              
                error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return error;
        }

        public static int EliminarEstampe(int codemp, string[] ids)
        {
            int error = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Elimina_Estampe");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ddeid", ids[0]);
                sp.AgregarParametro("pclid", ids[1]);
                sp.AgregarParametro("ctcid", ids[2]);
                sp.AgregarParametro("rolid", ids[3]);

                error = sp.EjecutarProcedimientoTrans();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return error;
        }

        public static List<dto.Autocomplete> ListarEstadoJudicial(int codemp, int idioma,string nombre, int esjid)
        {
            List<dto.Autocomplete> lst = new List<dto.Autocomplete>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Estado_Judicial_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("idioma", idioma);
                sp.AgregarParametro("esjid", esjid);
                sp.AgregarParametro("texto", nombre);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Autocomplete()
                        {
                            label = ds.Tables[0].Rows[i][0].ToString(),
                            value = ds.Tables[0].Rows[i][1].ToString(),
                            desc = ds.Tables[0].Rows[i][2].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lst;
        }
       
        #endregion

        #region "Rol Demandados"
        public static List<dto.Demandado> ListarRolDemandadosGrilla(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Demandado> lstEnteJudicial = new List<dto.Demandado>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Demandados_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstEnteJudicial.Add(new dto.Demandado()
                        {
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            RepresentanteLegal = ds.Tables[0].Rows[i]["RepresentanteLegal"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEnteJudicial;
        }

        public static int ListarRolDemandadosGrillaCount(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Demandados_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }

        public static int InsertarDemandadoRol(int codemp, int rolid, string rut, string nombre, bool repLegal)
        {
            int error = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Rol_Demandados");
                sp.AgregarParametro("rld_codemp", codemp);
                sp.AgregarParametro("rld_rolid", rolid);
                sp.AgregarParametro("rld_rut", rut);
                sp.AgregarParametro("rld_nombre", nombre);
                if (repLegal)
                {
                    sp.AgregarParametro("rld_repleg", "S");
                }
                else
                {
                    sp.AgregarParametro("rld_repleg", "N");
                }
                error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return error;
            }
            return error;
        }

        public static int EliminarDemandadoRol(int codemp, int rolid, string rut)
        {
            int error = 0;
            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Rol_Demandados");

                sp.AgregarParametro("rld_codemp", codemp);
                sp.AgregarParametro("rld_rolid", rolid);
                sp.AgregarParametro("rld_rut", rut);

                error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return error;
            }
            return error;
        }

        #endregion

        #region "Asociados"

        public static List<dto.Asociados> ListarAsociados(int codemp, int ctcid)
        {
            List<dto.Asociados> lst = new List<dto.Asociados>();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Asociados");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("ctcid", ctcid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(new dto.Asociados()
                        {
                            Id = ds.Tables[0].Rows[i]["Id"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Caratulado = ds.Tables[0].Rows[i]["Caratulado"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["Rol"].ToString(),
                            Tribunal = ds.Tables[0].Rows[i]["Tribunal"].ToString(),
                            Monto = ds.Tables[0].Rows[i]["Monto"].ToString(),
                            RepLegal = ds.Tables[0].Rows[i]["RepLegal"].ToString(),
                            Tooltip = ds.Tables[0].Rows[i]["Tooltip"].ToString(),
                            Padre = ds.Tables[0].Rows[i]["Padre"].ToString()
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

        #endregion

        #region "Rol Documentos"

        public static int InsertarDocumentosRol(int codemp, int rolid, int pclid, int ctcid, int ccbid, decimal monto, decimal saldo)
        {
            int error = -1;

            try
            {
                StoredProcedure sp = new StoredProcedure("Insertar_Rol_Documentos");
                sp.AgregarParametro("rdc_codemp", codemp);
                sp.AgregarParametro("rdc_rolid", rolid);
                sp.AgregarParametro("rdc_pclid", pclid);
                sp.AgregarParametro("rdc_ctcid", ctcid);
                sp.AgregarParametro("rdc_ccbid", ccbid);
                sp.AgregarParametro("rdc_monto", monto);
                sp.AgregarParametro("rdc_saldo", saldo);

                error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return error;
        }

        public static int EliminarDocumentosRol(int codemp, int rolid, int pclid, int ctcid, int ccbid)
        {
            int error = 0;

            try
            {
                StoredProcedure sp = new StoredProcedure("Delete_Rol_Documentos");

                sp.AgregarParametro("rdc_codemp", codemp);
                sp.AgregarParametro("rdc_rolid", rolid);
                sp.AgregarParametro("rdc_pclid", pclid);
                sp.AgregarParametro("rdc_ctcid", ctcid);
                sp.AgregarParametro("rdc_ccbid", ccbid);

                error = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                return error;
            }

            return error;
        }

        #endregion

        #region "Borradores"
        public static Dimol.dto.Combobox HistoriaBorrador(int codemp, int rolid, int idBorrador)
        {
            Dimol.dto.Combobox salida = new Combobox();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Historia_Borradores");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("id_borrador", idBorrador);
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

        #region "Rol Demandados"
        public static List<dto.Asegurado> ListarRolAseguradosGrilla(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.Asegurado> lstEnteJudicial = new List<dto.Asegurado>();
            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Asegurados_Grilla");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lstEnteJudicial.Add(new dto.Asegurado()
                        {
                            Rut = ds.Tables[0].Rows[i]["Rut"].ToString(),
                            Nombre = ds.Tables[0].Rows[i]["Nombre"].ToString(),
                            Numero = ds.Tables[0].Rows[i]["Numero"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstEnteJudicial;
        }

        public static int ListarRolAseguradosGrillaCount(int codemp, int rolid, string where, string sidx, string sord, int inicio, int limite)
        {
            int count = 0;

            try
            {

                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Rol_Asegurados_Grilla_Count");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                sp.AgregarParametro("where", where);
                sp.AgregarParametro("sidx", sidx);
                sp.AgregarParametro("sord", sord);
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", limite);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    count = Int32.Parse(ds.Tables[0].Rows[0]["count"].ToString());
                }

                return count;
            }
            catch (Exception ex)
            {
                return count;
            }
        }
        #endregion

        public static string TraeEstAdmPoderJudicial( int rolId)
        {
            string flag = "";
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Trae_Est_Adm_Rol");
                sp.AgregarParametro("rolid", rolId);

                ds = sp.EjecutarProcedimiento();
                if (ds.Tables.Count > 0)
                {
                    flag = ds.Tables[0].Rows[0]["ESTADO_ADM"].ToString() ;
                }

                return flag;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.dao.TraeEstAdmPoderJudicial", rolId);
                return flag;
            }
        }

        public static int BloquearRol(int codemp, int rolId,string bloqueo, int usrid)
        {
            int result = -1;
            try
            {
                StoredProcedure sp = new StoredProcedure("_Bloquear_Rol");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolId);
                sp.AgregarParametro("bloqueo", bloqueo);
                sp.AgregarParametro("usrid", usrid);
                result = sp.EjecutarProcedimientoTrans();
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Judicial.dao.BloquearRol", rolId);
                return result;
            }
            return result;
        }

    }
}
