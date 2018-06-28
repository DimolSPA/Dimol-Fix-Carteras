using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dimol.ArchivoCopec.bcp
{
    public class ArchivoCopec
    {
        public static void ListarGestionesCopec()
        {

            try
            {
                Dimol.bcp.Funciones objFunc = new Dimol.bcp.Funciones();
                string fileName = "";
                string path = "";
                Regex re = new Regex("[;\\\\/:*?\"<>|&']|\t|\n");

                List<Dimol.ArchivoCopec.dto.ArchivoCopecGestiones> lst = new List<Dimol.ArchivoCopec.dto.ArchivoCopecGestiones>();

                lst = Dimol.ArchivoCopec.dao.ArchivoCopec.ListarGestionesCopec(86);
                var csv = new StringBuilder();
                var firstLine = "RUT_CLIENTE;ROL;TRIBUNAL;GESTION_REALIZADA;FECHA_GESTION;RUT_GESTION";
                csv.AppendLine(firstLine);
                foreach (Dimol.ArchivoCopec.dto.ArchivoCopecGestiones a in lst)
                {
                    //Construir csv, con gestiones internas
                    if (!string.IsNullOrEmpty(a.GestionRealizada))
                    {
                        var newLine = string.Format("{0};{1};{2};{3};{4};{5}", a.RutCliente, a.Rol, a.Tribunal, a.GestionRealizada, a.FechaGestion, a.RutGestion);
                        csv.AppendLine(newLine);
                    }

                }

                fileName = "pcj_gestiones_" + ConfigurationManager.AppSettings["CodigoOficina"] + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                path = ConfigurationManager.AppSettings["RutaArchivosCopec"];
                objFunc.CreaCarpetas(path);
                //To save file
                FileInfo fi = new FileInfo(path + fileName);

                if (!fi.Exists)
                {
                    fi.Create().Dispose();
                }

                File.AppendAllText(path + fileName, csv.ToString(), Encoding.UTF8);

                //using
                //    (
                //        var sw = new StreamWriter(
                //                                new FileStream(path + fileName, FileMode.Open, FileAccess.ReadWrite),
                //                                Encoding.UTF8
                //                                )
                //    )
                //{
                //    sw.Write(csv.ToString());
                //}

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "ListarGestionesCopec", 0);
            }
        }

        public static void ListarJuiciosCopec()
        {

            try
            {
                Dimol.bcp.Funciones objFunc = new Dimol.bcp.Funciones();
                string fileName = "";
                string path = "";
                Regex re = new Regex("[;\\\\/:*?\"<>|&']|\t|\n");

                List<Dimol.ArchivoCopec.dto.ArchivoCopec> lst = new List<Dimol.ArchivoCopec.dto.ArchivoCopec>();

                lst = Dimol.ArchivoCopec.dao.ArchivoCopec.ListarJuiciosCopec(86);
                var csv = new StringBuilder();
                var firstLine = "Cuenta_Interna;Rut_Cliente;Tipo_Persona;Razon_Social;Nombre;Apellido_Paterno;Apellido_Materno;Tipo_Cliente;Total_Deuda;Moneda_Total_Deuda;Fecha_Envio;Tipo_Cobranza_Documento;Tipo_Juicio;Rol;Tribunal;Etapa_Juicio;Estado_Juicio;Ultima_Gestion_Judicial;Fecha_Ultima_Gestion_Judicial;Rut_Abogado;Nombre_Abogado;Fecha_Inicio_Juicio;Fecha_Notificacion;Fecha_Embargo;Fecha_Remate;Fecha_Incitacion;Fecha_Fin_Juicio;Rut_Ejecutivo;Nombre_Ejecutivo;Observaciones_Copec;Observaciones_Oficina;Localidad;Numero_Cuota;Sociedad;Anio;Numero_Documento;Rol_Padre;Tribunal_Padre";
                csv.AppendLine(firstLine);
                foreach (Dimol.ArchivoCopec.dto.ArchivoCopec a in lst)
                {
                    //Construir csv, con gestiones internas
                    if (!string.IsNullOrEmpty(a.CuentaInterna))
                    {
                        var newLine = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25};{26};{27};{28};{29};{30};{31};{32};{33};{34};{35};{36};{37}", a.CuentaInterna, a.RutCliente, a.TipoPersona, a.RazonSocial, a.Nombre, a.ApellidoPaterno, a.ApellidoMaterno, a.TipoCliente, a.TotalDeuda, a.MonedaTotalDeuda, a.FechaEnvio, a.TipoCobranzaDocumento, a.TipoJuicio, a.Rol, a.Tribunal, a.EtapaJuicio, a.EstadoJuicio, a.UltimaGestion, a.FechaUltimaGestion, a.RutAbogado, a.NombreAbogado, a.FechaInicioJuicio, a.FechaNotificacion, a.FechaEmbargo, a.FechaRemate, a.FechaIncitacion, a.FechaFinJuicio, a.RutEjecutivo, a.NombreEjecutivo, a.ObservacionesCopec, a.ObservacionesOficina, a.Localidad, a.NumeroCuota, a.Sociedad, a.Anio, a.NumeroDocumento, a.RolPadre, a.TribunalPadre);
                        csv.AppendLine(newLine);
                    }

                }
                

                fileName = "pcj_juicios_" + ConfigurationManager.AppSettings["CodigoOficina"] + "_" + DateTime.Now.ToString("yyyyMMdd") + ".csv";
                path = ConfigurationManager.AppSettings["RutaArchivosCopec"];
                objFunc.CreaCarpetas(path);
                //To save file
                FileInfo fi = new FileInfo(path + fileName);

                if (!fi.Exists)
                {
                    fi.Create().Dispose();
                }

                File.AppendAllText(path + fileName, csv.ToString(), Encoding.UTF8);                

            }
            catch (Exception ex)
            {
                Dimol.dao.Funciones.InsertarError(ex.Message, ex.StackTrace, "ListarJuiciosCopec", 0);
            }
        }
    }
}
