using Dimol.CKEditor.dto;
using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Dimol.CKEditor.dao
{
    public class BorradorJudicial
    {
        public static dto.BorradorJudicial ReporteCertificados(int codemp, int rolid)
        {
            dto.BorradorJudicial lst = new dto.BorradorJudicial();

            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_rpt_Trae_Certificado_Judicial");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("rolid", rolid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst= new dto.BorradorJudicial()
                        {
                            Rolid = rolid,
                            TipoRol = ds.Tables[0].Rows[i]["tipo_rol"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["rol_numero"].ToString(),
                            Anio= DateTime.Today.Year,
                            Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                            RutEmpleado =  Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["epl_rut"].ToString()),
                            NombreEmpleado = ds.Tables[0].Rows[i]["epl_nombre"].ToString(),
                            PaternoEmpleado = ds.Tables[0].Rows[i]["epl_apepat"].ToString(),
                            MaternoEmpleado = ds.Tables[0].Rows[i]["epl_apemat"].ToString()
                        };
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static dto.BorradorJudicial Demanda(int codemp, int rolid)
        {
            dto.BorradorJudicial lst = new dto.BorradorJudicial();

            try
            {
                //Obtiene los datos de la demanda
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Rol_Demandas");
                sp.AgregarParametro("rol_codemp", codemp);
                sp.AgregarParametro("rol_rolid", rolid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime dt = DateTime.MinValue;

                        var f = ds.Tables[0].Rows[i]["FecResolucion"].ToString();
                        if (f != "")
                        {
                            dt = DateTime.ParseExact(f, "dd-MM-yyyy H:mm:ss", CultureInfo.InvariantCulture);
                        }

                        lst.Rolid = rolid;
                        lst.Rol = ds.Tables[0].Rows[i]["rol_numero"].ToString();
                        lst.Anio = DateTime.Today.Year;
                        lst.RutCliente = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rut"].ToString());
                        lst.Cliente = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString();
                        lst.RutDeudor = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["ctc_numero"].ToString() + ds.Tables[0].Rows[i]["ctc_digito"].ToString());
                        lst.NombreFantasiaDeudor = ds.Tables[0].Rows[i]["ctc_nomfant"].ToString();

                        lst.PaisDeudor = ds.Tables[0].Rows[i]["pai_nombre"].ToString();
                        lst.RegionDeudor = ds.Tables[0].Rows[i]["reg_nombre"].ToString();
                        lst.CiudadDeudor = ds.Tables[0].Rows[i]["ciu_nombre"].ToString();
                        lst.ComunaDeudor = ds.Tables[0].Rows[i]["com_nombre"].ToString();
                        lst.CodigoPostalDeudor = ds.Tables[0].Rows[i]["com_codpost"].ToString();

                        lst.DireccionDeudor = ds.Tables[0].Rows[i]["ctc_direccion"].ToString();

                        lst.RutEmpleado = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["epl_rut"].ToString());
                        lst.NombreEmpleado = ds.Tables[0].Rows[i]["epl_nombre"].ToString();
                        lst.PaternoEmpleado = ds.Tables[0].Rows[i]["epl_apepat"].ToString();
                        lst.MaternoEmpleado = ds.Tables[0].Rows[i]["epl_apemat"].ToString();
                        lst.Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString();
                        lst.TelefonoEmpleado = ds.Tables[0].Rows[i]["epl_telefono"].ToString();
                        lst.MailEmpleado = ds.Tables[0].Rows[i]["epl_mail"].ToString();

                        lst.FechaDemanda = ds.Tables[0].Rows[i]["rad_fecdem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecdem"].ToString()) : DateTime.MinValue;
                        lst.Cuotas = Int32.Parse(ds.Tables[0].Rows[i]["rad_cuodem"].ToString());
                        lst.Monto = decimal.Parse(ds.Tables[0].Rows[i]["rad_mondem"].ToString());
                        lst.MontoPrimeraCuota = decimal.Parse(ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString());
                        lst.MontoUltimaCuota = decimal.Parse(ds.Tables[0].Rows[i]["rad_monucoudem"].ToString());
                        lst.FechaPrimeraCuota = ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString()) : DateTime.MinValue;
                        lst.FechaUltimaCuota = ds.Tables[0].Rows[i]["rad_fecucoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecucoudem"].ToString()) : DateTime.MinValue;
                        lst.Interes = decimal.Parse(ds.Tables[0].Rows[i]["rad_intdem"].ToString());

                        lst.NombreCliente = ds.Tables[0].Rows[i]["pcl_nombre"].ToString();
                        lst.PaternoCliente = ds.Tables[0].Rows[i]["pcl_apepat"].ToString();
                        lst.MaternoCliente = ds.Tables[0].Rows[i]["pcl_apemat"].ToString();

                        lst.MateriaJudicial = ds.Tables[0].Rows[i]["mji_nombre"].ToString();
                        lst.TipoCausa = ds.Tables[0].Rows[i]["tci_nombre"].ToString();
                        lst.Estado = ds.Tables[0].Rows[i]["eci_nombre"].ToString();

                        lst.RepresentanteLegal = ds.Tables[0].Rows[i]["pcl_replegal"].ToString();
                        lst.RutRepresentanteLegal = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rutlegal"].ToString());

                        lst.Numero = ds.Tables[0].Rows[i]["ccb_numero"].ToString();
                        lst.FechaVencimiento = ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString()) : DateTime.MinValue;

                        lst.Saldo = decimal.Parse(ds.Tables[0].Rows[i]["rdc_saldo"].ToString());

                        lst.DireccionEmpresa = ds.Tables[0].Rows[i]["esu_direccion"].ToString();
                        lst.TelefonoEmpresa = ds.Tables[0].Rows[i]["esu_telefono"].ToString();
                        lst.PaisEmpresa = ds.Tables[0].Rows[i][38].ToString();
                        lst.RegionEmpresa = ds.Tables[0].Rows[i][39].ToString();
                        lst.CiudadEmpresa = ds.Tables[0].Rows[i][40].ToString();
                        lst.ComunaEmpresa = ds.Tables[0].Rows[i][41].ToString();

                        lst.NombreDeudor = ds.Tables[0].Rows[i]["ctc_nombre"].ToString();
                        lst.PaternoDeudor = ds.Tables[0].Rows[i]["ctc_apepat"].ToString();
                        lst.MaternoDeudor = ds.Tables[0].Rows[i]["ctc_apemat"].ToString();

                        lst.DiaPago = ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString()).Day : 1;

                        lst.MontoDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_mondem"].ToString());
                        lst.MontoPCuotaDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString());
                        lst.MontoUCuotaDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_monucoudem"].ToString());
                        lst.MontoSaldoDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rdc_saldo"].ToString());
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static dto.BorradorJudicial DemandaPrevisional(int codemp, int rolid)
        {
            dto.BorradorJudicial lst = new dto.BorradorJudicial();

            try
            {
                //Obtiene los datos de la demanda
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Rol_Demandas");
                sp.AgregarParametro("rol_codemp", codemp);
                sp.AgregarParametro("rol_rolid", rolid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime dt = DateTime.MinValue;

                        var f = ds.Tables[0].Rows[i]["FecResolucion"].ToString();
                        if (f != "")
                        {
                            dt = DateTime.ParseExact(f, "dd-MM-yyyy H:mm:ss", CultureInfo.InvariantCulture);
                        }

                        //lst = new dto.BorradorJudicial()
                        //{
                        lst.Rolid = rolid;
                        lst.Rol = ds.Tables[0].Rows[i]["rol_numero"].ToString();
                        lst.Anio = DateTime.Today.Year;
                        lst.RutCliente = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rut"].ToString());
                        lst.Cliente = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString();
                        lst.RutDeudor = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["ctc_numero"].ToString() + ds.Tables[0].Rows[i]["ctc_digito"].ToString());
                        lst.NombreFantasiaDeudor = ds.Tables[0].Rows[i]["ctc_nomfant"].ToString();

                        lst.PaisDeudor = ds.Tables[0].Rows[i]["pai_nombre"].ToString();
                        lst.RegionDeudor = ds.Tables[0].Rows[i]["reg_nombre"].ToString();
                        lst.CiudadDeudor = ds.Tables[0].Rows[i]["ciu_nombre"].ToString();
                        lst.ComunaDeudor = ds.Tables[0].Rows[i]["com_nombre"].ToString();
                        lst.CodigoPostalDeudor = ds.Tables[0].Rows[i]["com_codpost"].ToString();

                        lst.DireccionDeudor = ds.Tables[0].Rows[i]["ctc_direccion"].ToString();

                        lst.RutEmpleado = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["epl_rut"].ToString());
                        lst.NombreEmpleado = ds.Tables[0].Rows[i]["epl_nombre"].ToString();
                        lst.PaternoEmpleado = ds.Tables[0].Rows[i]["epl_apepat"].ToString();
                        lst.MaternoEmpleado = ds.Tables[0].Rows[i]["epl_apemat"].ToString();
                        lst.Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString();
                        lst.TelefonoEmpleado = ds.Tables[0].Rows[i]["epl_telefono"].ToString();
                        lst.MailEmpleado = ds.Tables[0].Rows[i]["epl_mail"].ToString();

                        lst.FechaDemanda = ds.Tables[0].Rows[i]["rad_fecdem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecdem"].ToString()) : DateTime.MinValue;
                        lst.Cuotas = ds.Tables[0].Rows[i]["rad_cuodem"].ToString() != "" ? Int32.Parse(ds.Tables[0].Rows[i]["rad_cuodem"].ToString()) : 0;
                        //lst.Monto = decimal.Parse(ds.Tables[0].Rows[i]["rad_mondem"].ToString());
                        lst.MontoPrimeraCuota = ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString()) : 0;
                        lst.MontoUltimaCuota = ds.Tables[0].Rows[i]["rad_monucoudem"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[i]["rad_monucoudem"].ToString()) : 0;
                        lst.FechaPrimeraCuota = ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString()) : DateTime.MinValue;
                        lst.FechaUltimaCuota = ds.Tables[0].Rows[i]["rad_fecucoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecucoudem"].ToString()) : DateTime.MinValue;
                        lst.Interes = ds.Tables[0].Rows[i]["rad_intdem"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[i]["rad_intdem"].ToString()): 0;

                        lst.NombreCliente = ds.Tables[0].Rows[i]["pcl_nombre"].ToString();
                        lst.PaternoCliente = ds.Tables[0].Rows[i]["pcl_apepat"].ToString();
                        lst.MaternoCliente = ds.Tables[0].Rows[i]["pcl_apemat"].ToString();

                        lst.MateriaJudicial = ds.Tables[0].Rows[i]["mji_nombre"].ToString();
                        lst.TipoCausa = ds.Tables[0].Rows[i]["tci_nombre"].ToString();
                        lst.Estado = ds.Tables[0].Rows[i]["eci_nombre"].ToString();

                        lst.RepresentanteLegal = ds.Tables[0].Rows[i]["pcl_replegal"].ToString();
                        lst.RutRepresentanteLegal = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rutlegal"].ToString());

                        lst.Numero = ds.Tables[0].Rows[i]["ccb_numero"].ToString();
                        lst.FechaVencimiento = ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString()) : DateTime.MinValue;

                        lst.Saldo = ds.Tables[0].Rows[i]["rdc_saldo"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[i]["rdc_saldo"].ToString()) : 0;

                        lst.DireccionEmpresa = ds.Tables[0].Rows[i]["esu_direccion"].ToString();
                        lst.TelefonoEmpresa = ds.Tables[0].Rows[i]["esu_telefono"].ToString();
                        lst.PaisEmpresa = ds.Tables[0].Rows[i][38].ToString();
                        lst.RegionEmpresa = ds.Tables[0].Rows[i][39].ToString();
                        lst.CiudadEmpresa = ds.Tables[0].Rows[i][40].ToString();
                        lst.ComunaEmpresa = ds.Tables[0].Rows[i][41].ToString();

                        lst.NombreDeudor = ds.Tables[0].Rows[i]["ctc_nombre"].ToString();
                        lst.PaternoDeudor = ds.Tables[0].Rows[i]["ctc_apepat"].ToString();
                        lst.MaternoDeudor = ds.Tables[0].Rows[i]["ctc_apemat"].ToString();

                        lst.DiaPago = ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString()).Day : 1;

                        lst.MontoDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_mondem"].ToString());
                        lst.MontoPCuotaDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString());
                        lst.MontoUCuotaDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_monucoudem"].ToString());
                        lst.MontoSaldoDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rdc_saldo"].ToString());

                        //Previsionales
                        lst.NumContrato = ds.Tables[0].Rows[i]["NumContrato"].ToString();
                        lst.NumResolucion = ds.Tables[0].Rows[i]["NumResolucion"].ToString();

                        lst.FecResolucion = dt.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));
                        lst.RutRepresentante1 = ds.Tables[0].Rows[i]["RutRepresentante1"].ToString();
                        lst.RutRepresentante2 = ds.Tables[0].Rows[i]["RutRepresentante2"].ToString();
                        lst.RutRepresentante3 = ds.Tables[0].Rows[i]["RutRepresentante3"].ToString();
                        lst.NombRepresentante1 = ds.Tables[0].Rows[i]["NombRepresentante1"].ToString();
                        lst.NombRepresentante2 = ds.Tables[0].Rows[i]["NombRepresentante2"].ToString();
                        lst.NombRepresentante3 = ds.Tables[0].Rows[i]["NombRepresentante3"].ToString();
                        //};

                        //Obtiene los datos de los documentos previsionales
                        lst.ListaResoluciones = CargaTablaDocumentosXsl(codemp, rolid);
                        decimal total = 0;

                        for (int j = 0; j < lst.ListaResoluciones.Count; j++)
                        {
                            var obj = lst.ListaResoluciones[j];
                            total = total + obj.MontoTotal;
                        }

                        lst.MontoPrevisional = total;
                        lst.MontoPrevisionalStr = dao.Documento.NumeroEnletras(lst.MontoPrevisional.ToString("N2"));
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }
        public static dto.BorradorJudicial DemandaPrevisionalPorIdPanel(int codEmp, int panelId)
        {
            dto.BorradorJudicial lst = new dto.BorradorJudicial();

            try
            {
                //Obtiene los datos de la demanda
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Rol_Demandas_Para_PanelDemandaPrevisional");
                sp.AgregarParametro("codEmp", codEmp);
                sp.AgregarParametro("panelId", panelId);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DateTime dt = DateTime.MinValue;

                        var f = ds.Tables[0].Rows[i]["FecResolucion"].ToString();
                        if (f != "")
                        {
                            dt = DateTime.ParseExact(f, "dd-MM-yyyy H:mm:ss", CultureInfo.InvariantCulture);
                        }

                        //lst = new dto.BorradorJudicial()
                        //{
                        //lst.Rolid = rolid;
                        //lst.Rol = ds.Tables[0].Rows[i]["rol_numero"].ToString();
                        lst.Anio = DateTime.Today.Year;
                        lst.RutCliente = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rut"].ToString());
                        lst.Cliente = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString();
                        lst.RutDeudor = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["ctc_numero"].ToString() + ds.Tables[0].Rows[i]["ctc_digito"].ToString());
                        lst.NombreFantasiaDeudor = ds.Tables[0].Rows[i]["ctc_nomfant"].ToString();
                        lst.PaisDeudor = ds.Tables[0].Rows[i]["pai_nombre"].ToString();
                        lst.RegionDeudor = ds.Tables[0].Rows[i]["reg_nombre"].ToString();
                        lst.CiudadDeudor = ds.Tables[0].Rows[i]["ciu_nombre"].ToString();
                        lst.ComunaDeudor = ds.Tables[0].Rows[i]["com_nombre"].ToString();
                        lst.CodigoPostalDeudor = ds.Tables[0].Rows[i]["com_codpost"].ToString();
                        lst.DireccionDeudor = ds.Tables[0].Rows[i]["ctc_direccion"].ToString();
                        //lst.RutEmpleado = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["epl_rut"].ToString());
                        //lst.NombreEmpleado = ds.Tables[0].Rows[i]["epl_nombre"].ToString();
                        //lst.PaternoEmpleado = ds.Tables[0].Rows[i]["epl_apepat"].ToString();
                        //lst.MaternoEmpleado = ds.Tables[0].Rows[i]["epl_apemat"].ToString();
                        //lst.Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString();
                        //lst.TelefonoEmpleado = ds.Tables[0].Rows[i]["epl_telefono"].ToString();
                        //lst.MailEmpleado = ds.Tables[0].Rows[i]["epl_mail"].ToString();
                        //lst.FechaDemanda = ds.Tables[0].Rows[i]["rad_fecdem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecdem"].ToString()) : DateTime.MinValue;
                        //lst.Cuotas = ds.Tables[0].Rows[i]["rad_cuodem"].ToString() != "" ? Int32.Parse(ds.Tables[0].Rows[i]["rad_cuodem"].ToString()) : 0;
                        //lst.MontoPrimeraCuota = ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString()) : 0;
                        //lst.MontoUltimaCuota = ds.Tables[0].Rows[i]["rad_monucoudem"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[i]["rad_monucoudem"].ToString()) : 0;
                        //lst.FechaPrimeraCuota = ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString()) : DateTime.MinValue;
                        //lst.FechaUltimaCuota = ds.Tables[0].Rows[i]["rad_fecucoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecucoudem"].ToString()) : DateTime.MinValue;
                        //lst.Interes = ds.Tables[0].Rows[i]["rad_intdem"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[i]["rad_intdem"].ToString()) : 0;
                        lst.NombreCliente = ds.Tables[0].Rows[i]["pcl_nombre"].ToString();
                        lst.PaternoCliente = ds.Tables[0].Rows[i]["pcl_apepat"].ToString();
                        lst.MaternoCliente = ds.Tables[0].Rows[i]["pcl_apemat"].ToString();
                        lst.MateriaJudicial = ds.Tables[0].Rows[i]["mji_nombre"].ToString();
                        lst.TipoCausa = ds.Tables[0].Rows[i]["tci_nombre"].ToString();
                        //lst.Estado = ds.Tables[0].Rows[i]["eci_nombre"].ToString();
                        lst.RepresentanteLegal = ds.Tables[0].Rows[i]["pcl_replegal"].ToString();
                        lst.RutRepresentanteLegal = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rutlegal"].ToString());
                        lst.Numero = ds.Tables[0].Rows[i]["ccb_numero"].ToString();
                        lst.FechaVencimiento = ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString()) : DateTime.MinValue;
                        //lst.Saldo = decimal.Parse(ds.Tables[0].Rows[i]["rdc_saldo"].ToString());
                        //lst.DireccionEmpresa = ds.Tables[0].Rows[i]["esu_direccion"].ToString();
                        //lst.TelefonoEmpresa = ds.Tables[0].Rows[i]["esu_telefono"].ToString();
                        //lst.PaisEmpresa = ds.Tables[0].Rows[i][38].ToString();
                        //lst.RegionEmpresa = ds.Tables[0].Rows[i][39].ToString();
                        //lst.CiudadEmpresa = ds.Tables[0].Rows[i][40].ToString();
                        //lst.ComunaEmpresa = ds.Tables[0].Rows[i][41].ToString();
                        lst.NombreDeudor = ds.Tables[0].Rows[i]["ctc_nombre"].ToString();
                        lst.PaternoDeudor = ds.Tables[0].Rows[i]["ctc_apepat"].ToString();
                        lst.MaternoDeudor = ds.Tables[0].Rows[i]["ctc_apemat"].ToString();
                        //lst.DiaPago = ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString()).Day : 1;
                        //lst.MontoDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_mondem"].ToString());
                        //lst.MontoPCuotaDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString());
                        //lst.MontoUCuotaDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_monucoudem"].ToString());
                        //lst.MontoSaldoDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rdc_saldo"].ToString());

                        //Previsionales
                        lst.NumContrato = ds.Tables[0].Rows[i]["NumContrato"].ToString();
                        lst.NumResolucion = ds.Tables[0].Rows[i]["NumResolucion"].ToString();
                        lst.FecResolucion = dt.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));
                        lst.RutRepresentante1 = ds.Tables[0].Rows[i]["RutRepresentante1"].ToString();
                        lst.RutRepresentante2 = ds.Tables[0].Rows[i]["RutRepresentante2"].ToString();
                        lst.RutRepresentante3 = ds.Tables[0].Rows[i]["RutRepresentante3"].ToString();
                        lst.NombRepresentante1 = ds.Tables[0].Rows[i]["NombRepresentante1"].ToString();
                        lst.NombRepresentante2 = ds.Tables[0].Rows[i]["NombRepresentante2"].ToString();
                        lst.NombRepresentante3 = ds.Tables[0].Rows[i]["NombRepresentante3"].ToString();
                        //};

                        //Obtiene los datos de los documentos previsionales
                        //lst.ListaResoluciones = CargaTablaDocumentosXsl(codEmp, panelId); //HACER UN NUEVO METODO Q OBTENGA LOS DOCS POR ID PANEL
                        //decimal total = 0;

                        //for (int j = 0; j < lst.ListaResoluciones.Count; j++)
                        //{
                        //    var obj = lst.ListaResoluciones[j];
                        //    total = total + obj.MontoTotal;
                        //}

                        //lst.MontoPrevisional = total;
                        //lst.MontoPrevisionalStr = dao.Documento.NumeroEnletras(lst.MontoPrevisional.ToString("N2"));
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        public static dto.BorradorJudicial DemandaMasivaPorIdPanel(int codEmp, int panelId)
        {
            dto.BorradorJudicial lst = new dto.BorradorJudicial();

            try
            {
                //Obtiene los datos de la demanda
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Rol_Demandas_Para_PanelDemandaMasiva");
                sp.AgregarParametro("codEmp", codEmp);
                sp.AgregarParametro("panelId", panelId);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //lst.Rolid = rolid;
                        //lst.Rol = ds.Tables[0].Rows[i]["rol_numero"].ToString();
                        lst.Anio = DateTime.Today.Year;
                        lst.RutCliente = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rut"].ToString());
                        lst.Cliente = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString();
                        lst.RutDeudor = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["ctc_numero"].ToString() + ds.Tables[0].Rows[i]["ctc_digito"].ToString());
                        lst.NombreFantasiaDeudor = ds.Tables[0].Rows[i]["ctc_nomfant"].ToString();
                        lst.PaisDeudor = ds.Tables[0].Rows[i]["pai_nombre"].ToString();
                        lst.RegionDeudor = ds.Tables[0].Rows[i]["reg_nombre"].ToString();
                        lst.CiudadDeudor = ds.Tables[0].Rows[i]["ciu_nombre"].ToString();
                        lst.ComunaDeudor = ds.Tables[0].Rows[i]["com_nombre"].ToString();
                        lst.CodigoPostalDeudor = ds.Tables[0].Rows[i]["com_codpost"].ToString();
                        lst.DireccionDeudor = ds.Tables[0].Rows[i]["ctc_direccion"].ToString();
                        lst.RutEmpleado = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["epl_rut"].ToString());
                        lst.NombreEmpleado = ds.Tables[0].Rows[i]["epl_nombre"].ToString();
                        lst.PaternoEmpleado = ds.Tables[0].Rows[i]["epl_apepat"].ToString();
                        lst.MaternoEmpleado = ds.Tables[0].Rows[i]["epl_apemat"].ToString();
                        //lst.Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString();
                        lst.TelefonoEmpleado = ds.Tables[0].Rows[i]["epl_telefono"].ToString();
                        lst.MailEmpleado = ds.Tables[0].Rows[i]["epl_mail"].ToString();
                        lst.FechaDemanda = ds.Tables[0].Rows[i]["rad_fecdem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecdem"].ToString()) : DateTime.MinValue;
                        lst.Cuotas = ds.Tables[0].Rows[i]["rad_cuodem"].ToString() != "" ? Int32.Parse(ds.Tables[0].Rows[i]["rad_cuodem"].ToString()) : 0;
                        lst.Monto = decimal.Parse(ds.Tables[0].Rows[i]["rad_mondem"].ToString());
                        lst.MontoPrimeraCuota = ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString()) : 0;
                        lst.MontoUltimaCuota = ds.Tables[0].Rows[i]["rad_monucoudem"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[i]["rad_monucoudem"].ToString()) : 0;
                        lst.FechaPrimeraCuota = ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString()) : DateTime.MinValue;
                        lst.FechaUltimaCuota = ds.Tables[0].Rows[i]["rad_fecucoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecucoudem"].ToString()) : DateTime.MinValue;
                        lst.Interes = ds.Tables[0].Rows[i]["rad_intdem"].ToString() != "" ? decimal.Parse(ds.Tables[0].Rows[i]["rad_intdem"].ToString()) : 0;
                        lst.NombreCliente = ds.Tables[0].Rows[i]["pcl_nombre"].ToString();
                        lst.PaternoCliente = ds.Tables[0].Rows[i]["pcl_apepat"].ToString();
                        lst.MaternoCliente = ds.Tables[0].Rows[i]["pcl_apemat"].ToString();
                        lst.MateriaJudicial = ds.Tables[0].Rows[i]["mji_nombre"].ToString();
                        lst.TipoCausa = ds.Tables[0].Rows[i]["tci_nombre"].ToString();
                        //lst.Estado = ds.Tables[0].Rows[i]["eci_nombre"].ToString();
                        lst.RepresentanteLegal = ds.Tables[0].Rows[i]["pcl_replegal"].ToString();
                        lst.RutRepresentanteLegal = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rutlegal"].ToString());
                        lst.Numero = ds.Tables[0].Rows[i]["ccb_numero"].ToString();
                        lst.FechaVencimiento = ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString()) : DateTime.MinValue;
                        lst.Saldo = decimal.Parse(ds.Tables[0].Rows[i]["rdc_saldo"].ToString());
                        //lst.DireccionEmpresa = ds.Tables[0].Rows[i]["esu_direccion"].ToString();
                        //lst.TelefonoEmpresa = ds.Tables[0].Rows[i]["esu_telefono"].ToString();
                        //lst.PaisEmpresa = ds.Tables[0].Rows[i][38].ToString();
                        //lst.RegionEmpresa = ds.Tables[0].Rows[i][39].ToString();
                        //lst.CiudadEmpresa = ds.Tables[0].Rows[i][40].ToString();
                        //lst.ComunaEmpresa = ds.Tables[0].Rows[i][41].ToString();
                        lst.NombreDeudor = ds.Tables[0].Rows[i]["ctc_nombre"].ToString();
                        lst.PaternoDeudor = ds.Tables[0].Rows[i]["ctc_apepat"].ToString();
                        lst.MaternoDeudor = ds.Tables[0].Rows[i]["ctc_apemat"].ToString();
                        lst.DiaPago = ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString() != "" ? DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecpcoudem"].ToString()).Day : 1;
                        lst.MontoDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_mondem"].ToString());
                        lst.MontoPCuotaDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_monpcuodem"].ToString());
                        lst.MontoUCuotaDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_monucoudem"].ToString());
                        lst.MontoSaldoDemandaEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rdc_saldo"].ToString());
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                return lst;
            }
        }

        protected static List<ResolucionParaXsl> CargaTablaDocumentosXsl(int codemp, int rolid) {
            DataSet ds = new DataSet();
            StoredProcedure sp = new StoredProcedure("_Listar_Rol_Doc_Asig_Grilla_GroupByResolucion");
            sp.AgregarParametro("codemp", codemp);
            sp.AgregarParametro("idid", 1);
            sp.AgregarParametro("rolid", rolid);
            sp.AgregarParametro("where", "");
            sp.AgregarParametro("sidx", "FechaResolucion");
            sp.AgregarParametro("sord", "asc");
            sp.AgregarParametro("inicio", 0);
            sp.AgregarParametro("limite", 9999);
            ds = sp.EjecutarProcedimiento();

            var lst = new List<ResolucionParaXsl>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var Resolucion = new ResolucionParaXsl();
                    Resolucion.NumResolucion = ds.Tables[0].Rows[i]["Resolucion"].ToString();
                    Resolucion.MontoTotal = decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString());
                    Resolucion.MontoTotalStr = (decimal.Parse(ds.Tables[0].Rows[i]["Monto"].ToString())).ToString("0.00").Replace(".00", "");

                    DateTime dt = DateTime.ParseExact(ds.Tables[0].Rows[i]["FechaResolucion"].ToString(), "dd-MM-yyyy H:mm:ss", CultureInfo.InvariantCulture);
                    Resolucion.FecResolucion = dt.ToString("dd-MMMM-yyyy", new CultureInfo("es-ES"));
                    Resolucion.FecResolucionStr = dt.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));

                    Resolucion.ListaPeriodos = CargarPeriodosTablaDocumentosXsl(Resolucion.NumResolucion);

                    lst.Add(Resolucion);
                }
            }

            return lst;
        }

        public static List<PeriodosParaXsl> CargarPeriodosTablaDocumentosXsl(string NumResolucion)
        {
            DataSet ds = new DataSet();
            StoredProcedure sp = new StoredProcedure("_Listar_Cartera_Clientes_Cpbt_Documento_Por_Numero_Resolucion");
            sp.AgregarParametro("numResolucion", NumResolucion);
            ds = sp.EjecutarProcedimiento();

            var lst = new List<PeriodosParaXsl>();
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    var Periodo = new PeriodosParaXsl();

                    DateTime dt = DateTime.ParseExact(ds.Tables[0].Rows[i]["FechaDoc"].ToString(), "dd-MM-yyyy H:mm:ss", CultureInfo.InvariantCulture);
                    Periodo.Periodo = dt.ToString("MMMM-yyyy", new CultureInfo("es-ES"));

                    lst.Add(Periodo);
                }
            }

            return lst;
        }

        public static dto.BorradorJudicial Avenimiento(int codemp, int rolid)
        {
            dto.BorradorJudicial lst = new dto.BorradorJudicial();
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("Trae_Reporte_Rol_Avenimiento");
                sp.AgregarParametro("rol_codemp", codemp);
                sp.AgregarParametro("rol_rolid", rolid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst = new dto.BorradorJudicial()
                        {
                            Rolid = rolid,
                            //TipoRol = ds.Tables[0].Rows[i]["tipo_rol"].ToString(),
                            Rol = ds.Tables[0].Rows[i]["rol_numero"].ToString(),
                            Anio = DateTime.Today.Year,
                            RutCliente = ds.Tables[0].Rows[i]["pcl_rut"].ToString(),
                            Cliente = ds.Tables[0].Rows[i]["pcl_nomfant"].ToString(),
                            RutDeudor = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["ctc_numero"].ToString() + ds.Tables[0].Rows[i]["ctc_digito"].ToString()),
                            NombreFantasiaDeudor = ds.Tables[0].Rows[i]["ctc_nomfant"].ToString(),

                            PaisDeudor = ds.Tables[0].Rows[i]["pai_nombre"].ToString(),
                            RegionDeudor = ds.Tables[0].Rows[i]["reg_nombre"].ToString(),
                            CiudadDeudor = ds.Tables[0].Rows[i]["ciu_nombre"].ToString(),
                            ComunaDeudor = ds.Tables[0].Rows[i]["com_nombre"].ToString(),
                            CodigoPostalDeudor = ds.Tables[0].Rows[i]["com_codpost"].ToString(),

                            DireccionDeudor = ds.Tables[0].Rows[i]["ctc_direccion"].ToString(),

                            RutEmpleado = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["epl_rut"].ToString()),
                            NombreEmpleado = ds.Tables[0].Rows[i]["epl_nombre"].ToString(),
                            PaternoEmpleado = ds.Tables[0].Rows[i]["epl_apepat"].ToString(),
                            MaternoEmpleado = ds.Tables[0].Rows[i]["epl_apemat"].ToString(),
                            Tribunal = ds.Tables[0].Rows[i]["trb_nombre"].ToString(),
                            //TelefonoEmpleado = ds.Tables[0].Rows[i]["epl_telefono"].ToString(),
                            //MailEmpleado = ds.Tables[0].Rows[i]["epl_mail"].ToString(),

                            FechaDemanda = DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecave"].ToString()),
                            Cuotas = Int32.Parse(ds.Tables[0].Rows[i]["rad_cuoave"].ToString()),
                            MontoCuota = decimal.Parse(ds.Tables[0].Rows[i]["rad_monave"].ToString()),
                            MontoPrimeraCuota = decimal.Parse(ds.Tables[0].Rows[i]["rad_monpcouave"].ToString()),
                            MontoUltimaCuota = decimal.Parse(ds.Tables[0].Rows[i]["rad_monucouave"].ToString()),
                            FechaPrimeraCuota = DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecpcouave"].ToString()),
                            FechaUltimaCuota = DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecucouave"].ToString()),
                            Interes = decimal.Parse(ds.Tables[0].Rows[i]["rad_intave"].ToString()),

                            DiaPago = DateTime.Parse(ds.Tables[0].Rows[i]["rad_fecpcouave"].ToString()).Day,
                            //NombreCliente = ds.Tables[0].Rows[i]["pcl_nombre"].ToString(),
                            //PaternoCliente = ds.Tables[0].Rows[i]["pcl_apepat"].ToString(),
                            //MaternoCliente = ds.Tables[0].Rows[i]["pcl_apemat"].ToString(),

                            //MateriaJudicial = ds.Tables[0].Rows[i]["mji_nombre"].ToString(),
                            //TipoCausa = ds.Tables[0].Rows[i]["tci_nombre"].ToString(),
                            //Estado = ds.Tables[0].Rows[i]["eci_nombre"].ToString(),

                            //RepresentanteLegal = ds.Tables[0].Rows[i]["pcl_replegal"].ToString(),
                            //RutRepresentanteLegal = Dimol.bcp.Funciones.formatearRut(ds.Tables[0].Rows[i]["pcl_rutlegal"].ToString()),

                            //Numero = ds.Tables[0].Rows[i]["ccb_numero"].ToString(),
                            //FechaVencimiento = DateTime.Parse(ds.Tables[0].Rows[i]["ccb_fecvenc"].ToString()),

                            //Saldo = decimal.Parse(ds.Tables[0].Rows[i]["rdc_saldo"].ToString()),

                            //DireccionEmpresa = ds.Tables[0].Rows[i]["esu_direccion"].ToString(),
                            //TelefonoEmpresa = ds.Tables[0].Rows[i]["esu_telefono"].ToString(),
                            //PaisEmpresa = ds.Tables[0].Rows[i]["pai_nombre"].ToString(),
                            //RegionEmpresa = ds.Tables[0].Rows[i]["reg_nombre"].ToString(),
                            //CiudadEmpresa = ds.Tables[0].Rows[i]["ciu_nombre"].ToString(),
                            //ComunaEmpresa = ds.Tables[0].Rows[i]["com_nombre"].ToString(),

                            NombreDeudor = ds.Tables[0].Rows[i]["ctc_nombre"].ToString(),
                            PaternoDeudor = ds.Tables[0].Rows[i]["ctc_apepat"].ToString(),
                            MaternoDeudor = ds.Tables[0].Rows[i]["ctc_apemat"].ToString(),
                            MontoAvenimientoEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_monave"].ToString()),
                            MontoCuotaAvenimientoEscrito = dao.Documento.NumeroEnletras(ds.Tables[0].Rows[i]["rad_monpcouave"].ToString())
                        };
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