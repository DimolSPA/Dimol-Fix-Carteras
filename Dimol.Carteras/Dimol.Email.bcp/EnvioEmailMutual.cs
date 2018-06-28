using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;
using System.Transactions;
using Dimol.Reportes.bcp;
using Dimol.Reportes.dto;
using System.Net.Mail;
using System.Net.Mime;

namespace Dimol.Email.bcp
{
    public class EnvioEmailMutual
    {
        public static List<int> ListarCarteraEmail(int codemp, int pclid, string estid, int tipoCartera, int gestor, int sucid, string listaGestores)
        {
            return dao.EnvioEmail.ListarCarteraEmail(codemp, pclid, estid, tipoCartera, gestor, sucid, listaGestores);
        }

        public static List<dto.DatosDeudor> ListarContactosEmailDeudor(int codemp, int ctcid, int todo, int contacto)
        {
            return dao.EnvioEmail.ListarContactosEmailDeudor(codemp, ctcid, todo, contacto);
        }

        public static bool Enviar(int pclid, int ctcid, UserSession objSession, string path, string emailDestino, dto.DocumentoCocha docEmailCocha, List<string[]> Documentos, string TipoReporte)
        {
            int exitoInsertarHistorial = 0;
            string pathArchivo = "";
            bool error = false;
            Dimol.dao.Funciones objFunc = new Dimol.dao.Funciones();

            Dimol.Email.dto.Gestor DatosGestor = new Dimol.Email.dto.Gestor();
            //objSession.Email = "julieta.romo@dimol.cl";
            DatosGestor = Dimol.Email.dao.EnvioEmailCocha.TraeGestor(objSession.Email);

            if (!string.IsNullOrEmpty(DatosGestor.Telefono))
            {

                docEmailCocha.Numero = DatosGestor.Telefono; // Numero de teléfono del gestor
                docEmailCocha.Cuenta = objSession.Email; //DatosGestor.Email; // Email del gestor
                docEmailCocha.Banco = objSession.Nombre;//DatosGestor.Nombre; // Nombre del gestor

                Transformador objTransformador = new Transformador();

                dto.EmailBodyCocha mailCocha = Dimol.Email.dao.EnvioEmailCocha.TraeMailCocha(Int32.Parse(TipoReporte), Int32.Parse(TipoReporte));
                string salida = objTransformador.TransformXSLToHTML(docEmailCocha, mailCocha.Body);

                exitoInsertarHistorial = 0;

                try
                {

                    if (pclid == 318 || pclid == 609)
                    {

                        Liquidacion obj = new Liquidacion();
                        obj.Codemp = objSession.CodigoEmpresa;
                        obj.Pclid = pclid;
                        obj.Ctcid = ctcid;
                        obj.TipoCartera = 1;
                        obj.EstadoCpbt = "V";
                        obj.Idioma = objSession.Idioma;
                        obj.Sucid = objSession.CodigoSucursal;
                        obj.FechaReporte = DateTime.Now;
                        obj.NombreArchivo = "Liquidacion_Cliente_" + docEmailCocha.Deudor + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                        obj.PathArchivo = path + obj.NombreArchivo + ".pdf";
                        obj.Pagina = 355;
                        obj.IdReporte = 3;
                        obj.Ccbid = "";

                        pathArchivo = obj.PathArchivo;

                        foreach (var item in Documentos)
                        {
                            obj.Ccbid += item[2] + ",";
                        }

                        obj.Ccbid = obj.Ccbid.Substring(0, obj.Ccbid.Length - 1);

                        var result = GeneraReporteCochaMutualParcial(obj);
                        error = !result;
                    }

                    else if (pclid == 559)
                    {

                        LiquidacionDura objLey = new LiquidacionDura();
                        objLey.Codemp = objSession.CodigoEmpresa;
                        objLey.Pclid = pclid;
                        objLey.Ctcid = ctcid;
                        objLey.TipoCartera = 1;
                        objLey.EstadoCpbt = "V";
                        objLey.Idioma = objSession.Idioma;
                        objLey.Sucid = objSession.CodigoSucursal;
                        objLey.FechaReporte = DateTime.Now;
                        objLey.NombreArchivo = "Liquidacion_Cliente_" + docEmailCocha.Deudor + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                        objLey.PathArchivo = path + objLey.NombreArchivo + ".pdf";
                        objLey.Pagina = 355;
                        objLey.IdReporte = 5;
                        //objLey.Ccbid = "";

                        pathArchivo = objLey.PathArchivo;

                        /*
                        foreach (var item in Documentos)
                        {
                            objLey.Ccbid += item[2] + ",";
                        }    

                        objLey.Ccbid = objLey.Ccbid.Substring(0, objLey.Ccbid.Length-1);*/

                        error = GeneraReporteMutualLey(objLey) ? false : true;
                    }

                    if (!error)
                    {
                        //datos.PathReporte = obj.PathArchivo.Replace("\\\\", "\\");
                        Email emailSender = new Email();
                        dto.Email email = new dto.Email();

                        email = new dto.Email();
                        email.From = DatosGestor.Email; //DatosGestor.Email; // objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133);
                        email.Codemp = objSession.CodigoEmpresa;
                        //email.From = objSession.Email; //DatosGestor.Email; //objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133);
                        //email.Sender = objSession.Email; //DatosGestor.Email; //objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133);
                        //email.From = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 133);
                        //if(pclid == 318)
                        //    email.Sender = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 132);
                        //else
                            //email.Sender = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 200);
                        foreach (string mail in emailDestino.Split(','))
                            email.To.Add(mail);

                        //email.Bcc.Add(objSession.Email);
                        email.Subject = objFunc.ConfiguracionEmpStr(objSession.CodigoEmpresa, 138) + " " + docEmailCocha.RutDeudor + " " + docEmailCocha.Deudor;//"Estado de Cuenta " + docEmailCocha.Cliente;
                        email.Body = salida;
                        email.Html = true;
                        Attachment objAtt = new Attachment(pathArchivo);
                        email.Attachments.Add(objAtt);
                        error = emailSender.EnviarEmailMutual(email, pclid) ? false : true;
                        if (error)
                        {
                            Dimol.dao.Funciones.InsertarError("Correo no enviado, CTCID: " + ctcid + ", PCLID: " + pclid, "", "Dimol.Email.bcp.EnvioEmailMutual.Enviar", objSession.UserId);
                            //dao.EnvioEmail.ReversarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, pclid, ctcid);
                        }
                        else
                        {
                            //exitoInsertarHistorial = Dimol.Email.dao.EnvioEmailCocha.InsertarCarteraClientesEstadosHistorialEspecial(objSession.CodigoEmpresa, pclid, ctcid, Int32.Parse(Documentos.FirstOrDefault()[2]), DateTime.Now, objFunc.ConfiguracionEmpNum(objSession.CodigoEmpresa, 141), objSession.CodigoSucursal, objSession.Gestor, objSession.IpRed, objSession.IpPc, docEmailCocha.Comentario, 0, docEmailCocha.Saldo, objSession.UserId);
                            exitoInsertarHistorial = Dimol.Email.dao.EnvioEmailCocha.InsertarCarteraClientesEstadosAcciones(objSession.CodigoEmpresa, pclid, ctcid, objSession.Gestor, objSession.CodigoSucursal, "S", objSession.IpRed, objSession.IpPc, objSession.UserId, docEmailCocha.Comentario);

                            if (exitoInsertarHistorial > 0)
                            {
                                error = true;
                            }
                            else
                            {
                                error = false;
                            }
                        }

                        System.IO.File.Delete(pathArchivo + ".fo");
                    }


                }
                catch (Exception ex)
                {
                    Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmailMutual.Enviar, CTCID: " + ctcid + ", PCLID: " + pclid, objSession.UserId);
                }
            }
            else
            {
                Dimol.dao.Funciones.InsertarError("El mail " + DatosGestor.Email + " no se encuentra en la tabla Gestor o Usuario, o bien el mail no coincide en ambas tablas. Codigo de Gestor: " + objSession.Gestor.ToString(), "", "Dimol.Email.bcp.EnvioEmailMutual.Enviar, CTCID: " + ctcid + ", PCLID: " + pclid, objSession.UserId);
                error = false;
            }
            return error;
        }

        public static bool GeneraReporteCochaMutualParcial(Liquidacion obj)
        {
            try
            {
                return Cartera.TraeLiquidacionCochaMutualParcial(obj);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmailMutual.GeneraReporteCochaMutualParcial", 0);
                return false;
            }
        }

        public static bool GeneraReporteMutualLey(LiquidacionDura obj)
        {
            try
            {
                return Cartera.TraeLiquidacionMutualLey(obj);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmailMutual.GeneraReporteMutualLeyParcial", 0);
                return false;
            }
        }

        public static bool GeneraReporteMutualLeyParcial(LiquidacionDura obj)
        {
            try
            {
                return Cartera.TraeLiquidacionMutualLeyParcial(obj);
            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Email.bcp.EnvioEmailMutual.GeneraReporteMutualLeyParcial", 0);
                return false;
            }
        }

    }

    /*Private Function Mail_Archivo() As Boolean
       Dim i As Integer
       Dim strSql As String
       Dim dsTel As New DataSet
       Dim dsCart As New DataSet
       Dim TabCall As New DataTable
       Dim TabSMS As New DataTable
       Dim x As Integer
       Dim tel As String = ""
       Dim row As DataRow
       Dim fila As DataRow
       Dim codCiu As String = ""
       Dim nombre As String
       Dim cont As Integer = 0
       Dim ini As Date
       Dim fin As Date
       Dim tipo As String
       Dim cont2 As Integer
       Dim dsAste As New DataSet
       Dim row2 As DataRow
       Dim estado As String
       Dim estid As Integer
       Dim coment As String
       Dim ctcid As Integer
       Dim dCont As String
       Dim err As Integer
       Dim hoy As Date = func.FechaServer
       Dim mensaje As String
       Dim rut As String
       Dim TabDet As String
       Dim tot As Decimal
       Dim hMail As String
       Dim nomCli As String
       Dim digito As String
       Dim dsGest As New DataSet
       Dim mailGest As String
       Dim passGest As String = func.Configuracion_Emp_Str(codemp, 63)
       Dim smtp As String = func.Configuracion_Emp_Str(codemp, 60)
       Dim Path As String = func.App_Path
       Dim NomRep As String
       Dim nomGest As String
       Dim Destino As String = ""
       Dim DirDest As String = func.Configuracion_Str(12)
       Dim path2 As String
       Dim NomDeu As String




       Try
           RaG_Res.Visible = False

           Dim columna As DataColumn

           '------------Tipo Llamadas------------
           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.String")
           columna.ColumnName = "Nombre"
           TabCall.Columns.Add(columna)

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.String")
           columna.ColumnName = "Mail"
           TabCall.Columns.Add(columna)

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.String")
           columna.ColumnName = "Tipo"
           TabCall.Columns.Add(columna)

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.String")
           columna.ColumnName = "Deudor"
           TabCall.Columns.Add(columna)

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.String")
           columna.ColumnName = "Contacto"
           TabCall.Columns.Add(columna)

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.String")
           columna.ColumnName = "Rut"
           TabCall.Columns.Add(columna)

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.String")
           columna.ColumnName = "NomDeu"
           TabCall.Columns.Add(columna)


           '------------Tipo SMS-----------------

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.Double")
           columna.ColumnName = "Correlativo"
           TabSMS.Columns.Add(columna)

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.String")
           columna.ColumnName = "Rut"
           TabSMS.Columns.Add(columna)

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.Double")
           columna.ColumnName = "Mail"
           TabSMS.Columns.Add(columna)

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.String")
           columna.ColumnName = "Mensaje"
           TabSMS.Columns.Add(columna)

           columna = New DataColumn()
           columna.DataType = System.Type.GetType("System.String")
           columna.ColumnName = "NomDeu"
           TabSMS.Columns.Add(columna)




           PanErr.Visible = False

           If txtProvcli.Text = 0 Then
               PanErr.Error_Display(func.TraeEtiqueta("Cliente", idioma) + " " + func.TraeError("DatNull", idioma))
               PanErr.Visible = True
               Return False
           End If

           'If txtMen.Text = "" Then
           '    PanErr.Error_Display(func.TraeEtiqueta("Mensaje", idioma) + " " + func.TraeError("DatNull", idioma))
           '    PanErr.Visible = True
           '    Return False
           'End If


           Dim conn As New Horus.Conexion
           conn.SQLConn.Open()
           Dim myTrans As SqlTransaction = conn.SQLConn.BeginTransaction()

           '-------------Busco la Cartera----------------------
           Buscar_Cartera()
           dsCart = ds

           '---------Comienzo la primera lectura--------------

           RadProgressArea1.Localization.Uploaded = "Total avance"
           RadProgressArea1.Localization.UploadedFiles = "Avances"
           RadProgressArea1.Localization.CurrentFileName = "Actual proceso en accion: "


           TablaTiempo(dsCart.Tables(0).Rows.Count - 1)


           For i = 0 To dsCart.Tables(0).Rows.Count - 1
               ctcid = dsCart.Tables(0).Rows(i)("ccb_ctcid")
               cont = cont + 1
               If cont > 100 Then
                   TablaTiempoAvanza(dsCart.Tables(0).Rows.Count - 1, i, "Revisando E-Mails")
                   cont = 0
               End If


               '-----------Busco los Telefonos del Titula--------------
               strSql = "  SELECT ddm_mail,   "
               strSql = strSql + "  ddm_tipo,   "
               strSql = strSql + " view_datos_geograficos.ciu_codarea, ctc_nombre, 'N' as Contacto, ctc_rut, ctc_nomfant"
               strSql = strSql + " FROM deudores,   "
               strSql = strSql + " deudores_mail,   "
               strSql = strSql + " view_datos_geograficos"
               strSql = strSql + " WHERE  deudores_mail.ddm_codemp = deudores.ctc_codemp  and  "
               strSql = strSql + " deudores_mail.ddm_ctcid = deudores.ctc_ctcid  and  "
               strSql = strSql + " deudores.ctc_comid = view_datos_geograficos.com_comid"
               strSql = strSql + " and  deudores_mail.ddm_codemp = " + codemp.ToString
               strSql = strSql + " and deudores_mail.ddm_ctcid = " + dsCart.Tables(0).Rows(i)("ccb_ctcid").ToString
               If cb_todo.Checked = False Then
                   strSql = strSql + " and ddm_masivo ='S'"
               End If

               If cb_contactos.Checked = True Then
                   strSql = strSql + " Union "

                   strSql = strSql + " SELECT deudores_contactos_mail.dcm_mail,   "
                   strSql = strSql + " deudores_contactos_mail.dcm_tipo,   "
                   strSql = strSql + " view_datos_geograficos.ciu_codarea, ddc_nombre, 'S' as Contacto, ctc_rut, ctc_nomfant"
                   strSql = strSql + " FROM {oj deudores_contactos LEFT OUTER JOIN view_datos_geograficos ON deudores_contactos.ddc_comid = view_datos_geograficos.com_comid},   "
                   strSql = strSql + " deudores_contactos_mail, deudores"
                   strSql = strSql + " WHERE  deudores_contactos_mail.dcm_codemp = deudores_contactos.ddc_codemp  "
                   strSql = strSql + " and deudores_contactos_mail.dcm_ctcid = deudores_contactos.ddc_ctcid  "
                   strSql = strSql + " and deudores_contactos_mail.dcm_ddcid = deudores_contactos.ddc_ddcid"

                   strSql = strSql + " and deudores_contactos_mail.dcm_codemp = deudores.ctc_codemp  "
                   strSql = strSql + " and deudores_contactos_mail.dcm_ctcid = deudores.ctc_ctcid  "

                   strSql = strSql + " and  deudores_contactos_mail.dcm_codemp = " + codemp.ToString
                   strSql = strSql + " and deudores_contactos_mail.dcm_ctcid = " + dsCart.Tables(0).Rows(i)("ccb_ctcid").ToString

                   If cb_todo.Checked = False Then
                       strSql = strSql + " and dcm_masivo ='S'"
                   End If


               End If


               dsTel = Hds.ConsultaBD(strSql)

               '---------------Cargo los Telefonos a los Cuales voy a Llamar------------------

               If dsTel.Tables(0).Rows.Count > 0 Then
                   For x = 0 To dsTel.Tables(0).Rows.Count - 1
                       codCiu = ""
                       nombre = dsTel.Tables(0).Rows(x)("ctc_nombre")
                       tipo = dsTel.Tables(0).Rows(x)("ddm_tipo")
                       dCont = dsTel.Tables(0).Rows(x)("Contacto")
                       rut = dsTel.Tables(0).Rows(x)("ctc_rut")
                       NomDeu = dsTel.Tables(0).Rows(x)("ctc_nomfant")


                       '-------------------Reviso los telefonos---------------------------
                       tel = dsTel.Tables(0).Rows(x)("ddm_mail").ToString


                       '----------------------Reviso si esta creado ya-------------------------------
                       strSql = "Mail = '" + tel + "'"

                       Try
                           row = TabCall.Select(strSql)(0)
                           If row("Mail") = "" Then
                               fila = TabCall.NewRow()
                               fila("Nombre") = nombre
                               fila("Tipo") = tipo
                               fila("Mail") = tel
                               fila("Deudor") = ctcid
                               fila("Contacto") = dCont
                               fila("Rut") = rut
                               fila("NomDeu") = NomDeu
                               TabCall.Rows.Add(fila)
                           End If
                       Catch ex As Exception
                           fila = TabCall.NewRow()
                           fila("Nombre") = nombre
                           fila("Tipo") = tipo
                           fila("Mail") = tel
                           fila("Deudor") = ctcid
                           fila("Contacto") = dCont
                           fila("Rut") = rut
                           fila("NomDeu") = NomDeu
                           TabCall.Rows.Add(fila)
                       End Try
                   Next
               End If
           Next

           '--------------------Reviso si hay que hacer llamadas---------------------------
           ctcid = 0
           TablaTiempo(TabCall.Rows.Count - 1)
           If TabCall.Rows.Count > 0 Then
               '------Comienzo a Crear el IVR-----------

               i = 0

               Buscar_Cartera(conn, myTrans)
               dsCart = ds

               For Each row In TabCall.Rows
                   i = i + 1

                   cont2 = cont2 + 1
                   If cont2 > 20 Then
                       TablaTiempoAvanza(TabCall.Rows.Count - 1, i, "Enviando Mail")
                       cont2 = 0
                   End If




                   If ctcid <> row("Deudor") Then
                       ctcid = row("Deudor")
                       Dim spAc As New Horus.StoredProcedure("Insertar_Cartera_Clientes_Estados_Acciones")

                       spAc.AgregarParametro("cea_codemp", codemp)
                       spAc.AgregarParametro("cea_pclid", txtProvcli.Text)
                       spAc.AgregarParametro("cea_ctcid", ctcid)
                       spAc.AgregarParametro("cea_accid", func.Configuracion_Emp_Num(codemp, 65))
                       spAc.AgregarParametro("cea_sucid", codsuc)
                       spAc.AgregarParametro("cea_gesid", DBNull.Value)
                       spAc.AgregarParametro("cea_contacto", row("Contacto"))
                       spAc.AgregarParametro("cea_ipred", Request.ServerVariables("LOCAL_ADDR"))
                       spAc.AgregarParametro("cea_ipmaquina", Request.ServerVariables("REMOTE_HOST"))
                       spAc.AgregarParametro("cea_comentario", "EMAIL")
                       spAc.AgregarParametro("cea_estado", "S")
                       spAc.AgregarParametro("cea_usrid", usuario)
                       spAc.AgregarParametro("cea_ddcid", DBNull.Value)
                       spAc.AgregarParametro("cea_telefono", DBNull.Value)

                       err = spAc.EjecutarProcedimiento(conn, myTrans)

                       If err <= 0 Then
                           Exit For
                       End If



                   Else
                       '----------------Genero el Archivo con el cual enviare el mail------------------------
                       Pprovcli = txtProvcli.Text
                       Pctcid = ctcid

                       NomRep = Revisa_ParRep()

                       NomRep = Left(NomRep, Len(NomRep) - 4) + ".pdf"



                       NomRep = "Liquidacion_Cliente.pdf"

                       NomRep = "Liquidacion_Cliente_" + rutD.ToString + ".pdf"



                       If Trim(txtMail.Text) <> "" Then
                           ' mailGest = txtMail.Text
                           'passGest = txtPass.Text

                           Destino = ""
                           DirDest = func.Configuracion_Str(12)
                           path2 = ""

                           NomRep = "Liquidacion_Cliente.pdf"

                           NomRep = "Liquidacion_Cliente_" + rutD.ToString + ".pdf"



                           path2 = Path + DirDest

                           Destino = path2 + "\" + codemp.ToString + "\" + NomRep


                           '-----Envio Mail---------------
                           func.Mail2(codemp, row("Nombre"), txtMail.Text, hMail, "Cuenta Corriente " + nomCli, True, 1, nomCli, smtp, mailGest, passGest, Destino)



                           '----------------Reviso si esta el archivo y lo elimino-------------
                           func.EliminaArchivo(Destino, idioma)

                       Else
                           Destino = ""
                           DirDest = func.Configuracion_Str(12)
                           path2 = ""


                           path2 = Path + DirDest

                           NomRep = "Liquidacion_Cliente.pdf"

                           NomRep = "Liquidacion_Cliente_" + rutD.ToString + ".pdf"


                           Destino = path2 + "\" + codemp.ToString + "\" + NomRep


                           '-----Envio Mail---------------
                           func.Mail2(codemp, row("Nombre"), row("Mail"), hMail, "Cuenta Corriente " + nomCli, True, 1, nomCli, smtp, mailGest, passGest, Destino)

                           'func.Mail2(codemp, row("Nombre"), "cindy.castro@dimol.cl", hMail, "INFORMACION " + nomCli, True, 1, nomCli, smtp, mailGest, passGest, Destino)

                           '----------------Reviso si esta el archivo y lo elimino-------------
                           func.EliminaArchivo(Destino, idioma)

                       End If

                       ' func.Mail2(codemp, row("Nombre"), "andres.gaete@dimol.cl", hMail, "INFORMACION " + nomCli, True, 1, nomCli, smtp, mailGest, passGest)

                       If Trim(txtMail.Text) <> "" Then
                           Exit For
                       End If
                   End If
               Next

           End If

           If Trim(txtMail.Text) <> "" Then
               err = -1
           End If

           '  err = -1

           If err < 0 Then
               myTrans.Rollback()
               conn.SQLConn.Close()
               PanErr.Error_Display(func.TraeError("ErrAcc", idioma))
               PanErr.Visible = True
           Else
               myTrans.Commit()
               conn.SQLConn.Close()

               'RaG_Res.DataSource = TabSMS
               'RaG_Res.DataBind()
               'RaG_Res.Visible = True

           End If


       Catch ex As Exception
           PanErr.Error_Display(ex.Message)
           PanErr.Visible = True
       End Try

   End Function*/
}
