using Dimol.Carteras.dto;
using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.bcp
{
    public class Deudor: Carteras.dto.Deudor
    {
        #region "Datos Deudor"
        public void BuscarDeudor()
        {
            dao.Deudor.BuscarDeudor(this);
        }

        #endregion

        public List<Autocomplete> ListarRutDeudor(string numero)
        {
            return dao.Deudor.ListarRutDeudor(numero);
        }

        public List<Combobox> ListarEstado(int idioma)
        {
            return dao.Deudor.ListarEstado(idioma);
        }

        public static int GuardarDocumentoEstampe(int codemp, int pclid, int ctcid, string[] param, string path, string nombre, string ext, int usrid)
        {
            return dao.Deudor.GuardarDocumentoEstampe(codemp, pclid, ctcid, param, path, nombre, ext, usrid);
        }

        public static List<string> ListarRutaEstampes(int codemp, int pclid, int ctcid, string[] param)
        {
            return dao.Deudor.ListarRutaEstampes(codemp, pclid, ctcid, param);
        }

        public static List<string> ListarRutaEstampesDeudor(int codemp, int pclid, int ctcid)
        {
            return dao.Deudor.ListarRutaEstampesDeudor(codemp, pclid, ctcid);
        }

        public static int EliminarDatosEstampes(int codemp, int pclid, int ctcid, string[] param)
        {
            return dao.Deudor.EliminarDatosEstampes(codemp, pclid, ctcid, param);
        }

        public void OperDeudor(string oper, int? id, UserSession objsession,Email objEmail)
        {
            //switch (oper)
            //{
            //    case "add":
            //        daoAccion.InsertarAccion((dto.Accion)this, objsession.CodigoEmpresa, objsession.Idioma);
            //        break;
            //    case "edit":
            //        this.IdAccion = (int)id;
            //        daoAccion.EditarAccion((dto.Accion)this, objsession.CodigoEmpresa, objsession.Idioma);
            //        break;
            //    case "del":
            //        daoAccion.BorrarAccion(objsession.CodigoEmpresa, (int)id);
            //        break;
            //}
        }

        public void OperTelefono(string oper, string id, UserSession objsession, Telefono objTelefono)
        {
            string[] ids = id.Split('|');
            //switch (oper)
            //{
            //    case "add":
            //        dao.Deudor.InsertarTelefono(objTelefono, objsession.CodigoEmpresa);
            //        break;
            //    case "edit":
            //        dao.Deudor.EditarTelefono(objTelefono, objsession.CodigoEmpresa, ids[0]);
            //        break;
            //    case "del":
            //        dao.Deudor.BorrarTelefono(objsession.CodigoEmpresa, ids[0],ids[2]);
            //        break;
            //}
        }

        public void OperTelefonoDeudor(string oper, string id, UserSession objsession, TelefonoDeudor objTelefono)
        {
            string[] ids = id.Split('|');
            switch (oper)
            {
                case "add":
                    dao.Deudor.InsertarTelefono(objTelefono, objsession.CodigoEmpresa);
                    break;
                case "edit":
                    dao.Deudor.EditarTelefono(objTelefono, objsession.CodigoEmpresa, ids[0]);
                    break;
                case "del":
                    dao.Deudor.BorrarTelefono(objsession.CodigoEmpresa, ids[1], ids[2]);
                    break;
            }
        }

        public void OperEmail(string oper, int? id, UserSession objsession,Email objEmail)
        {
            //switch (oper)
            //{
            //    case "add":
            //        daoAccion.InsertarAccion((dto.Accion)this, objsession.CodigoEmpresa, objsession.Idioma);
            //        break;
            //    case "edit":
            //        this.IdAccion = (int)id;
            //        daoAccion.EditarAccion((dto.Accion)this, objsession.CodigoEmpresa, objsession.Idioma);
            //        break;
            //    case "del":
            //        daoAccion.BorrarAccion(objsession.CodigoEmpresa, (int)id);
            //        break;
            //}
        }

        public void OperEmailDeudor(string oper, string id,  EmailDeudor objEmail)
        {
            string[] ids = id.Split('|');
            switch (oper)
            {
                case "add":
                    dao.Deudor.InsertarEmail(objEmail);
                    break;
                case "edit":
                    dao.Deudor.EditarEmail(objEmail);
                    break;
                case "del":
                    dao.Deudor.BorrarEmail(ids[0], ids[1], ids[2]);
                    break;
            }
        }

        public void OperContacto(string oper, string id,UserSession objSession, Contacto obj)
        {
            string[] ids = id.Split('|');
            switch (oper)
            {
                case "add":
                    dao.Deudor.InsertarContacto(obj);
                    break;
                case "edit":
                    dao.Deudor.EditarContacto(obj);
                    break;
                case "del":
                    dao.Deudor.BorrarContacto(ids[0], ids[1], ids[2]);
                    break;
            }
        }

        public void OperHistorial(string oper, int? id, UserSession objsession, Historial objHistorial)
        {
            //switch (oper)
            //{
            //    case "add":
            //        daoAccion.InsertarAccion((dto.Accion)this, objsession.CodigoEmpresa, objsession.Idioma);
            //        break;
            //    case "edit":
            //        this.IdAccion = (int)id;
            //        daoAccion.EditarAccion((dto.Accion)this, objsession.CodigoEmpresa, objsession.Idioma);
            //        break;
            //    case "del":
            //        daoAccion.BorrarAccion(objsession.CodigoEmpresa, (int)id);
            //        break;
            //}
        }

        public List<dto.BuscarDeudor> ListarDeudoresGrilla(int codemp, int sucid, int usrid, string pclid, string nombre, string paterno, string materno, string rut, string nomFant, string telefono, string email, string direccion, string gestor, string rol, string estado, string numCPBT, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BuscarDeudor> lst = new List<dto.BuscarDeudor>();
            lst = dao.Deudor.ListarDeudores(codemp,sucid,usrid,pclid,nombre,paterno,materno,rut, nomFant,telefono, email,direccion, gestor, rol, estado,numCPBT, where, sidx, sord, inicio, limite);
            return lst;
        }

        public List<dto.BuscarDeudor> ListarDeudor(int codemp, int sucid, int usrid,  string rut)
        {
            List<dto.BuscarDeudor> lst = new List<dto.BuscarDeudor>();
            lst = dao.Deudor.ListarDeudor(codemp, sucid, usrid, rut);
            return lst;
        }

        public List<dto.BuscarDeudor> ListarDeudorCli(int codemp, int sucid, int usrid, string rut, int pclid)
        {
            List<dto.BuscarDeudor> lst = new List<dto.BuscarDeudor>();
            lst = dao.Deudor.ListarDeudorCli(codemp, sucid, usrid, rut, pclid);
            return lst;
        }

        public int ListarDeudoresGrillaCount(int codemp, int sucid, int usrid, string pclid, string nombre, string paterno, string materno, string rut, string nomFant, string telefono, string email, string direccion, string gestor, string rol, string estado, string numCPBT, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDeudoresCount(codemp, sucid, usrid, pclid, nombre, paterno, materno, rut, nomFant, telefono, email, direccion, gestor, rol, estado, numCPBT, where, sidx, sord, inicio, limite);
        }

        public List<Telefono> ListarTelefonos(int codemp, int ctcid, string telefono, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarTelefonos(codemp, ctcid, telefono,idioma, where, sidx, sord, inicio,limite);
        }

        public int ListarTelefonosCount(int codemp, int ctcid, string telefono, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarTelefonosCount(codemp, ctcid, telefono, idioma, where, sidx, sord, inicio, limite);
        }

        public List<Email> ListarEmail(int codemp, int ctcid, string email, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarEmail(codemp, ctcid, email, idioma, where, sidx, sord, inicio, limite);
        }

        public List<Email> ListarEmailProv(int codemp, int ctcid, string email, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarEmailProv(codemp, ctcid, email, idioma, where, sidx, sord, inicio, limite);
        }

        public int ListarEmailCount(int codemp, int ctcid, string email, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarEmailCount(codemp, ctcid, email, idioma, where, sidx, sord, inicio, limite);
        }

        public List<Historial> ListarHistorial(int codemp, int pclid, int ctcid, int idioma,string tipo, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarHstorial(codemp, pclid,  ctcid,  idioma, tipo, where,  sidx,  sord,  inicio,  limite);
        }

        public int ListarHistorialCount(int codemp, int pclid, int ctcid, int idioma, string tipo,string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarHistorialCount(codemp, pclid, ctcid, idioma,tipo, where, sidx, sord, inicio, limite);
        }

        public List<Documento> ListarDocCliente(int codemp, int pclid, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocCliente(codemp, pclid, ctcid, idioma, where, sidx, sord, inicio, limite);
        }

        public int ListarDocClienteCount(int codemp, int pclid, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocClienteCount(codemp, pclid, ctcid, idioma, where, sidx, sord, inicio, limite);
        }

        public List<Documento> ListarDocDeudor(int codemp, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocDeudor(codemp, ctcid, idioma, where, sidx, sord, inicio, limite);
        }

        public int ListarDocDeudorCount(int codemp, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocDeudorCount(codemp, ctcid, idioma, where, sidx, sord, inicio, limite);
        }

        public List<Rol> ListarRol(int codemp, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarRol(codemp, ctcid, idioma, where, sidx, sord, inicio, limite);
        }

        public int ListarRolCount(int codemp, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarRolCount(codemp, ctcid, idioma, where, sidx, sord, inicio, limite);
        }

        public List<DocumentoRol> ListarDocRol(int codemp, int rolid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocRol(codemp, rolid, idioma, where, sidx, sord, inicio, limite);
        }

        public int ListarDocRolCount(int codemp, int rolid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocRolCount(codemp, rolid, idioma, where, sidx, sord, inicio, limite);
        }

        public List<EstadosRol> ListarEstadoRol(int codemp, int rolid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarEstadoRol(codemp, rolid, idioma, where, sidx, sord, inicio, limite);
        }

        public int ListarEstadoRolCount(int codemp, int rolid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarEstadoRolCount(codemp, rolid, idioma, where, sidx, sord, inicio, limite);
        }

        public List<Historial> ListarObservacion(int codemp, int pclid, int ctcid, int idioma, string tipo, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarObservacion(codemp, pclid, ctcid, idioma, tipo, where, sidx, sord, inicio, limite);
        }

        public int ListarObservacionCount(int codemp, int pclid, int ctcid, int idioma, string tipo, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarObservacionCount(codemp, pclid, ctcid, idioma, tipo, where, sidx, sord, inicio, limite);
        }


        #region "Deudor"
        public List<dto.BuscarDeudor> ListarBuscarDeudoresGrilla(int codemp, int usrid, string nombre, string paterno, string materno, string rut, string nomFant, string telefono, string email, string direccion, string where, string sidx, string sord, int inicio, int limite)
        {
            List<dto.BuscarDeudor> lst = new List<dto.BuscarDeudor>();
            lst = dao.Deudor.ListarBuscarDeudoresGrilla(codemp,  usrid,  nombre, paterno, materno, rut, nomFant, telefono, email, direccion, where, sidx, sord, inicio, limite);
            return lst;
        }

        public int ListarBuscarDeudoresGrillaCount(int codemp,  int usrid, string nombre, string paterno, string materno, string rut, string nomFant, string telefono, string email, string direccion, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarBuscarDeudoresCount(codemp, usrid,  nombre, paterno, materno, rut, nomFant, telefono, email, direccion, where, sidx, sord, inicio, limite);
        }

        public int BuscarIdDeudor(string rut, int codemp)
        {
            return dao.Deudor.BuscarIdDeudor( rut, codemp);
        }

        public int GuardarDeudor(int codemp, int ctcid, string nombre, string paterno, string materno, string rut, string nomFant, int idComuna, string partEmp, string direccion, string idSociedad, string quiebra, string nacional, int estadoDireccion, bool solicitaQuiebra)
        {
            int salida = dao.Deudor.GuardarDeudor(codemp, ctcid, nombre, paterno, materno, rut, nomFant, idComuna, partEmp, direccion, idSociedad, quiebra, nacional, estadoDireccion, solicitaQuiebra);
            //Marca al deudor en quiebra si tiene algun rol en el panel de quiebra
            dao.Deudor.MarcarQuiebraDeudor(codemp, ctcid);
            return salida;
        }

        public int EditarDeudorParcial(int codemp, int ctcid, string nombre, string paterno, string materno, string nomFant, int idComuna, string direccion, int estadoDireccion)
        {
            int salida = dao.Deudor.EditarDeudorParcial(codemp, ctcid, nombre, paterno, materno, nomFant, idComuna, direccion, estadoDireccion);
            return salida;
        }

        public string ListarTipoTelefono(int idioma)
        {
            return dao.Deudor.ListarTipoTelefono(idioma);
        }

        public string ListarEstadoTelefono(int idioma)
        {
            return dao.Deudor.ListarEstadoTelefono(idioma);
        }

        public List<Combobox> ListarTipoTelefonoC(int idioma)
        {
            return dao.Deudor.ListarTipoTelefonoC(idioma);
        }

        public List<Combobox> ListarEstadoTelefonoC(int idioma)
        {
            return dao.Deudor.ListarEstadoTelefonoC(idioma);
        }

        public List<TelefonoDeudor> ListarTelefonosDeudor(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarTelefonosDeudor(codemp, ctcid,  where, sidx, sord, inicio, limite);
        }

        public string GetCuentaProvcliBanco(int Pclid, int Tipo, int codemp)
        {
            return dao.Deudor.GetCuentaProvcliBanco(Pclid, Tipo, codemp);
        }

        public int GetDeudorCodigoCargaCount(int Pclid, int Ctcid, string EstCpbt, int CodCarga, int codemp)
        {
            return dao.Deudor.GetDeudorCodigoCargaCount(Pclid, Ctcid, EstCpbt, CodCarga, codemp);
        }

        public int ListarTelefonosDeudorCount(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarTelefonosDeudorCount(codemp, ctcid, where, sidx, sord, inicio, limite);
        }

        public List<EmailDeudor> ListarEmailDeudor(int codemp, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarEmailDeudor(codemp, ctcid, where, sidx, sord, inicio, limite);
        }

        public int ListarEmailDeudorCount(int codemp, int ctcid,string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarEmailDeudorCount(codemp, ctcid, where, sidx, sord, inicio, limite);
        }

        public string ListarTipoMail(int idioma)
        {
            return dao.Deudor.ListarTipoMail(idioma);
        }

        public List<Contacto> ListarContactos(int codemp, int ctcid, int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarContactos(codemp, ctcid,idioma, where, sidx, sord, inicio, limite);
        }

        public List<Combobox> ListarContactos(int codemp, int ctcid)
        {
            List<Contacto> lst = dao.Deudor.ListarContactos(codemp, ctcid);
            List<Combobox> lstCombo = new List<Combobox>();
            foreach(Contacto c in lst){
                lstCombo.Add( new Combobox {Text = c.Nombre, Value= c.Ddcid.ToString()});
            }
            return lstCombo;
        }

        public int ListarContactosCount(int codemp, int ctcid,int idioma, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarContactosCount(codemp, ctcid, idioma, where, sidx, sord, inicio, limite);
        }

        public  string ListarTipoContacto(int codemp, int idioma)
        {
            return dao.Deudor.ListarTipoContacto(codemp, idioma);
        }

        public List<Combobox> ListarTipoContactoC(int codemp, int idioma)
        {
            return dao.Deudor.ListarTipoContactoC(codemp, idioma);
        }

        public string ListarComunaGrilla()
        {
            return dao.Deudor.ListarComunaGrilla();
        }

        public int BuscarContacto(int codemp, int ctcid, int ticid, string nombre)
        {
            return dao.Deudor.BuscarContacto( codemp, ctcid,  ticid,  nombre);
        }

        #endregion

        #region "Deudor Cpbt"

        public List<Combobox> ListarTipoDocumento(int codemp, int idioma, string first)
        {
            return dao.Deudor.ListarTipoDocumento(codemp,idioma, first );
        }

        public List<DeudorDocumento> ListarDeudoresCpbt(int codemp, int idioma, string pclid, string nombre, string paterno, string materno, string rut, string nomFant, string tipoDocumento, string numero, string sbcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDeudoresCpbt(codemp, idioma, pclid, nombre, paterno, materno, rut, nomFant, tipoDocumento, numero, sbcid, where, sidx, sord, inicio, limite);
        }

        public int ListarDeudoresCpbtCount(int codemp, int idioma, string pclid, string nombre, string paterno, string materno, string rut, string nomFant, string tipoDocumento, string numero, string telefono, string email, string direccion, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDeudoresCpbtCount(codemp, idioma, pclid, nombre, paterno, materno, rut, nomFant, tipoDocumento, numero, telefono, email, direccion, where, sidx, sord, inicio, limite);
        }

        public List<dto.DeudorDocumento> ListarDeudoresMailCochaCpbt(int codemp, int idioma, string pclid, string ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDeudoresMailCochaCpbt(codemp, idioma, pclid, ctcid, where, sidx, sord, inicio, limite);
        }


        public int ListarDeudoresMailCochaCpbtCount(int codemp, int idioma, string pclid, string ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDeudoresMailCochaCpbtCount(codemp, idioma, pclid, ctcid, where, sidx, sord, inicio, limite);
        }

        public List<Combobox> ListarBancos(int codemp)
        {
            return dao.Deudor.ListarBancos(codemp);
        }

        public List<Combobox> ListarTipoBancos(int pclid)
        {
            return dao.Deudor.ListarTipoBancos(pclid);
        }

        public List<Combobox> ListarBancos()
        {
            return dao.Deudor.ListarBancos();
        }

        public List<Combobox> ListarEjecutivosMutual(int pclid)
        {
            return dao.Deudor.ListarEjecutivosMutual(pclid);
        }

        public static int InsertarCuentaEjecutivo(string cuenta, int idEjecutivo, int idBanco)
        {
            return dao.Deudor.InsertarCuentaEjecutivo(cuenta, idEjecutivo, idBanco);
        }

        public static int InsertarEjecutivoMutual(int pclid, string ejecutivo, string email, string oficina, int idEjecutivo)
        {
            return dao.Deudor.InsertarEjecutivoMutual(pclid, ejecutivo, email, oficina, idEjecutivo);
        }

        public static int EliminarCuentaEjecutivo(int cuenta)
        {
            return dao.Deudor.EliminarCuentaEjecutivo(cuenta);
        }

        public static int EliminarEjecutivoMutual(int idejecutivo)
        {
            return dao.Deudor.EliminarEjecutivoMutual(idejecutivo);
        }

        public List<dto.EjecutivoMutual> ListarEjecutivoMutual(int ejecutivo, int cuenta, int pclid)
        {
            return dao.Deudor.ListarEjecutivoMutual(ejecutivo, cuenta, pclid);
        }

        public BuscarDeudor BuscarDeudorCliente(int codemp, int pclid, int ctcid)
        {
           return dao.Deudor.BuscarDeudorCliente(codemp, pclid, ctcid);
        }

        #endregion

        #region "Documentos Deudor"

        public List<Autocomplete> ListarRutNombreDeudor(string nombre)
        {
            return dao.Deudor.ListarRutNombreDeudor(nombre);
        }

        public List<Autocomplete> ListarRutNombreDeudorPJ(string nombre)
        {
            return dao.Deudor.ListarRutNombreDeudorPJ(nombre);
        }

        public string ListarNombreDeudorPJ(string nombre)
        {
            return dao.Deudor.ListarNombreDeudorPJ(nombre);
        }

        public List<Combobox> ListarTipoDocumentosDeudor(int codemp, int idioma, string first)
        {
            return dao.Deudor.ListarTipoDocumentosDeudor(codemp, idioma, first);
        }

        public static List<DocumentoDeudor> ListarDocumentosDeudor(int codemp, int idioma, int ctcid, string pclid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocumentosDeudor(codemp, idioma,  ctcid,  pclid,  where,sidx,  sord,  inicio, limite);
        }

        public static int ListarDocumentosDeudorCount(int codemp, int idioma, int ctcid, string pclid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocumentosDeudorCount(codemp, idioma, ctcid, pclid, where, sidx, sord, inicio, limite);
        }

         public static string TraeTipoTipoDocumento(int codemp, int tipo)
        {
            return dao.Deudor.TraeTipoTipoDocumento(codemp, tipo);
        }

         public static int GuardarDocumentoDeudor(int codemp, int ctcid, int tipo, string nombre, string pclid)
         {
             return dao.Deudor.GuardarDocumentoDeudor(codemp, ctcid, tipo, nombre, pclid);
         }
         public List<Combobox> ListarCpbt(int codemp, int ctcid, string pclid, string estadoCPBT)
         {
             return dao.Deudor.ListarCpbt(codemp, ctcid, pclid, estadoCPBT);
         }
         public List<Combobox> ListarTiposImagenesCpbt(int codemp)
         {
             return dao.Deudor.ListarTiposImagenesCpbt(codemp);
         }

         public List<Combobox> ListarCuentaTipoBanco(int tipoBanco, int pclid)
         {
            return dao.Deudor.ListarCuentaTipoBanco(tipoBanco, pclid);
         }
        
         public static int NumCarteraClientesCpbtDocImagenes(int codemp, string pclid, int ctcid, int ccbid)
         {
             return dao.Deudor.NumCarteraClientesCpbtDocImagenes(codemp, pclid, ctcid, ccbid);
         }

         public static int GrabarImagenesCpbt(int codemp, string pclid, int ctcid, int ccbid, int cdid, int tpcid, string rutaImagen)
         {
             return dao.Deudor.GrabarImagenesCpbt(codemp, Int32.Parse(pclid), ctcid, ccbid, cdid, tpcid, rutaImagen);
         }
        #endregion

        #region "Agregar Gestiones"

        public static List<Combobox> ListarTelefonosContactos(int codemp, int ctcid)
        {
            return dao.Deudor.ListarTelefonosContactos(codemp, ctcid);
        }

        public static int EditarTelefonoPrioridad(int codemp, string ctcid, long numero, string estado, int prioridad)
        {
            return dao.Deudor.EditarTelefonoPrioridad(codemp, ctcid, numero, estado, prioridad);
        }

        public static int InsertarContacto(dto.Telefono obj)
        {
            return dao.Deudor.InsertarContacto(obj);
        }

        public static int EditarContacto(dto.Telefono obj)
        {
            return dao.Deudor.EditarContacto(obj);
        }

        public static int InsertarContacto(dto.Email obj)
        {
            return dao.Deudor.InsertarContacto(obj);
        }

        public static int EditarContacto(dto.Email obj)
        {
            return dao.Deudor.EditarContacto(obj);
        }

        public static int InsertarTelefonoContacto(dto.Telefono objTelefono)
        {
            return dao.Deudor.InsertarTelefonoContacto(objTelefono);
        }

        public static int InsertarEmailContacto(dto.Email obj)
        {
            return dao.Deudor.InsertarEmailContacto(obj);
        }

        public static int InsertarEmailContactoProvider(dto.Email obj)
        {
            return dao.Deudor.InsertarEmailContactoProvider(obj);
        }

        public static void EliminarTelefonoTodos(int codemp, string ctcid, string telefono)
        {
            dao.Deudor.BorrarTelefono(codemp,ctcid, telefono);
        }

        public static void EliminarEmailTodos(string codemp, string ctcid, string email)
        {
            dao.Deudor.BorrarEmailTodos(codemp, ctcid, email);
        }

        public static void EliminarEmailProv(string codemp, string ctcid, string email)
        {
            dao.Deudor.BorrarEmailProv(codemp, ctcid, email);
        }

        #endregion

        #region "Mover Cartera"

        public static List<dto.DocumentoMover> ListarDocumentosMoverGrilla(int codemp, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocumentosMoverGrilla(codemp, pclid, ctcid, where, sidx, sord, inicio, limite);
        }

        public static int ListarDocumentosMoverGrillaCount(int codemp, int pclid, int ctcid, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocumentosMoverGrillaCount(codemp, pclid, ctcid, where, sidx, sord, inicio, limite);
        }

        public static List<Autocomplete> ListarAutoEstadoCartera(string nombre)
        {
            return dao.Deudor.ListarAutoEstadoCartera(nombre);
        }

        public static bool MoverCartera(int codemp, int pclid, int ctcid, List<int> ids, string comentario, string estCpbt, int estado)
        {
            int salida = 0;
            foreach (int ccbid in ids)
            {
                salida = dao.Deudor.MoverCartera(codemp, pclid, ctcid, ccbid,comentario,estCpbt, estado);
                if (salida <= 0)
                {
                    return true ;
                }
            }

            return false;
        }

        #endregion

        #region "Castigo y devolucion"

        public static List<dto.DocumentoCastDev> ListarDocumentosCastDevGrilla(int codemp, int pclid, int ctcid, string estcpbt, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocumentosCastDevGrilla(codemp, pclid, ctcid, estcpbt, where, sidx, sord, inicio, limite);
        }

        public static int ListarDocumentosCastDevGrillaCount(int codemp, int pclid, int ctcid, string estcpbt, string where, string sidx, string sord, int inicio, int limite)
        {
            return dao.Deudor.ListarDocumentosCastDevGrillaCount(codemp, pclid, ctcid, estcpbt, where, sidx, sord, inicio, limite);
        }
        public static List<dto.DocumentoCastDev> ListarDocsSelectedCastigoDevolucion(List<String> ids)
        {
            List<dto.DocumentoCastDev> lst = new List<dto.DocumentoCastDev>();
            string[] splitted;
            
            if (ids.Count > 0)
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    splitted = ids[i].Split('|');
                    lst.Add(new dto.DocumentoCastDev()
                    {
                        Pclid = Int32.Parse(splitted[0]),
                        Ctcid = Int32.Parse(splitted[1]),
                        Ccbid = Int32.Parse(splitted[2]),
                        Tipo = splitted[5],
                        Numero = splitted[7],
                        Moneda = splitted[6],
                        Monto = decimal.Parse(splitted[3]),
                        Saldo = decimal.Parse(splitted[4]),
                        Asignado = decimal.Parse(splitted[10]),
                        UltimoEstado = splitted[12],
                        Estado = splitted[9],
                        EstadoCpbt = splitted[11],
                        FechaVencimiento = DateTime.Parse(splitted[14]),
                        FechaAsignacion = DateTime.Parse(splitted[8]),
                        Deudor = splitted[13],
                        Asegurado = splitted.Length > 15 ? splitted[15]: "",
                        RolNumero = splitted.Length > 17 ? splitted[17]: "",
                        RolId = Int32.Parse(splitted.Length > 16 ? splitted[16] : "0")
                    });
                }
            }

            return lst;
        }
        #endregion


        public static void InsertarCategoriaDeudor(int codemp, int ctcid, string categoria, int usrid)
        {
            dao.Deudor.InsertarCategoriaDeudor(codemp, ctcid, categoria, usrid);
        }
    }
}
