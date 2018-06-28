using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dimol.dao;
using System.Data.SqlClient;
using CYPH;

namespace Dimol.dao
{
    public class Funciones
    {
        public Funciones()
        {
        }

        public decimal ConfiguracionNum (int clave)
        {
            DataSet dsConf2;
            StoredProcedure sp6 = new StoredProcedure("Trae_Configuracion_Sistema");
            sp6.AgregarParametro("cfid", clave);
            dsConf2 = sp6.EjecutarProcedimiento();
            return decimal.Parse( dsConf2.Tables[0].Rows[0][0].ToString());
        }

        public string ConfiguracionStr(int clave)
        {
            DataSet dsConf2;
            StoredProcedure sp6 = new StoredProcedure("Trae_Configuracion_Sistema");
            sp6.AgregarParametro("cfid", clave);
            dsConf2 = sp6.EjecutarProcedimiento();
            return dsConf2.Tables[0].Rows[0][1].ToString();
        }

        public string TraeError(string errNum, int idioma)
        {
            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            SqlTransaction trans = conn.SQLConn.BeginTransaction();

            StoredProcedure sp2 = new StoredProcedure("Trae_Error");
            DataSet dsError = new DataSet();
            sp2.AgregarParametro("error", errNum);
            sp2.AgregarParametro("idioma", idioma);
            dsError = sp2.EjecProdEspecial(conn, trans);

            if (dsError.Tables[0].Rows.Count > 0)
            {
                return dsError.Tables[0].Rows[0][0].ToString();
            } else
            {
                return "Error no Definido";
            }
        }

        public string AppPath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        }

        public string Encripta(string password)
        {
            string encrip = "";

            Ucode objUcode = new Ucode();

            encrip = objUcode.Encripta(password);

            return encrip;

        }

        public string Desencripta(string psw_encriptada)
        {
            string result = "";

            Ucode objUcode = new Ucode();

            result = objUcode.Desencripta(psw_encriptada);

            return result;

        }

        public string FechaServer()
        {
            DataSet dsFecha = new DataSet();
            StoredProcedure sp6 = new StoredProcedure("Trae_Fecha_Servidor");
            dsFecha = sp6.EjecutarProcedimiento();

            return dsFecha.Tables[0].Rows[0][0].ToString();
        }

        public int ConfiguracionEmpNum(int codemp, int clave)
        {
            DataSet ds = new DataSet();
            StoredProcedure sp = new StoredProcedure("Trae_Configuracion_Empresa");
            sp.AgregarParametro("emc_codemp", codemp);
            sp.AgregarParametro("emc_emcid", clave);
            ds = sp.EjecutarProcedimiento();
            return (int)( decimal.Parse( ds.Tables[0].Rows[0][0].ToString()));
        }

        public string ConfiguracionEmpStr(int codemp, int clave)
        {
            DataSet ds = new DataSet();
            StoredProcedure sp = new StoredProcedure("Trae_Configuracion_Empresa");
            sp.AgregarParametro("emc_codemp", codemp);
            sp.AgregarParametro("emc_emcid", clave);
            ds = sp.EjecutarProcedimiento();
            return ds.Tables[0].Rows[0][1].ToString();
        }

        public int TraeGestor(int codemp, int sucid, int emplid){
            int gestId = 0;
            if (emplid == 0)
            {
                gestId = 0;
            }
            else
            {
                try
                {
                    DataSet ds = new DataSet();
                    StoredProcedure sp = new StoredProcedure("Trae_Gestor");
                    sp.AgregarParametro("ges_codemp", codemp);
                    sp.AgregarParametro("ges_sucid", sucid);
                    sp.AgregarParametro("ges_emplid", emplid);
                    ds = sp.EjecutarProcedimiento();
                    gestId = Int32.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                catch (Exception ex)
                {
                    InsertarError(ex.Message, ex.StackTrace, "Dimol.dao.Fuciones.TraeGestor", emplid);
                    gestId = 0;
                }
                
            }
            return gestId;
        }

        public static void TraeDolarUFHoy(int codemp, dto.Indicadores objInd)
        {
            try
            {
                DataSet ds = new DataSet();
                List<dto.Menu> lstMenu = new List<dto.Menu>();
                StoredProcedure sp = new StoredProcedure("_Trae_Indicadores");
                sp.AgregarParametro("codemp", codemp);
                ds = sp.EjecutarProcedimiento();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["dolar"].ToString() != "")
                    {
                        objInd.DolarObservado = decimal.Parse(ds.Tables[0].Rows[i]["dolar"].ToString());
                    }

                    if (ds.Tables[0].Rows[i]["uf"].ToString() != "")
                    {
                        objInd.UF = decimal.Parse(ds.Tables[0].Rows[i]["uf"].ToString());
                    }
     
                }
            }
            catch (Exception ex)
            {
                InsertarError(ex.Message, ex.StackTrace, "Dimol.dao.Fuciones.TraeDolarUFHoy", 0);
                throw ex;
            }
        }

        public static int InsertarError( string mensaje, string stacktrace, string pagina, int usuario)
        {
            try
            {
                StoredProcedure spAc = new StoredProcedure("[_Insertar_Log_Error]");
                spAc.AgregarParametro("mensaje", mensaje);
                spAc.AgregarParametro("stacktrace", stacktrace);
                spAc.AgregarParametro("pagina",pagina);
                spAc.AgregarParametro("usuario", usuario);

                int error = spAc.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string TraeEtiqueta(string clave, int idioma)
        {
            DataSet ds = new DataSet();
            StoredProcedure sp = new StoredProcedure("Trae_Etiquetas");
            sp.AgregarParametro("codigo", clave);
            sp.AgregarParametro("idioma", idioma);
            ds = sp.EjecutarProcedimiento();

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return "Etiqueta no Definida";
            }
        }

        public static int InsertaMonedaValor(int codemp, int codmon, DateTime fecha, decimal valorMoneda)
        {
            try
            {
                StoredProcedure spAc = new StoredProcedure("[Insertar_Monedas_Valores]");
                spAc.AgregarParametro("mnv_codemp", codemp);
                spAc.AgregarParametro("mnv_codmon", codmon);
                spAc.AgregarParametro("mnv_fecha", fecha);
                spAc.AgregarParametro("mnv_valor", valorMoneda);

                int error = spAc.EjecutarProcedimientoTrans();
                return error;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
            
    }


}


/*
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System.IO
Imports System.Drawing
Imports System.Collections
Imports System.Globalization
Imports System.Web.UI
Imports Horus
Imports System.Data.OleDb
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf




Namespace Horus
    Public Class Funciones
        Public Function Configuracion_Num(ByVal clave As Integer) As Decimal
            Dim dsConf2 As New DataSet
            Dim sp6 As New StoredProcedure("Trae_Configuracion_Sistema")
            sp6.AgregarParametro("cfid", clave)
            dsConf2 = sp6.EjecutarProcedimiento()

            Return dsConf2.Tables(0).Rows(0)(0)

        End Function

        Public Function Configuracion_Str(ByVal clave As Integer) As String
            Dim dsConf2 As New DataSet
            Dim ConfText As String
            Dim sp6 As New StoredProcedure("Trae_Configuracion_Sistema")
            sp6.AgregarParametro("cfid", clave)
            dsConf2 = sp6.EjecutarProcedimiento()

            ConfText = dsConf2.Tables(0).Rows(0)(1).ToString

            Return ConfText

        End Function



        Public Function FechaGenerica(ByVal fecha As Date) As String
            Dim dia As Integer
            Dim mes As Integer
            Dim anio As Integer
            Dim fecfin As String

            dia = Day(fecha)
            mes = Month(fecha)
            anio = Year(fecha)

            'fecfin = Right("0000" + CStr(anio), 4) + "/" + Right("00" + CStr(mes), 2) + "/" + Right("00" + CStr(dia), 2)
            fecfin = Right("00" + CStr(dia), 2) + "/" + Right("00" + CStr(mes), 2) + "/" + Right("0000" + CStr(anio), 4)

            Return fecfin

        End Function





        Public Function TraeError(ByVal errNum As String, ByVal errText As String, ByVal idioma As Integer) As String
            Dim Conn As New Conexion
            Conn.SQLConn.Open()
            Dim myTrans As SqlTransaction = Conn.SQLConn.BeginTransaction()

            Dim Find As Integer
            Dim err As Integer

            '-----------Busco el Ultimo Numero------------- 
            Dim sp1 As New StoredProcedure("Find_Error")
            Dim DsFind As New DataSet
            sp1.AgregarParametro("err_codigo", errNum)
            DsFind = sp1.EjecProdEspecial(Conn, myTrans)

            Find = CInt(DsFind.Tables(0).Rows(0)(0).ToString())

            If Find > 0 Then
                Dim sp2 As New StoredProcedure("Trae_Error")
                Dim DsErr As New DataSet
                sp2.AgregarParametro("error", errNum)
                sp2.AgregarParametro("idioma", idioma)
                DsErr = sp2.EjecProdEspecial(Conn, myTrans)

                Return DsErr.Tables(0).Rows(0)(0).ToString()
            Else
                '--Busco el ultimo numero de error---
                Dim sp3 As New StoredProcedure("UltNum_Error")
                Dim DsUlt As New DataSet
                DsUlt = sp3.EjecProdEspecial(Conn, myTrans)

                Find = CInt(DsUlt.Tables(0).Rows(0)(0).ToString())

                '----Hago el Insert----

                Dim sp4 As New StoredProcedure("Insert_Error")
                sp4.AgregarParametro("err_errid", Find)
                sp4.AgregarParametro("err_codigo", errNum)
                sp4.AgregarParametro("err_descripcion", errText)
                err = sp4.EjecutarProcedimiento(Conn, myTrans)

                Dim sp5 As New StoredProcedure("Insert_Error_Idiomas")
                sp5.AgregarParametro("eri_errid", Find)
                sp5.AgregarParametro("eri_idid", idioma)
                sp5.AgregarParametro("eri_descripcion", errText)
                err = sp5.EjecutarProcedimiento(Conn, myTrans)

                Try
                    If err < 0 Then
                        myTrans.Rollback()
                        Conn.SQLConn.Close()
                        Return ""
                    End If

                    myTrans.Commit()
                    Conn.SQLConn.Close()
                    Return errText

                Catch ex As Exception
                    myTrans.Rollback()
                    Conn.SQLConn.Close()
                    Return ""
                End Try

            End If

        End Function

        Public Function TraeEtiqueta(ByVal EtiClave As String, ByVal idioma As Integer) As String
            Dim dsEti As New DataSet
            Dim Etiqueta As String = "Sin Etiqueta"

            Try
                Dim sp4 As New StoredProcedure("Trae_Etiquetas")
                sp4.AgregarParametro("codigo", EtiClave)
                sp4.AgregarParametro("idioma", idioma)
                dsEti = sp4.EjecutarProcedimiento()

                Etiqueta = dsEti.Tables(0).Rows(0)(0)

                Return Etiqueta

            Catch ex As Exception
                Return "Sin Etiqueta"
            End Try


            Return Etiqueta

        End Function

        Public Function Mail(ByVal codemp As Integer, ByVal UsuDest As String, ByVal MailDest As String, ByVal MailText As String, ByVal subject As String, ByVal html As Boolean, ByVal idioma As Integer) As String
            Dim correo As New System.Net.Mail.MailMessage
            Dim Host As String
            Dim CtaMail As String
            Dim PasMail As String
            Dim From As String
            Dim Smtp As New System.Net.Mail.SmtpClient
            Dim MsgMail As String


            '----------Busco los datos con cual se envia el mail-------
            Host = Configuracion_Emp_Str(codemp, 60)
            CtaMail = Configuracion_Emp_Str(codemp, 61)
            PasMail = Configuracion_Emp_Str(codemp, 62)
            From = Configuracion_Emp_Str(codemp, 64)
            'From = "DIMERC"

            correo.From = New System.Net.Mail.MailAddress(CtaMail, From)
            Try
                correo.To.Add(MailDest)
                correo.Subject = subject
                correo.Body = MailText
                correo.IsBodyHtml = html
                correo.Priority = System.Net.Mail.MailPriority.Normal
                'correo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess

                Smtp.Host = Host
                Smtp.UseDefaultCredentials = False
                Smtp.Credentials = New System.Net.NetworkCredential(CtaMail, PasMail)
                Smtp.EnableSsl = True

            Catch ex As Exception

            End Try



            MsgMail = ""

            Try
                Smtp.Send(correo)
                MsgMail = ""
            Catch ex As Exception
                MsgMail = "BAD"
            End Try

            Return MsgMail

        End Function

        Public Function Mail2(ByVal codemp As Integer, ByVal UsuDest As String, ByVal MailDest As String, ByVal MailText As String, ByVal subject As String, ByVal html As Boolean, ByVal idioma As Integer, ByVal from As String, ByVal host As String, ByVal CtaMail As String, ByVal PasMail As String) As String
            Dim correo As New System.Net.Mail.MailMessage
            Dim Smtp As New System.Net.Mail.SmtpClient
            Dim MsgMail As String


            '----------Busco los datos con cual se envia el mail-------
            '  host = Configuracion_Emp_Str(codemp, 3)
            ' CtaMail = Configuracion_Emp_Str(codemp, 4)
            ' PasMail = Configuracion_Emp_Str(codemp, 5)
            correo.From = New System.Net.Mail.MailAddress(CtaMail, from)
            Try
                correo.To.Add(MailDest)
                correo.Subject = subject
                correo.Body = MailText
                correo.IsBodyHtml = html
                correo.Priority = System.Net.Mail.MailPriority.High
                correo.Bcc.Add("andres.gaete@dimol.cl")
                correo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess

                Smtp.Host = host
                Smtp.UseDefaultCredentials = False
                Smtp.Credentials = New System.Net.NetworkCredential(CtaMail, PasMail)
                ' Smtp.EnableSsl = True

                MsgMail = ""

                Try
                    Smtp.Send(correo)
                    MsgMail = ""
                Catch objException As Exception
                    MsgMail = "BAD"
                End Try


            Catch ex As Exception

            End Try


            Return MsgMail

        End Function

        Public Function Mail2(ByVal codemp As Integer, ByVal UsuDest As String, ByVal MailDest As String, ByVal MailText As String, ByVal subject As String, ByVal html As Boolean, ByVal idioma As Integer, ByVal from As String, ByVal host As String, ByVal CtaMail As String, ByVal PasMail As String, ByVal archivo As String) As String
            Dim correo As New System.Net.Mail.MailMessage
            Dim Smtp As New System.Net.Mail.SmtpClient
            Dim MsgMail As String
            Dim Attach As New System.Net.Mail.Attachment(archivo)


            '----------Busco los datos con cual se envia el mail-------
            '  host = Configuracion_Emp_Str(codemp, 3)
            ' CtaMail = Configuracion_Emp_Str(codemp, 4)
            ' PasMail = Configuracion_Emp_Str(codemp, 5)
            correo.From = New System.Net.Mail.MailAddress(CtaMail, from)
            Try
                correo.To.Add(MailDest)
                correo.Subject = subject
                correo.Body = MailText
                correo.IsBodyHtml = html
                correo.Priority = System.Net.Mail.MailPriority.High
                correo.Bcc.Add("andres.gaete@dimol.cl")
                ' correo.Bcc.Add(CtaMail)
                correo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess
                correo.Attachments.Add(Attach)

                Smtp.Host = host
                Smtp.UseDefaultCredentials = False
                Smtp.Credentials = New System.Net.NetworkCredential(CtaMail, PasMail)
                ' Smtp.EnableSsl = True

                MsgMail = ""

                Try
                    Smtp.Send(correo)
                    MsgMail = ""
                Catch objException As Exception
                    MsgMail = "BAD"
                End Try


            Catch ex As Exception

            End Try


            Return MsgMail

        End Function

        Public Function Mail(ByVal codemp As Integer, ByVal UsuDest As String, ByVal MailDest As String, ByVal MailText As String, ByVal subject As String, ByVal html As Boolean, ByVal idioma As Integer, ByVal archivo As String) As String
            Dim correo As New System.Net.Mail.MailMessage
            Dim Host As String
            Dim CtaMail As String
            Dim PasMail As String
            Dim From As String
            Dim Smtp As New System.Net.Mail.SmtpClient
            Dim MsgMail As String
            Dim Attach As New System.Net.Mail.Attachment(archivo)


            '----------Busco los datos con cual se envia el mail-------
            Host = Configuracion_Emp_Str(codemp, 60)
            CtaMail = Configuracion_Emp_Str(codemp, 61)
            PasMail = Configuracion_Emp_Str(codemp, 62)
            From = Configuracion_Emp_Str(codemp, 64)

            correo.From = New System.Net.Mail.MailAddress(CtaMail, From)

            Try
                correo.To.Add(MailDest)
                correo.Subject = subject
                correo.Body = MailText
                correo.IsBodyHtml = html
                correo.Priority = System.Net.Mail.MailPriority.Normal
                correo.Attachments.Add(Attach)


                'correo.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess

                Smtp.Host = Host
                Smtp.UseDefaultCredentials = False
                Smtp.Credentials = New System.Net.NetworkCredential(CtaMail, PasMail)
                'Smtp.EnableSsl = True

                MsgMail = ""

                Try
                    Smtp.Send(correo)
                    MsgMail = ""
                Catch ex As Exception
                    MsgMail = "BAD"
                End Try

            Catch ex As Exception

            End Try



           

            Return MsgMail

        End Function


        Public Function Valida_Rut(ByVal rut As String) As Boolean
            Dim i As Integer
            Dim numero As String
            Dim mult As Integer
            Dim total As Integer
            Dim tot1 As Integer
            Dim digito As Integer
            Dim dv As String

            Try
                If Len(rut) >= 10 Then
                    Return False
                End If

                dv = Right(rut, 1)

                If Left(Trim(rut), 1) = "0" Then
                    Return False
                End If

                If dv = "0" Then
                    dv = "11"
                End If

                If dv = "K" Then
                    dv = "10"
                End If

                mult = 2
                For i = rut.Length - 1 To 1 Step -1
                    numero = Mid(rut, i, 1)
                    If Asc(numero) >= 48 Or Asc(numero) <= 57 Then
                        tot1 = (CInt(numero) * mult)
                        total = total + tot1
                        mult = mult + 1
                        If mult > 7 Then
                            mult = 2
                        End If
                    Else
                        Return False
                    End If
                Next

                total = total Mod 11
                digito = 11 - total

                If CInt(dv) <> digito Then
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try



            Return True

        End Function

        Public Function Valida_Cuit(ByVal rut As String) As Boolean
            Dim i As Integer
            Dim numero As String
            Dim total As Integer
            Dim tot1 As Integer
            Dim digito As Integer
            Dim dv As String
            Dim mult() As Integer = {5, 4, 3, 2, 7, 6, 5, 4, 3, 2, 1}
            Dim x As Integer = 0

            Try
                dv = Right(rut, 1)

                If dv = "N" Then
                    dv = 9
                End If



                If Left(Trim(rut), 1) = "0" Then
                    Return False
                End If

                For i = 1 To rut.Length - 1
                    numero = Mid(rut, i, 1)
                    If Asc(numero) >= 48 Or Asc(numero) <= 57 Then
                        tot1 = (CInt(numero) * mult(x))
                        total = total + tot1
                        x = x + 1
                        If x > 9 Then
                            x = 0
                        End If
                    Else
                        Return False
                    End If
                Next

                total = total Mod 11
                digito = 11 - total

                If CInt(dv) <> digito Then
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try



            Return True

        End Function

        Public Function Rut_Digito(ByVal rut As String) As String
            Dim i As Integer
            Dim numero As String
            Dim mult As Integer
            Dim total As Integer
            Dim tot1 As Integer
            Dim digito As Integer
            Dim dv As String

            dv = Right(rut, 1)

            If dv = "0" Then
                dv = "11"
            End If

            If dv = "K" Then
                dv = "10"
            End If

            mult = 2
            For i = rut.Length To 1 Step -1
                numero = Mid(rut, i, 1)
                If Asc(numero) >= 48 Or Asc(numero) <= 57 Then
                    tot1 = (CInt(numero) * mult)
                    total = total + tot1
                    mult = mult + 1
                    If mult > 7 Then
                        mult = 2
                    End If
                Else
                    Return False
                End If
            Next

            total = total Mod 11
            digito = 11 - total

            dv = CStr(digito)

            If digito = "11" Then
                dv = "0"
            End If

            If digito = "10" Then
                dv = "K"
            End If

            Return dv

        End Function

        

        Public Function Vendedor(ByVal codemp As Integer, ByVal codsuc As Integer, ByVal empleado As Integer) As Integer
            Dim gestId As Integer
            Dim dsGest As New DataSet

            If empleado = 0 Then
                gestId = 0
            Else
                Dim sp4 As New StoredProcedure("Trae_Vendedor")
                sp4.AgregarParametro("vde_codemp", codemp)
                sp4.AgregarParametro("vde_sucid", codsuc)
                sp4.AgregarParametro("vde_emplid", empleado)
                dsGest = sp4.EjecutarProcedimiento()

                Try
                    gestId = dsGest.Tables(0).Rows(0)(0)
                Catch ex As Exception
                    gestId = 0
                End Try
            End If
            Return gestId
        End Function

        Public Function MontoEscrito(ByVal monto As Decimal, ByVal idioma As Integer) As String
            Dim MontoEsc As String
            Dim u As String
            Dim d As String
            Dim c As String
            Dim ArrU() As String
            Dim ArrD() As String
            Dim ArrC() As String
            Dim paso As Integer
            Dim numT As String
            Dim nume As Integer
            Dim vu As Integer
            Dim vn As Integer
            Dim vd As Integer
            Dim vc As Integer
            Dim cientos As String
            Dim decenas As String
            Dim unidades As String


            MontoEsc = ""
            cientos = ""

            '--------Unidades------------

            u = "CERO," + TraeEtiqueta("monesc01", idioma) + "," + TraeEtiqueta("monesc02", idioma) + ","
            u = u + TraeEtiqueta("monesc03", idioma) + "," + TraeEtiqueta("monesc04", idioma) + ","
            u = u + TraeEtiqueta("monesc05", idioma) + "," + TraeEtiqueta("monesc06", idioma) + ","
            u = u + TraeEtiqueta("monesc07", idioma) + "," + TraeEtiqueta("monesc08", idioma) + ","
            u = u + TraeEtiqueta("monesc09", idioma)
            ArrU = Split(u, ",")

            '--------Decimales------------
            d = "CERO," + TraeEtiqueta("monesc10", idioma) + "," + TraeEtiqueta("monesc11", idioma) + ","
            d = d + TraeEtiqueta("monesc12", idioma) + "," + TraeEtiqueta("monesc13", idioma) + ","
            d = d + TraeEtiqueta("monesc14", idioma) + "," + TraeEtiqueta("monesc15", idioma) + ","
            d = d + TraeEtiqueta("monesc16", idioma) + "," + TraeEtiqueta("monesc17", idioma) + ","
            d = d + TraeEtiqueta("monesc44", idioma)
            ArrD = Split(d, ",")

            '--------Centecimas------------
            c = "CERO," + TraeEtiqueta("monesc18", idioma) + "," + TraeEtiqueta("monesc19", idioma) + ","
            c = c + TraeEtiqueta("monesc20", idioma) + "," + TraeEtiqueta("monesc21", idioma) + ","
            c = c + TraeEtiqueta("monesc22", idioma) + "," + TraeEtiqueta("monesc23", idioma) + ","
            c = c + TraeEtiqueta("monesc24", idioma) + "," + TraeEtiqueta("monesc25", idioma) + ","
            c = c + TraeEtiqueta("monesc26", idioma)
            ArrC = Split(c, ",")

            If monto = 0 Then
                MontoEsc = TraeEtiqueta("monesc27", idioma)
            ElseIf monto < 0 Then
                MontoEsc = TraeEtiqueta("monesc28", idioma)
                monto = monto * -1
                Return MontoEsc
            End If
            paso = Decimal.Round(monto, 0)
            numT = Right("000000000000" & paso, 12)
            nume = CDbl(Mid(numT, 1, 3))
            If CDbl(nume) > 0 Then
                If nume = 1 Then
                    MontoEsc = MontoEsc & TraeEtiqueta("monesc29", idioma) & " "
                End If
                If nume > 1 Then
                    vc = Int(nume / 100)
                    vn = Int(nume - vc * 100)
                    vd = Int(vn / 10)
                    vu = Int(vn - vd * 10)
                    If vn = 0 And vd = 0 And vu = 0 Then
                        ArrC(1) = TraeEtiqueta("monesc30", idioma)
                    End If
                    If vc > 0 Then
                        cientos = ArrC(vc)
                        MontoEsc = MontoEsc & " " & cientos
                    End If

                    If vn > 0 And vn <= 9 Then
                        MontoEsc = MontoEsc & " "
                    ElseIf vn = 10 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc31", idioma)
                    ElseIf vn = 11 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc32", idioma)
                    ElseIf vn = 12 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc33", idioma)
                    ElseIf vn = 13 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc34", idioma)
                    ElseIf vn = 14 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc35", idioma)
                    ElseIf vn = 15 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc36", idioma)
                    ElseIf vn > 15 And vn <= 19 Then
                        decenas = ArrD(vd)
                        MontoEsc = MontoEsc & " " & decenas
                    ElseIf vn > 20 And vn <= 99 Then
                        decenas = ArrD(vd)
                        MontoEsc = MontoEsc & " " & decenas
                    ElseIf vn = 20 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc38", idioma)
                    End If

                    If (vn < 10 Or vn > 15) And vu <> 0 Then
                        unidades = ArrU(vu)
                        If vn < 30 Then
                            If vu = 1 Then
                                MontoEsc = MontoEsc & TraeEtiqueta("monesc45", idioma)
                            Else
                                MontoEsc = MontoEsc & unidades
                            End If
                        Else
                            If vu = 1 Then
                                MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc39", idioma) & " " & TraeEtiqueta("monesc45", idioma)
                            Else
                                MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc39", idioma) & " " & unidades
                            End If

                        End If
                    End If
                    MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc40", idioma)
                End If
            End If
            nume = Int(Mid(numT, 4, 3))
            If nume > 0 Then
                If nume = 1 Then
                    MontoEsc = TraeEtiqueta("monesc42", idioma)
                End If
                If nume > 1 Then
                    vc = Int(nume / 100)
                    vn = Int(nume - vc * 100)
                    vd = Int(vn / 10)
                    vu = Int(vn - vd * 10)
                    If vc > 0 Then
                        cientos = ArrC(vc)
                        MontoEsc = MontoEsc & " " & cientos
                    End If
                    If vn > 0 And vn <= 9 Then
                        MontoEsc = MontoEsc & " "
                    ElseIf vn = 10 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc31", idioma)
                    ElseIf vn = 11 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc32", idioma)
                    ElseIf vn = 12 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc33", idioma)
                    ElseIf vn = 13 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc34", idioma)
                    ElseIf vn = 14 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc35", idioma)
                    ElseIf vn = 15 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc36", idioma)
                    ElseIf vn > 15 And vn <= 19 Then
                        decenas = ArrD(vd)
                        MontoEsc = MontoEsc & " " & decenas
                    ElseIf vn > 20 And vn <= 99 Then
                        decenas = ArrD(vd)
                        MontoEsc = MontoEsc & " " & decenas
                    ElseIf vn = 20 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc38", idioma)
                    End If

                    If (vn < 10 Or vn > 15) And vu <> 0 Then
                        'If monto > 99 Then
                        '    unidades = ArrU(vu)
                        'Else
                        '    unidades = TraeEtiqueta("monesc39", idioma)
                        'End If

                        unidades = ArrU(vu)

                        If vn < 30 Then
                            If vu = 1 Then
                                MontoEsc = MontoEsc & TraeEtiqueta("monesc45", idioma)
                            Else
                                MontoEsc = MontoEsc & unidades
                            End If
                        Else
                            If vu = 1 Then
                                MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc39", idioma) & " " & TraeEtiqueta("monesc45", idioma)
                            Else
                                MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc39", idioma) & " " & unidades
                            End If

                            '  MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc39", idioma) & " " & unidades
                        End If
                    End If
                    MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc43", idioma)
                End If
            End If
            nume = Int(Mid(numT, 7, 3))
            If nume > 0 Then
                If nume > 1 Then
                    vc = Int(nume / 100)
                    vn = Int(nume - vc * 100)
                    vd = Int(vn / 10)
                    vu = Int(vn - vd * 10)

                    If vc > 0 Then
                        If monto > 99999 And monto <= 100999 And vc = 100 Then
                            cientos = TraeEtiqueta("monesc30", idioma)
                        Else
                            If monto = 100000 Then
                                cientos = TraeEtiqueta("monesc30", idioma)
                            Else
                                cientos = ArrC(vc)
                            End If

                        End If

                        MontoEsc = MontoEsc & " " & cientos
                    End If
                    If vn > 0 And vn <= 9 Then
                        MontoEsc = MontoEsc & " "
                    ElseIf vn = 10 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc31", idioma)
                    ElseIf vn = 11 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc32", idioma)
                    ElseIf vn = 12 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc33", idioma)
                    ElseIf vn = 13 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc34", idioma)
                    ElseIf vn = 14 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc35", idioma)
                    ElseIf vn = 15 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc36", idioma)
                    ElseIf vn > 15 And vn <= 19 Then
                        decenas = ArrD(vd)
                        MontoEsc = MontoEsc & " " & decenas
                    ElseIf vn > 20 And vn <= 99 Then
                        decenas = ArrD(vd)
                        MontoEsc = MontoEsc & " " & decenas
                    ElseIf vn = 20 Then
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc38", idioma)
                    End If

                    If (vn < 10 Or vn > 15) And vu <> 0 Then
                        unidades = ArrU(vu)
                        If vn < 30 Then
                            If vu = 1 Then
                                MontoEsc = MontoEsc & TraeEtiqueta("monesc45", idioma)
                            Else
                                MontoEsc = MontoEsc & unidades
                            End If
                        Else
                            ' MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc39", idioma) & " " & unidades
                            If vu = 1 Then
                                MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc39", idioma) & " " & TraeEtiqueta("monesc45", idioma)
                            Else
                                'MontoEsc = MontoEsc & unidades
                                MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc39", idioma) & " " & unidades
                            End If
                        End If
                    End If
                End If
                MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc40", idioma)
            End If
            nume = Int(Mid(numT, 10, 3))
            If nume > 0 Then
                vc = Int(nume / 100)
                vn = Int(nume - vc * 100)
                vd = Int(vn / 10)
                vu = Int(vn - vd * 10)
                If vn = 0 And vd = 0 And vu = 0 Then
                    ArrC(1) = TraeEtiqueta("monesc30", idioma)
                End If
                If vc > 0 Then
                    cientos = ArrC(vc)
                    MontoEsc = MontoEsc & " " & cientos
                End If
                If vn > 0 And vn <= 9 Then
                    MontoEsc = MontoEsc & " "
                ElseIf vn = 10 Then
                    MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc31", idioma)
                ElseIf vn = 11 Then
                    MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc32", idioma)
                ElseIf vn = 12 Then
                    MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc33", idioma)
                ElseIf vn = 13 Then
                    MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc34", idioma)
                ElseIf vn = 14 Then
                    MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc35", idioma)
                ElseIf vn = 15 Then
                    MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc36", idioma)
                ElseIf vn > 15 And vn <= 19 Then
                    decenas = ArrD(vd)
                    MontoEsc = MontoEsc & " " & decenas
                ElseIf vn > 20 And vn <= 99 Then
                    decenas = ArrD(vd)
                    MontoEsc = MontoEsc & " " & decenas
                ElseIf vn = 20 Then
                    MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc38", idioma)
                End If

                If (vn < 10 Or vn > 15) And vu <> 0 Then
                    unidades = ArrU(vu)
                    If vn < 30 Then
                        If vu = 1 Then
                            MontoEsc = MontoEsc & TraeEtiqueta("monesc45", idioma)
                        Else
                            MontoEsc = MontoEsc & unidades
                        End If

                    Else
                        MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc39", idioma) & " " & unidades
                    End If
                End If
            ElseIf CDbl(monto) < 0 Then
                MontoEsc = MontoEsc & " " & TraeEtiqueta("monesc27", idioma)
            End If

            If CDbl(monto) = 0 Then
                MontoEsc = ""
            End If

            If Decimal.Round(monto, 1) - monto <= 0 Then
                'MontoEsc = MontoEsc & " CON " & string((NUM - round(NUM,1) )*100,"00") & "/100." 
                MontoEsc = MontoEsc
            End If

            If Mid(MontoEsc, 1, 1) = " " Then
                MontoEsc = Right(MontoEsc, MontoEsc.Length - 1)
            End If

            Return MontoEsc

        End Function

        Public Function SubeArchivo(ByVal archivo As FileUpload, ByVal path As String, ByVal ArchNom As String, ByVal idioma As Integer) As String
            Dim file2 As String = ""
            Dim MensErr As String = ""

            Try
                file2 = path + ArchNom
                If archivo.HasFile <> False Then
                    If File.Exists(file2) Then
                        File.Delete(file2)
                    End If
                    archivo.SaveAs(file2)
                Else
                    MensErr = TraeError("FileBad", idioma)
                    Return MensErr
                End If

            Catch ex As Exception
                MensErr = TraeError("FileBad", idioma) + " | " + ex.Message
                Return MensErr
            End Try

            Return ""

        End Function

        Public Function SubeArchivo(ByVal archivo As FileUpload, ByVal path As String, ByVal ArchNom As String, ByVal idioma As Integer, ByVal extencion As String) As String
            Dim file2 As String = ""
            Dim MensErr As String = ""
            Dim ArrExt() As String = Split(extencion, ",")
            Dim i As Integer
            Dim bad As Boolean = True

            Try
                file2 = path
                Dim dir As New DirectoryInfo(file2)

                If Not dir.Exists Then
                    Directory.CreateDirectory(file2)
                End If

                file2 = path + ArchNom
                If archivo.HasFile <> False Then
                    If File.Exists(file2) Then
                        File.Delete(file2)
                    End If
                    For i = 0 To ArrExt.Length - 1
                        If Right(archivo.FileName, 4).ToLower = ArrExt(i).ToLower Then
                            bad = False
                            i = ArrExt.Length
                        End If
                    Next
                    If bad = False Then
                        archivo.SaveAs(file2)
                    Else
                        MensErr = TraeError("FileBad", idioma)
                        Return MensErr
                    End If
                Else
                    MensErr = TraeError("FileBad", idioma)
                    Return MensErr
                End If

            Catch ex As Exception
                MensErr = TraeError("FileBad", idioma) + " | " + ex.Message
                Return MensErr
            End Try

            Return ""

        End Function

        Public Function CreaDir(ByVal path As String) As Boolean
            Dim file2 As String = ""

            Try
                file2 = path
                Dim dir As New DirectoryInfo(file2)

                If Not dir.Exists Then
                    Directory.CreateDirectory(file2)
                End If
            Catch ex As Exception
                Return False

            End Try
            Return True

        End Function

        Public Function EliminaArchivo(ByVal Archivo As String, ByVal idioma As Integer) As String
            Dim MensErr As String = ""

            Try
                If File.Exists(Archivo) Then
                    File.Delete(Archivo)
                End If


            Catch ex As Exception
                MensErr = TraeError("DelFilBad", idioma) + " | " + ex.Message
                Return MensErr
            End Try
            Return ""
        End Function

        Public Function TipCambio(ByVal codemp As Integer, ByVal codmon As Integer) As Decimal
            Dim hoy As DateTime
            Dim strSql As String
            Dim cambio As Decimal
            Dim ArrEmp(0) As String
            Dim ds As New DataSet
            Dim Hds As New Horus.DatSet


            hoy = FechaServer()

            '---busco el tipo de cmabiop---
            strSql = "select mnv_valor from monedas_valores where mnv_codemp=" + codemp.ToString
            strSql = strSql + " and mnv_codmon=" + codmon.ToString + " and datepart(year,mnv_fecha)=" + hoy.Year.ToString + " and datepart(month, mnv_fecha)=" + hoy.Month.ToString + " and datepart(day, mnv_fecha)=" + hoy.Day.ToString

            ds = Hds.ConsultaBD(strSql)

            Try
                cambio = ds.Tables(0).Rows(0)(0)
            Catch ex As Exception
                cambio = 1
            End Try

            Return cambio

        End Function

        Public Function Moneda_Decimales(ByVal codemp As Integer, ByVal codmon As Integer) As Decimal
            Dim strSql As String
            Dim cambio As Decimal
            Dim ds As New DataSet
            Dim Hds As New Horus.DatSet

            '---busco el tipo de cmabiop---
            strSql = "select mon_decimales from monedas where mon_codemp=" + codemp.ToString + " and mon_codmon=" + codmon.ToString

            ds = Hds.ConsultaBD(strSql)

            Try
                cambio = ds.Tables(0).Rows(0)(0)
            Catch ex As Exception
                cambio = 0
            End Try

            Return cambio
        End Function

        Public Function ValidaMail(ByVal email As String) As Boolean
            Dim ArrMail() As String
            Dim mail
            Dim letra As String
            Dim i As Integer
            Dim fin As Integer
            Dim ArrPun() As String

            '---------Reviso si viene vacio------------
            If Trim(email) = "" Then
                Return False
            End If

            '------------Reviso si viene la Arroba --------------
            ArrMail = Split(email, "@")
            fin = ArrMail.Length - 1
            If fin > 1 Or fin = 0 Then
                Return False
            End If

            For Each mail In ArrMail
                For i = 1 To Len(mail)
                    letra = LCase(Mid(mail, i, 1))
                    If InStr("._-abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ", letra) <= 0 Then
                        Return False
                        Exit For
                    End If
                Next i
                'si la parte actual acaba o empieza en punto la dirección no es válida
                If Left(mail, 1) = "." Or Right(mail, 1) = "." Then
                    Return False
                End If

            Next

            '-------------------Reviso si la parte derecha el punto --------------
            If InStr(ArrMail(1), ".") <= 0 Then
                '   Return False
            End If

            ArrPun = Split(ArrMail(1), ".")

            fin = ArrPun.Length - 1

            If fin > 2 Then
                Return False
            End If



            Return True

        End Function

        Public Function App_Path() As String
            Return System.AppDomain.CurrentDomain.BaseDirectory()
        End Function


        Private Function ArrSeg(ByVal DatUsu As String, ByVal i As Integer) As String
            Dim ArrUsu() As String

            Try

                ArrUsu = Split(DatUsu, ";")

                Return (ArrUsu(i))

            Catch ex As Exception

                Return 0

            End Try

            'Session("Usuario") = Qssec("codemp").ToString + ";"
            'Session("Usuario") = Session("Usuario") + Qssec("codsuc").ToString + ";"
            'Session("Usuario") = Session("Usuario") + Qssec("usrid").ToString + ";"
            'Session("Usuario") = Session("Usuario") + Qssec("nombre").ToString + ";"
            'Session("Usuario") = Session("Usuario") + Qssec("prfid").ToString + ";"
            'Session("Usuario") = Session("Usuario") + Qssec("permisos").ToString

            'If IsDBNull(Qssec("emplid").ToString) Or IsNothing(Qssec("emplid").ToString) Or Trim(Qssec("emplid").ToString) = "" Then
            '    Session("Usuario") = Session("Usuario") + ";0"
            'Else
            '    Session("Usuario") = Session("Usuario") + ";" + Qssec("emplid").ToString
            'End If

            'If IsDBNull(Qssec("pclid").ToString) Or IsNothing(Qssec("pclid").ToString) Or Trim(Qssec("pclid").ToString) = "" Then
            '    Session("Usuario") = Session("Usuario") + ";0"
            'Else
            '    Session("Usuario") = Session("Usuario") + ";" + Qssec("pclid").ToString
            'End If
            'Session("Usuario") = Session("Usuario") + ";" + Qssec("idioma").ToString
            'Session("Permiso") = Qssec("permisos").ToString

            Return (ArrUsu(i))

        End Function

        Public Function Carga_Excel(ByVal nombre As String) As DataSet
            Dim ds As New DataSet
            Dim StrSql As String = ""


            Try

                ' StrSql = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\Excel 12.0 Xml;HDR=No;IMEX=1\;" + Configuracion_Str(14) + App_Path() + Configuracion_Str(15) + "\" + nombre

                StrSql = Configuracion_Str(14) + App_Path() + Configuracion_Str(15) + "\" + nombre

                '----------Creo la Conexion------------------
                Dim ConExc As New OleDb.OleDbConnection(StrSql)
                Dim AdapExc As New OleDbDataAdapter("SELECT * FROM [Datos$]", ConExc)

                ConExc.Open()
                AdapExc.Fill(ds)

                ConExc.Close()

                Return ds

            Catch ex As Exception
                Return ds
            End Try


            Return ds
        End Function

        Public Function Carga_Excel(ByVal nombre As String, ByVal Hoja As String) As DataSet
            Dim ds As New DataSet
            Dim StrSql As String = ""


            Try

                ' StrSql = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\Excel 12.0 Xml;HDR=No;IMEX=1\;" + Configuracion_Str(14) + App_Path() + Configuracion_Str(15) + "\" + nombre

                StrSql = Configuracion_Str(14) + App_Path() + Configuracion_Str(15) + "\" + nombre

                '----------Creo la Conexion------------------
                Dim ConExc As New OleDb.OleDbConnection(StrSql)
                Dim AdapExc As New OleDbDataAdapter("SELECT * FROM [" + Hoja + "$]", ConExc)

                ConExc.Open()
                AdapExc.Fill(ds)

                ConExc.Close()

                Return ds

            Catch ex As Exception
                Return ds
            End Try


            Return ds
        End Function

        Public Function Carga_Excel_Err(ByVal nombre As String) As String
            Dim ds As New DataSet
            Dim StrSql As String = ""


            Try

                ' StrSql = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\Excel 12.0 Xml;HDR=No;IMEX=1\;" + Configuracion_Str(14) + App_Path() + Configuracion_Str(15) + "\" + nombre

                StrSql = Configuracion_Str(14) + App_Path() + Configuracion_Str(15) + "\" + nombre

                '----------Creo la Conexion------------------
                Dim ConExc As New OleDb.OleDbConnection(StrSql)
                Dim AdapExc As New OleDbDataAdapter("SELECT * FROM [Datos$]", ConExc)

                ConExc.Open()
                AdapExc.Fill(ds)

                ConExc.Close()

                Return ""

            Catch ex As Exception
                Return ex.Message

            End Try


            Return ""
        End Function

        Public Function Carga_Archivo_Plano(ByVal nombre As String, byval codigoCarga as String) As DataSet
            Dim ds As New DataSet
            Dim dtb As New DataTable

            Dim StrSql As String = ""
            Dim sr As System.IO.StreamReader
            Dim arrayFila(47) As Object
            Dim numero As String = ""
            Dim cuotaVencida As String
            Dim cuotasTotales As String

            Dim capitalInsoluto As Decimal
            Dim montoCapital As Decimal
            Dim montoInteres As Decimal
            Dim valorCuota As Decimal
            Dim insoluto As Decimal
            Dim motoDisponible As Decimal
            Dim excedente As Decimal

            Dim moneda As String
            Dim sucursal As Integer
            Dim nombreSucursal As String = ""

            Dim fila As Integer = 1
            Dim columna As Integer = 1
            Dim mensajeError As String = ""


            Try
                Dim fic As String = ""
                fic = App_Path() + Configuracion_Str(15) + "\" + nombre
                Dim strLinea As String = ""
                dtb = Tabla_Oriencoop()


                sr = File.OpenText(fic)
                While sr.EndOfStream = False
                    strLinea = sr.ReadLine()
                    mensajeError = ""
                    columna = 49
                    cuotaVencida = strLinea.Substring(49, 8).Substring(4, 4).Substring(1, 3)
                    columna = 57
                    cuotasTotales = strLinea.Substring(57, 4).Substring(1, 3)
                    columna = 0
                    numero = strLinea.Substring(0, 13).Substring(8, 5) & "/" & cuotaVencida & "-" & cuotasTotales
                    mensajeError = "Capital Insoluto"
                    columna = 30
                    capitalInsoluto = Decimal.Parse(strLinea.Substring(30, 15) & "," & strLinea.Substring(45, 4))
                    columna = 69
                    mensajeError = "Monto Capital"
                    montoCapital = Decimal.Parse(strLinea.Substring(69, 15) & "," & strLinea.Substring(84, 4))
                    columna = 88
                    mensajeError = "Monto Interés"
                    montoInteres = Decimal.Parse(strLinea.Substring(88, 15) & "," & strLinea.Substring(103, 4))
                    columna = 107
                    mensajeError = "Valor Cuota"
                    valorCuota = Decimal.Parse(strLinea.Substring(107, 15) & "," & strLinea.Substring(122, 4))
                    columna = 126
                    mensajeError = "Insoluto"
                    insoluto = Decimal.Parse(strLinea.Substring(126, 15) & "," & strLinea.Substring(141, 4))
                    columna = 454
                    mensajeError = "Monto Disponible"
                    motoDisponible = Decimal.Parse(strLinea.Substring(454, 15) & "," & strLinea.Substring(469, 4))
                    columna = 564
                    mensajeError = "Excedente"
                    excedente = Decimal.Parse(strLinea.Substring(564, 15) & "," & strLinea.Substring(579, 4))
                    columna = 21
                    If strLinea.Substring(21, 1) = "1" Then
                        moneda = "PESOS"
                    Else
                        moneda = "UF"
                    End If

                    sucursal = Integer.Parse(strLinea.Substring(13, 4))
                    nombreSucursal = SucursalOriencoop(sucursal)
                    columna = 0
                    mensajeError = "Rut Cliente"
                    arrayFila(0) = "700109208"
                    columna = 145
                    arrayFila(1) = strLinea.Substring(145, 9)
                    columna = 154
                    arrayFila(2) = strLinea.Substring(154, 1)
                    columna = 155
                    arrayFila(3) = strLinea.Substring(155, 30)
                    columna = 185
                    arrayFila(4) = strLinea.Substring(185, 20)
                    columna = 205
                    arrayFila(5) = strLinea.Substring(205, 20)
                    columna = 285
                    arrayFila(6) = strLinea.Substring(285, 30)
                    columna = columna + 1
                    arrayFila(7) = ""
                    columna = 225
                    arrayFila(8) = strLinea.Substring(225, 60)
                    columna = 335
                    arrayFila(9) = strLinea.Substring(335, 60)
                    columna = 315
                    arrayFila(10) = strLinea.Substring(315, 20)
                    columna = 425
                    arrayFila(11) = strLinea.Substring(425, 20)
                    columna = 603
                    arrayFila(12) = strLinea.Substring(603, 20)
                    columna = 623
                    arrayFila(13) = strLinea.Substring(623, 20)
                    columna = 643
                    arrayFila(14) = strLinea.Substring(643, 20)
                    columna = 5831
                    arrayFila(15) = strLinea.Substring(583, 20)
                    columna = columna + 1
                    arrayFila(16) = ""
                    columna = columna + 1
                    arrayFila(17) = ""
                    columna = columna + 1
                    arrayFila(18) = ""
                    columna = columna + 1
                    arrayFila(19) = ""
                    columna = columna + 1
                    arrayFila(20) = ""
                    columna = columna + 1
                    arrayFila(21) = ""
                    columna = columna + 1
                    arrayFila(22) = ""
                    columna = columna + 1
                    arrayFila(23) = ""
                    columna = 0
                    arrayFila(24) = "CREDITO"
                    columna = columna + 1
                    arrayFila(25) = numero
                    columna = 22
                    arrayFila(26) = DateTime.ParseExact(strLinea.Substring(22, 8), "ddMMyyyy", CultureInfo.InvariantCulture) 'strLinea.Substring(22, 8)
                    columna = 61
                    arrayFila(27) = DateTime.ParseExact(strLinea.Substring(61, 8), "ddMMyyyy", CultureInfo.InvariantCulture) 'strLinea.Substring(61, 8)
                    columna = columna + 1
                    arrayFila(28) = "NO PAGO"
                    columna = 0
                    mensajeError = "Codigo Carga"
                    arrayFila(29) = codigoCarga
                    columna = 0
                    mensajeError = "Moneda"
                    arrayFila(30) = moneda
                    columna = columna + 1
                    arrayFila(31) = "1,00"
                    columna = 107
                    mensajeError = "Valor Cuota"
                    arrayFila(32) = valorCuota
                    columna = 30
                    mensajeError = "Capital Insoluto"
                    arrayFila(33) = capitalInsoluto
                    columna = 107
                    mensajeError = "Valor Cuota"
                    arrayFila(34) = valorCuota
                    columna = columna + 1
                    arrayFila(35) = "0,00"
                    columna = columna + 1
                    arrayFila(36) = "0,00"
                    columna = columna + 1
                    arrayFila(37) = ""
                    columna = columna + 1
                    arrayFila(38) = ""
                    columna = columna + 1
                    arrayFila(39) = ""
                    columna = 0
                    arrayFila(40) = strLinea.Substring(0, 13)
                    columna = columna + 1
                    arrayFila(41) = nombreSucursal
                    columna = columna + 1
                    arrayFila(42) = ""
                    columna = columna + 1
                    arrayFila(43) = ""
                    columna = columna + 1
                    arrayFila(44) = "N"
                    columna = columna + 1
                    arrayFila(45) = "S"
                    columna = 663
                    mensajeError = "Producto o Campaña"
                    arrayFila(46) = "PRODUCTO: " & ProductoOriencoop(Integer.Parse(strLinea.Substring(17, 4))) & " - CAMPAÑA: " & strLinea.Substring(663, strLinea.Length - 663)

                    If strLinea.Substring(445, 1) = "T" Then
                        dtb.Rows.Add(arrayFila)
                    End If
                    fila = fila + 1
                End While

                sr.Close()
                ds.Tables.Add(dtb)
                Return ds
            Catch ex As Exception
                sr.Close()
                mensajeError = "El archivo cargado tiene problemas de formato, error en la línea: " + fila.ToString() + ", columna: " + columna.ToString() + ". " + mensajeError
                Dim t As Int16 = 0
                Throw New Exception(mensajeError)
                'Return ds
            End Try



            Return ds
        End Function

        Public Function Carga_Archivo_Plano_Pago(ByVal nombre As String) As DataSet
            Dim ds As New DataSet
            Dim dtb As New DataTable

            Dim StrSql As String = ""
            Dim sr As System.IO.StreamReader
            Dim arrayFila(8) As Object

            Dim rut As Integer
            Dim dv As String
            Dim numero As String = ""
            Dim montoArchivo As Integer
            Dim montoInteres As Decimal = 0
            Dim montoHonorario As Decimal = 0
            Dim cuotaVencida As String

            Dim fila As Integer = 1
            Dim columna As Integer = 1
            Dim mensajeError As String = ""

            Try
                Dim fic As String = ""
                fic = App_Path() + Configuracion_Str(15) + "\" + nombre
                Dim strLinea As String = ""
                dtb = Tabla_Oriencoop_Pago()


                sr = File.OpenText(fic)
                While sr.EndOfStream = False
                    strLinea = sr.ReadLine()
                    mensajeError = ""
                    columna = 1
                    mensajeError = "Rut deudor"
                    rut = Integer.Parse(strLinea.Substring(0, 9))
                    columna = 2
                    mensajeError = "Dígito verificador deudor"
                    dv = strLinea.Substring(9, 1)
                    columna = 6
                    mensajeError = "Cuota crédito"
                    cuotaVencida = strLinea.Substring(35, 8).Substring(5, 3)
                    columna = 3
                    mensajeError = "Número crédito"
                    numero = strLinea.Substring(10, 13).Substring(8, 5) & "/" & cuotaVencida
                    mensajeError = "Monto capital"
                    columna = 7
                    montoArchivo = Integer.Parse(strLinea.Substring(43, 15))

                    columna = 1
                    mensajeError = "Rut Cliente"
                    arrayFila(0) = "700109208"
                    columna = columna + 1
                    arrayFila(1) = rut
                    columna = columna + 1
                    arrayFila(2) = dv
                    columna = columna + 1
                    arrayFila(3) = numero
                    columna = columna + 1
                    arrayFila(4) = montoArchivo
                    columna = columna + 1
                    arrayFila(5) = 0
                    columna = columna + 1
                    arrayFila(6) = 0
                    columna = columna + 1
                    arrayFila(7) = ""
                    columna = columna + 1
                    arrayFila(8) = 1

                    fila = fila + 1
                    dtb.Rows.Add(arrayFila)
                End While

                sr.Close()
                ds.Tables.Add(dtb)
                Return ds
            Catch ex As Exception
                sr.Close()
                mensajeError = "El archivo cargado tiene problemas de formato, error en la línea: " + fila.ToString() + ", columna: " + columna.ToString() + ". " + mensajeError
                Throw New Exception(mensajeError)
                'Return ds
            End Try



            Return ds
        End Function
        Public Function SucursalOriencoop(ByVal sucursal As Integer) As String
            Select Case sucursal
                Case 1
                    Return "Talca - 14 oriente"
                Case 2
                    Return "Curepto"
                Case 4
                    Return "Curico - yungay"
                Case 5
                    Return "Linares - maipu"
                Case 6
                    Return "Parral"
                Case 7
                    Return "Constitución"
                Case 8
                    Return "Cauquenes"
                Case 9
                    Return "San Clemente"
                Case 10
                    Return "Talca - 1 sur"
                Case 13
                    Return "Talca - 1 Norte"
                Case 18
                    Return "Talca plaza armas"
                Case 20
                    Return "San Fernando"
                Case 30
                    Return "Chillan"
                Case 31
                    Return "Concepción"
                Case 32
                    Return "Los Angeles"
                Case 40
                    Return "Santiago - Estado"
                Case 50
                    Return "Valparaíso"
                Case 60
                    Return "Puerto Montt"
                Case 70
                    Return "Temuco"
                Case 92
                    Return "Talca - San Miguel"
                Case 93
                    Return "Talca-la florida"
                Case 98
                    Return "Canal Venta Terreno"
            End Select
            Return ""
        End Function

        Public Function ProductoOriencoop(ByVal producto As Integer) As String
            Select Case producto
                Case 106
                    Return "Renegociacion Microempresa cuotas mensuales"
                Case 124
                    Return "Renegociacion Micro Corfo Reprogramacion $"
                Case 125
                    Return "Renegociacion  Micro Corfo Reprogramacion UF"
                Case 131
                    Return "Re - Microempresa CONGARANTIA SGR en $"
                Case 315
                    Return "Microempresa Reconstrucción Fianza SGR en $"
                Case 317
                    Return "Microempresa Corfo Reconstrucción en $"
                Case 318
                    Return "Microempresa Corfo Reconstruccion en UF"
                Case 319
                    Return "Microempresa Fianza SGR Congarantia $"
                Case 323
                    Return "Microemp. Fianza SGR Congarantía UF"
                Case 325
                    Return "Microempresa Fogape anual hasta UF3000"
                Case 330
                    Return "Microempresa Corfo Reprogramacion"
                Case 332
                    Return "Microempresa Corfo Inv. y Capital de Trabajo"
                Case 333
                    Return "Microempresa Corfo Inv. y Capital de Trabajo UF"
                Case 337
                    Return "Microempresa Corfo Inv. y Capital de Trabajo L Plaz"
                Case 340
                    Return "Microempresa  SGR Confianza individual $"
                Case 341
                    Return "Microempresa Fianza SGR Congarantia Externa"
                Case 351
                    Return "Microempresa  SGR Confianza individual UF"
                Case 353
                    Return "Microempresa SGR  AVALPYME individual $"
                Case 9106
                    Return "Renegociacion Pyme cuotas mensuales"
                Case 9124
                    Return "Renegociacion Pyme Corfo Reprogramacion"
                Case 9125
                    Return "Renegociacion Pyme Corfo Reprogramacion UF"
                Case 9132
                    Return "Re -Pyme CONGARANTIA SGR en UF"
                Case 9203
                    Return "Pyme Largo Plazo reajustable UF"
                Case 9318
                    Return "Pyme Corfo Reconstruccion en UF"
                Case 9322
                    Return "Pyme Reconstrucción Fianza SGR UF"
                Case 9324
                    Return "Pyme Fogape mensual hasta UF3000"
                Case 9325
                    Return "Pyme Fogape anual hasta UF3000"
                Case 9329
                    Return "Pyme Fogape Largo Plazo en UF"
                Case 9330
                    Return "Pyme Corfo Reprogramacion"
                Case 9331
                    Return "Pyme Corfo Reprogramacion UF"
                Case 9332
                    Return "Pyme Corfo Inv. y Capital de Trabajo"
                Case 9333
                    Return "Pyme Corfo Inv. y Capital de Trabajo UF"
                Case 9337
                    Return "Pyme Corfo Inv. y Capital de Trabajo L Plazo"
                Case 9338
                    Return "Pyme Corfo Inv.yCapital de Trabajo UF LP"
                Case 9353
                    Return "Pyme SGR AVAL PYME Individual $"
            End Select
            Return ""
        End Function


        Public Function Fill_Archivo_Plano(ByVal nombre As String) As DataSet

        End Function

        Public Function Tabla_Oriencoop() As DataTable

            ' Generate a new DataTable.
            ' ... Add columns.
            Dim table As DataTable = New DataTable
            table.Columns.Add("RUT CLIENTE", GetType(String))
            table.Columns.Add("RUTNUM", GetType(Integer))
            table.Columns.Add("RUTDV", GetType(String))
            table.Columns.Add("NOMBRE", GetType(String))
            table.Columns.Add("APEPAT", GetType(String))
            table.Columns.Add("APEMAT", GetType(String))
            table.Columns.Add("COMUNA", GetType(String))
            table.Columns.Add("NULO", GetType(String))
            table.Columns.Add("DIRECCION1", GetType(String))
            table.Columns.Add("DIRECCION2", GetType(String))

            table.Columns.Add("TELEFONO1", GetType(String))
            table.Columns.Add("TELEFONO2", GetType(String))
            table.Columns.Add("TELEFONO3", GetType(String))
            table.Columns.Add("TELEFONO4", GetType(String))
            table.Columns.Add("TELEFONO5", GetType(String))
            table.Columns.Add("CELULAR1", GetType(String))
            table.Columns.Add("CELULAR2", GetType(String))
            table.Columns.Add("CELULAR3", GetType(String))
            table.Columns.Add("CELULAR4", GetType(String))
            table.Columns.Add("CELULAR5", GetType(String))

            table.Columns.Add("FAX", GetType(String))
            table.Columns.Add("MAIL1", GetType(String))
            table.Columns.Add("MAIL2", GetType(String))
            table.Columns.Add("MAIL3", GetType(String))
            table.Columns.Add("TIPO DOCUMENTO", GetType(String))
            table.Columns.Add("NUMERO", GetType(String))
            table.Columns.Add("FECDOC", GetType(DateTime))
            table.Columns.Add("FECVENC", GetType(DateTime))
            table.Columns.Add("MOTIVO COBRANZA", GetType(String))
            table.Columns.Add("CODIGO CARGA", GetType(String))

            table.Columns.Add("MONEDA", GetType(String))
            table.Columns.Add("TIPO CAMBIO", GetType(Decimal))
            table.Columns.Add("MONTO ASIGNADO", GetType(Decimal))
            table.Columns.Add("CAPITAL", GetType(Decimal))
            table.Columns.Add("SALDO", GetType(Decimal))
            table.Columns.Add("GASTOJUD", GetType(Decimal))
            table.Columns.Add("GASTOPRE", GetType(Decimal))
            table.Columns.Add("BANCO", GetType(String))
            table.Columns.Add("RUT GIRADOR", GetType(String))
            table.Columns.Add("NOMBRE GIRADOR", GetType(String))

            table.Columns.Add("NEGOCIO", GetType(String))
            table.Columns.Add("NUMERO AGRUPAR", GetType(String))
            table.Columns.Add("RUT ASEGURADO", GetType(String))
            table.Columns.Add("NOMBRE ASEGURADO", GetType(String))
            table.Columns.Add("DOCORI", GetType(String))
            table.Columns.Add("DOCANT", GetType(String))
            table.Columns.Add("COMENTARIO", GetType(String))
            table.Columns.Add("relleno", GetType(String))

            Return table
        End Function

        Public Function Tabla_Oriencoop_Pago() As DataTable

            ' Generate a new DataTable.
            ' ... Add columns.
            Dim table As DataTable = New DataTable
            table.Columns.Add("RUTCLIENTE", GetType(String))
            table.Columns.Add("RUT", GetType(Integer))
            table.Columns.Add("DV", GetType(String))
            table.Columns.Add("NUMCPBT", GetType(String))
            table.Columns.Add("MONTO", GetType(Decimal))
            table.Columns.Add("INTERES", GetType(Decimal))
            table.Columns.Add("HONORARIO", GetType(Decimal))
            table.Columns.Add("MONEDA", GetType(String))
            table.Columns.Add("TIPCAMBIO", GetType(Integer))

            Return table
        End Function

        Private Function CreateTestTable(ByVal dsMen As DataSet) As DataTable
            Dim table As New DataTable()
            Dim Url As String
            Dim i As Integer
            Dim ArrUsu() As String


            'Try
            '    table.Columns.Add("ID")
            '    table.Columns.Add("ParentID")
            '    table.Columns.Add("Text")
            '    table.Columns.Add("URL")
            '    table.Columns.Add("Tooltip")

            '    ArrUsu = Split(Session("Usuario"), ";")

            '    For i = 0 To dsMen.Tables(0).Rows.Count - 1
            '        QSsec = New TSHAK.Components.SecureQueryString(New Byte() {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 8})


            '        QSsec("codemp") = ArrUsu(0)
            '        QSsec("codsuc") = ArrUsu(1)
            '        QSsec("usrid") = ArrUsu(2)
            '        QSsec("nombre") = ArrUsu(3)
            '        QSsec("prfid") = ArrUsu(4)
            '        QSsec("permisos") = ArrUsu(5)
            '        If IsDBNull(ArrUsu(6)) Then
            '            QSsec("emplid") = "0"
            '        Else
            '            QSsec("emplid") = ArrUsu(6)
            '        End If
            '        If IsDBNull(ArrUsu(7)) Then
            '            QSsec("pclid") = "0"
            '        Else
            '            QSsec("pclid") = ArrUsu(7)
            '        End If
            '        QSsec("idioma") = ArrUsu(8)

            '        If IsDBNull(dsMen.Tables(0).Rows(i)(3)) Or IsNothing(dsMen.Tables(0).Rows(i)(3)) Or Trim(dsMen.Tables(0).Rows(i)(3)) = "" Then
            '            Url = ""
            '        Else
            '            Url = dsMen.Tables(0).Rows(i)(3) + "&datos=" + HttpUtility.UrlEncode(QSsec.ToString())
            '        End If

            '        If IsDBNull(dsMen.Tables(0).Rows(i)(1)) Then
            '            table.Rows.Add(New String() {dsMen.Tables(0).Rows(i)(0), Nothing, dsMen.Tables(0).Rows(i)(2), Url, dsMen.Tables(0).Rows(i)(2)})
            '        Else
            '            table.Rows.Add(New String() {dsMen.Tables(0).Rows(i)(0), dsMen.Tables(0).Rows(i)(1), dsMen.Tables(0).Rows(i)(2), Url, dsMen.Tables(0).Rows(i)(2)})
            '        End If

            '    Next

            'Catch ex As Exception

            'End Try

            '  SELECT treeview.trv_trvid,   
            ' treeview.trv_padid,   
            ' treeview_idiomas.tvi_idiomas, 
            'CASE   trv_ventana when '' THEN '' ELSE  '~/' + men_directorio + '/' + trv_ventana END as Ventana,  
            '        TreeView.trv_imagen()



            'table.Rows.Add(New String() {"2", Nothing, "root 2", "", "root 1 tooltip"})
            'table.Rows.Add(New String() {"3", "1", "child 1.1", "child11.aspx", "child 1.1 tooltip"})
            'table.Rows.Add(New String() {"4", "1", "child 1.2", "child12.aspx", "child 1.2 tooltip"})
            'table.Rows.Add(New String() {"5", "1", "child 1.3", "child13.aspx", "child 1.3 tooltip"})
            'table.Rows.Add(New String() {"6", "5", "child 1.3.1", "child131.aspx", "child 1.3.1 tooltip"})
            'table.Rows.Add(New String() {"7", "5", "child 1.3.2", "child132.aspx", "child 1.3.2 tooltip"})
            'table.Rows.Add(New String() {"8", "5", "child 1.3.3", "child133.aspx", "child 1.3.3 tooltip"})

            Return table
        End Function

        Public Function Deudor_Telefono(ByVal codemp As Integer, ByVal ctcid As Integer, ByVal tipo As String) As String
            Dim strSql As String
            Dim ds As New DataSet
            Dim Hds As New Horus.DatSet
            Dim tel As String = ""
            Dim i As Integer

            Try
                '------------------Cargo los Telefonos------------------
                strSql = " SELECT view_deudores_telefonos_contactos.dct_numero, ciu_codarea  "
                strSql = strSql + " FROM view_deudores_telefonos_contactos"
                strSql = strSql + " WHERE  view_deudores_telefonos_contactos.dct_codemp = " + codemp.ToString
                strSql = strSql + " and dct_estado='A'"
                strSql = strSql + " and dct_tipo='" + tipo + "'"
                strSql = strSql + " and tci_ticid=0"
                strSql = strSql + " and ddc_ctcid=" + ctcid.ToString

                ds = Hds.ConsultaBD(strSql)

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    If IsDBNull(ds.Tables(0).Rows(i)(1)) = False Then
                        If Trim(ds.Tables(0).Rows(i)(1)) <> "" Then
                            tel = tel + "(" + ds.Tables(0).Rows(i)(1).ToString + ")" + ds.Tables(0).Rows(i)(0).ToString + "-"
                        Else
                            tel = tel + ds.Tables(0).Rows(i)(0).ToString + "-"
                        End If
                    Else
                        tel = tel + ds.Tables(0).Rows(i)(0).ToString + "-"
                    End If
                Next

                tel = Left(tel, Len(tel) - 1)
            Catch ex As Exception
                tel = ""
            End Try

            Return tel

        End Function

        Public Function Deudor_Mail(ByVal codemp As Integer, ByVal ctcid As Integer) As String
            Dim strSql As String
            Dim ds As New DataSet
            Dim Hds As New Horus.DatSet
            Dim tel As String = ""
            Dim i As Integer

            Try
                '------------------Cargo los Mail------------------
                strSql = "  SELECT dcm_mail  "
                strSql = strSql + " FROM view_deudores_mail_contactos"
                strSql = strSql + " WHERE  view_deudores_mail_contactos.ddc_codemp = " + codemp.ToString
                strSql = strSql + " and view_deudores_mail_contactos.ddc_ctcid =" + ctcid.ToString + " and ddc_estado ='A'"
                strSql = strSql + " and tci_ticid=0"
                strSql = strSql + " union "
                strSql = strSql + " SELECT dcm_mail  "
                strSql = strSql + " FROM view_deudores_mail_contactos"
                strSql = strSql + " WHERE  view_deudores_mail_contactos.ddc_codemp = " + codemp.ToString
                strSql = strSql + " and view_deudores_mail_contactos.ddc_ctcid =" + ctcid.ToString + " and ddc_estado ='A'"
                strSql = strSql + " and tci_ticid >0 and dcm_masivo = 'S'"




                ds = Hds.ConsultaBD(strSql)

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    tel = tel + ds.Tables(0).Rows(i)(0).ToString + "-"
                Next

                tel = Left(tel, Len(tel) - 1)
            Catch ex As Exception
                tel = ""
            End Try

            Return tel

        End Function

        Public Function CreaCodigo(ByVal codemp As Integer, ByVal cat As Integer, ByVal ins As Integer) As String
            Dim strSql As String
            Dim ds As New DataSet
            Dim cod As String = ""
            Dim sup As String = ""
            Dim cate As String = ""
            Dim larCod As Integer = Configuracion_Emp_Num(codemp, 42)
            Dim Hds As New Horus.DatSet
            Dim i As Integer
            Dim codfin As String = ""


            Try
                strSql = "Select  supcat_categorias.scc_spcid"
                strSql = strSql + " FROM supcat_categorias"
                strSql = strSql + " WHERE  supcat_categorias.scc_codemp = " + codemp.ToString
                strSql = strSql + " and supcat_categorias.scc_catid = " + cat.ToString

                ds = Hds.ConsultaBD(strSql)

                sup = Left(ds.Tables(0).Rows(0)(0).ToString + "000", 3)
                cate = Left(cat.ToString + "000", 3)

                For i = 1 To larCod
                    cod = cod + "0"
                Next
                cod = cod + ins.ToString

                codfin = sup + cate + Right(cod, Len(cod) - 6)



            Catch ex As Exception
                Return codfin
            End Try

            Return codfin


        End Function

        Public Function Trae_Datos_CabCpbt(ByVal codemp As Integer, ByVal codsuc As Integer, ByVal estado As String, ByVal idioma As Integer, ByVal perfil As Integer, ByVal cartcli As String, ByVal tipo As String) As DataSet
            Dim strsql As String
            Dim hds As New Horus.DatSet
            Dim ds As New DataSet

            Try
                strsql = "SELECT view_cabecera_comprobantes.cbc_tpcid,   "
                strsql = strsql + "        view_cabecera_comprobantes.cbc_numero,   "
                strsql = strsql + "         view_cabecera_comprobantes.pcl_rut,   "
                strsql = strsql + " view_cabecera_comprobantes.pcl_nomfant,   "
                strsql = strsql + " view_cabecera_comprobantes.tci_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_numprovcli,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_feccpbt,   "
                strsql = strsql + " view_cabecera_comprobantes.mon_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_final,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_contable,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_cartcli,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_findeuda, cbc_tipcambio, cbc_feccont"
                strsql = strsql + " FROM view_cabecera_comprobantes,   "
                strsql = strsql + " clasificacion_cpbtdoc,   "
                strsql = strsql + " tipos_cpbtdoc"
                strsql = strsql + " WHERE  tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp  and  "
                strsql = strsql + " tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = tipos_cpbtdoc.tpc_codemp  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_tpcid = tipos_cpbtdoc.tpc_tpcid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = " + codemp.ToString
                strsql = strsql + " and view_cabecera_comprobantes.cbc_sucid = " + codsuc.ToString
                strsql = strsql + " and view_cabecera_comprobantes.cbt_estado = '" + estado + "'"
                strsql = strsql + " and view_cabecera_comprobantes.idi_idid = " + idioma.ToString
                strsql = strsql + " and clb_cartcli = '" + cartcli + "'"
                strsql = strsql + " and clb_tipcpbtdoc = '" + tipo + "'"

                strsql = strsql + " and cbc_tpcid in ( "
                strsql = strsql + " Select perfiles_comprobantes.pfc_tpcid"
                strsql = strsql + " FROM perfiles_comprobantes"
                strsql = strsql + " WHERE  perfiles_comprobantes.pfc_codemp =" + codemp.ToString
                strsql = strsql + " and  perfiles_comprobantes.pfc_prfid = " + perfil.ToString + ")"
                'strsql = strsql + " order by tci_nombre, cbc_numero "

                strsql = strsql + " union "
                strsql = strsql + " SELECT view_cabecera_comprobantes.cbc_tpcid,   "
                strsql = strsql + "        view_cabecera_comprobantes.cbc_numero,   "
                strsql = strsql + "         view_cabecera_comprobantes.pcl_rut,   "
                strsql = strsql + " view_cabecera_comprobantes.pcl_nomfant,   "
                strsql = strsql + " view_cabecera_comprobantes.tci_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_numprovcli,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_feccpbt,   "
                strsql = strsql + " view_cabecera_comprobantes.mon_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_final,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_contable,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_cartcli,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_findeuda, cbc_tipcambio, cbc_feccont"
                strsql = strsql + " FROM view_cabecera_comprobantes,   "
                strsql = strsql + " clasificacion_cpbtdoc,   "
                strsql = strsql + " tipos_cpbtdoc"
                strsql = strsql + " WHERE  tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp  and  "
                strsql = strsql + " tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = tipos_cpbtdoc.tpc_codemp  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_tpcid = tipos_cpbtdoc.tpc_tpcid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = " + codemp.ToString
                strsql = strsql + " and view_cabecera_comprobantes.cbc_sucid = " + codsuc.ToString
                strsql = strsql + " and view_cabecera_comprobantes.cbt_estado = '" + estado + "'"
                strsql = strsql + " and view_cabecera_comprobantes.idi_idid = " + idioma.ToString
                strsql = strsql + " and clb_cartcli <> '" + cartcli + "'"
                strsql = strsql + " and clb_tipcpbtdoc = '" + tipo + "'"

                strsql = strsql + " and cbc_tpcid in ( "
                strsql = strsql + " Select perfiles_comprobantes.pfc_tpcid"
                strsql = strsql + " FROM perfiles_comprobantes"
                strsql = strsql + " WHERE  perfiles_comprobantes.pfc_codemp =" + codemp.ToString
                strsql = strsql + " and  perfiles_comprobantes.pfc_prfid = " + perfil.ToString + ")"
                strsql = strsql + " order by tci_nombre, cbc_numero "

                ds = hds.ConsultaBD(strsql)


            Catch ex As Exception

            End Try

            Return ds


        End Function

        Public Function Trae_Datos_CabCpbt(ByVal codemp As Integer, ByVal codsuc As Integer, ByVal estado As String, ByVal idioma As Integer, ByVal perfil As Integer, ByVal cartcli As String, ByVal tipo As String, ByVal cobranza As Boolean) As DataSet
            Dim strsql As String
            Dim hds As New Horus.DatSet
            Dim ds As New DataSet
            Dim func As New Horus.Funciones

            Try
                strsql = "SELECT view_cabecera_comprobantes.cbc_tpcid,   "
                strsql = strsql + "        view_cabecera_comprobantes.cbc_numero,   "
                strsql = strsql + "         view_cabecera_comprobantes.pcl_rut,   "
                strsql = strsql + " view_cabecera_comprobantes.pcl_nomfant,   "
                strsql = strsql + " view_cabecera_comprobantes.tci_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_numprovcli,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_feccpbt,   "
                strsql = strsql + " view_cabecera_comprobantes.mon_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_final,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_contable,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_cartcli,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_findeuda, cbc_tipcambio, cbc_feccont"
                strsql = strsql + " FROM view_cabecera_comprobantes,   "
                strsql = strsql + " clasificacion_cpbtdoc,   "
                strsql = strsql + " tipos_cpbtdoc"
                strsql = strsql + " WHERE  tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp  and  "
                strsql = strsql + " tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = tipos_cpbtdoc.tpc_codemp  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_tpcid = tipos_cpbtdoc.tpc_tpcid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = " + codemp.ToString
                strsql = strsql + " and view_cabecera_comprobantes.cbc_sucid = " + codsuc.ToString
                strsql = strsql + " and view_cabecera_comprobantes.cbt_estado in ('" + estado + "','B','A')"
                strsql = strsql + " and view_cabecera_comprobantes.idi_idid = " + idioma.ToString
                strsql = strsql + " and clb_cartcli = '" + cartcli + "'"
                strsql = strsql + " and clb_tipcpbtdoc = '" + tipo + "'"
                strsql = strsql + " and clb_contable = 'S'"

                strsql = strsql + " and cbc_tpcid in ( "
                strsql = strsql + " Select perfiles_comprobantes.pfc_tpcid"
                strsql = strsql + " FROM perfiles_comprobantes"
                strsql = strsql + " WHERE  perfiles_comprobantes.pfc_codemp =" + codemp.ToString
                strsql = strsql + " and  perfiles_comprobantes.pfc_prfid = " + perfil.ToString + ")"
                'strsql = strsql + " order by tci_nombre, cbc_numero "

                If cobranza = True Then
                    strsql = strsql + " and cbc_numprovcli not in (select ccb_numero from cartera_clientes_cpbt_doc "
                    strsql = strsql + " where ccb_codemp =" + codemp.ToString
                    strsql = strsql + " and ccb_pclid =" + CInt(func.Configuracion_Emp_Num(codemp, 104)).ToString + ")"
                End If

                strsql = strsql + " union "
                strsql = strsql + " SELECT view_cabecera_comprobantes.cbc_tpcid,   "
                strsql = strsql + "        view_cabecera_comprobantes.cbc_numero,   "
                strsql = strsql + "         view_cabecera_comprobantes.pcl_rut,   "
                strsql = strsql + " view_cabecera_comprobantes.pcl_nomfant,   "
                strsql = strsql + " view_cabecera_comprobantes.tci_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_numprovcli,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_feccpbt,   "
                strsql = strsql + " view_cabecera_comprobantes.mon_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_final,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_contable,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_cartcli,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_findeuda, cbc_tipcambio, cbc_feccont"
                strsql = strsql + " FROM view_cabecera_comprobantes,   "
                strsql = strsql + " clasificacion_cpbtdoc,   "
                strsql = strsql + " tipos_cpbtdoc"
                strsql = strsql + " WHERE  tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp  and  "
                strsql = strsql + " tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = tipos_cpbtdoc.tpc_codemp  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_tpcid = tipos_cpbtdoc.tpc_tpcid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = " + codemp.ToString
                strsql = strsql + " and view_cabecera_comprobantes.cbc_sucid = " + codsuc.ToString
                strsql = strsql + " and view_cabecera_comprobantes.cbt_estado in ('" + estado + "','B','A')"
                strsql = strsql + " and view_cabecera_comprobantes.idi_idid = " + idioma.ToString
                strsql = strsql + " and clb_cartcli <> '" + cartcli + "'"
                strsql = strsql + " and clb_tipcpbtdoc = '" + tipo + "'"
                strsql = strsql + " and clb_contable = 'S'"

                strsql = strsql + " and cbc_tpcid in ( "
                strsql = strsql + " Select perfiles_comprobantes.pfc_tpcid"
                strsql = strsql + " FROM perfiles_comprobantes"
                strsql = strsql + " WHERE  perfiles_comprobantes.pfc_codemp =" + codemp.ToString
                strsql = strsql + " and  perfiles_comprobantes.pfc_prfid = " + perfil.ToString + ")"


                If cobranza = True Then
                    strsql = strsql + " and cbc_numprovcli not in (select ccb_numero from cartera_clientes_cpbt_doc "
                    strsql = strsql + " where ccb_codemp =" + codemp.ToString
                    strsql = strsql + " and ccb_pclid =" + CInt(func.Configuracion_Emp_Num(codemp, 104)).ToString + ")"
                End If

                strsql = strsql + " order by tci_nombre, cbc_numero "

                ds = hds.ConsultaBD(strsql)


            Catch ex As Exception

            End Try

            Return ds


        End Function

        Public Function Trae_Datos_CabCpbt(ByVal codemp As Integer, ByVal codsuc As Integer, ByVal tpcid As Integer, ByVal numero As Integer) As DataSet
            Dim strsql As String
            Dim hds As New Horus.DatSet
            Dim ds As New DataSet

            Try
                strsql = "SELECT view_cabecera_comprobantes.cbc_tpcid,   "
                strsql = strsql + "        view_cabecera_comprobantes.cbc_numero,   "
                strsql = strsql + "         view_cabecera_comprobantes.pcl_rut,   "
                strsql = strsql + " view_cabecera_comprobantes.pcl_nomfant,   "
                strsql = strsql + " view_cabecera_comprobantes.tci_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_numprovcli,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_feccpbt,   "
                strsql = strsql + " view_cabecera_comprobantes.mon_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_final,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_contable,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_cartcli,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_findeuda, cbc_tipcambio, cbc_feccont"
                strsql = strsql + " FROM view_cabecera_comprobantes,   "
                strsql = strsql + " clasificacion_cpbtdoc,   "
                strsql = strsql + " tipos_cpbtdoc"
                strsql = strsql + " WHERE  tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp  and  "
                strsql = strsql + " tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = tipos_cpbtdoc.tpc_codemp  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_tpcid = tipos_cpbtdoc.tpc_tpcid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = " + codemp.ToString
                strsql = strsql + " and view_cabecera_comprobantes.cbc_sucid = " + codsuc.ToString
                strsql = strsql + " and cbc_tpcid =" + tpcid.ToString
                strsql = strsql + " and cbc_numero =" + numero.ToString
                strsql = strsql + " order by tci_nombre, cbc_numero "

                ds = hds.ConsultaBD(strsql)


            Catch ex As Exception

            End Try

            Return ds


        End Function

        Public Function Trae_Datos_CabCpbt(ByVal conn As Horus.Conexion, ByVal mytrans As SqlTransaction, ByVal codemp As Integer, ByVal codsuc As Integer, ByVal tpcid As Integer, ByVal numero As Integer) As DataSet
            Dim strsql As String
            Dim hds As New Horus.DatSet
            Dim ds As New DataSet

            Try
                strsql = "SELECT view_cabecera_comprobantes.cbc_tpcid,   "
                strsql = strsql + "        view_cabecera_comprobantes.cbc_numero,   "
                strsql = strsql + "         view_cabecera_comprobantes.pcl_rut,   "
                strsql = strsql + " view_cabecera_comprobantes.pcl_nomfant,   "
                strsql = strsql + " view_cabecera_comprobantes.tci_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_numprovcli,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_feccpbt,   "
                strsql = strsql + " view_cabecera_comprobantes.mon_nombre,   "
                strsql = strsql + " view_cabecera_comprobantes.cbc_final,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_contable,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_cartcli,   "
                strsql = strsql + " clasificacion_cpbtdoc.clb_findeuda, cbc_tipcambio, cbc_feccont, clb_selcpbt"
                strsql = strsql + " FROM view_cabecera_comprobantes,   "
                strsql = strsql + " clasificacion_cpbtdoc,   "
                strsql = strsql + " tipos_cpbtdoc"
                strsql = strsql + " WHERE  tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp  and  "
                strsql = strsql + " tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = tipos_cpbtdoc.tpc_codemp  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_tpcid = tipos_cpbtdoc.tpc_tpcid  and  "
                strsql = strsql + " view_cabecera_comprobantes.cbc_codemp = " + codemp.ToString
                strsql = strsql + " and view_cabecera_comprobantes.cbc_sucid = " + codsuc.ToString
                strsql = strsql + " and cbc_tpcid =" + tpcid.ToString
                strsql = strsql + " and cbc_numero =" + numero.ToString
                strsql = strsql + " order by tci_nombre, cbc_numero "

                ds = hds.ConsultaBD(conn, mytrans, strsql)


            Catch ex As Exception

            End Try

            Return ds


        End Function

        Public Function ConvertImageToByteArray(ByVal imageIn As System.Drawing.Image) As Byte()
            Dim ms As New IO.MemoryStream()
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
            Return ms.ToArray()
        End Function

        Public Function ConvertByteArrayToImage(ByVal byteArrayIn As Byte()) As Bitmap
            Dim ms As New IO.MemoryStream(byteArrayIn)
            Dim returnImage As Bitmap = Bitmap.FromStream(ms)
            Return returnImage
        End Function

        Public Function CodBarImagen(ByVal codigo As String, ByVal size As Integer) As Bitmap
            Dim barCode As New iTextSharp.text.pdf.BarcodeEAN
            Dim dv As String
            Dim CodImg As Bitmap


            Try
                dv = barCode.CalculateEANParity(codigo)

                If Len(codigo) = 12 Then
                    codigo = codigo + dv
                End If

                If Len(codigo) > 13 Then
                    codigo = Left(codigo, 13)
                End If

                barCode.X = 0.8
                barCode.Size = size
                barCode.Baseline = size * 3
                barCode.GuardBars = True
                barCode.CodeType = barCode.EAN13
                barCode.Code = codigo
                barCode.AltText = codigo
                barCode.GenerateChecksum = True


                CodImg = barCode.CreateDrawingImage(Color.Black, Color.White)

            Catch ex As Exception

            End Try


            Return CodImg


        End Function

        Public Function CodBarImagenBit(ByVal codigo As String, ByVal size As Integer) As Byte()
            Dim barCode As New iTextSharp.text.pdf.BarcodeEAN
            Dim dv As String
            Dim CodImg As Bitmap = Nothing
            Dim ms() As Byte


            Try
                codigo = Replace(codigo, ".", "")
                dv = barCode.CalculateEANParity(codigo)

                If Len(codigo) = 12 Then
                    codigo = codigo + dv
                End If

                If Len(codigo) > 13 Then
                    codigo = Left(codigo, 13)
                End If

                barCode.X = 0.8
                barCode.Size = size
                barCode.Baseline = size * 3
                barCode.GuardBars = True
                barCode.CodeType = barCode.EAN13
                barCode.Code = codigo
                barCode.AltText = codigo
                barCode.GenerateChecksum = True


                CodImg = barCode.CreateDrawingImage(Color.Black, Color.White)

                ms = ConvertImageToByteArray(CodImg)

            Catch ex As Exception

                Return ms

            End Try


            Return ms


        End Function

        Public Function CodBarImagenBitEsp(ByVal codigo As String, ByVal size As Integer) As Byte()
            Dim barCode As New iTextSharp.text.pdf.BarcodePDF417
            Dim dv As String
            Dim CodImg As Bitmap = Nothing
            Dim ms() As Byte


            Try
                'codigo = Replace(codigo, ".", "")
                'dv = barCode.CalculateEANParity(codigo)

                'If Len(codigo) = 12 Then
                '    codigo = codigo + dv
                'End If

                'If Len(codigo) > 13 Then
                '    codigo = Left(codigo, 13)
                'End If

                'barCode.X = 0.8
                'barCode.Size = size
                'barCode.Baseline = size * 3
                'barCode.GuardBars = True
                'barCode.CodeType = barCode.EAN13
                'barCode.Code = codigo
                'barCode.AltText = codigo
                'barCode.GenerateChecksum = True

                barCode.SetText(codigo)

                CodImg = barCode.CreateDrawingImage(Color.Black, Color.White)

                ms = ConvertImageToByteArray(CodImg)

            Catch ex As Exception

                Return ms

            End Try


            Return ms


        End Function

        Public Function RevisaAcentos(ByVal texto As String) As Boolean
            Dim i As Integer
            Dim letra As String


            For i = 1 To Len(texto)
                letra = Mid(texto, i, 1)
                If InStr("áéíóúÁÉÍÓÚ", letra) > 0 Then
                    Return False
                    Exit For
                End If
            Next

            Return True

        End Function

        Public Function Comuna(ByVal tipo As String) As Integer
            Dim strSql As String
            Dim hds As New Horus.DatSet
            Dim ds As New DataSet

            Try

                strSql = "  Select  Comuna.com_comid"
                strSql = strSql + "  FROM Comuna"
                strSql = strSql + "  WHERE comuna.com_nombre = '" + UCase(tipo) + "'"

                ds = hds.ConsultaBD(strSql)

                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)(0)
                Else
                    Return 0
                End If

            Catch ex As Exception
                Return 0

            End Try

        End Function


        Public Function MonedaRound(ByVal codemp As Integer, ByVal codmon As Integer, ByVal monto As Decimal) As Decimal
            Dim strSql As String
            Dim hds As New Horus.DatSet
            Dim ds As New DataSet
            Dim Redondea As Integer = 0

            Try

                '----------Busco los decimales de la moneda------------
                strSql = "select mon_decimales from monedas where mon_codemp=" + codemp.ToString
                strSql = strSql + " and mon_codmon=" + codmon.ToString
                ds = hds.ConsultaBD(strSql)
                Redondea = ds.Tables(0).Rows(0)(0)

                ds = hds.ConsultaBD(strSql)

                If ds.Tables(0).Rows.Count > 0 Then
                    monto = Math.Round(monto, Redondea)
                    Return monto
                Else
                    Return monto
                End If

            Catch ex As Exception
                Return 0

            End Try

        End Function



    End Class












End Namespace



*/
