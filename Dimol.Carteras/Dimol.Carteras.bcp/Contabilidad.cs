using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.bcp
{
    public class Contabilidad
    {
        public static int GrabarCpbt(dto.AceptarComprobante obj)
        {
            /*
          Try


                dsDoc = func.Trae_Datos_CabCpbt(Empresa, Sucursal, TipCpbt, numCpbt)
                dsDet = Revis_DetCpbt()

                For Each RCpbtDoc In dsDoc.Tables(0).Rows
                    Exento = False
                    TipCam = RCpbtDoc("cbc_tipcambio")

                    '----------------Reviso si ya esta contabilizado---------------------
                    StrSql = " select count(ada_aniodoc) from asientos_contables_cpbtdoc_apl"
                    StrSql = StrSql + " where ada_codemp = " + Empresa.ToString
                    StrSql = StrSql + " and  ada_tpcid = " + RCpbtDoc("cbc_tpcid").ToString
                    StrSql = StrSql + " and  ada_numcpbt = " + RCpbtDoc("cbc_numero").ToString

                    dsFind = Hds.ConsultaBD(StrSql)

                    If dsFind.Tables(0).Rows(0)(0) > 0 Then
                        StrSql = " select ada_anio, ada_tipo, ada_numero from asientos_contables_cpbtdoc_apl"
                        StrSql = StrSql + " where ada_codemp = " + Empresa.ToString
                        StrSql = StrSql + " and  ada_tpcid = " + RCpbtDoc("cbc_tpcid").ToString
                        StrSql = StrSql + " and  ada_numcpbt = " + RCpbtDoc("cbc_numero").ToString

                        dsCont = Hds.ConsultaBD(StrSql)
                    End If


                    '--------------Elimino todo el Detalle-------------------------------
                    If dsFind.Tables(0).Rows(0)(0) > 0 Then
                        conn.SQLConn.Open()
                        myTrans = conn.SQLConn.BeginTransaction()

                        Dim sp1 As New Horus.StoredProcedure("Delete_Asientos_Contables_Detalle_Todo")
                        sp1.AgregarParametro("acd_codemp", Empresa)
                        sp1.AgregarParametro("acd_anio", Anio)
                        sp1.AgregarParametro("acd_tipo", Tipo)
                        sp1.AgregarParametro("acd_numero", Numero)

                        ErrBase = sp1.EjecutarProcedimiento(conn, myTrans)

                        If ErrBase < 0 Then
                            myTrans.Rollback()
                            conn.SQLConn.Close()

                            Return func.TraeError("ErrDetAsi", Idioma)
                        Else
                            myTrans.Commit()
                            conn.SQLConn.Close()
                        End If
                    End If


                    '----------------Busco el tipo de Cpbt--------------------------
                    cont = Revisa_Cpbt(RCpbtDoc("cbc_tpcid"))

                    If cont = True Then
                        '-------------Coemizo a Revisar los montos--------------------
                        For Each RCpbtDet In dsDet.Tables(0).Rows
                            If RCpbtDet("dcc_porcfact") <> 0 Then
                                TotNet = TotNet + (RCpbtDet("dcc_precio") * RCpbtDet("dcc_porcfact"))
                            Else
                                TotNet = TotNet + RCpbtDet("dcc_neto")
                            End If

                            If RCpbtDet("dcc_retenido") = "S" Then
                                TotRet = TotRet + RCpbtDet("dcc_impuesto")
                                'TotRet = 0
                            Else
                                TotImp = TotImp + RCpbtDet("dcc_impuesto")
                            End If


                        Next

                        '----------------Grabo la cabecera---------------------
                        If IsDBNull(RCpbtDoc("pcl_nomfant")) = False Then
                            glo = glo + RCpbtDoc("pcl_nomfant")
                        End If
                        If IsDBNull(RCpbtDoc("tci_nombre")) = False Then
                            glo = glo + ", " + RCpbtDoc("tci_nombre") + " numero :" + RCpbtDoc("cbc_numprovcli")
                        End If

                        GloCab = glo
                        Usuario = Usuario
                        ActCab = True
                        TotDebe = (TotNet + TotImp) * TipCam
                        TotHaber = (TotNet + TotImp) * TipCam


                        If func.Configuracion_Emp_Str(Empresa, 46) = "S" Then
                            TotDebe = CInt(Math.Round(TotDebe, 2))
                            TotHaber = CInt(Math.Round(TotHaber, 2))
                        End If

                        If dsFind.Tables(0).Rows(0)(0) > 0 Then
                            If IsDBNull(RCpbtDoc("cbc_feccont")) = True Then
                                Anio = Year(RCpbtDoc("cbc_feccpbt"))
                                Tipo = "T"
                                Numero = dsCont.Tables(0).Rows(0)(2)
                            Else
                                Anio = Year(RCpbtDoc("cbc_feccont"))
                                Tipo = "T"
                                Numero = dsCont.Tables(0).Rows(0)(2)
                            End If
                        Else
                            If IsDBNull(RCpbtDoc("cbc_feccont")) = True Then
                                Anio = Year(RCpbtDoc("cbc_feccpbt"))
                                Tipo = "T"
                                Numero = 0
                            Else
                                Anio = Year(RCpbtDoc("cbc_feccont"))
                                Tipo = "T"
                                Numero = 0
                            End If

                        End If
                        err = Grabar()


                        '-------------------Creo el Detalle Asiento con la cuenta del CPBT------------------------

                        '------------Detalle del Cpbt---------
                        Glodet = RCpbtDoc("pcl_nomfant")
                        Item = 0
                        Usuario = Usuario
                        ActCab = False
                        Cuenta = Cuenta1
                        If DebHabDoc = 1 Then
                            Debe = (TotNet + TotImp) * TipCam
                            Haber = 0
                        Else
                            Debe = 0
                            Haber = (TotNet + TotImp) * TipCam
                        End If

                        If func.Configuracion_Emp_Str(Empresa, 46) = "S" Then
                            Debe = CInt(Math.Round(Debe, 2))
                            Haber = CInt(Math.Round(Haber, 2))
                        End If


                        err = Grabar()

                        '-------------------Reviso el impuesto-----------------
                        StrSql = "  Select  impuestos.ipt_pctid, ipt_nombre, ipt_retenido"
                        StrSql = StrSql + " FROM provcli,   "
                        StrSql = StrSql + " provcli_impuestos,   "
                        StrSql = StrSql + " impuestos"
                        StrSql = StrSql + " WHERE  provcli_impuestos.pci_codemp = provcli.pcl_codemp  and  "
                        StrSql = StrSql + " provcli_impuestos.pci_pclid = provcli.pcl_pclid  and  "
                        StrSql = StrSql + " impuestos.ipt_codemp = provcli_impuestos.pci_codemp  and  "
                        StrSql = StrSql + " impuestos.ipt_iptid = provcli_impuestos.pci_iptid  and  "
                        StrSql = StrSql + " provcli.pcl_codemp = " + Empresa.ToString
                        StrSql = StrSql + " and provcli.pcl_rut = '" + RCpbtDoc("pcl_rut") + "'"

                        If AnulaImp = "S" Then
                            StrSql = StrSql + " and provcli.pcl_codemp = -1"
                        End If

                        If TipImp = "R" Then
                            StrSql = StrSql + "  and ipt_retenido = 'S'"
                        End If



                        dsImp = Hds.ConsultaBD(StrSql)

                        If dsImp.Tables(0).Rows.Count > 0 Then
                            Item = 0
                            ' GloCab = ""
                            Usuario = Usuario
                            ActCab = False
                            Cuenta = dsImp.Tables(0).Rows(0)("ipt_pctid")
                            Glodet = dsImp.Tables(0).Rows(0)("ipt_nombre")
                            If DebHabDoc = 1 Then
                                Debe = 0
                                If TotRet > 0 Then
                                    Haber = TotRet * TipCam
                                Else
                                    Haber = TotImp * TipCam
                                End If
                                Debe = 0
                            Else
                                If TotRet > 0 Then
                                    Debe = TotRet * TipCam
                                Else
                                    Debe = TotImp * TipCam
                                End If
                                Haber = 0
                            End If

                            If func.Configuracion_Emp_Str(Empresa, 46) = "S" Then
                                Debe = CInt(Math.Round(Debe, 2))
                                Haber = CInt(Math.Round(Haber, 2))
                            End If

                            err = Grabar()
                        End If


                        '-------------------------Creo el Detalle----------------------------------.
                        For Each RCpbtDet In dsDet.Tables(0).Rows
                            TotNet = TotNet + RCpbtDet("dcc_neto")
                            If RCpbtDet("dcc_retenido") = "S" Then
                                TotRet = TotImp + RCpbtDet("dcc_impuesto")
                            Else
                                TotImp = TotImp + RCpbtDet("dcc_impuesto")
                            End If

                            If RCpbtDet("dcc_retenido") = "N" And RCpbtDet("dcc_impuesto") = 0 Then
                                Exento = True
                            Else
                                Exento = False
                            End If

                            '--------------------Creo el Detalle-------------------
                            Item = 0

                            If IsDBNull(RCpbtDet("ins_pctid")) = False Then
                                Cuenta = RCpbtDet("ins_pctid")
                                Glodet = RCpbtDet("ins_nombre")
                            End If

                            If IsDBNull(RCpbtDet("pdt_pctid")) = False Then
                                Cuenta = RCpbtDet("pdt_pctid")
                                Glodet = RCpbtDet("pdt_nombre")
                            End If


                            If DebHabDoc = 1 Then
                                Debe = 0
                                Haber = RCpbtDet("dcc_neto") * TipCam

                                If RCpbtDet("dcc_porcfact") <> 0 Then
                                    Haber = (RCpbtDet("dcc_precio") - TotRet) * TipCam
                                Else
                                    Haber = (RCpbtDet("dcc_neto") - TotRet) * TipCam
                                End If

                                If RCpbtDet("dcc_porcfact") <> 0 Then
                                    Haber = Haber * (RCpbtDet("dcc_porcfact") - TotRet)
                                End If


                            Else
                                Debe = RCpbtDet("dcc_neto") * TipCam
                                Haber = 0

                                If RCpbtDet("dcc_porcfact") <> 0 Then
                                    Debe = (RCpbtDet("dcc_precio") - TotRet) * TipCam
                                Else
                                    Debe = (RCpbtDet("dcc_neto") - TotRet) * TipCam
                                End If


                                If RCpbtDet("dcc_porcfact") <> 0 Then
                                    Debe = Debe * (RCpbtDet("dcc_porcfact") - TotRet)
                                End If
                            End If

                            '  Exento = 0
                            ActCab = False

                            If func.Configuracion_Emp_Str(Empresa, 46) = "S" Then
                                Debe = CInt(Math.Round(Debe, 2))
                                Haber = CInt(Math.Round(Haber, 2))
                            End If

                            err = Grabar()

                        Next

                        '---------------------Grabo el documento Asiento Documento-----------------
                        If dsFind.Tables(0).Rows(0)(0) = 0 Then
                            conn.SQLConn.Open()
                            myTrans = conn.SQLConn.BeginTransaction()

                            Dim sp As New Horus.StoredProcedure("Insertar_Asientos_Contables_CpbtDoc_Apl")
                            sp.AgregarParametro("ada_codemp", Empresa)
                            sp.AgregarParametro("ada_anio", Anio)
                            sp.AgregarParametro("ada_tipo", Tipo)
                            sp.AgregarParametro("ada_numero", Numero)
                            sp.AgregarParametro("ada_item", 1)
                            sp.AgregarParametro("ada_sucid", Sucursal)
                            sp.AgregarParametro("ada_tpcid", TipCpbt)
                            sp.AgregarParametro("ada_numcpbt", numCpbt)
                            sp.AgregarParametro("ada_aniodoc", DBNull.Value)
                            sp.AgregarParametro("ada_numdoc", DBNull.Value)
                            sp.AgregarParametro("ada_anioapl", DBNull.Value)
                            sp.AgregarParametro("ada_numapl", DBNull.Value)

                            ErrBase = sp.EjecutarProcedimiento(conn, myTrans)

                            If ErrBase < 0 Then
                                myTrans.Rollback()
                                conn.SQLConn.Close()

                                Return func.TraeError("ErrDetAsi", Idioma)
                            Else
                                myTrans.Commit()
                                conn.SQLConn.Close()

                            End If

                        End If

                    End If

                Next


            Catch ex As Exception

                Return ex.Message

            End Try
             * 
             */
            return 1;
        }
    }
}
