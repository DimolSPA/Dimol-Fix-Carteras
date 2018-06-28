using Dimol.bcp;
using Dimol.Carteras.dto;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Dimol.Carteras.bcp
{
    public class CargaMasiva
    {
        #region "Carga de datos"
        public static List<DatosCarga> CargarDatos(string nombreArchivo)
        {
            List<DatosCarga> lst = new List<DatosCarga>();

            try
            {
                DataSet ds = Funciones.CargarExcel(nombreArchivo);

                if (ds.Tables.Count > 0)
                {
                    DataColumnCollection columns = ds.Tables[0].Columns;
                    ds.AcceptChanges();
                    DataRow drn = ds.Tables[0].Rows[0];
                    for (int i = 0; i < drn.Table.Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = drn[i].ToString();
                    }
                    if (columns.Contains("IDCUENTA") && columns.Contains("DESC_CUENTA") && !columns.Contains("NUMERO_RESOLUCION"))
                    {
                        for (int j = 1; j < ds.Tables[0].Rows.Count; j++)
                        {
                            DataRow dr = ds.Tables[0].Rows[j];
                            lst.Add(new DatosCarga
                            {
                                Antecedentes = dr["DOCANT"].ToString(),
                                Banco = dr["BANCO"].ToString(),
                                Capital = decimal.Parse(dr["CAPITAL"].ToString()),
                                Celular1 = dr["CELULAR1"].ToString(),
                                Celular2 = dr["CELULAR2"].ToString(),
                                Celular3 = dr["CELULAR3"].ToString(),
                                Celular4 = dr["CELULAR4"].ToString(),
                                Celular5 = dr["CELULAR5"].ToString(),
                                CodigoCarga = dr["CODIGO CARGA"].ToString(),
                                Comentario = dr["COMENTARIO"].ToString(),
                                Comuna = dr["COMUNA"].ToString(),
                                Direccion1 = dr["DIRECCION1"].ToString(),
                                Direccion2 = dr["DIRECCION2"].ToString(),
                                Dv = dr["RUTDV"].ToString(),
                                Fax = dr["FAX"].ToString(),
                                FechaDocumento = DateTime.Parse(dr["FECDOC"].ToString()),
                                FechaVencimiento = DateTime.Parse(dr["FECVENC"].ToString()),
                                GastoJudicial = decimal.Parse(dr["GASTOJUD"].ToString()),
                                GastoPreJudicial = decimal.Parse(dr["GASTOPRE"].ToString()),
                                Mail1 = dr["MAIL1"].ToString(),
                                Mail2 = dr["MAIL2"].ToString(),
                                Mail3 = dr["MAIL3"].ToString(),
                                Materno = dr["APEMAT"].ToString(),
                                Moneda = dr["MONEDA"].ToString(),
                                MontoAsignado = decimal.Parse(dr["MONTO ASIGNADO"].ToString()),
                                MotivoCobranza = dr["MOTIVO COBRANZA"].ToString(),
                                Negocio = dr["NEGOCIO"].ToString(),
                                Nombre = dr["NOMBRE"].ToString(),
                                NombreAsegurado = dr["NOMBRE ASEGURADO"].ToString(),
                                NombreGirador = dr["NOMBRE GIRADOR"].ToString(),
                                Numero = dr["NUMERO"].ToString().TrimStart('0'),
                                NumeroAgrupar = dr["NUMERO AGRUPAR"].ToString(),
                                Originales = dr["DOCORI"].ToString(),
                                Paterno = dr["APEPAT"].ToString(),
                                Rut = dr["RUTNUM"].ToString().TrimStart('0'),
                                RutAsegurado = dr["RUT ASEGURADO"].ToString(),
                                RutCliente = dr["RUT CLIENTE"].ToString(),
                                RutGirador = dr["RUT GIRADOR"].ToString(),
                                Saldo = decimal.Parse(dr["SALDO"].ToString()),
                                Telefono1 = dr["TELEFONO1"].ToString(),
                                Telefono2 = dr["TELEFONO2"].ToString(),
                                Telefono3 = dr["TELEFONO3"].ToString(),
                                Telefono4 = dr["TELEFONO4"].ToString(),
                                Telefono5 = dr["TELEFONO5"].ToString(),
                                TipoCambio = decimal.Parse(dr["TIPO CAMBIO"].ToString()),
                                TipoDocumento = dr["TIPO DOCUMENTO"].ToString(),
                                RutTercero = dr["RUT TERCERO"].ToString(),
                                NombreTercero = dr["NOMBRE TERCERO"].ToString(),
                                IdCuenta = dr["IDCUENTA"].ToString(),
                                DescripcionCuenta = dr["DESC_CUENTA"].ToString()
                            });
                        }
                    }
                    else if (columns.Contains("IDCUENTA") && columns.Contains("DESC_CUENTA") && columns.Contains("NUMERO_RESOLUCION"))
                    {

                        for (int j = 1; j < ds.Tables[0].Rows.Count; j++)
                        {
                            DataRow dr = ds.Tables[0].Rows[j];
                            lst.Add(new DatosCarga
                            {
                                Antecedentes = dr["DOCANT"].ToString(),
                                Banco = dr["BANCO"].ToString(),
                                Capital = decimal.Parse(dr["CAPITAL"].ToString()),
                                Celular1 = dr["CELULAR1"].ToString(),
                                Celular2 = dr["CELULAR2"].ToString(),
                                Celular3 = dr["CELULAR3"].ToString(),
                                Celular4 = dr["CELULAR4"].ToString(),
                                Celular5 = dr["CELULAR5"].ToString(),
                                CodigoCarga = dr["CODIGO CARGA"].ToString(),
                                Comentario = dr["COMENTARIO"].ToString(),
                                Comuna = dr["COMUNA"].ToString(),
                                Direccion1 = dr["DIRECCION1"].ToString(),
                                Direccion2 = dr["DIRECCION2"].ToString(),
                                Dv = dr["RUTDV"].ToString(),
                                Fax = dr["FAX"].ToString(),
                                FechaDocumento = DateTime.Parse(dr["FECDOC"].ToString()),
                                FechaVencimiento = DateTime.Parse(dr["FECVENC"].ToString()),
                                GastoJudicial = decimal.Parse(dr["GASTOJUD"].ToString()),
                                GastoPreJudicial = decimal.Parse(dr["GASTOPRE"].ToString()),
                                Mail1 = dr["MAIL1"].ToString(),
                                Mail2 = dr["MAIL2"].ToString(),
                                Mail3 = dr["MAIL3"].ToString(),
                                Materno = dr["APEMAT"].ToString(),
                                Moneda = dr["MONEDA"].ToString(),
                                MontoAsignado = decimal.Parse(dr["MONTO ASIGNADO"].ToString()),
                                MotivoCobranza = dr["MOTIVO COBRANZA"].ToString(),
                                Negocio = dr["NEGOCIO"].ToString(),
                                Nombre = dr["NOMBRE"].ToString(),
                                NombreAsegurado = dr["NOMBRE ASEGURADO"].ToString(),
                                NombreGirador = dr["NOMBRE GIRADOR"].ToString(),
                                Numero = dr["NUMERO"].ToString().TrimStart('0'),
                                NumeroAgrupar = dr["NUMERO AGRUPAR"].ToString(),
                                Originales = dr["DOCORI"].ToString(),
                                Paterno = dr["APEPAT"].ToString(),
                                Rut = dr["RUTNUM"].ToString().TrimStart('0'),
                                RutAsegurado = dr["RUT ASEGURADO"].ToString(),
                                RutCliente = dr["RUT CLIENTE"].ToString(),
                                RutGirador = dr["RUT GIRADOR"].ToString(),
                                Saldo = decimal.Parse(dr["SALDO"].ToString()),
                                Telefono1 = dr["TELEFONO1"].ToString(),
                                Telefono2 = dr["TELEFONO2"].ToString(),
                                Telefono3 = dr["TELEFONO3"].ToString(),
                                Telefono4 = dr["TELEFONO4"].ToString(),
                                Telefono5 = dr["TELEFONO5"].ToString(),
                                TipoCambio = decimal.Parse(dr["TIPO CAMBIO"].ToString()),
                                TipoDocumento = dr["TIPO DOCUMENTO"].ToString(),
                                RutTercero = dr["RUT TERCERO"].ToString(),
                                NombreTercero = dr["NOMBRE TERCERO"].ToString(),
                                IdCuenta = dr["IDCUENTA"].ToString(),
                                DescripcionCuenta = dr["DESC_CUENTA"].ToString(),
                                NumeroResolucion = dr["NUMERO_RESOLUCION"] != null ? dr["NUMERO_RESOLUCION"].ToString() : "",
                                FechaResolucion = !string.IsNullOrEmpty(dr["FECHA_RESOLUCION"].ToString()) ? DateTime.Parse(dr["FECHA_RESOLUCION"].ToString()): new DateTime(),
                                RutRepresentante1 = dr["RutRepresentante1"] != null ? dr["RutRepresentante1"].ToString() : "",
                                NombreRepresentante1 = dr["NombreRepresentante1"] != null ? dr["NombreRepresentante1"].ToString() : "",
                                RutRepresentante2 = dr["RutRepresentante2"] != null ? dr["RutRepresentante2"].ToString() : "",
                                NombreRepresentante2 = dr["NombreRepresentante2"] != null ? dr["NombreRepresentante2"].ToString() : "",
                                RutRepresentante3 = dr["RutRepresentante3"] != null ? dr["RutRepresentante3"].ToString() : "",
                                NombreRepresentante3 = dr["NombreRepresentante3"] != null ? dr["NombreRepresentante3"].ToString() : ""
                            });
                        }
                    }
                    else if (columns.Contains("NUMRESOLUCION"))
                    {
                        for (int j = 1; j < ds.Tables[0].Rows.Count; j++)
                        {
                            DataRow dr = ds.Tables[0].Rows[j];
                            lst.Add(new DatosCarga
                            {
                                Antecedentes = dr["DOCANT"].ToString(),
                                Banco = dr["BANCO"].ToString(),
                                Capital = decimal.Parse(dr["CAPITAL"].ToString()),
                                Celular1 = dr["CELULAR1"].ToString(),
                                Celular2 = dr["CELULAR2"].ToString(),
                                Celular3 = dr["CELULAR3"].ToString(),
                                Celular4 = dr["CELULAR4"].ToString(),
                                Celular5 = dr["CELULAR5"].ToString(),
                                CodigoCarga = dr["CODIGO CARGA"].ToString(),
                                Comentario = dr["COMENTARIO"].ToString(),
                                Comuna = dr["COMUNA"].ToString(),
                                Direccion1 = dr["DIRECCION1"].ToString(),
                                Direccion2 = dr["DIRECCION2"].ToString(),
                                Dv = dr["RUTDV"].ToString(),
                                Fax = dr["FAX"].ToString(),
                                FechaDocumento = DateTime.Parse(dr["FECDOC"].ToString()),
                                FechaVencimiento = DateTime.Parse(dr["FECVENC"].ToString()),
                                GastoJudicial = decimal.Parse(dr["GASTOJUD"].ToString()),
                                GastoPreJudicial = decimal.Parse(dr["GASTOPRE"].ToString()),
                                Mail1 = dr["MAIL1"].ToString(),
                                Mail2 = dr["MAIL2"].ToString(),
                                Mail3 = dr["MAIL3"].ToString(),
                                Materno = dr["APEMAT"].ToString(),
                                Moneda = dr["MONEDA"].ToString(),
                                MontoAsignado = decimal.Parse(dr["MONTO ASIGNADO"].ToString()),
                                MotivoCobranza = dr["MOTIVO COBRANZA"].ToString(),
                                Negocio = dr["NEGOCIO"].ToString(),
                                Nombre = dr["NOMBRE"].ToString(),
                                NombreAsegurado = dr["NOMBRE ASEGURADO"].ToString(),
                                NombreGirador = dr["NOMBRE GIRADOR"].ToString(),
                                Numero = dr["NUMERO"].ToString().TrimStart('0'),
                                NumeroAgrupar = dr["NUMERO AGRUPAR"].ToString(),
                                Originales = dr["DOCORI"].ToString(),
                                Paterno = dr["APEPAT"].ToString(),
                                Rut = dr["RUTNUM"].ToString().TrimStart('0'),
                                RutAsegurado = dr["RUT ASEGURADO"].ToString(),
                                RutCliente = dr["RUT CLIENTE"].ToString(),
                                RutGirador = dr["RUT GIRADOR"].ToString(),
                                Saldo = decimal.Parse(dr["SALDO"].ToString()),
                                Telefono1 = dr["TELEFONO1"].ToString(),
                                Telefono2 = dr["TELEFONO2"].ToString(),
                                Telefono3 = dr["TELEFONO3"].ToString(),
                                Telefono4 = dr["TELEFONO4"].ToString(),
                                Telefono5 = dr["TELEFONO5"].ToString(),
                                TipoCambio = decimal.Parse(dr["TIPO CAMBIO"].ToString()),
                                TipoDocumento = dr["TIPO DOCUMENTO"] != null ? dr["TIPO DOCUMENTO"].ToString(): "",
                                NumeroResolucion = dr["TIPO DOCUMENTO"] != null ? dr["NUMRESOLUCION"].ToString() : "",
                                RutRepresentante1 = dr["TIPO DOCUMENTO"] != null ? dr["RutRepresentante1"].ToString() : "",
                                NombreRepresentante1 = dr["TIPO DOCUMENTO"] != null ? dr["NombreRepresentante1"].ToString() : "",
                                RutRepresentante2 = dr["TIPO DOCUMENTO"] != null ? dr["RutRepresentante2"].ToString() : "",
                                NombreRepresentante2 = dr["TIPO DOCUMENTO"] != null ? dr["NombreRepresentante2"].ToString() : "",
                                RutRepresentante3 = dr["TIPO DOCUMENTO"] != null ? dr["RutRepresentante3"].ToString() : "",
                                NombreRepresentante3 = dr["TIPO DOCUMENTO"] != null ? dr["NombreRepresentante3"].ToString() : ""
                            });
                        }
                    }
                    else
                    {
                        for (int j = 1; j < ds.Tables[0].Rows.Count; j++)
                        {
                            DataRow dr = ds.Tables[0].Rows[j];
                            lst.Add(new DatosCarga
                            {
                                Antecedentes = dr["DOCANT"].ToString(),
                                Banco = dr["BANCO"].ToString(),
                                Capital = decimal.Parse(dr["CAPITAL"].ToString()),
                                Celular1 = dr["CELULAR1"].ToString(),
                                Celular2 = dr["CELULAR2"].ToString(),
                                Celular3 = dr["CELULAR3"].ToString(),
                                Celular4 = dr["CELULAR4"].ToString(),
                                Celular5 = dr["CELULAR5"].ToString(),
                                CodigoCarga = dr["CODIGO CARGA"].ToString(),
                                Comentario = dr["COMENTARIO"].ToString(),
                                Comuna = dr["COMUNA"].ToString(),
                                Direccion1 = dr["DIRECCION1"].ToString(),
                                Direccion2 = dr["DIRECCION2"].ToString(),
                                Dv = dr["RUTDV"].ToString(),
                                Fax = dr["FAX"].ToString(),
                                FechaDocumento = DateTime.Parse(dr["FECDOC"].ToString()),
                                FechaVencimiento = DateTime.Parse(dr["FECVENC"].ToString()),
                                GastoJudicial = decimal.Parse(dr["GASTOJUD"].ToString()),
                                GastoPreJudicial = decimal.Parse(dr["GASTOPRE"].ToString()),
                                Mail1 = dr["MAIL1"].ToString(),
                                Mail2 = dr["MAIL2"].ToString(),
                                Mail3 = dr["MAIL3"].ToString(),
                                Materno = dr["APEMAT"].ToString(),
                                Moneda = dr["MONEDA"].ToString(),
                                MontoAsignado = decimal.Parse(dr["MONTO ASIGNADO"].ToString()),
                                MotivoCobranza = dr["MOTIVO COBRANZA"].ToString(),
                                Negocio = dr["NEGOCIO"].ToString(),
                                Nombre = dr["NOMBRE"].ToString(),
                                NombreAsegurado = dr["NOMBRE ASEGURADO"].ToString(),
                                NombreGirador = dr["NOMBRE GIRADOR"].ToString(),
                                Numero = dr["NUMERO"].ToString().TrimStart('0'),
                                NumeroAgrupar = dr["NUMERO AGRUPAR"].ToString(),
                                Originales = dr["DOCORI"].ToString(),
                                Paterno = dr["APEPAT"].ToString(),
                                Rut = dr["RUTNUM"].ToString().TrimStart('0'),
                                RutAsegurado = dr["RUT ASEGURADO"].ToString(),
                                RutCliente = dr["RUT CLIENTE"].ToString(),
                                RutGirador = dr["RUT GIRADOR"].ToString(),
                                Saldo = decimal.Parse(dr["SALDO"].ToString()),
                                Telefono1 = dr["TELEFONO1"].ToString(),
                                Telefono2 = dr["TELEFONO2"].ToString(),
                                Telefono3 = dr["TELEFONO3"].ToString(),
                                Telefono4 = dr["TELEFONO4"].ToString(),
                                Telefono5 = dr["TELEFONO5"].ToString(),
                                TipoCambio = decimal.Parse(dr["TIPO CAMBIO"].ToString()),
                                TipoDocumento = dr["TIPO DOCUMENTO"].ToString(),
                                RutTercero = dr["RUT TERCERO"].ToString(),
                                NombreTercero = dr["NOMBRE TERCERO"].ToString()
                            });
                        }
                    }
                   
                    //ds.Tables[0].Rows[0].Delete();
                    
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                throw new Exception(ex.Message);
            }

            return lst;
        }

        public static List<DatosCarga> CargarDatosOriencoop(string nombreArchivo, int codemp, int pclid, int codigoCarga)
        {
            List<DatosCarga> lst = new List<DatosCarga>();
            Funciones obj = new Funciones();
            try
            {
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["RutaArchivos"] + obj.Configuracion_Str(15) + "\\" + nombreArchivo, false))
                {
                    string strLinea;
                    while ((strLinea = sr.ReadLine()) != null)
                    {
                        string cuotaVencida = strLinea.Substring(49, 8).Substring(4, 4).Substring(1, 3);
                        string cuotasTotales = strLinea.Substring(57, 4).Substring(1, 3);
                        string numero = strLinea.Substring(0, 13).Substring(8, 5) + "/" + cuotaVencida + "-" + cuotasTotales;
                        decimal capitalInsoluto = Decimal.Parse(strLinea.Substring(30, 15) + ","+ strLinea.Substring(45, 4));
                        decimal montoCapital = Decimal.Parse(strLinea.Substring(69, 15) + "," + strLinea.Substring(84, 4));
                        decimal montoInteres = Decimal.Parse(strLinea.Substring(88, 15) + "," + strLinea.Substring(103, 4));
                        decimal valorCuota = Decimal.Parse(strLinea.Substring(107, 15) +"," + strLinea.Substring(122, 4));
                        decimal insoluto = Decimal.Parse(strLinea.Substring(126, 15) + "," + strLinea.Substring(141, 4));
                        decimal montoDisponible = Decimal.Parse(strLinea.Substring(454, 15) + "," + strLinea.Substring(469, 4));
                        decimal excedente = Decimal.Parse(strLinea.Substring(564, 15) + "," + strLinea.Substring(579, 4));
                        string moneda = "";
                        if( strLinea.Substring(21, 1) == "1"){
                            moneda = "PESOS";
                        } else {
                            moneda = "UF";
                        }
                        int sucursal = Int32.Parse(strLinea.Substring(13, 4));
                        string nombreSucursal = dao.Cliente.BuscarNombreSucursalCliente(codemp, pclid,sucursal);
                        if(strLinea.Substring(445, 1) == "T"){
                        lst.Add(new DatosCarga
                        {
                            RutCliente = "700109208",
                            Rut = strLinea.Substring(145, 9).TrimStart('0'),
                            Dv = strLinea.Substring(154, 1),
                            Nombre=strLinea.Substring(155, 30),
                            Paterno=strLinea.Substring(185, 20),
                            Materno=strLinea.Substring(205, 20),
                            Comuna=strLinea.Substring(285, 30),
                            //nulo
                            Direccion1=strLinea.Substring(225, 60),
                            Direccion2=strLinea.Substring(335, 60),
                            Telefono1=strLinea.Substring(315, 20),
                            Telefono2=strLinea.Substring(425, 20),
                            Telefono3=strLinea.Substring(603, 20),
                            Telefono4=strLinea.Substring(623, 20),
                            Telefono5=strLinea.Substring(643, 20),
                            Celular1=strLinea.Substring(583, 20),
                            Celular2="",
                            Celular3="",
                            Celular4="",
                            Celular5="",
                            Fax="",
                            Mail1="",
                            Mail2="",
                            Mail3="",
                            TipoDocumento="CREDITO",
                            Numero=numero,
                            FechaDocumento=DateTime.ParseExact(strLinea.Substring(22, 8), "ddMMyyyy", CultureInfo.InvariantCulture), //strLinea.Substring(22, 8),
                            FechaVencimiento=DateTime.ParseExact(strLinea.Substring(61, 8), "ddMMyyyy", CultureInfo.InvariantCulture), //strLinea.Substring(61, 8),
                            MotivoCobranza="NO PAGO",
                            CodigoCarga= codigoCarga.ToString() ,
                            Moneda=moneda,
                            TipoCambio=1,
                            MontoAsignado=valorCuota,
                            Capital=capitalInsoluto,
                            Saldo=valorCuota,
                            GastoJudicial=0,
                            GastoPreJudicial=0,
                            Banco="",
                            RutGirador="",
                            NombreGirador="",
                            Negocio=strLinea.Substring(0, 13),
                            NumeroAgrupar=nombreSucursal,
                            RutAsegurado="",
                            NombreAsegurado="",
                            Originales="N",
                            Antecedentes="S",
                            RutTercero = "",
                            NombreTercero = "",
                            Comentario="PRODUCTO: " + dao.Cliente.BuscarNombreProductoCliente(codemp,pclid, Int32.Parse(strLinea.Substring(17, 4))) + " - CAMPAÑA: " + strLinea.Substring(663, strLinea.Length - 663)
                        });
                        }
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return lst;
            }
        }

        public static List<CargaJudicial> CargarDatosJudicialCsv(string nombreArchivo)
        {
            List<CargaJudicial> lst = new List<CargaJudicial>();
            DateTime fechaDocumento = new DateTime(), fechaVencimiento = new DateTime();
            Funciones obj = new Funciones();
            CultureInfo cl = new CultureInfo("es-CL");
            string Format = "dd-MM-yyyy";

            try
            {
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["RutaArchivos"] + obj.Configuracion_Str(15) + "\\" + nombreArchivo, false))
                {
                    string strLinea;
                    int i = 0;
                    while ((strLinea = sr.ReadLine()) != null)
                    {
                        if(i >= 1){
                            var values = strLinea.Split(';');
                            if(values.Length < 66){
                                values = strLinea.Split(',');
                            }

                            var rutCliente = int.Parse(values[0]);

                            fechaDocumento = DateTime.ParseExact(values[25].ToString(), Format, cl);
                            fechaVencimiento = DateTime.ParseExact(values[26].ToString(), Format, cl);
                            if (!string.IsNullOrEmpty(values[0].ToString()))
                            {
                                lst.Add(new CargaJudicial
                                {
                                    Antecedentes = values[44].ToString(),
                                    Banco = values[36].ToString(),
                                    Capital = decimal.Parse(values[32].ToString().Replace(".", "")),
                                    Celular1 = values[14].ToString(),
                                    Celular2 = values[15].ToString(),
                                    Celular3 = values[16].ToString(),
                                    Celular4 = values[17].ToString(),
                                    Celular5 = values[18].ToString(),
                                    CodigoCarga = values[28].ToString(),
                                    Comentario = values[45].ToString(),
                                    Comuna = values[6].ToString(),
                                    Direccion1 = values[7].ToString(),
                                    Direccion2 = values[8].ToString(),
                                    Dv = values[2].ToString(),
                                    Fax = values[19].ToString(),
                                    FechaDocumento = fechaDocumento,// DateTime.Parse(dr["FECDOC"].ToString()),
                                    FechaVencimiento = fechaVencimiento,//DateTime.Parse(dr["FECVENC"].ToString()),
                                    GastoJudicial = decimal.Parse(values[34].ToString().Replace(".", "")),
                                    GastoPreJudicial = decimal.Parse(values[35].ToString().Replace(".", "")),
                                    Mail1 = values[20].ToString(),
                                    Mail2 = values[21].ToString(),
                                    Mail3 = values[22].ToString(),
                                    Materno = values[5].ToString(),
                                    Moneda = values[29].ToString(),
                                    MontoAsignado = decimal.Parse(values[31].ToString().Replace(".", "")),
                                    MotivoCobranza = values[27].ToString(),
                                    Negocio = values[39].ToString(),
                                    Nombre = values[3].ToString(),
                                    NombreAsegurado = values[42].ToString(),
                                    NombreGirador = values[38].ToString(),
                                    Numero = values[24].ToString(),
                                    NumeroAgrupar = values[40].ToString(),
                                    Originales = values[43].ToString(),
                                    Paterno = values[4].ToString(),
                                    Rut = values[1].ToString(),
                                    RutAsegurado = values[41].ToString(),
                                    RutCliente = values[0].ToString(),
                                    RutGirador = values[37].ToString(),
                                    Saldo = decimal.Parse(values[33].ToString().Replace(".", "")),
                                    Telefono1 = values[9].ToString(),
                                    Telefono2 = values[10].ToString(),
                                    Telefono3 = values[11].ToString(),
                                    Telefono4 = values[12].ToString(),
                                    Telefono5 = values[13].ToString(),
                                    TipoCambio = decimal.Parse(values[30].ToString().Replace(".", "")),
                                    TipoDocumento = values[23].ToString(),

                                    RutTribunal = values[46].ToString(),
                                    TipoCausa = values[47].ToString(),
                                    RutAbogado = values[48].ToString(),
                                    RutProcurador = values[49].ToString(),
                                    FechaAvenimiento = !string.IsNullOrEmpty(values[50].ToString()) ? DateTime.ParseExact(values[50].ToString(), Format, cl) : new DateTime(),
                                    MontoAvenimiento = !string.IsNullOrEmpty(values[51].ToString()) ? decimal.Parse(values[51].ToString().Replace(".", ""), CultureInfo.InvariantCulture) : 0,
                                    NumeroCuotasAvenimiento = !string.IsNullOrEmpty(values[52].ToString()) ? Int32.Parse(values[52].ToString()) : 0,
                                    MontoCuotasAvenimiento = !string.IsNullOrEmpty(values[53].ToString()) ? decimal.Parse(values[53].ToString().Replace(".", "")) : 0,
                                    FechaPrimeraCuotaAvenimiento = !string.IsNullOrEmpty(values[54].ToString()) ? DateTime.ParseExact(values[54].ToString(), Format, cl) : new DateTime(),
                                    InteresAvenimiento = !string.IsNullOrEmpty(values[55].ToString()) ? decimal.Parse(values[55].ToString().Replace(".", "")) : 0,
                                    FechaDemanda = !string.IsNullOrEmpty(values[56].ToString()) ? DateTime.ParseExact(values[56].ToString(), Format, cl) : new DateTime(),
                                    MontoDemanda = !string.IsNullOrEmpty(values[57].ToString()) ? decimal.Parse(values[57].ToString().Replace(".", "")) : 0,
                                    NumeroCuotasDemanda = !string.IsNullOrEmpty(values[58].ToString()) ? Int32.Parse(values[58].ToString()) : 0,
                                    MontoCuotasDemanda = !string.IsNullOrEmpty(values[59].ToString()) ? decimal.Parse(values[59].ToString().Replace(".", "")) : 0,
                                    MontoUltimaCuotaDemanda = !string.IsNullOrEmpty(values[60].ToString()) ? decimal.Parse(values[60].ToString().Replace(".", "")) : 0,
                                    FechaPrimeraCuotaDemanda = !string.IsNullOrEmpty(values[61].ToString()) ? DateTime.ParseExact(values[61].ToString(), Format, cl) : new DateTime(),
                                    FechaUltimaCuotaDemanda = !string.IsNullOrEmpty(values[62].ToString()) ? DateTime.ParseExact(values[62].ToString(), Format, cl) : new DateTime(),
                                    InteresDemanda = !string.IsNullOrEmpty(values[63].ToString()) ? decimal.Parse(values[63].ToString().Replace(".", "")) : 0,
                                    RutTercero = values[64].ToString(),
                                    NombreTercero = values[65].ToString()
                                });
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lst;
        }

        public static List<CargaJudicial> CargarDatosJudicial(string nombreArchivo)
        {
            List<CargaJudicial> lst = new List<CargaJudicial>();
            DateTime fechaDocumento = new DateTime(), fechaVencimiento = new DateTime();

            try
            {
                DataSet ds = Funciones.CargarExcel(nombreArchivo);
                if (ds.Tables.Count > 0)
                {
                    //Ya que las columnas viene renombradas por defecto con F1 hasta Fn
                    // Se le asigna el nombre de las columnas que vienen en el excel
                    DataRow drn = ds.Tables[0].Rows[0];
                    for (int i = 0; i < drn.Table.Columns.Count; i++)
                    {
                        ds.Tables[0].Columns[i].ColumnName = drn[i].ToString();
                    }
                    //Inicio el ciclo con la segunda fila, ya que la primera contiene nombre de las columnas
                    for (int j = 1; j < ds.Tables[0].Rows.Count; j++)
                    {
                        DataRow dr = ds.Tables[0].Rows[j];
                        if (dr["RUT CLIENTE"].ToString() != string.Empty)
                        {
                            if(DateTime.TryParse(dr["FECDOC"].ToString(),out fechaDocumento) && DateTime.TryParse(dr["FECVENC"].ToString(), out fechaVencimiento))
                            {

                            } else
                            {
                                // Creates a CultureInfo for German in Germany.
                                CultureInfo cl = new CultureInfo("es-CL");
                                // Displays dt, formatted using the ShortDatePattern
                                // and the CultureInfo.
                                fechaDocumento=DateTime.Parse(dr["FECDOC"].ToString(), cl);
                                fechaVencimiento=DateTime.Parse(dr["FECVENC"].ToString(), cl);
                            }
                            lst.Add(new CargaJudicial
                            {
                                Antecedentes = dr["DOCANT"].ToString(),
                                Banco = dr["BANCO"].ToString(),
                                Capital = decimal.Parse(dr["CAPITAL"].ToString()),
                                Celular1 = dr["CELULAR1"].ToString(),
                                Celular2 = dr["CELULAR2"].ToString(),
                                Celular3 = dr["CELULAR3"].ToString(),
                                Celular4 = dr["CELULAR4"].ToString(),
                                Celular5 = dr["CELULAR5"].ToString(),
                                CodigoCarga = dr["CODIGO CARGA"].ToString(),
                                Comentario = dr["COMENTARIO"].ToString(),
                                Comuna = dr["COMUNA"].ToString(),
                                Direccion1 = dr["DIRECCION1"].ToString(),
                                Direccion2 = dr["DIRECCION2"].ToString(),
                                Dv = dr["RUTDV"].ToString(),
                                Fax = dr["FAX"].ToString(),
                                FechaDocumento = fechaDocumento,// DateTime.Parse(dr["FECDOC"].ToString()),
                                FechaVencimiento = fechaVencimiento,//DateTime.Parse(dr["FECVENC"].ToString()),
                                GastoJudicial = decimal.Parse(dr["GASTOJUD"].ToString()),
                                GastoPreJudicial = decimal.Parse(dr["GASTOPRE"].ToString()),
                                Mail1 = dr["MAIL1"].ToString(),
                                Mail2 = dr["MAIL2"].ToString(),
                                Mail3 = dr["MAIL3"].ToString(),
                                Materno = dr["APEMAT"].ToString(),
                                Moneda = dr["MONEDA"].ToString(),
                                MontoAsignado = decimal.Parse(dr["MONTO ASIGNADO"].ToString()),
                                MotivoCobranza = dr["MOTIVO COBRANZA"].ToString(),
                                Negocio = dr["NEGOCIO"].ToString(),
                                Nombre = dr["NOMBRE"].ToString(),
                                NombreAsegurado = dr["NOMBRE ASEGURADO"].ToString(),
                                NombreGirador = dr["NOMBRE GIRADOR"].ToString(),
                                Numero = dr["NUMERO"].ToString(),
                                NumeroAgrupar = dr["NUMERO AGRUPAR"].ToString(),
                                Originales = dr["DOCORI"].ToString(),
                                Paterno = dr["APEPAT"].ToString(),
                                Rut = dr["RUTNUM"].ToString(),
                                RutAsegurado = dr["RUT ASEGURADO"].ToString(),
                                RutCliente = dr["RUT CLIENTE"].ToString(),
                                RutGirador = dr["RUT GIRADOR"].ToString(),
                                Saldo = decimal.Parse(dr["SALDO"].ToString()),
                                Telefono1 = dr["TELEFONO1"].ToString(),
                                Telefono2 = dr["TELEFONO2"].ToString(),
                                Telefono3 = dr["TELEFONO3"].ToString(),
                                Telefono4 = dr["TELEFONO4"].ToString(),
                                Telefono5 = dr["TELEFONO5"].ToString(),
                                TipoCambio = decimal.Parse(dr["TIPO CAMBIO"].ToString()),
                                TipoDocumento = dr["TIPO DOCUMENTO"].ToString(),

                                RutTribunal = dr["TRIBUNAL RUT"].ToString(),
                                TipoCausa = dr["TIPOCAUSA"].ToString(),
                                RutAbogado = dr["ABOGADO RUT"].ToString(),
                                RutProcurador = dr["PROCURADOR RUT"].ToString(),
                                FechaAvenimiento = !string.IsNullOrEmpty(dr["FECAVE"].ToString()) ? DateTime.Parse(dr["FECAVE"].ToString()) : new DateTime(),
                                MontoAvenimiento = !string.IsNullOrEmpty(dr["MONTAVE"].ToString()) ? decimal.Parse(dr["MONTAVE"].ToString(), CultureInfo.InvariantCulture) : 0,
                                NumeroCuotasAvenimiento = !string.IsNullOrEmpty(dr["NROCUOAVE"].ToString()) ? Int32.Parse(dr["NROCUOAVE"].ToString()) : 0,
                                MontoCuotasAvenimiento = !string.IsNullOrEmpty(dr["MONTCUOAVE"].ToString()) ? decimal.Parse(dr["MONTCUOAVE"].ToString()) : 0,
                                FechaPrimeraCuotaAvenimiento = !string.IsNullOrEmpty(dr["FEC1CUOAVE"].ToString()) ? DateTime.Parse(dr["FEC1CUOAVE"].ToString()) : new DateTime(),
                                InteresAvenimiento = !string.IsNullOrEmpty(dr["INTAVE"].ToString()) ? decimal.Parse(dr["INTAVE"].ToString()) : 0,
                                FechaDemanda = !string.IsNullOrEmpty(dr["FECDEM"].ToString()) ? DateTime.Parse(dr["FECDEM"].ToString()) : new DateTime(),
                                MontoDemanda = !string.IsNullOrEmpty(dr["MTODEM"].ToString()) ? decimal.Parse(dr["MTODEM"].ToString()) : 0,
                                NumeroCuotasDemanda = !string.IsNullOrEmpty(dr["NROCUODEM"].ToString()) ? Int32.Parse(dr["NROCUODEM"].ToString()) : 0,
                                MontoCuotasDemanda = !string.IsNullOrEmpty(dr["MONTCUO"].ToString()) ? decimal.Parse(dr["MONTCUO"].ToString()) : 0,
                                MontoUltimaCuotaDemanda = !string.IsNullOrEmpty(dr["MONTULTCUO"].ToString()) ? decimal.Parse(dr["MONTULTCUO"].ToString()) : 0,
                                FechaPrimeraCuotaDemanda = !string.IsNullOrEmpty(dr["FEC1CUODEM"].ToString()) ? DateTime.Parse(dr["FEC1CUODEM"].ToString()) : new DateTime(),
                                FechaUltimaCuotaDemanda = !string.IsNullOrEmpty(dr["FECUCUODEM"].ToString()) ? DateTime.Parse(dr["FECUCUODEM"].ToString()) : new DateTime(),
                                InteresDemanda = !string.IsNullOrEmpty(dr["INTDEM"].ToString()) ? decimal.Parse(dr["INTDEM"].ToString()) : 0,
                                RutTercero = dr["RUT TERCERO"].ToString(),
                                NombreTercero = dr["NOMBRE TERCERO"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lst;
        }

        public static List<DatosCargaPago> CargarDatosPago(string nombreArchivo)
        {
            List<DatosCargaPago> lst = new List<DatosCargaPago>();
            try
            {
                DataSet ds = Funciones.CargarExcel(nombreArchivo);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        lst.Add(new DatosCargaPago
                        {
                            RutCliente = dr["RUTCLIENTE"].ToString(),
                            Rut = dr["RUT"].ToString(),
                            Dv = dr["DV"].ToString(),
                            NumeroCpbt = dr["NUMCPBT"].ToString(),
                            Monto = decimal.Parse(dr["MONTO"].ToString()),
                            Interes = decimal.Parse(dr["INTERES"].ToString()),
                            Honorario = decimal.Parse(dr["HONORARIO"].ToString()),
                            Moneda = dr["MONEDA"].ToString(),
                            TipoCambio = decimal.Parse(dr["TIPCAMBIO"].ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public static List<DatosCargaPago> CargarDatosPagoOriencoop(string nombreArchivo, int codemp, int pclid)
        {
            List<DatosCargaPago> lst = new List<DatosCargaPago>();
            Funciones obj = new Funciones();
            try
            {
                using (StreamReader sr = new StreamReader(ConfigurationManager.AppSettings["RutaArchivos"] + obj.Configuracion_Str(15) + "\\" + nombreArchivo, false))
                {
                    string strLinea;
                    string cuotaVencida = "";
                    string numero = "";
                    decimal monto = 0;
                    while ((strLinea = sr.ReadLine()) != null)
                    {
                        cuotaVencida = strLinea.Substring(35, 8).Substring(5, 3);
                        numero = strLinea.Substring(10, 13).Substring(8, 5) + "/" + cuotaVencida;
                        monto = Decimal.Parse(strLinea.Substring(43, 15));
                        lst.Add(new DatosCargaPago
                        {
                            RutCliente = "700109208",
                            Rut = strLinea.Substring(0, 9),
                            Dv = strLinea.Substring(9, 1),
                            NumeroCpbt = numero,
                            Monto = monto,
                            Interes = 0,
                            Honorario = 0,
                            Moneda = "",
                            TipoCambio = 1
                        });
                    }
                }
                return lst;
            }
            catch (Exception)
            {
                return lst;
            }
        }

        #endregion

        #region "Proceso Carga Masiva"

        public static bool ProcesoCarga(List<dto.DatosCarga> lst, dto.CargaMasiva objCarga, UserSession objSession)
        {
            bool error = false;
            int ctcid=0;
            int idTipoDocumento = 0;
            ErrorCarga objError = new ErrorCarga();
            Funciones objFunc = new Funciones();
            bool[] salida = {false,false};
            List<Combobox> lstTipoDocumento = dao.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma);
            Combobox tipoDocumento = new Combobox();
            //REcorremos los registros del archivo
            if (lst.Count > 0)
            {
                foreach (dto.DatosCarga datos in lst)
                {
                    //Valido que el cliente del archivo sea igual al cliente ingresado
                    if (objCarga.RutCliente != datos.RutCliente)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "CLIENTE NO CONCUERDA",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                    if (!error)
                    {
                        ctcid = dao.Deudor.BuscarIdDeudor(datos.Rut + datos.Dv, objSession.CodigoEmpresa);
                        salida = CrearDeudoresCarga(datos, objCarga, ctcid, objSession);
                    }
                }
                if (!salida[0] && !error)
                {
                    foreach (dto.DatosCarga datos in lst)
                    {
                        tipoDocumento = lstTipoDocumento.Find(x => x.Text == datos.TipoDocumento);
                        if (tipoDocumento == null)
                        {
                            tipoDocumento = lstTipoDocumento.Find(x => x.Text == datos.TipoDocumento.Replace("FACTURA","FACT."));
                        }
                        if (tipoDocumento == null)
                        {
                            idTipoDocumento = -1;
                        }
                        else
                        {
                            idTipoDocumento = Int32.Parse(tipoDocumento.Value);
                        }
                        salida = ValidarCargaNueva(datos, objCarga, objSession, idTipoDocumento);
                        if (!salida[0] && !salida[1] && objCarga.Pclid == 424)
                        {
                            salida = ValidarCargaAnterior(datos, objCarga, new UserSession(), idTipoDocumento);
                            
                        }
                        //Comienzo a Grabar los Documentos
                        if (!salida[0] && !salida[1])
                        {
                            salida = GrabarDocumentos(datos, objCarga, objSession, idTipoDocumento);
                        }
                        if (salida[0])
                        {
                            objCarga.ListaErrores.Add(new ErrorCarga
                            {
                                TipoError = "ERROR AL GRABAR DOCUMENTO",
                                Rut = datos.Rut + "-" +datos.Dv,
                                Dv = datos.Dv,
                                Nombre = datos.NombreCompleto(),
                                TipoDocumento = datos.TipoDocumento,
                                Numero = datos.Numero
                            });
                            error = true;
                        }
                    }
                }
                 if(!salida[0] && !error){
                     error = false;
                 } else {
                     error =true;
                 }
                
            }
            return error;
        }

        public static bool ProcesoCargaJudicial(List<dto.CargaJudicial> lst, dto.CargaMasiva objCarga, UserSession objSession)
        {
            bool error = false;
            int ctcid = 0;
            int idTipoDocumento = 0;
            ErrorCarga objError = new ErrorCarga();
            Funciones objFunc = new Funciones();
            bool[] salida = { false, false };
            List<Combobox> lstTipoDocumento = dao.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma);
            Combobox tipoDocumento = new Combobox();

            //Recorremos los registros del archivo
            if (lst.Count > 0)
            {
                foreach (dto.CargaJudicial datos in lst)
                {
                    //Valido que el cliente del archivo sea igual al cliente ingresado
                    if (objCarga.RutCliente != datos.RutCliente)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "CLIENTE NO CONCUERDA",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                    if (!error)
                    {
                        ctcid = dao.Deudor.BuscarIdDeudor(datos.Rut + datos.Dv, objSession.CodigoEmpresa);
                        salida = CrearDeudoresCarga(datos, objCarga, ctcid, objSession);
                    }
                }
                if (!salida[0])
                {
                    foreach (dto.CargaJudicial datos in lst)
                    {
                        tipoDocumento = lstTipoDocumento.Find(x => x.Text == datos.TipoDocumento);
                        if (tipoDocumento == null)
                        {
                            idTipoDocumento = -1;
                        }
                        else
                        {
                            idTipoDocumento = Int32.Parse(tipoDocumento.Value);
                        }
                        salida = ValidarProcesoCargaJudicial(datos, objCarga, objSession, idTipoDocumento);
                       /* if (!salida[0] && !salida[1])
                        {
                            salida[0] = ValidarCargaNuevaJudicial(datos, objCarga, objSession);

                        }*/
                        //Comienzo a Grabar los Documentos
                        if (!salida[0] && !salida[1])
                        {
                            ctcid = dao.Deudor.BuscarIdDeudor(datos.Rut + datos.Dv, objSession.CodigoEmpresa);
                            salida = GrabarDocumentos(datos, objCarga, ctcid, objSession, idTipoDocumento);
                        }
                        if (salida[0])
                        {
                            objCarga.ListaErrores.Add(new ErrorCarga
                            {
                                TipoError = "ERROR AL GRABAR DOCUMENTO",
                                Rut = datos.Rut + "-" + datos.Dv,
                                Dv = datos.Dv,
                                Nombre = datos.NombreCompleto(),
                                TipoDocumento = datos.TipoDocumento,
                                Numero = datos.Numero
                            });
                            error = true;
                        }
                        else
                        {
                            //salida = GrabarRol(datos, objCarga, ctcid, objSession);
                        }
                    }
                }
                if (!salida[0] && !error)
                {
                    error = false;
                }
                else
                {
                    error = true;
                }

            }
            return error;
        }

        public static bool ProcesoPanelCargaMasiva(List<dto.CargaJudicial> lst, dto.CargaMasiva objCarga, UserSession objSession)
        {
            bool error = false;
            int ctcid = 0;
            int idTipoDocumento = 0;
            ErrorCarga objError = new ErrorCarga();
            Funciones objFunc = new Funciones();
            bool[] salida = { false, false };
            List<Combobox> lstTipoDocumento = dao.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma);
            Combobox tipoDocumento = new Combobox();
            //REcorremos los registros del archivo
            if (lst.Count > 0)
            {
                foreach (dto.CargaJudicial datos in lst)
                {
                    //Valido que el cliente del archivo sea igual al cliente ingresado
                    if (objCarga.RutCliente != datos.RutCliente)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "CLIENTE NO CONCUERDA",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                    if (!error)
                    {
                        ctcid = dao.Deudor.BuscarIdDeudor(datos.Rut + datos.Dv, objSession.CodigoEmpresa);
                        salida = CrearDeudoresCarga(datos, objCarga, ctcid, objSession);
                    }
                }
                if (!salida[0])
                {
                    foreach (dto.CargaJudicial datos in lst)
                    {
                        tipoDocumento = lstTipoDocumento.Find(x => x.Text == datos.TipoDocumento);
                        if (tipoDocumento == null)
                        {
                            idTipoDocumento = -1;
                        }
                        else
                        {
                            idTipoDocumento = Int32.Parse(tipoDocumento.Value);
                        }
                        salida = ValidarCargaNueva(datos, objCarga, objSession, idTipoDocumento);

                        /*if (!salida[0] && !salida[1])
                        {
                            salida[0] = ValidarCargaNuevaJudicial(datos, objCarga, objSession);

                        }*/

                        //Comienzo a Grabar los Documentos
                        if (!salida[0] && !salida[1])
                        {
                            salida = GrabarDocumentosPanelMasivo(datos, objCarga, ctcid, objSession, idTipoDocumento);
                        }
                        if (salida[0])
                        {
                            objCarga.ListaErrores.Add(new ErrorCarga
                            {
                                TipoError = "ERROR AL GRABAR DOCUMENTO",
                                Rut = datos.Rut + "-" + datos.Dv,
                                Dv = datos.Dv,
                                Nombre = datos.NombreCompleto(),
                                TipoDocumento = datos.TipoDocumento,
                                Numero = datos.Numero
                            });
                            error = true;
                        }
                        else
                        {
                            //salida = GrabarRolTemporal(datos, objCarga, ctcid, objSession);
                        }
                    }
                }
                if (!salida[0] && !error)
                {
                    error = false;
                }
                else
                {
                    error = true;
                }

            }
            return error;
        }

        public static bool ProcesoCargaQuiebra(List<dto.DatosCarga> lst, dto.CargaMasiva objCarga, UserSession objSession)
        {
            int ctcid = 0;
            int error = 0;
            bool salida = false;
            foreach (DatosCarga datos in lst)
            {
                //Comienzo Grabado
                ctcid = dao.Deudor.BuscarIdDeudor(datos.Rut + datos.Dv, objSession.CodigoEmpresa);
                if (ctcid == 0)
                {
                    int idComuna = Direccion.BuscarComuna(datos.Comuna);
                    ctcid = dao.Deudor.GuardarDeudor(objSession.CodigoEmpresa, 0, datos.Nombre, datos.Paterno, datos.Materno, datos.Rut + datos.Dv, datos.NombreCompleto(), idComuna, "P", datos.Direccion1, "", "S", "N", 1, false);
                    if (error < 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL CREAR AL DEUDOR",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        salida = true;
                    }
                }
                else
                {
                    error = dao.Deudor.QuiebrarDeudor(objSession.CodigoEmpresa, ctcid, "S");
                    if (error < 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL ACTUALIZAR EN QUIEBRA AL DEUDOR",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        salida = true;
                    }
                }
            }

            return salida;
        }

        public static bool[] CrearDeudoresCarga(dto.DatosCarga datos, dto.CargaMasiva objCarga, int ctcid, UserSession objSession)
        {
            bool error = false;
            bool repetido = false;
            Funciones objFunc = new Funciones();
            int idComuna = Direccion.BuscarComuna(datos.Comuna);
            List<Contacto> lstContactos = new List<Contacto>();

            //Cliente no coincide
            if (datos.RutCliente != objCarga.RutCliente)
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "CLIENTE NO CONCUERDA",
                    Rut = datos.Rut + "-" +datos.Dv,
                    Dv = datos.Dv,
                    Nombre = datos.NombreCompleto(),
                    TipoDocumento = datos.TipoDocumento,
                    Numero = datos.Numero
                });
                error = true;
            }

            if (!error)
            {
                if (ctcid == 0)
                {
                    ctcid = dao.Deudor.GuardarDeudor(objSession.CodigoEmpresa, 0, datos.Nombre, datos.Paterno, datos.Materno, datos.Rut + datos.Dv, datos.NombreCompleto(), idComuna, "P", datos.Direccion1, "", "N", "N", 1, false);
                }
                else if (ctcid != 0 && objCarga.Pclid == 279) //actualiza datos deudor coopeuch
                {
                    ctcid = dao.Deudor.EditarDeudorParcial(objSession.CodigoEmpresa, ctcid, datos.Nombre, datos.Paterno, datos.Materno, datos.NombreCompleto(), idComuna, datos.Direccion1, 1);
                }
                //Marca al deudor en quiebra si tiene algun rol en el panel de quiebra
                dao.Deudor.MarcarQuiebraDeudor(objSession.CodigoEmpresa, ctcid);
                if (ctcid > 0)
                {
                    datos.ctcid = ctcid;
                    lstContactos = dao.Deudor.ListarContactos(objSession.CodigoEmpresa,ctcid);
                    if (lstContactos.Count == 0 || lstContactos.Find(x => x.Nombre == datos.NombreCompleto()) == null)//(dao.Deudor.BuscarContacto(objSession.CodigoEmpresa, ctcid, 4, (datos.Nombre + " " + datos.Paterno + " "+ datos.Materno).ToUpper()) == 0)
                    {
                        try
                        {

                            dao.Deudor.InsertarContacto(new Contacto
                            {
                                Codemp = objSession.CodigoEmpresa,
                                Comuna = idComuna.ToString(),
                                Ctcid = ctcid,
                                Ddcid = 0,
                                Direccion = datos.Direccion2 != "" ? datos.Direccion2 : datos.Direccion1,
                                Estado = "A",
                                EstadoContacto = "A",
                                Nombre = datos.NombreCompleto(),
                                Tipo = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 19).ToString()
                            });
                        }
                        catch (Exception ex)
                        {
                            objCarga.ListaErrores.Add(new ErrorCarga
                            {
                                TipoError = "ERROR AL CREAR CONTACTO",
                                Rut = datos.Rut + "-" +datos.Dv,
                                Dv = datos.Dv,
                                Nombre = datos.NombreCompleto(),
                                TipoDocumento = datos.TipoDocumento,
                                Numero = datos.Numero
                            });
                            error = true;
                        }
                    }

                    try
                    { 
                        long telefono = 0;
                        if (datos.Telefono1 != "")
                        {
                            telefono = Int64.Parse(datos.Telefono1);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "C",
                                    Numero = telefono,
                                    TipoTelefono = "C"
                                }, objSession.CodigoEmpresa);
                            }
                        }
                        if (datos.Telefono2 != "")
                        {
                            telefono = Int64.Parse(datos.Telefono2);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "C",
                                    Numero = telefono,
                                    TipoTelefono = "C"
                                }, objSession.CodigoEmpresa);
                            }
                        }
                        if (datos.Telefono3 != "")
                        {
                            telefono = Int64.Parse(datos.Telefono3);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "C",
                                    Numero = telefono,
                                    TipoTelefono = "C"
                                }, objSession.CodigoEmpresa);
                            }
                        }
                        if (datos.Telefono4 != "")
                        {
                            telefono = Int64.Parse(datos.Telefono4);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "C",
                                    Numero = telefono,
                                    TipoTelefono = "C"
                                }, objSession.CodigoEmpresa);
                            }
                        }
                        if (datos.Telefono5 != "")
                        {
                            telefono = Int64.Parse(datos.Telefono5);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "C",
                                    Numero = telefono,
                                    TipoTelefono = "C"
                                }, objSession.CodigoEmpresa);
                            }
                        }
                        if (datos.Celular1 != "")
                        {
                            telefono = Int64.Parse(datos.Celular1);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "M",
                                    Numero = telefono,
                                    TipoTelefono = "M"
                                }, objSession.CodigoEmpresa);
                            }
                        }
                        if (datos.Celular2 != "")
                        {
                            telefono = Int64.Parse(datos.Celular2);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "M",
                                    Numero = telefono,
                                    TipoTelefono = "M"
                                }, objSession.CodigoEmpresa);
                            }
                        }
                        if (datos.Celular3 != "")
                        {
                            telefono = Int64.Parse(datos.Celular3);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "M",
                                    Numero = telefono,
                                    TipoTelefono = "M"
                                }, objSession.CodigoEmpresa);
                            }
                        }
                        if (datos.Celular4 != "")
                        {
                            telefono = Int64.Parse(datos.Celular4);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "M",
                                    Numero = telefono,
                                    TipoTelefono = "M"
                                }, objSession.CodigoEmpresa);
                            }
                        }
                        if (datos.Celular5 != "")
                        {
                            telefono = Int64.Parse(datos.Celular5);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "M",
                                    Numero = telefono,
                                    TipoTelefono = "M"
                                }, objSession.CodigoEmpresa);
                            }
                        }
                        if (datos.Fax != "")
                        {
                            telefono = Int64.Parse(datos.Fax);
                            if (telefono != 0)
                            {
                                dao.Deudor.InsertarTelefono(new TelefonoDeudor
                                {
                                    Ctcid = ctcid,
                                    Codemp = objSession.CodigoEmpresa,
                                    EstadoTelefono = "A",
                                    IdEstadoTelefono = "A",
                                    IdTipoTelefono = "O",
                                    Numero = telefono,
                                    TipoTelefono = "O"
                                }, objSession.CodigoEmpresa);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL GUARDAR TELEFONO",
                            Rut = datos.Rut + "-" +datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }

                    try
                    {
                        if (datos.Mail1 != "")
                        {
                            dao.Deudor.InsertarEmail(new EmailDeudor
                            {
                                Ctcid = ctcid,
                                Codemp = objSession.CodigoEmpresa,
                                IdTipoMail = "P",
                                Mail = datos.Mail1,
                                Masivo = "S",
                                TipoMail = "P"
                            });
                        }
                        if (datos.Mail2 != "")
                        {
                            dao.Deudor.InsertarEmail(new EmailDeudor
                            {
                                Ctcid = ctcid,
                                Codemp = objSession.CodigoEmpresa,
                                IdTipoMail = "P",
                                Mail = datos.Mail2,
                                Masivo = "S",
                                TipoMail = "P"
                            });
                        }
                        if (datos.Mail3 != "")
                        {
                            dao.Deudor.InsertarEmail(new EmailDeudor
                            {
                                Ctcid = ctcid,
                                Codemp = objSession.CodigoEmpresa,
                                IdTipoMail = "P",
                                Mail = datos.Mail3,
                                Masivo = "S",
                                TipoMail = "P"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL GUARDAR MAIL",
                            Rut = datos.Rut + "-" +datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }

                    if (datos.RutAsegurado != "" && objCarga.Pclid != 424)
                    {
                        int sbcid = dao.Deudor.BuscarSubcartera(datos.RutAsegurado, objSession.CodigoEmpresa);
                        if (sbcid == 0)
                        {
                            try
                            {
                                sbcid = dao.Deudor.GuardarSubcartera(objSession.CodigoEmpresa, datos.RutAsegurado, datos.NombreAsegurado, 1, " ", 0);
                                if (sbcid < 0)
                                {
                                    objCarga.ListaErrores.Add(new ErrorCarga
                                    {
                                        TipoError = "ERROR AL CREAR ASEGURADO",
                                        Rut = datos.Rut + "-" +datos.Dv,
                                        Dv = datos.Dv,
                                        Nombre = datos.NombreCompleto(),
                                        TipoDocumento = datos.TipoDocumento,
                                        Numero = datos.Numero
                                    });
                                    error = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "ERROR AL CREAR ASEGURADO",
                                    Rut = datos.Rut + "-" +datos.Dv,
                                    Dv = datos.Dv,
                                    Nombre = datos.NombreCompleto(),
                                    TipoDocumento = datos.TipoDocumento,
                                    Numero = datos.Numero
                                });
                                error = true;
                            }
                        }

                    }

                    //Nueva seccion para crear un Tercero o socio de deuda
                    if (!string.IsNullOrEmpty(datos.RutTercero) && objCarga.Pclid != 424)
                    {
                        int idtercero = dao.Deudor.BuscarTercero(objSession.CodigoEmpresa, datos.RutTercero);
                        if (idtercero == 0)
                        {
                            try
                            {
                                idtercero = dao.Deudor.GuardarTercero(objSession.CodigoEmpresa, datos.RutTercero, datos.NombreTercero);
                                if (idtercero < 0)
                                {
                                    objCarga.ListaErrores.Add(new ErrorCarga
                                    {
                                        TipoError = "ERROR AL CREAR TERCERO",
                                        Rut = datos.Rut + "-" + datos.Dv,
                                        Dv = datos.Dv,
                                        Nombre = datos.NombreCompleto(),
                                        TipoDocumento = datos.TipoDocumento,
                                        Numero = datos.Numero
                                    });
                                    error = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "ERROR AL CREAR TERCERO",
                                    Rut = datos.Rut + "-" + datos.Dv,
                                    Dv = datos.Dv,
                                    Nombre = datos.NombreCompleto(),
                                    TipoDocumento = datos.TipoDocumento,
                                    Numero = datos.Numero
                                });
                                error = true;
                            }
                        }
                    }
                }
                else
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL CREAR DEUDOR",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }

            bool[] salida = { error, repetido  };
            return salida;
        }

        public static bool[] ValidarCargaNueva(dto.DatosCarga datos, dto.CargaMasiva objCarga, UserSession objSession, int idTipoDocumento)
        {
            bool error = false;
            bool repetido = false;
            Funciones objFunc = new Funciones();

            int idComuna = Direccion.BuscarComuna(datos.Comuna);
            //x = x + 1
            //                bad = True
            //                cont = cont + 1

            //Valido que el cliente del archivo sea igual al cliente ingresado
            if (objCarga.RutCliente != datos.RutCliente)
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "CLIENTE NO CONCUERDA",
                    Rut = datos.Rut + "-" +datos.Dv,
                    Dv = datos.Dv,
                    Nombre = datos.NombreCompleto(),
                    TipoDocumento = datos.TipoDocumento,
                    Numero = datos.Numero
                });
                error = true;
            }
            if (!error)
            {
                //Valido el rut del deudor
                if (!Funciones.ValidaRut(datos.Rut + datos.Dv))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("RutBad", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido las fechas
                if (datos.FechaDocumento > datos.FechaVencimiento)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "FECHA VENCIMIENTO INVALIDA",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido el nombre
                if (string.IsNullOrEmpty(datos.Nombre))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Nombre", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido la dirección
                if (string.IsNullOrEmpty(datos.Direccion1))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("DirNul", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido el tipo de documento
                if (string.IsNullOrEmpty(datos.TipoDocumento))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Tipo", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                if (idTipoDocumento < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "TIPO DOCUMENTO NO EXISTE",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido el numero
                if (string.IsNullOrEmpty(datos.Numero))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Numero", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido motivo cobranza
                if (string.IsNullOrEmpty(datos.MotivoCobranza))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Motivo", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                if (dao.Comprobante.BuscarMotivoCobranza(objSession.CodigoEmpresa, objSession.Idioma, datos.MotivoCobranza) < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MOTIVO COBRANZA NO EXISTE",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido moneda
                if (string.IsNullOrEmpty(datos.Moneda))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MONEDA NO PUEDE SER NULO",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);
                if (lstMoneda.Find(x => x.Text == datos.Moneda.ToUpper()) == null)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MONEDA NO EXISTE",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido montos
                if (datos.MontoAsignado == 0 || datos.Saldo == 0 || datos.Capital == 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Saldo", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                if (datos.Saldo > datos.Capital || datos.Saldo > datos.MontoAsignado)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("ErrSaldo", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido banco
                if (!string.IsNullOrEmpty(datos.Banco))
                {
                    if (dao.Comprobante.TraeBanco(objSession.CodigoEmpresa, datos.Banco) == 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "BANCO NO EXISTE",
                            Rut = datos.Rut + "-" +datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }

            }
            if (!error)
            {
                //Valido rut girador
                if (!string.IsNullOrEmpty(datos.RutGirador))
                {
                    if (datos.RutGirador == "0" || !Funciones.ValidaRut(datos.RutGirador) && objCarga.Pclid != 559) // mutual ley previsional)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "RUT GIRADOR INVALIDO",
                            Rut = datos.Rut + "-" +datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }
            }
            if (!error)
            {
                //Valido comuna
                if (string.IsNullOrEmpty(datos.Comuna) || idComuna == 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "COMUNA NO EXISTE",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }

            }
            if (!error)
            {
                //Valido subcartera
                if (!string.IsNullOrEmpty(datos.RutAsegurado)) 
                {
                    if (!Funciones.ValidaRut(datos.RutAsegurado) && objCarga.Pclid != 559) // mutual ley previsional
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "RUT ASEGURADO INVALIDO",
                            Rut = datos.Rut + "-" +datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }
            }

            if (!error)
            {
                //Valido Tercero
                if (!string.IsNullOrEmpty(datos.RutTercero))
                {
                    if (!Funciones.ValidaRut(datos.RutTercero) && objCarga.Pclid != 559) // mutual ley previsional
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "RUT TERCERO INVALIDO",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }
            }

            bool[] salida = { error, repetido };
            return salida;
        }

        public static bool[] ValidarProcesoCargaJudicial(dto.CargaJudicial datos, dto.CargaMasiva objCarga, UserSession objSession, int idTipoDocumento)
        {
            bool error = false;
            bool repetido = false;
            Funciones objFunc = new Funciones();

            int idComuna = Direccion.BuscarComuna(datos.Comuna);
            int tribunal = dao.Comprobante.BuscarTribunal(objSession.CodigoEmpresa, datos.RutTribunal);
            //x = x + 1
            //                bad = True
            //                cont = cont + 1

            //Valido que el cliente del archivo sea igual al cliente ingresado
            if (objCarga.RutCliente != datos.RutCliente)
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "CLIENTE NO CONCUERDA",
                    Rut = datos.Rut + "-" + datos.Dv,
                    Dv = datos.Dv,
                    Nombre = datos.NombreCompleto(),
                    TipoDocumento = datos.TipoDocumento,
                    Numero = datos.Numero
                });
                error = true;
            }
            if (!error)
            {
                //Valido el rut del deudor
                if (!Funciones.ValidaRut(datos.Rut + datos.Dv))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("RutBad", objSession.Idioma),
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido las fechas
                if (datos.FechaDocumento > datos.FechaVencimiento)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "FECHA VENCIMIENTO INVALIDA",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido el nombre
                if (string.IsNullOrEmpty(datos.Nombre))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Nombre", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido la dirección
                if (string.IsNullOrEmpty(datos.Direccion1))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("DirNul", objSession.Idioma),
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido el tipo de documento
                if (string.IsNullOrEmpty(datos.TipoDocumento))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Tipo", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                if (idTipoDocumento < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "TIPO DOCUMENTO NO EXISTE",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido el numero
                if (string.IsNullOrEmpty(datos.Numero))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Numero", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido motivo cobranza
                if (string.IsNullOrEmpty(datos.MotivoCobranza))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Motivo", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                if (dao.Comprobante.BuscarMotivoCobranza(objSession.CodigoEmpresa, objSession.Idioma, datos.MotivoCobranza) < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MOTIVO COBRANZA NO EXISTE",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido moneda
                if (string.IsNullOrEmpty(datos.Moneda))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MONEDA NO PUEDE SER NULO",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);
                if (lstMoneda.Find(x => x.Text == datos.Moneda.ToUpper()) == null)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MONEDA NO EXISTE",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido montos
                if (datos.MontoAsignado == 0 || datos.Saldo == 0 || datos.Capital == 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Saldo", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                if (datos.Saldo > datos.Capital || datos.Saldo > datos.MontoAsignado)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("ErrSaldo", objSession.Idioma),
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido banco
                if (!string.IsNullOrEmpty(datos.Banco))
                {
                    if (dao.Comprobante.TraeBanco(objSession.CodigoEmpresa, datos.Banco) == 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "BANCO NO EXISTE",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }

            }
            if (!error)
            {
                //Valido rut girador
                if (!string.IsNullOrEmpty(datos.RutGirador))
                {
                    if (datos.RutGirador == "0" || !Funciones.ValidaRut(datos.RutGirador) && objCarga.Pclid != 559) // mutual ley previsional)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "RUT GIRADOR INVALIDO",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }
            }
            if (!error)
            {
                //Valido comuna
                if (string.IsNullOrEmpty(datos.Comuna) || idComuna == 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "COMUNA NO EXISTE",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }

            }
            if (!error)
            {
                //Valido subcartera
                if (!string.IsNullOrEmpty(datos.RutAsegurado))
                {
                    if (!Funciones.ValidaRut(datos.RutAsegurado) && objCarga.Pclid != 559) // mutual ley previsional
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "RUT ASEGURADO INVALIDO",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }
            }

            if (!error)
            {
                //Valido Tercero
                if (!string.IsNullOrEmpty(datos.RutTercero))
                {
                    if (!Funciones.ValidaRut(datos.RutTercero) && objCarga.Pclid != 559) // mutual ley previsional
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "RUT TERCERO INVALIDO",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }
            }

            if (!error)
            {
                //Valido el Tribunal
                if (string.IsNullOrEmpty(datos.RutTribunal))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "TRIBUNAL SIN DATOS",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                else
                {
                    if (tribunal <= 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "TRIBUNAL NO ENCONTRADO",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                    else
                    {
                        datos.IdTribunal = tribunal;
                    }
                }
            }

            bool[] salida = { error, repetido };
            return salida;
        }

        public static bool ValidarCargaNuevaJudicial(dto.CargaJudicial datos, dto.CargaMasiva objCarga,  UserSession objSession)
        {
            bool error = false;
            int tribunal = dao.Comprobante.BuscarTribunal(objSession.CodigoEmpresa, datos.RutTribunal);
            int tipoCausa = dao.Comprobante.BuscarTipoCausa(objSession.CodigoEmpresa, datos.TipoCausa);
            int IdAbogado = dao.Comprobante.BuscarEnteJudicial(objSession.CodigoEmpresa, datos.RutAbogado);
            int idProcurador =dao.Comprobante.BuscarEnteJudicial(objSession.CodigoEmpresa, datos.RutProcurador);
            //Valido el Tribunal
            if (string.IsNullOrEmpty(datos.RutTribunal))
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "TRIBUNAL SIN DATOS",
                    Rut = datos.Rut + "-" + datos.Dv,
                    Dv = datos.Dv,
                    Nombre = datos.NombreCompleto(),
                    TipoDocumento = datos.TipoDocumento,
                    Numero = datos.Numero
                });
                error = true;
            }
            else
            {
                if (tribunal <= 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "TRIBUNAL NO ENCONTRADO",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                else
                {
                    datos.IdTribunal = tribunal ;
                }
            }
            //Valido tipo de causa
            if (string.IsNullOrEmpty(datos.TipoCausa))
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "TIPO CAUSA SIN DATOS",
                    Rut = datos.Rut + "-" + datos.Dv,
                    Dv = datos.Dv,
                    Nombre = datos.NombreCompleto(),
                    TipoDocumento = datos.TipoDocumento,
                    Numero = datos.Numero
                });
                error = true;
            }
            else
            {
                if (tipoCausa <= 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "TIPO CAUSA NO ENCONTRADA",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                else
                {
                    datos.IdTipoCausa = tipoCausa;
                }
            }
            //Valido el abogado
            if (string.IsNullOrEmpty(datos.RutAbogado))
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "ABOGADO SIN DATOS",
                    Rut = datos.Rut + "-" + datos.Dv,
                    Dv = datos.Dv,
                    Nombre = datos.NombreCompleto(),
                    TipoDocumento = datos.TipoDocumento,
                    Numero = datos.Numero
                });
                error = true;
            }
            else
            {
                if (IdAbogado <= 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ABOGADO NO ENCONTRADO",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                else
                {
                    datos.IdAbogado = IdAbogado;
                }
            }
            //Valido el procurador
            if (string.IsNullOrEmpty(datos.RutProcurador))
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "PROCURADOR SIN DATOS",
                    Rut = datos.Rut + "-" + datos.Dv,
                    Dv = datos.Dv,
                    Nombre = datos.NombreCompleto(),
                    TipoDocumento = datos.TipoDocumento,
                    Numero = datos.Numero
                });
                error = true;
            }
            else
            {
                if (idProcurador <= 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "PROCURADOR NO ENCONTRADO",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                else
                {
                    datos.IdProcurador = idProcurador;
                }
            }
            return error;
        }
        
        public static bool[] ValidarCargaAnterior(dto.DatosCarga datos, dto.CargaMasiva objCarga, UserSession objSession, int idTipoDocumento)
        {
            //Consulta de ultimo documento cargado
            int judicial = 0;
            bool error = false;
            bool repetido = false;

            if (objCarga.Pclid == 424)
            {
                //Reviso el estado
                judicial = dao.Deudor.DeudorEnJudicial(objSession.CodigoEmpresa, objCarga.Pclid, datos.ctcid);
                if(judicial < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL CONSULTAR ESTADO JUDICIAL",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                else if (judicial > 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "DEUDOR EN JUDICIAL",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                if (!error)
                {
                    //Reviso documentos previos del deudor
                    List<dto.DocumentoAnterior> lstDocumentosAnteriores = dao.Comprobante.ListarDocumentosAnteriores(objSession.CodigoEmpresa, objCarga.Pclid, datos.ctcid);
                    foreach (DocumentoAnterior dcto in lstDocumentosAnteriores)
                    {
                        if (dcto.Numero != "0")
                        {
                            switch (dcto.EstadoCpbt)
                            {
                                case "V":
                                    objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "CUOTA EXISTENTE, SE ACTUALIZA EL CODIGO DE CARGA",
                                    Rut = datos.Rut + "-" +datos.Dv,
                                    Dv = datos.Dv,
                                    Nombre = datos.NombreCompleto(),
                                    TipoDocumento = datos.TipoDocumento,
                                    Numero = datos.Numero
                                });
                                    break;
                                case "X":
                                    objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "CUOTA EXISTENTE ANULADA, SE REACTIVA LA CUOTA Y SE ACTUALIZA EL CODIGO DE CARGA",
                                    Rut = datos.Rut + "-" +datos.Dv,
                                    Dv = datos.Dv,
                                    Nombre = datos.NombreCompleto(),
                                    TipoDocumento = datos.TipoDocumento,
                                    Numero = datos.Numero
                                });
                                    dcto.EstadoId = 19;
                                    if (dao.Comprobante.ActualizarDocumento(dcto, objSession.CodigoEmpresa, objCarga.Pclid, datos.ctcid) < 0)
                                    {
                                        objCarga.ListaErrores.Add(new ErrorCarga
                                        {
                                            TipoError = "ERROR AL ACTUALIZAR CODIGO DE CARGA",
                                            Rut = datos.Rut + "-" +datos.Dv,
                                            Dv = datos.Dv,
                                            Nombre = datos.NombreCompleto(),
                                            TipoDocumento = datos.TipoDocumento,
                                            Numero = datos.Numero
                                        });
                                    }
                                    break;
                                case "F":
                                    objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "CUOTA EXISTENTE, SE ACTUALIZA EL CODIGO DE CARGA",
                                    Rut = datos.Rut + "-" +datos.Dv,
                                    Dv = datos.Dv,
                                    Nombre = datos.NombreCompleto(),
                                    TipoDocumento = datos.TipoDocumento,
                                    Numero = datos.Numero
                                });
                                    dcto.EstadoId = 19;
                                    if (dao.Comprobante.ActualizarDocumentoFinalizado(dcto, objSession.CodigoEmpresa, objCarga.Pclid, datos.ctcid) < 0)
                                    {
                                        objCarga.ListaErrores.Add(new ErrorCarga
                                        {
                                            TipoError = "ERROR AL ACTUALIZAR CODIGO DE CARGA",
                                            Rut = datos.Rut + "-" +datos.Dv,
                                            Dv = datos.Dv,
                                            Nombre = datos.NombreCompleto(),
                                            TipoDocumento = datos.TipoDocumento,
                                            Numero = datos.Numero
                                        });
                                    }
                                    break;

                            }

                            //Grabo el Historial
                            string comentarioHistorial = "Documento actualizado al código de carga: " + objCarga.CodigoCarga + ", y estado vigente";
                            if (dao.Comprobante.InsertarHistorial(objSession.CodigoEmpresa, objCarga.Pclid, datos.ctcid, dcto.Ccbid, idTipoDocumento, objSession.CodigoSucursal, 0, objSession.IpRed, objSession.IpPc, comentarioHistorial, datos.Capital, datos.Saldo, objSession.UserId, datos.FechaVencimiento) < 0)
                            {
                                objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "ERROR AL GRABAR HISTORIAL",
                                    Rut = datos.Rut + "-" +datos.Dv,
                                    Dv = datos.Dv,
                                    Nombre = datos.NombreCompleto(),
                                    TipoDocumento = datos.TipoDocumento,
                                    Numero = datos.Numero
                                });
                            }
                            repetido = true;
                        }


                    }
                }
            }
            bool[] salida = { error, repetido };
            return salida;
        }

        public static bool[] GrabarDocumentos(dto.DatosCarga datos, dto.CargaMasiva objCarga, UserSession objSession, int idTipoDocumento)
        {
            //Comienzo a Grabar los Documentos-------------------
            dto.Comprobante dcto = new dto.Comprobante();
            int existe = 0;
            int resultado = 0;
            bool[] error = {false, false};
            int estid = 0;
            Funciones objFunc = new Funciones();

            //Busco si esta asignado a la cartera
            existe = dao.Comprobante.BuscarCarteraCliente(objSession.CodigoEmpresa, objCarga.Pclid, datos.ctcid);
            if (existe == 0)
            {
                if (dao.Comprobante.InsertarCarteraCliente(objSession.CodigoEmpresa, objCarga.Pclid, datos.ctcid) < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL INSERTAR CARTERA CLIENTE",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error[0] = true;
                }
            }
            //Grabo el Documento
            if (!error[0])
            {
                dcto.Pclid = objCarga.Pclid;
                dcto.Ctcid = datos.ctcid;
                dcto.Ccbid = 0;
                dcto.TipoDocumento = idTipoDocumento.ToString();
                dcto.TipoCartera = Int32.Parse( objCarga.TipoCartera);
                dcto.NumeroCpbt = datos.Numero.ToLower();
                dcto.FechaVencimiento = datos.FechaVencimiento;
                dcto.FechaDocumento = datos.FechaDocumento;
                if (objCarga.TipoCartera == "1")
                {
                    if (datos.FechaVencimiento > DateTime.Parse( objFunc.FechaServer()))
                    {
                        estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 100);
                    }
                    else
                    {
                        estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 99);
                    }
                }
                else
                {
                    if (objCarga.Pclid == 424)
                    {
                        estid = 228;//APROBACION AREA COMERCIAL
                    }
                    else
                    {
                        estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 17);
                    }
                }
                dcto.EstadoCartera = estid.ToString();
                dcto.EstadoCpbt = "V";
                List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);

                dcto.CodigoMoneda = Int32.Parse( lstMoneda.Find(x => x.Text == datos.Moneda.ToUpper()).Value);
                dcto.TipoCambio = datos.TipoCambio;
                dcto.MontoAsignado = datos.MontoAsignado;
                dcto.Monto = datos.Capital;
                dcto.Saldo = datos.Saldo;
                dcto.GastoJudicial = datos.GastoJudicial;
                dcto.GastoOtros = datos.GastoPreJudicial;
                if (!string.IsNullOrEmpty(datos.Banco))
                {
                    int idBanco = dao.Comprobante.TraeBanco(objSession.CodigoEmpresa, datos.Banco);
                    if (idBanco != 0)
                    {
                        dcto.NombreBanco = idBanco.ToString();
                    }
                    else
                    {
                        dcto.NombreBanco = null;
                    }

                }
                else
                {
                    dcto.NombreBanco = null;
                }
                if (!string.IsNullOrEmpty(datos.RutGirador))
                {
                    if (datos.RutGirador.Length > 0 && Funciones.ValidaRut(datos.RutGirador))
                    {
                        dcto.RutGirador = datos.RutGirador;
                        dcto.NombreGirador = datos.NombreGirador;
                    }
                    else
                    {
                        if (objCarga.Pclid == 559)
                        {
                            dcto.RutGirador = datos.RutGirador;
                            dcto.NombreGirador = datos.NombreGirador;
                        }
                        else
                        {
                            dcto.RutGirador = null;
                            dcto.NombreGirador = null;
                        }
                    }
                }
                else
                {
                    dcto.RutGirador = null;
                    dcto.NombreGirador = null;
                }
                dcto.MotivoCobranza = dao.Comprobante.BuscarMotivoCobranza(objSession.CodigoEmpresa, objSession.Idioma, datos.MotivoCobranza).ToString();
                if (string.IsNullOrEmpty(datos.Comentario))
                {
                    dcto.Comentario = "";
                }
                else
                {
                    dcto.Comentario = datos.Comentario;
                }
                if (!string.IsNullOrEmpty(objCarga.CodigoCarga))
                {
                    dcto.CodigoCarga = objCarga.CodigoCarga;
                }
                else
                {
                    dcto.CodigoCarga = null;
                }
                //if (!string.IsNullOrEmpty(datos.CodigoCarga))
                //{
                //    int codigoCarga = dao.Comprobante.TraeCodigoCarga(objSession.CodigoEmpresa, objCarga.Pclid, datos.CodigoCarga);
                //    if (codigoCarga > 0)
                //    {
                //        dcto.CodigoCarga = codigoCarga.ToString();
                //    }
                //    else
                //    {
                //        if (objCarga.Pclid == 424)
                //        {
                //            dcto.CodigoCarga = objCarga.CodigoCarga;
                //        }
                //        else
                //        {
                //            dcto.CodigoCarga = null;
                //        }
                //    }
                //}
                //else
                //{
                //    dcto.CodigoCarga = null;
                //}
                if (!string.IsNullOrEmpty(datos.Negocio))
                {
                    dcto.NumeroEspecial = datos.Negocio.ToLower();
                }
                else
                {
                    dcto.NumeroEspecial = null;
                }
                if (!string.IsNullOrEmpty(datos.NumeroAgrupar))
                {
                    dcto.NumeroAgrupa = datos.NumeroAgrupar.ToLower();
                }
                else
                {
                    dcto.NumeroAgrupa = null;
                }
                dcto.Contrato = Int32.Parse( objCarga.Contrato);
                int idSubcartera = dao.Comprobante.BuscarSubcartera(objSession.CodigoEmpresa, datos.RutAsegurado) ;
                if (idSubcartera > 0)
                {
                    dcto.SubcarteraRut = idSubcartera.ToString();
                    dcto.Sbcid = idSubcartera;
                }
                else
                {
                    dcto.SubcarteraRut = null;
                    dcto.Sbcid = 0;
                }
                //Tercero
                int idTercero = dao.Deudor.BuscarTercero(objSession.CodigoEmpresa, datos.RutTercero);
                dcto.TerceroId = idTercero > 0 ? idTercero : 0;

                //Cuenta
                dcto.IdCuenta = datos.IdCuenta;
                dcto.DescripcionCuenta = datos.DescripcionCuenta;
                
                dcto.Antecedentes = datos.Antecedentes;
                dcto.Originales = datos.Originales;
                dcto.NumeroResolucion = datos.NumeroResolucion;
                dcto.FechaResolucion = datos.FechaResolucion;
                dcto.RutRepresentante1 = datos.RutRepresentante1;
                dcto.NombreRepresentante1 = datos.NombreRepresentante1;
                dcto.RutRepresentante2 = datos.RutRepresentante2;
                dcto.NombreRepresentante2 = datos.NombreRepresentante2;
                dcto.RutRepresentante3 = datos.RutRepresentante3;
                dcto.NombreRepresentante3 = datos.NombreRepresentante3;


                if (!error[0])
                {
                    resultado = dao.Comprobante.GrabarDocumento(dcto, objSession.CodigoEmpresa);
                }
                if (resultado <= 0)
                {
                    error[0] = true;
                    return error;
                }
                else
                {
                    resultado = dao.Comprobante.InsertarHistorialCarga(objSession.CodigoEmpresa, objCarga.Pclid, datos.ctcid, resultado, estid, objSession.CodigoSucursal, null, objSession.IpRed,objSession.IpPc , "", datos.MontoAsignado, datos.Saldo, objSession.UserId, datos.FechaVencimiento);
                    if (resultado < 0)
                    {
                        error[0] = true;
                    }
                    return error;
                }

            }

            if (resultado < 0)
            {
                error[0] = true;
                return error;
            }
            else
            {
                return error;
            }

        }

        public static bool[] GrabarDocumentos(dto.CargaJudicial datos, dto.CargaMasiva objCarga, int ctcid, UserSession objSession, int idTipoDocumento)
        {
            //Comienzo a Grabar los Documentos-------------------
            dto.Comprobante dcto = new dto.Comprobante();
            int existe = 0;
            int resultado = 0;
            bool[] error = { false, false };
            Funciones objFunc = new Funciones();
            //Busco si esta asignado a la cartera
            existe = dao.Comprobante.BuscarCarteraCliente(objSession.CodigoEmpresa, objCarga.Pclid, ctcid);
            int estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 127);
            if (existe == 0)
            {
                if (dao.Comprobante.InsertarCarteraCliente(objSession.CodigoEmpresa, objCarga.Pclid, ctcid) < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL INSERTAR CARTERA CLIENTE",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error[0] = true;
                }
            }
            //Grabo el Documento
            if (!error[0])
            {
                dcto.Pclid = objCarga.Pclid;
                dcto.Ctcid = ctcid;
                dcto.Ccbid = 0;
                dcto.TipoDocumento = idTipoDocumento.ToString();
                dcto.TipoCartera = Int32.Parse(objCarga.TipoCartera);
                dcto.NumeroCpbt = datos.Numero.ToLower();
                dcto.FechaVencimiento = datos.FechaVencimiento;
                dcto.FechaDocumento = datos.FechaDocumento;
                dcto.EstadoCartera = estid.ToString();
                dcto.EstadoCpbt = "J";
                List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);

                dcto.CodigoMoneda = Int32.Parse(lstMoneda.Find(x => x.Text == datos.Moneda.ToUpper()).Value);
                dcto.TipoCambio = datos.TipoCambio;
                dcto.MontoAsignado = datos.MontoAsignado;
                dcto.Monto = datos.Capital;
                dcto.Saldo = datos.Saldo;
                dcto.GastoJudicial = datos.GastoJudicial;
                dcto.GastoOtros = datos.GastoPreJudicial;
                if (!string.IsNullOrEmpty(datos.Banco))
                {
                    int idBanco = dao.Comprobante.TraeBanco(objSession.CodigoEmpresa, datos.Banco);
                    if (idBanco != 0)
                    {
                        dcto.NombreBanco = idBanco.ToString();
                    }
                    else
                    {
                        dcto.NombreBanco = null;
                    }

                }
                else
                {
                    dcto.NombreBanco = null;
                }
                if (!string.IsNullOrEmpty(datos.RutGirador))
                {
                    if (datos.RutGirador.Length > 0 && Funciones.ValidaRut(datos.RutGirador))
                    {
                        dcto.RutGirador = datos.RutGirador;
                        dcto.NombreGirador = datos.NombreGirador;
                    }
                    else
                    {
                        dcto.RutGirador = null;
                        dcto.NombreGirador = null;
                    }
                }
                else
                {
                    dcto.RutGirador = null;
                    dcto.NombreGirador = null;
                }
                dcto.MotivoCobranza = dao.Comprobante.BuscarMotivoCobranza(objSession.CodigoEmpresa, objSession.Idioma, datos.MotivoCobranza).ToString();
                if (string.IsNullOrEmpty(datos.Comentario))
                {
                    dcto.Comentario = "";
                }
                else
                {
                    dcto.Comentario = datos.Comentario;
                }
                if (!string.IsNullOrEmpty(datos.CodigoCarga))
                {
                    int codigoCarga = dao.Comprobante.TraeCodigoCarga(objSession.CodigoEmpresa, objCarga.Pclid, datos.CodigoCarga);
                    if (codigoCarga > 0)
                    {
                        dcto.CodigoCarga = codigoCarga.ToString();
                    }
                    else
                    {
                        if (objCarga.Pclid == 424)
                        {
                            dcto.CodigoCarga = objCarga.CodigoCarga;
                        }
                        else
                        {
                            dcto.CodigoCarga = null;
                        }
                    }
                }
                else
                {
                    dcto.CodigoCarga = null;
                }
                if (!string.IsNullOrEmpty(datos.Negocio))
                {
                    dcto.NumeroEspecial = datos.Negocio.ToLower();
                }
                else
                {
                    dcto.NumeroEspecial = null;
                }
                if (!string.IsNullOrEmpty(datos.NumeroAgrupar))
                {
                    dcto.NumeroAgrupa = datos.NumeroAgrupar.ToLower();
                }
                else
                {
                    dcto.NumeroAgrupa = null;
                }
                if (!string.IsNullOrEmpty(objCarga.Contrato))
                { 
                    dcto.Contrato = Int32.Parse(objCarga.Contrato);
                } else {
                    dcto.Contrato = 0;
                }
                int idSubcartera = dao.Comprobante.BuscarSubcartera(objSession.CodigoEmpresa, datos.RutAsegurado);
                if (idSubcartera > 0)
                {
                    dcto.SubcarteraRut = idSubcartera.ToString();
                }
                else
                {
                    dcto.SubcarteraRut = null;
                }
                //tercero
                int terceroId = dao.Deudor.BuscarTercero(objSession.CodigoEmpresa, datos.RutTercero);
                dcto.TerceroId = terceroId > 0 ? terceroId : 0;
                dcto.Antecedentes = datos.Antecedentes;
                dcto.Originales = datos.Originales;

                if (!error[0])
                {
                    resultado = dao.Comprobante.GrabarDocumento(dcto, objSession.CodigoEmpresa);
                }

                if (resultado <= 0)
                {
                    error[0] = true;
                    return error;
                } else {
                    var ccbid = resultado;
                    resultado = dao.Comprobante.InsertarHistorialCarga(objSession.CodigoEmpresa, objCarga.Pclid, ctcid, resultado, estid, objSession.CodigoSucursal, null, objSession.IpRed, objSession.IpPc, "DEUDOR INGRESA A JUDICIAL", datos.MontoAsignado, datos.Saldo, objSession.UserId, datos.FechaVencimiento); 

                    if (resultado < 0) {
                        error[0] = true;
                        return error;
                    } else {

                        switch (objCarga.Pclid)
                        {
                            case 279:  //Si es cliente COOPEUCH
                                resultado = dao.Comprobante.GrabarPanelDemandaMasiva(objSession.CodigoEmpresa, dcto.Pclid, dcto.Ctcid, objSession.UserId, ccbid, 0, 0, datos);
                                int panelId = resultado;

                                if (resultado <= 0)
                                {
                                    error[0] = true;
                                    return error;
                                }
                                else
                                {
                                    //Buscar encargado por comuna
                                    var EsRegionMetrop = dao.Direccion.VerificarComunaPerteneceRegionMetropolitana(datos.Comuna);

                                    string userEncargado = ConfigurationManager.AppSettings["Encargado_RegionMetropolitana"];
                                    if (!EsRegionMetrop)
                                    {
                                        userEncargado = ConfigurationManager.AppSettings["Encargado_RegionDefault"];
                                    }

                                    //Crea PANEL_DEMANDA_DETALLE por defecto
                                    resultado = dao.Comprobante.GrabarPanelDemandaMasivaDetalle(panelId, userEncargado, DateTime.Now, DateTime.Now, null, "", objSession.UserId);

                                    if (resultado <= 0)
                                    {
                                        error[0] = true;
                                        return error;
                                    }
                                    else
                                    {
                                        //Crea PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL
                                        resultado = dao.Comprobante.GrabarPanelDemandaMasivaCorreccionHistorial(panelId, objSession.UserId);

                                        if (resultado <= 0)
                                        {
                                            error[0] = true;
                                            return error;
                                        }
                                    }
                                }

                                break;
                        }
                        

                        return error;
                    }
                }
            }

            if (resultado < 0) {
                error[0] = true;
                return error;
            } else {
                return error;
            }
        }

        public static bool[] GrabarDocumentosPanelMasivo(dto.CargaJudicial datos, dto.CargaMasiva objCarga, int ctcid, UserSession objSession, int idTipoDocumento)
        {
            //Comienzo a Grabar los Documentos-------------------
            dto.Comprobante dcto = new dto.Comprobante();
            int existe = 0;
            int resultado = 0;
            bool[] error = { false, false };
            Funciones objFunc = new Funciones();
            //Busco si esta asignado a la cartera
            existe = dao.Comprobante.BuscarCarteraCliente(objSession.CodigoEmpresa, objCarga.Pclid, ctcid);
            int estid = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 127);
            if (existe == 0)
            {
                if (dao.Comprobante.InsertarCarteraCliente(objSession.CodigoEmpresa, objCarga.Pclid, ctcid) < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL INSERTAR CARTERA CLIENTE",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error[0] = true;
                }
            }
            //Grabo el Documento
            if (!error[0])
            {
                dcto.Pclid = objCarga.Pclid;
                dcto.Ctcid = ctcid;
                dcto.Ccbid = 0;
                dcto.TipoDocumento = idTipoDocumento.ToString();
                dcto.TipoCartera = Int32.Parse(objCarga.TipoCartera);
                dcto.NumeroCpbt = datos.Numero.ToLower();
                dcto.FechaVencimiento = datos.FechaVencimiento;
                dcto.FechaDocumento = datos.FechaDocumento;
                dcto.EstadoCartera = estid.ToString();
                dcto.EstadoCpbt = "J";
                List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);

                dcto.CodigoMoneda = Int32.Parse(lstMoneda.Find(x => x.Text == datos.Moneda.ToUpper()).Value);
                dcto.TipoCambio = datos.TipoCambio;
                dcto.MontoAsignado = datos.MontoAsignado;
                dcto.Monto = datos.Capital;
                dcto.Saldo = datos.Saldo;
                dcto.GastoJudicial = datos.GastoJudicial;
                dcto.GastoOtros = datos.GastoPreJudicial;
                if (!string.IsNullOrEmpty(datos.Banco))
                {
                    int idBanco = dao.Comprobante.TraeBanco(objSession.CodigoEmpresa, datos.Banco);
                    if (idBanco != 0)
                    {
                        dcto.NombreBanco = idBanco.ToString();
                    }
                    else
                    {
                        dcto.NombreBanco = null;
                    }

                }
                else
                {
                    dcto.NombreBanco = null;
                }
                if (!string.IsNullOrEmpty(datos.RutGirador))
                {
                    if (datos.RutGirador.Length > 0 && Funciones.ValidaRut(datos.RutGirador))
                    {
                        dcto.RutGirador = datos.RutGirador;
                        dcto.NombreGirador = datos.NombreGirador;
                    }
                    else
                    {
                        dcto.RutGirador = null;
                        dcto.NombreGirador = null;
                    }
                }
                else
                {
                    dcto.RutGirador = null;
                    dcto.NombreGirador = null;
                }
                dcto.MotivoCobranza = dao.Comprobante.BuscarMotivoCobranza(objSession.CodigoEmpresa, objSession.Idioma, datos.MotivoCobranza).ToString();
                if (string.IsNullOrEmpty(datos.Comentario))
                {
                    dcto.Comentario = "";
                }
                else
                {
                    dcto.Comentario = datos.Comentario;
                }
                if (!string.IsNullOrEmpty(datos.CodigoCarga))
                {
                    int codigoCarga = dao.Comprobante.TraeCodigoCarga(objSession.CodigoEmpresa, objCarga.Pclid, datos.CodigoCarga);
                    if (codigoCarga > 0)
                    {
                        dcto.CodigoCarga = codigoCarga.ToString();
                    }
                    else
                    {
                        if (objCarga.Pclid == 424)
                        {
                            dcto.CodigoCarga = objCarga.CodigoCarga;
                        }
                        else
                        {
                            dcto.CodigoCarga = null;
                        }
                    }
                }
                else
                {
                    dcto.CodigoCarga = null;
                }
                if (!string.IsNullOrEmpty(datos.Negocio))
                {
                    dcto.NumeroEspecial = datos.Negocio.ToLower();
                }
                else
                {
                    dcto.NumeroEspecial = null;
                }
                if (!string.IsNullOrEmpty(datos.NumeroAgrupar))
                {
                    dcto.NumeroAgrupa = datos.NumeroAgrupar.ToLower();
                }
                else
                {
                    dcto.NumeroAgrupa = null;
                }

                if (!string.IsNullOrEmpty(objCarga.Contrato))
                { 
                    dcto.Contrato = Int32.Parse(objCarga.Contrato);
                }
                else
                {
                    dcto.Contrato = 0;
                }

                int idSubcartera = dao.Comprobante.BuscarSubcartera(objSession.CodigoEmpresa, datos.RutAsegurado);
                if (idSubcartera > 0)
                {
                    dcto.SubcarteraRut = idSubcartera.ToString();
                }
                else
                {
                    dcto.SubcarteraRut = null;
                }
                //tercero
                int terceroId = dao.Deudor.BuscarTercero(objSession.CodigoEmpresa, datos.RutTercero);
                dcto.TerceroId = terceroId > 0 ? terceroId : 0;
                dcto.Antecedentes = datos.Antecedentes;
                dcto.Originales = datos.Originales;

                if (!error[0])
                {
                    resultado = dao.Comprobante.GrabarDocumento(dcto, objSession.CodigoEmpresa);
                }

                if (resultado <= 0)
                {
                    error[0] = true;
                    return error;
                }
                else
                {
                    resultado = dao.Comprobante.InsertarHistorialCarga(objSession.CodigoEmpresa, objCarga.Pclid, ctcid, resultado, estid, objSession.CodigoSucursal, null, objSession.IpRed, objSession.IpPc, "", datos.MontoAsignado, datos.Saldo, objSession.UserId, datos.FechaVencimiento);
                    if (resultado < 0)
                    {
                        error[0] = true;
                    }
                    return error;
                }
            }

            if (resultado < 0)
            {
                error[0] = true;
                return error;
            }
            else
            {
                return error;
            }
        }

        public static bool[] GrabarRol(dto.CargaJudicial datos, dto.CargaMasiva objCarga, int ctcid, UserSession objSession)
        {
            int resultado = 0;
            bool[] error = { false, false };
            Funciones objFunc = new Funciones();
            int rolid = 0;
            int estado = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 25);
            int materiaJudicial = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 24);
            DateTime fechaServer = DateTime.Parse( objFunc.FechaServer());
            //Busco el Ultimo Rol
            int ultimoRol = dao.Comprobante.TraeUltimoRol(objSession.CodigoEmpresa);
            

            //Comienzo a Crear el ROL
            rolid = dao.Comprobante.TraeRolClienteDeudor(objSession.CodigoEmpresa, objCarga.Pclid, ctcid);
            if (rolid <= 0)
            {
                rolid = ultimoRol;
            }
            
            //Inserto Rol
            resultado = dao.Comprobante.InsertarRol(objSession.CodigoEmpresa, rolid, objCarga.Pclid, ctcid, datos.IdTribunal, datos.IdTipoCausa, estado, new DateTime(), new DateTime(), "", materiaJudicial, fechaServer);
            if (resultado < 0)
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "ERROR AL INSERTAR ROL",
                    Rut = datos.Rut + "-" + datos.Dv,
                    Dv = datos.Dv,
                    Nombre = datos.NombreCompleto(),
                    TipoDocumento = datos.TipoDocumento,
                    Numero = datos.Numero
                });
                error[0] = true;
            }
            //Inserto rol estado
            if (!error[0])
            {
                resultado = dao.Comprobante.InsertarRolEstados(objSession.CodigoEmpresa, rolid,estado, materiaJudicial,objSession.UserId,objSession.IpRed,objSession.IpPc,  "", fechaServer);
                if (resultado < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL INSERTAR ROL ESTADOS",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error[0] = true;
                }
            }
            //UltRol = UltRol + 1

            //Inserto los Documentos al ROL
            if (!error[0])
            {
                resultado = dao.Comprobante.InsertarDocumentosRol(objSession.CodigoEmpresa, rolid, objCarga.Pclid, ctcid, datos.ccbid, datos.MontoAsignado,datos.Saldo);
                if (resultado < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL INSERTAR DOCUMENTOS AL ROL",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error[0] = true;
                }
            }
            //Inserto el ente
            if (!error[0])
            {
                resultado = dao.Comprobante.ExisteEnteRol(objSession.CodigoEmpresa, rolid,datos.IdAbogado);
                if (resultado <= 0)
                {
                    resultado = dao.Comprobante.InsertarEnteRol(objSession.CodigoEmpresa, rolid, datos.IdAbogado);
                    if (resultado <= 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL GRABAR ENTE ABOGADO AL ROL",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error[0] = true;
                    }
                }
            }

            if (!error[0])
            {
                resultado = dao.Comprobante.ExisteEnteRol(objSession.CodigoEmpresa, rolid, datos.IdProcurador);
                if (resultado <= 0)
                {
                    resultado = dao.Comprobante.InsertarEnteRol(objSession.CodigoEmpresa, rolid, datos.IdProcurador);
                    if (resultado <= 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL GRABAR ENTE PROCURADOR AL ROL",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error[0] = true;
                    }
                }
            }
            //Agregar Avenimiento Demanda
            if ((datos.FechaDemanda != new DateTime() || datos.FechaAvenimiento != new DateTime()) && !error[0])
            {
                if (dao.Comprobante.ExisteDemandaAvenimientoRol(objSession.CodigoEmpresa, rolid) <= 0)
                {
                    DemandaAvenimientoRol obj = new DemandaAvenimientoRol();
                    obj.Codemp = objSession.CodigoEmpresa;
                    obj.Rolid = rolid;
                    obj.FechaDemanda = datos.FechaDemanda;
                    obj.CuotasDemanda = datos.NumeroCuotasDemanda;
                    obj.MontoDemanda = datos.MontoDemanda;
                    obj.MontoPrimeraCuotaDemanda = datos.MontoCuotasDemanda;
                    obj.MontoUltimaCuotaDemanda = datos.MontoUltimaCuotaDemanda;
                    obj.FechaPrimeraCuotaDemanda = datos.FechaPrimeraCuotaDemanda;
                    obj.FechaUltimaCuotaDemanda = datos.FechaUltimaCuotaDemanda;
                    obj.InteresDemanda = datos.InteresDemanda;

                    obj.FechaAvenimiento = datos.FechaAvenimiento;
                    obj.CuotasAvenimiento = datos.NumeroCuotasAvenimiento;
                    obj.MontoAvenimiento = datos.MontoAvenimiento;
                    obj.MontoPrimeraCuotaAvenimiento = datos.MontoCuotasAvenimiento;
                    obj.MontoUltimaCuotaAvenimiento = datos.MontoCuotasAvenimiento;
                    obj.FechaPrimeraCuotaAvenimiento = datos.FechaAvenimiento;
                    obj.FechaUltimaCuotaAvenimiento = datos.FechaPrimeraCuotaAvenimiento;
                    obj.InteresAvenimiento = datos.InteresDemanda;
                    resultado = dao.Comprobante.InsertarDemandaAvenimientoRol(obj);
                    if (resultado <= 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL GRABAR AVENIMIENTOS",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                    } 
                }
            }

            return error;
        }

        public static bool[] GrabarRolTemporal(dto.CargaJudicial datos, dto.CargaMasiva objCarga, int ctcid, UserSession objSession)
        {
            int resultado = 0;
            bool[] error = { false, false };
            Funciones objFunc = new Funciones();
            int rolid = 0;
            int estado = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 25);
            int materiaJudicial = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 24);
            DateTime fechaServer = DateTime.Parse(objFunc.FechaServer());
            //Busco el Ultimo Rol
            int ultimoRol = dao.Comprobante.TraeUltimoRol(objSession.CodigoEmpresa);


            //Comienzo a Crear el ROL
            rolid = dao.Comprobante.TraeRolClienteDeudor(objSession.CodigoEmpresa, objCarga.Pclid, ctcid);
            if (rolid <= 0)
            {
                rolid = ultimoRol;
            }

            //Inserto Rol
            resultado = dao.Comprobante.InsertarRol(objSession.CodigoEmpresa, rolid, objCarga.Pclid, ctcid, datos.IdTribunal, datos.IdTipoCausa, estado, new DateTime(), new DateTime(), "", materiaJudicial, fechaServer);
            if (resultado < 0)
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "ERROR AL INSERTAR ROL",
                    Rut = datos.Rut + "-" + datos.Dv,
                    Dv = datos.Dv,
                    Nombre = datos.NombreCompleto(),
                    TipoDocumento = datos.TipoDocumento,
                    Numero = datos.Numero
                });
                error[0] = true;
            }
            //Inserto rol estado
            if (!error[0])
            {
                resultado = dao.Comprobante.InsertarRolEstados(objSession.CodigoEmpresa, rolid, estado, materiaJudicial, objSession.UserId, objSession.IpRed, objSession.IpPc, "", fechaServer);
                if (resultado < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL INSERTAR ROL ESTADOS",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error[0] = true;
                }
            }
            //UltRol = UltRol + 1

            //Inserto los Documentos al ROL
            if (!error[0])
            {
                resultado = dao.Comprobante.InsertarDocumentosRol(objSession.CodigoEmpresa, rolid, objCarga.Pclid, ctcid, datos.ccbid, datos.MontoAsignado, datos.Saldo);
                if (resultado < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "ERROR AL INSERTAR DOCUMENTOS AL ROL",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error[0] = true;
                }
            }
            //Inserto el ente
            if (!error[0])
            {
                resultado = dao.Comprobante.ExisteEnteRol(objSession.CodigoEmpresa, rolid, datos.IdAbogado);
                if (resultado <= 0)
                {
                    resultado = dao.Comprobante.InsertarEnteRol(objSession.CodigoEmpresa, rolid, datos.IdAbogado);
                    if (resultado <= 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL GRABAR ENTE ABOGADO AL ROL",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error[0] = true;
                    }
                }
            }

            if (!error[0])
            {
                resultado = dao.Comprobante.ExisteEnteRol(objSession.CodigoEmpresa, rolid, datos.IdProcurador);
                if (resultado <= 0)
                {
                    resultado = dao.Comprobante.InsertarEnteRol(objSession.CodigoEmpresa, rolid, datos.IdProcurador);
                    if (resultado <= 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL GRABAR ENTE PROCURADOR AL ROL",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error[0] = true;
                    }
                }
            }
            //Agregar Avenimiento Demanda
            if ((datos.FechaDemanda != new DateTime() || datos.FechaAvenimiento != new DateTime()) && !error[0])
            {
                if (dao.Comprobante.ExisteDemandaAvenimientoRol(objSession.CodigoEmpresa, rolid) <= 0)
                {
                    DemandaAvenimientoRol obj = new DemandaAvenimientoRol();
                    obj.Codemp = objSession.CodigoEmpresa;
                    obj.Rolid = rolid;
                    obj.FechaDemanda = datos.FechaDemanda;
                    obj.CuotasDemanda = datos.NumeroCuotasDemanda;
                    obj.MontoDemanda = datos.MontoDemanda;
                    obj.MontoPrimeraCuotaDemanda = datos.MontoCuotasDemanda;
                    obj.MontoUltimaCuotaDemanda = datos.MontoUltimaCuotaDemanda;
                    obj.FechaPrimeraCuotaDemanda = datos.FechaPrimeraCuotaDemanda;
                    obj.FechaUltimaCuotaDemanda = datos.FechaUltimaCuotaDemanda;
                    obj.InteresDemanda = datos.InteresDemanda;

                    obj.FechaAvenimiento = datos.FechaAvenimiento;
                    obj.CuotasAvenimiento = datos.NumeroCuotasAvenimiento;
                    obj.MontoAvenimiento = datos.MontoAvenimiento;
                    obj.MontoPrimeraCuotaAvenimiento = datos.MontoCuotasAvenimiento;
                    obj.MontoUltimaCuotaAvenimiento = datos.MontoCuotasAvenimiento;
                    obj.FechaPrimeraCuotaAvenimiento = datos.FechaAvenimiento;
                    obj.FechaUltimaCuotaAvenimiento = datos.FechaPrimeraCuotaAvenimiento;
                    obj.InteresAvenimiento = datos.InteresDemanda;
                    resultado = dao.Comprobante.InsertarDemandaAvenimientoRol(obj);
                    if (resultado <= 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "ERROR AL GRABAR AVENIMIENTOS",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                    }
                }
            }

            return error;
        }

        public static bool[] ValidarCargaNuevaJudicial(dto.DatosCarga datos, dto.CargaMasiva objCarga, int ctcid, UserSession objSession, int idTipoDocumento)
        {
            bool error = false;
            bool repetido = false;
            Funciones objFunc = new Funciones();

            int idComuna = Direccion.BuscarComuna(datos.Comuna);
            //x = x + 1
            //                bad = True
            //                cont = cont + 1

            //Valido que el cliente del archivo sea igual al cliente ingresado
            if (objCarga.RutCliente != datos.RutCliente)
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "CLIENTE NO CONCUERDA",
                    Rut = datos.Rut + "-" +datos.Dv,
                    Dv = datos.Dv,
                    Nombre = datos.NombreCompleto(),
                    TipoDocumento = datos.TipoDocumento,
                    Numero = datos.Numero
                });
                error = true;
            }
            if (!error)
            {
                //Valido el rut del deudor
                if (!Funciones.ValidaRut(datos.Rut + datos.Dv))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("RutBad", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido las fechas
                if (datos.FechaDocumento > datos.FechaVencimiento)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "FECHA VENCIMIENTO INVALIDA",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido el nombre
                if (string.IsNullOrEmpty(datos.Nombre))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Nombre", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido la dirección
                if (string.IsNullOrEmpty(datos.Direccion1))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("DirNul", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido el tipo de documento
                if (string.IsNullOrEmpty(datos.TipoDocumento))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Tipo", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                if (idTipoDocumento < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "TIPO DOCUMENTO NO EXISTE",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido el numero
                if (string.IsNullOrEmpty(datos.Numero))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Numero", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido motivo cobranza
                if (string.IsNullOrEmpty(datos.MotivoCobranza))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Motivo", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                if (dao.Comprobante.BuscarMotivoCobranza(objSession.CodigoEmpresa, objSession.Idioma, datos.MotivoCobranza) < 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MOTIVO COBRANZA NO EXISTE",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido moneda
                if (string.IsNullOrEmpty(datos.Moneda))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MONEDA NO PUEDE SER NULO",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);
                if (lstMoneda.Find(x => x.Text == datos.Moneda.ToUpper()) == null)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MONEDA NO EXISTE",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido montos
                if (datos.MontoAsignado == 0 || datos.Saldo == 0 || datos.Capital == 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Saldo", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
                if (datos.Saldo > datos.Capital || datos.Saldo > datos.MontoAsignado)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("ErrSaldo", objSession.Idioma),
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido banco
                if (!string.IsNullOrEmpty(datos.Banco))
                {
                    if (dao.Comprobante.TraeBanco(objSession.CodigoEmpresa, datos.Banco) == 0)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "BANCO NO EXISTE",
                            Rut = datos.Rut + "-" +datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }

            }
            if (!error)
            {
                //Valido rut girador
                if (!string.IsNullOrEmpty(datos.RutGirador))
                {
                    if (datos.RutGirador == "0" || !Funciones.ValidaRut(datos.RutGirador))
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "RUT GIRADOR INVALIDO",
                            Rut = datos.Rut + "-" +datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }
            }
            if (!error)
            {
                //Valido comuna
                if (string.IsNullOrEmpty(datos.Comuna) || idComuna == 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "COMUNA NO EXISTE",
                        Rut = datos.Rut + "-" +datos.Dv,
                        Dv = datos.Dv,
                        Nombre = datos.NombreCompleto(),
                        TipoDocumento = datos.TipoDocumento,
                        Numero = datos.Numero
                    });
                    error = true;
                }

            }
            if (!error)
            {
                //Valido subcartera
                if (!string.IsNullOrEmpty(datos.RutAsegurado)) 
                {
                    if (!Funciones.ValidaRut(datos.RutAsegurado))
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "RUT ASEGURADO INVALIDO",
                            Rut = datos.Rut + "-" +datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                }
            }

            bool[] salida = { error, repetido };
            return salida;
        }

        public static bool ProcesoCargaMutualLey(List<dto.DatosCarga> lst, dto.CargaMasiva objCarga, UserSession objSession)
        {
            bool error = false;
            int ctcid = 0;
            int idTipoDocumento = 0;
            ErrorCarga objError = new ErrorCarga();
            Funciones objFunc = new Funciones();
            bool[] salida = { false, false };
            List<Combobox> lstTipoDocumento = dao.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma);
            Combobox tipoDocumento = new Combobox();
            //REcorremos los registros del archivo
            if (lst.Count > 0)
            {
                foreach (dto.DatosCarga datos in lst)
                {
                    //Valido que el cliente del archivo sea igual al cliente ingresado
                    if (objCarga.RutCliente != datos.RutCliente)
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "CLIENTE NO CONCUERDA",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = datos.NombreCompleto(),
                            TipoDocumento = datos.TipoDocumento,
                            Numero = datos.Numero
                        });
                        error = true;
                    }
                    if (!error)
                    {
                        ctcid = dao.Deudor.BuscarIdDeudor(datos.Rut + datos.Dv, objSession.CodigoEmpresa);
                        salida = CrearDeudoresCarga(datos, objCarga, ctcid, objSession);
                    }
                }
                if (!salida[0] && !error)
                {
                    foreach (dto.DatosCarga datos in lst)
                    {
                        tipoDocumento = lstTipoDocumento.Find(x => x.Text == datos.TipoDocumento.Trim());
                        if (tipoDocumento == null)
                        {
                            tipoDocumento = lstTipoDocumento.Find(x => x.Text == datos.TipoDocumento.Replace("FACTURA", "FACT."));
                        }
                        if (tipoDocumento == null)
                        {
                            idTipoDocumento = -1;
                        }
                        else
                        {
                            idTipoDocumento = Int32.Parse(tipoDocumento.Value);
                        }
                        salida = ValidarCargaNueva(datos, objCarga, objSession, idTipoDocumento);
                        if (salida[0])
                        {
                            objCarga.ListaErrores.Add(new ErrorCarga
                            {
                                TipoError = "ERROR AL VALIDAR DOCUMENTO",
                                Rut = datos.Rut + "-" + datos.Dv,
                                Dv = datos.Dv,
                                Nombre = datos.NombreCompleto(),
                                TipoDocumento = datos.TipoDocumento,
                                Numero = datos.Numero
                            });
                            error = true;
                        }
                        if (!salida[0] && !salida[1] && objCarga.Pclid == 424)
                        {
                            salida = ValidarCargaAnterior(datos, objCarga, new UserSession(), idTipoDocumento);

                        }
                        //Comienzo a Grabar los Documentos
                        if (!salida[0] && !salida[1])
                        {
                            salida = GrabarDocumentos(datos, objCarga, objSession, idTipoDocumento);
                        }
                        if (salida[0])
                        {
                            objCarga.ListaErrores.Add(new ErrorCarga
                            {
                                TipoError = "ERROR AL GRABAR DOCUMENTO",
                                Rut = datos.Rut + "-" + datos.Dv,
                                Dv = datos.Dv,
                                Nombre = datos.NombreCompleto(),
                                TipoDocumento = datos.TipoDocumento,
                                Numero = datos.Numero
                            });
                            error = true;
                        }
                    }
                }
                if (!salida[0] && !error)
                {
                    error = false;
                }
                else
                {
                    error = true;
                }

            }
            return error;
        }

        #endregion

        #region "Anular Carga Masiva"

        public static List<AnularCargaMasiva> ListarCargasAnular(int codemp, int estid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.CargaMasiva.ListarCargasAnular(codemp, estid, where, sidx, sord, inicio, limite);
        }

        public static int ListarCargasAnularCount(int codemp, int estid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.CargaMasiva.ListarCargasAnularCount(codemp, estid, where, sidx, sord, inicio, limite);
        }

        public static string OperCargasAnular(string oper, string id, UserSession objSession, AnularCargaMasiva obj)
        {
            string[] ids = id.Split('|');
            Funciones objFunc = new Funciones();
            string salida="";
            objFunc.TraeError("DelCarga", objSession.Idioma);
            switch (oper)
            {
                case "add":
                    //dao.Deudor.InsertarTelefono(obj, objsession.CodigoEmpresa);
                    return "";
             
                case "edit":
                    //dao.Deudor.EditarTelefono(obj, objsession.CodigoEmpresa, ids[0]);
                    return "";
   
                case "del":
                    if (dao.CargaMasiva.BorrarCarga(ids[0], ids[1], ids[2]) <= 0)
                    {
                        salida = objFunc.TraeError("DelCarga", objSession.Idioma);
                    }
                    break;
            }
            return salida;
        }

        #endregion

        #region "Aprobar Carga Masiva"

        public static List<AprobarCargaMasiva> ListarCargasAprobar(int codemp,  string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.CargaMasiva.ListarCargasAprobar(codemp, where, sidx, sord, inicio, limite);
        }

        public static int ListarCargasAprobarCount(int codemp,  string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.CargaMasiva.ListarCargasAprobarCount(codemp, where, sidx, sord, inicio, limite);
        }

        public static string AprobarCarga(List<String> lst, UserSession objSession)
        {
            dto.Comprobante datos;
            int error = 0;
            Funciones objFunc = new Funciones();
            int estado = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 94);
            string salida = "";
            foreach(string ids in lst){
                string[] id = ids.Split('|');
                datos = dao.Comprobante.TraeCpbt(Int32.Parse(id[0]), Int32.Parse(id[1]), Int32.Parse(id[2]), Int32.Parse(id[3]));
                if (datos != null)
                {
                    error = dao.CargaMasiva.GrabarCarteraClientesEstadosHistorialEspecial(datos, objSession, estado);

                    if (error > 0)
                    {
                        dao.CargaMasiva.ActualizaCarteraEstados(datos.Pclid, datos.Ctcid, datos.Ccbid, estado, "V", objSession);
                        salida ="Documento(s) aprobado(s).";
                    }
                    else
                    {
                        salida =  objFunc.TraeError("ErrAcc", objSession.Idioma);
                
                    }

                }
                else
                {
                    salida =  objFunc.TraeError("ErrAcc", objSession.Idioma);
                }

            }
            return salida;
        }

        #endregion

        #region "Proceso Carga Pagos"

        public static bool ProcesoCargaPagos(List<dto.DatosCargaPago> lst, dto.CargaMasiva objCarga, UserSession objSession)
        {
            bool error = false;
            int err = 0;
            //ErrorCarga objError = new ErrorCarga();
            Funciones objFunc = new Funciones();
            dto.Comprobante cpbt = new dto.Comprobante();
            Dimol.dao.Utilidades util = dao.CargaMasiva.InstanciaUtilidades(objSession);

            //bool[] salida = { false, false };
            //List<Combobox> lstTipoDocumento = dao.Comprobante.ListarTipoDocumento(objSession.CodigoEmpresa, objSession.Idioma);
            //Combobox tipoDocumento = new Combobox();
            string[] cuotaSistema = { "", "", "", "" };
            //int ctcid = 0;

            decimal saldo = 0;
            decimal intereses = 0;
            decimal honorario = 0;
            decimal total = 0;
            int nuevoEstado = 0;
            string nuevoEstadoDocumento = "";
            string nuevoEstadoDescripcion = "";
            DateTime fechaServer = DateTime.Parse(objFunc.FechaServer());

            if (objCarga.Pclid == 424)
            {
                foreach (dto.DatosCargaPago datos in lst)
                {
                    cuotaSistema = dao.CargaMasiva.TraeCuotaPago(datos.RutCliente, datos.Rut + datos.Dv, datos.NumeroCpbt);
                    if (!string.IsNullOrEmpty(cuotaSistema[0]) && !string.IsNullOrEmpty(cuotaSistema[1]) && !string.IsNullOrEmpty(cuotaSistema[2]) && !string.IsNullOrEmpty(cuotaSistema[3]))
                    {
                        datos.NumeroCpbt = cuotaSistema[0];
                        datos.Monto = decimal.Parse(cuotaSistema[1]);
                        datos.Moneda = cuotaSistema[2];
                        datos.TipoCambio = decimal.Parse(cuotaSistema[3]);
                    }
                    else
                    {
                        objCarga.ListaErrores.Add(new ErrorCarga
                        {
                            TipoError = "EL ARCHIVO CARGADO TIENE PAGO DE CUOTAS QUE NO EXISTEN  O QUE NO SE ENCUENTRAN VIGENTES.",
                            Rut = datos.Rut + "-" + datos.Dv,
                            Dv = datos.Dv,
                            Nombre = "",
                            TipoDocumento = "",
                            Numero = datos.NumeroCpbt
                        });
                        error = true;
                    }
                }
            }
            // Recorremos los registros del archivo
            if (lst.Count > 0)
            {
                foreach (dto.DatosCargaPago datos in lst)
                {
                    // busco al deudor y valido los registros
                    if (!error)
                    {
                        datos.Ctcid = dao.Deudor.BuscarIdDeudor(datos.Rut + datos.Dv, objSession.CodigoEmpresa);
                        error = ValidarCargaPagos(datos, objCarga, objSession);
                        total += datos.Monto + datos.Interes + datos.Honorario;
                    }
                }
                if (!error)
                {
                    foreach (dto.DatosCargaPago datos in lst)
                    {
                        // busco la cuota
                        cpbt = dao.Comprobante.TraeCpbtNumero(objSession.CodigoEmpresa, objCarga.Pclid, datos.Ctcid, datos.NumeroCpbt);
                        if (cpbt != null && cpbt.EstadoCpbt == "V")
                        {
                            //Reviso el cambio de estado
                            saldo = cpbt.Saldo - datos.Monto;
                            intereses = cpbt.Intereses - datos.Interes;
                            honorario = cpbt.Honorarios - datos.Honorario;
                            datos.Ccbid = cpbt.Ccbid;
                            if (saldo <= 0)
                            {
                                nuevoEstado = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 30);
                                nuevoEstadoDocumento = "F";
                                nuevoEstadoDescripcion = "CANCELA DOCUMENTO";
                                saldo = 0;
                            }
                            else
                            {
                                nuevoEstado = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 29);
                                nuevoEstadoDocumento = cpbt.EstadoCpbt;
                                nuevoEstadoDescripcion = "ABONA DOCUMENTO";
                            }
                            if (cpbt.FechaCalculoInteres <= fechaServer)
                            {
                                cpbt.FechaCalculoInteres = fechaServer.AddDays(1);
                            }

                            cpbt.Saldo = saldo;
                            cpbt.Intereses = intereses;
                            cpbt.Honorarios = honorario;
                            cpbt.EstadoCartera = nuevoEstado.ToString();
                            cpbt.EstadoCpbt = nuevoEstadoDocumento;

                            //Actualizo el comprobante
                            err = dao.Comprobante.ActualizarCpbtValoresEspecial(cpbt,objSession.CodigoEmpresa);
                            if (err <= 0)
                            {
                                objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "NO SE PUDO ACTUALIZAR LA CARTERA.",
                                    Rut = datos.Rut + "-" + datos.Dv,
                                    Dv = datos.Dv,
                                    Nombre = "",
                                    TipoDocumento = "",
                                    Numero = datos.NumeroCpbt
                                });
                                error = true;
                            }
                            if (!error)
                            {
                                nuevoEstadoDescripcion += ": " + datos.NumeroCpbt + ",   MONTO CANCELADO:" + datos.Monto;
                                err = util.InsertarHistorialCartera(objSession.CodigoEmpresa,objSession.CodigoSucursal,objCarga.Pclid,datos.Ctcid,cpbt.Ccbid,Int32.Parse( cpbt.EstadoCartera), cpbt.Monto.ToString(),cpbt.Saldo.ToString(),objSession.UserId,nuevoEstadoDescripcion);
                            }
                            if (err <= 0)
                            {
                                objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "NO SE PUDO INSERTAR EL HISTORIAL DE CARTERA.",
                                    Rut = datos.Rut + "-" + datos.Dv,
                                    Dv = datos.Dv,
                                    Nombre = "",
                                    TipoDocumento = "",
                                    Numero = datos.NumeroCpbt
                                });
                                error = true;

                            }



                        }
                        else
                        {
                            objCarga.ListaErrores.Add(new ErrorCarga
                            {
                                TipoError = "EL DOCUMENTO NO EXISTE O NO SE ENCUENTRA VIGENTE.",
                                Rut = datos.Rut + "-" + datos.Dv,
                                Dv = datos.Dv,
                                Nombre = "",
                                TipoDocumento = "",
                                Numero = datos.NumeroCpbt
                            });
                            error = true;
                        }
                    }
                    if (!error)
                    {
                        int tipoDocumento = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 70);
                        int estadoDocumento = objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 33);
                        List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);
                        dto.DocumentoDiario dctoDiario = new DocumentoDiario();
                        dctoDiario.CodigoEmpresa = objSession.CodigoEmpresa;
                        dctoDiario.CodigoSucursal = objSession.CodigoSucursal;
                        dctoDiario.Anio = fechaServer.Year;
                        dctoDiario.NumeroDocumento = 0;
                        dctoDiario.TipoDocumento = tipoDocumento;
                        dctoDiario.TipoMovimiento = "I";
                        dctoDiario.IdCliente = objCarga.Pclid;
                        dctoDiario.Propio = "N";
                        dctoDiario.IdBanco = null;
                        dctoDiario.CuentaCorriente = "";
                        dctoDiario.FechaIngreso = fechaServer;
                        dctoDiario.FechaDocumento = fechaServer;
                        dctoDiario.FechaVencimiento = fechaServer;
                        dctoDiario.EstadoDocumento = estadoDocumento;
                        dctoDiario.NumeroCuenta = "";
                        dctoDiario.Monto = total;
                        dctoDiario.Saldo = 0;
                        dctoDiario.CodigoMoneda =  Int32.Parse( lstMoneda.Find(x=> x.Text == lst[0].Moneda).Value); // Revisar y corregir, aqui es donde se fija la moneda a pesos ERROR
                        dctoDiario.TipoCambio = lst[0].TipoCambio; // Revisar y corregir, aqui es donde se fija la moneda a pesos ERROR
                        dctoDiario.Titular="N";
                        dctoDiario.RutPagador = "";
                        dctoDiario.NombrePagador = "";
                        dctoDiario.IdCliente = null;
                        dctoDiario.Empleado = "N";
                        dctoDiario.IdEmpleado = null;
                        dctoDiario.Custodia = "N";
                        dctoDiario.DocumentoEmpresa = "N";
                        dctoDiario.PagDir = "N";
                        dctoDiario.VecesDepositado = 0;
                        dctoDiario.FechaDeposito = null;
                        dctoDiario.Depositar = false;
                        dctoDiario.RutDeposito = null;
                        dctoDiario.NombreDeposito = null;
                        dctoDiario.NumeroDeposito = null;
                        dctoDiario.FecDep = null;
                        dctoDiario.Pendiente = false;
                        dctoDiario.AnioNegociacion = null;
                        dctoDiario.IdNegociacion = null;
                        dctoDiario.Comentario = "";
                        int numeroDocumento = dao.DocumentoDiario.InsertarDocumentoDiario(dctoDiario);
                        dctoDiario.NumeroDocumento = numeroDocumento;
                        if (numeroDocumento > 0)
                        {
                            err = dao.DocumentoDiario.InsertarDocumentoDiarioEstados(dctoDiario, objSession.UserId);
                            if (err <= 0)
                            {
                                objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "NO SE PUDO INSERTAR EL HISTORIAL DE DOCUMENTO DIARIO.",
                                    Rut = "",
                                    Dv = "",
                                    Nombre = "",
                                    TipoDocumento = "",
                                    Numero = ""
                                });
                                error = true;
                            }
                        }
                        else
                        {
                            objCarga.ListaErrores.Add(new ErrorCarga
                            {
                                TipoError = "NO SE PUDO INSERTAR EL DOCUMENTO DIARIO.",
                                Rut = "",
                                Dv = "",
                                Nombre = "",
                                TipoDocumento = "",
                                Numero = ""
                            });
                            error = true;
                        }
                        if (!error)
                        {
                            Aplicaciones objApl = new Aplicaciones();
                            AplicaionesItems objAplItems; 

                            objApl.CodigoEmpresa = objSession.CodigoEmpresa;
                            objApl.CodigoSucursal = objSession.CodigoSucursal;
                            objApl.Anio = dctoDiario.Anio;
                            objApl.Mes = fechaServer.Month;
                            objApl.NumeroAplicacion = 0;
                            objApl.Tipo = 4;
                            objApl.Accion = -1;
                            objApl.IdUsuario = objSession.UserId;

                            int numeroAplicacion = dao.Aplicaciones.InsertarAplicaciones(objApl);
                            objApl.NumeroAplicacion = numeroAplicacion;
                            if (numeroAplicacion > 0)
                            {
                                int idGestor = dao.Aplicaciones.BuscarIdGestor(dctoDiario.CodigoEmpresa, dctoDiario.CodigoSucursal, (int)dctoDiario.IdCliente, (int)dctoDiario.IdDeudor);
                                int item = 1;
                                foreach (dto.DatosCargaPago datos in lst)
                                {
                                    objAplItems = new AplicaionesItems()
                                    {
                                        CodigoEmpresa = objApl.CodigoEmpresa,
                                        CodigoSucursal = objApl.CodigoSucursal,
                                        Anio = dctoDiario.Anio,
                                        NumeroAplicacion = numeroAplicacion,
                                        Item = item,
                                        AnioDocumento = dctoDiario.Anio,
                                        NumeroDocumento = dctoDiario.NumeroDocumento,
                                        AnioDocumento2 = null,
                                        NumeroDocumento2 = null,
                                        TipoDocumento = null,
                                        Numero = null,
                                        TipoDocumento2 = null,
                                        Numero2 = null,
                                        IdCliente = dctoDiario.IdCliente,
                                        IdDeudor = datos.Ctcid,
                                        Ccbid = datos.Ccbid,
                                        Capital = datos.Monto,
                                        Interes = datos.Interes,
                                        Honorario = datos.Honorario,
                                        GastoPrejudicial = 0,
                                        GastoJudicial = 0,
                                        IdGestor = idGestor,
                                        IdVendedor = null,
                                        Remesa = false
                                    };
                                    item++;
                                    err = dao.Aplicaciones.InsertarAplicacionesItems(objAplItems);
                                    if (err <= 0)
                                    {
                                        objCarga.ListaErrores.Add(new ErrorCarga
                                        {
                                            TipoError = "NO SE PUDO INSERTAR EL ITEM DE LA APLICACION.",
                                            Rut = datos.Rut + "-" + datos.Dv,
                                            Dv = datos.Dv,
                                            Nombre = "",
                                            TipoDocumento = "",
                                            Numero = datos.NumeroCpbt
                                        });
                                        error = true;
                                    }
                                }

                            }
                            else
                            {
                                objCarga.ListaErrores.Add(new ErrorCarga
                                {
                                    TipoError = "NO SE PUDO INSERTAR LA APLICACION.",
                                    Rut = "",
                                    Dv = "",
                                    Nombre = "",
                                    TipoDocumento = "",
                                    Numero = ""
                                });
                                error = true;
                            }

                        }
                    }
                    /*  If err >= 0 And TabApl.Rows.Count > 0 Then

                    '-------------Inserto el Detalle de la Aplicacion-------------------
                    Dim spA3 As New Horus.StoredProcedure("Insertar_Aplicaciones_Items")
                    spA3.AgregarParametro("api_pclid", RowDat("pclid"))
                    spA3.AgregarParametro("api_ctcid", RowDat("ctcid"))
                    spA3.AgregarParametro("api_ccbid", RowDat("ccbid"))
                    spA3.AgregarParametro("api_capital", RowDat("monto"))
                    spA3.AgregarParametro("api_interes", RowDat("interes"))
                    spA3.AgregarParametro("api_honorario", RowDat("honorario"))
                    spA3.AgregarParametro("api_gastpre", 0)
                    spA3.AgregarParametro("api_gastjud", 0)
                    If gesid > 0 Then
                        spA3.AgregarParametro("api_gesid", gesid)
                    Else
                        spA3.AgregarParametro("api_gesid", DBNull.Value)
                    End If
                    spA3.AgregarParametro("api_vdeid", DBNull.Value)

                    err = spA3.EjecutarProcedimiento(conn, myTrans)

                    If err < 0 Then
                        Exit For
                    End If
                     */
                }


            } 
            return error;
        }
          
            
        

        public static bool ValidarCargaPagos(dto.DatosCargaPago datos, dto.CargaMasiva objCarga, UserSession objSession)
        {
            bool error = false;
            Funciones objFunc = new Funciones();
            //Valido que el cliente del archivo sea igual al cliente ingresado
            if (objCarga.RutCliente != datos.RutCliente)
            {
                objCarga.ListaErrores.Add(new ErrorCarga
                {
                    TipoError = "CLIENTE NO CONCUERDA",
                    Rut = datos.Rut + "-" + datos.Dv,
                    Dv = datos.Dv,
                    Nombre = "",
                    TipoDocumento = "",
                    Numero = datos.NumeroCpbt
                });
                error = true;
            }
            if (!error)
            {
                //Valido el rut del deudor
                if (!Funciones.ValidaRut(datos.Rut + datos.Dv))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("RutBad", objSession.Idioma),
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = "",
                        TipoDocumento = "",
                        Numero = datos.NumeroCpbt
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido el numero
                if (string.IsNullOrEmpty(datos.NumeroCpbt))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "NUMERO DOCUMENTO NO PUEDE ESTAR NULO",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = "",
                        TipoDocumento = "",
                        Numero = datos.NumeroCpbt
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido moneda
                if (string.IsNullOrEmpty(datos.Moneda))
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MONEDA NO PUEDE SER NULO",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = "",
                        TipoDocumento = "",
                        Numero = datos.NumeroCpbt
                    });
                    error = true;
                }
                List<Combobox> lstMoneda = dao.Comprobante.ListarMonedas(objSession.CodigoEmpresa);
                if (lstMoneda.Find(x => x.Text == datos.Moneda.ToUpper()) == null)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "MONEDA NO EXISTE",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = "",
                        TipoDocumento = "",
                        Numero = datos.NumeroCpbt
                    });
                    error = true;
                }
            }
            if (!error)
            {
                //Valido tipo cambio
                if ((int)datos.TipoCambio == 0 )
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = "TIPO CAMBIO DEBE SER MAYOR O IGUAL A 1",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = "",
                        TipoDocumento = "",
                        Numero = datos.NumeroCpbt
                    });
                    error = true;
                }
            }

            if (!error)
            {
                //Valido monto
                if (datos.Monto == 0 )
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError = objFunc.TraeError("Monto", objSession.Idioma) + ", " + objFunc.TraeError("DatNull", objSession.Idioma),
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = "",
                        TipoDocumento = "",
                        Numero = datos.NumeroCpbt
                    });
                    error = true;
                }
            }
            //Valido deudor
            if (!error)
            {
                if (datos.Ctcid <= 0)
                {
                    objCarga.ListaErrores.Add(new ErrorCarga
                    {
                        TipoError ="DEUDOR NO ENCONTRADO",
                        Rut = datos.Rut + "-" + datos.Dv,
                        Dv = datos.Dv,
                        Nombre = "",
                        TipoDocumento = "",
                        Numero = datos.NumeroCpbt
                    });
                    error = true;
                }
            }
            return error;
        }



        #endregion
    }
}
