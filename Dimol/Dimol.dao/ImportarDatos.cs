using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
 
namespace Dimol.dao{
    public class ImportarDatos{
    #region " Propiedades "
        public DatSet Ds = new DatSet();
        public Funciones Func = new Funciones();
        public string Tabla { get; set; }
    #endregion
        /*
        public bool Importar() {
            bool Ret = false;
            if( Tabla.ToLower() == "empresa_sucursal" ){
                Ret = Empresa_Sucursal();
            }
 
            if( Tabla.ToLower() == "pais" ){
                Ret = Pais();
            }
 
            if( Tabla.ToLower() == "region" ){
                Ret = Region();
            }
 
            if( Tabla.ToLower() == "ciudad" ){
                Ret = Ciudad();
            }
 
            if( Tabla.ToLower() == "comuna" ){
                Ret = Comuna();
            }
 
            if( Tabla.ToLower() == "idiomas" ){
                Ret = Idiomas();
            }
 
            if( Tabla.ToLower() == "monedas" ){
                Ret = Monedas();
            }
 
            if( Tabla.ToLower() == "monedas_valores" ){
                Ret = Monedas_Valores();
            }
 
            if( Tabla.ToLower() == "cuentas_padres" ){
                Ret = Cuentas_Padres();
            }
 
            if( Tabla.ToLower() == "cuentas_padres_idiomas" ){
                Ret = Cuentas_Padres_Idiomas();
            }
 
            if( Tabla.ToLower() == "plan_cuentas" ){
                Ret = Plan_Cuentas();
            }
 
            if( Tabla.ToLower() == "plan_cuentas_idiomas" ){
                Ret = Plan_Cuentas_Idiomas();
            }
 
            if( Tabla.ToLower() == "cargos" ){
                Ret = Cargos();
            }
 
            if( Tabla.ToLower() == "cargos_idiomas" ){
                Ret = Cargos_Idiomas();
            }
 
            if( Tabla.ToLower() == "estado_empleado" ){
                Ret = Estados_Empleados();
            }
 
            if( Tabla.ToLower() == "estado_empleado_idiomas" ){
                Ret = Estados_Empleados_Idiomas();
            }
 
            if( Tabla.ToLower() == "tipos_contrato" ){
                Ret = Tipos_Contrato();
            }
 
            if( Tabla.ToLower() == "tipos_contrato_idiomas" ){
                Ret = Tipos_Contrato_Idiomas();
            }
 
            if( Tabla.ToLower() == "afp" ){
                Ret = Afp();
            }
 
            if( Tabla.ToLower() == "caja_compensacion" ){
                Ret = Caja_Compensacion();
            }
 
            if( Tabla.ToLower() == "isapres" ){
                Ret = Isapres();
            }
 
            if( Tabla.ToLower() == "usuarios" ){
                Ret = Usuarios();
            }
 
            if( Tabla.ToLower() == "usuarios_sucursal" ){
                Ret = Usuarios_Sucursal();
            }
 
 
            if( Tabla.ToLower() == "empleados" ){
                Ret = Empleados();
            }
 
            if( Tabla.ToLower() == "empleados_datos_contrato" ){
                Ret = Empleados_Datos_Contrato();
            }
 
            if( Tabla.ToLower() == "empleados_foto" ){
                Ret = Empleados_Foto();
            }
 
            if( Tabla.ToLower() == "tipo_asistencia" ){
                Ret = Tipo_Asistencia();
            }
 
            if( Tabla.ToLower() == "tipo_asistencia_idiomas" ){
                Ret = Tipo_Asistencia_Idiomas();
            }
 
            if( Tabla.ToLower() == "empleado_asistencia" ){
                Ret = Empleado_Asistencia();
            }
 
            if( Tabla.ToLower() == "empleado_asistencia_horas" ){
                Ret = Empleado_Asistencia_Horas();
            }
 
            if( Tabla.ToLower() == "tipos_provcli" ){
                Ret = tipos_provcli();
            }
 
            if( Tabla.ToLower() == "tipos_provcli_idiomas" ){
                Ret = Tipos_Provcli_Idiomas();
            }
 
            if( Tabla.ToLower() == "giros" ){
                Ret = Giros();
            }
 
            if( Tabla.ToLower() == "giros_idiomas" ){
                Ret = Giros_Idiomas();
            }
 
            if( Tabla.ToLower() == "tipos_contacto" ){
                Ret = Tipos_Contacto();
            }
 
            if( Tabla.ToLower() == "tipos_contacto_idiomas" ){
                Ret = Tipos_Contacto_Idiomas();
            }
 
            if( Tabla.ToLower() == "provcli" ){
                Ret = ProvCli();
            }
 
            if( Tabla.ToLower() == "bancos" ){
                Ret = Bancos();
            }
 
            if( Tabla.ToLower() == "provcli_sucursal" ){
                Ret = ProvCli_Sucursal();
            }
 
            if( Tabla.ToLower() == "provcli_impuestos" ){
                Ret = ProvCli_Impuestos();
            }
 
            if( Tabla.ToLower() == "provcli_sucursal_contacto" ){
                Ret = ProvCli_Sucursal_Contacto();
            }
 
            if( Tabla.ToLower() == "provcli_codigo_carga" ){
                Ret = ProvCli_Codigo_Carga();
            }
 
            if( Tabla.ToLower() == "cartera_clientes" ){
                Ret = Cartera_Clientes();
            }
 
            if( Tabla.ToLower() == "cartera_clientes_contacto" ){
                Ret = Cartera_Clientes_Contacto();
            }
 
            return Ret;
 
        }
         * 
      
     
              public bool Importar(DataSet ds) {
 
                  bool Ret = false;
                  if(Tabla.ToLower() == "configuracion_sistema" ){
                      Ret = Configuracion_sistema(ds);
                  }
 
                  if( Tabla.ToLower() == "tabla_importacion" ){
                      Ret = Tabla_Importacion(ds);
                  }
 
                  if( Tabla.ToLower() == "empresa" ){
                      Ret = Empresa(ds);
                  }
 
                  if( Tabla.ToLower() == "empresa_configuracion" ){
                      Ret = Empresa_Configuracion(ds);
                  }
 
                  if( Tabla.ToLower() == "errores" ){
                      Ret = Errores(ds);
                  }
 
                  if( Tabla.ToLower() == "errores_idiomas" ){
                      Ret = Errores_idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "etiquetas" ){
                      Ret = Etiquetas(ds);
                  }
 
                  if( Tabla.ToLower() == "etiquetas_idiomas" ){
                      Ret = Etiquetas_idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "menu" ){
                      Ret = Menu(ds);
                  }
 
                  if( Tabla.ToLower() == "menu_idiomas" ){
                      Ret = Menu_Idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "treeview" ){
                      Ret = TreeView(ds);
                  }
 
                  if( Tabla.ToLower() == "treeview_idiomas" ){
                      Ret = TreeView_Idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "perfiles" ){
                      Ret = Perfiles(ds);
                  }
 
                  if( Tabla.ToLower() == "perfiles_idiomas" ){
                      Ret = Perfiles_Idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "usuarios" ){
                      Ret = Usuarios(ds);
                  }
 
                  if( Tabla.ToLower() == "usuarios_sucursal" ){
                      Ret = Usuarios_Sucursal(ds);
                  }
 
                  if( Tabla.ToLower() == "clasificacion_cpbtdoc" ){
                      Ret = Clasificacion_CpbtDoc(ds);
                  }
 
                  if( Tabla.ToLower() == "empresa_sucursal" ){
                      Ret = Empresa_Sucursal(ds);
                  }
 
                  if( Tabla.ToLower() == "pais" ){
                      Ret = Pais(ds);
                  }
 
                  if( Tabla.ToLower() == "region" ){
                      Ret = Region(ds);
                  }
 
                  if( Tabla.ToLower() == "ciudad" ){
                      Ret = Ciudad(ds);
                  }
 
                  if( Tabla.ToLower() == "comuna" ){
                      Ret = Comuna(ds);
                  }
 
                  if( Tabla.ToLower() == "perfiles_menu" ){
                      Ret = Perfil_Menu(ds);
                  }
 
                  if( Tabla.ToLower() == "idiomas" ){
                      Ret = Idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "materia_judicial" ){
                      Ret = Materia_Judicial(ds);
                  }
 
                  if( Tabla.ToLower() == "materia_judicial_idiomas" ){
                      Ret = Materia_Judicial_idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "tipos_cpbtdoc" ){
                      Ret = Tipos_CpbtDoc(ds);
                  }
 
                  if( Tabla.ToLower() == "tipos_cpbtdoc_idiomas" ){
                      Ret = Tipos_CpbtDoc_Idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "tipos_imagenes_cpbtdoc" ){
                      Ret = Tipos_Imagenes_CpbtDoc(ds);
                  }
 
                  if( Tabla.ToLower() == "tipos_imagenes_cpbtdoc_idiomas" ){
                      Ret = Tipos_Imagenes_CpbtDoc_Idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "reportes" ){
                      Ret = Reportes(ds);
                  }
 
                  if( Tabla.ToLower() == "reportes_idiomas" ){
                      Ret = Reportes_Idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "tipos_cpbtdoc_report" ){
                      Ret = Tipos_Cpbtdoc_Report(ds);
                  }
 
                  if( Tabla.ToLower() == "tipos_insumo" ){
                      Ret = Tipos_Insumo(ds);
                  }
 
                  if( Tabla.ToLower() == "tipos_insumo_idiomas" ){
                      Ret = Tipos_Insumo_Idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "supercategorias" ){
                      Ret = SuperCategorias(ds);
                  }
 
                  if( Tabla.ToLower() == "supercategorias_idioma" ){
                      Ret = SuperCategorias_Idiomas(ds);
                  }
 
                  if( Tabla.ToLower() == "categorias" ){
                      Ret = Categorias(ds);
                  }
 
                  if( Tabla.ToLower() == "supcat_categorias" ){
                      Ret = Supcat_Categorias(ds);
                  }
 
                  if( Tabla.ToLower() == "periodos_contables" ){
                      Ret = Periodos_Contables(ds);
                  }
 
                  if( Tabla.ToLower() == "periodos_contables_meses" ){
                      Ret = Periodos_Contables_Meses(ds);
                  }
 
                  if( Tabla.ToLower() == "help" ){
                      Ret = Help(ds);
                  }
 
                  if( Tabla.ToLower() == "help_idiomas" ){
                      Ret = Help_Idiomas(ds);
                  }

                  return Ret;
 
              }  
 /*
              private bool Configuracion_sistema(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Configuracion_Sistema");
                          sp.AgregarParametro("cfs_cfsid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("cfs_nombre", ds.Tables[0].Rows[i](1));
                          if( IsDBNull(ds.Tables[0].Rows[i](2)) == false ){
                              sp.AgregarParametro("cfs_valnum", ds.Tables[0].Rows[i](2));
                          }else{
                              sp.AgregarParametro("cfs_valnum", 0);
                          }
                          if( IsDBNull(ds.Tables[0].Rows[i](3)) == false ){
                              sp.AgregarParametro("cfs_valtxt", ds.Tables[0].Rows[i](3));
                          }else{
                              sp.AgregarParametro("cfs_valtxt", "");
                          }
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tabla_Importacion(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tabla_Importacion");
                          sp.AgregarParametro("tbi_nombre", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tbi_orden", ds.Tables[0].Rows[i](1));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Empresa(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Empresa");
                          sp.AgregarParametro("emp_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("emp_rut", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("emp_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("emp_rutrepleg", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("emp_replegal", ds.Tables[0].Rows[i](4));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Empresa_Configuracion(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Empresa_Configuracion");
                          sp.AgregarParametro("emc_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("emc_emcid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("emc_nombre", ds.Tables[0].Rows[i](2));
 
                          if( IsDBNull(ds.Tables[0].Rows[i](3)) == false ){
                              sp.AgregarParametro("emc_valnum", ds.Tables[0].Rows[i](3));
                          }else{
                              sp.AgregarParametro("emc_valnum", 0);
                          }
                          if( IsDBNull(ds.Tables[0].Rows[i](4)) == false ){
                              sp.AgregarParametro("emc_valtxt", ds.Tables[0].Rows[i](4));
                          }else{
                              sp.AgregarParametro("emc_valtxt", "");
                          }
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Errores(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Errores");
                          sp.AgregarParametro("err_errid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("err_codigo", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("err_descripcion", ds.Tables[0].Rows[i](2));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Errores_idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Errores_Idiomas");
                          sp.AgregarParametro("eri_errid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("eri_idid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("eri_descripcion", ds.Tables[0].Rows[i](2));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Etiquetas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Etiquetas");
                          sp.AgregarParametro("etq_etqid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("etq_codigo", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("etq_descripcion", ds.Tables[0].Rows[i](2));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Etiquetas_idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Etiquetas_Idioma");
                          sp.AgregarParametro("eti_etiid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("eti_idid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("eti_descripcion", ds.Tables[0].Rows[i](2));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Menu(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Menu");
                          sp.AgregarParametro("men_menid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("men_nombre", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("men_imagen", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("men_orden", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("men_directorio", ds.Tables[0].Rows[i](4));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Menu_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Menu_Idiomas");
                          sp.AgregarParametro("mni_menid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("mni_idid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("mni_nombre", ds.Tables[0].Rows[i](2));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool TreeView(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Treeview");
                          sp.AgregarParametro("trv_trvid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("trv_padid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("trv_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("trv_imagen", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("trv_ventana", ds.Tables[0].Rows[i](4));
                          sp.AgregarParametro("trv_orden", ds.Tables[0].Rows[i](5));
                          sp.AgregarParametro("trv_menid", ds.Tables[0].Rows[i](6));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool TreeView_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Treeview_Idiomas");
                          sp.AgregarParametro("tvi_trvid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tvi_idid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tvi_idiomas", ds.Tables[0].Rows[i](2));
 
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Perfiles(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Perfiles");
                          sp.AgregarParametro("prf_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("prf_prfid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("prf_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("prf_administrador", ds.Tables[0].Rows[i](3));
 
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Perfiles_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Perfiles_Idiomas");
                          sp.AgregarParametro("pfi_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("pfi_prfid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("pfi_idid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("pfi_nombre", ds.Tables[0].Rows[i](3));
 
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Usuarios(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
                  DateTime fec;
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          fec = func.FechaGenerica(Convert.ToDateTime(Left(ds.Tables[0].Rows[i]("usr_feccambio").ToString(), 10)));
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Usuarios");
                          sp.AgregarParametro("usr_codemp", ds.Tables[0].Rows[i]("usr_codemp"));
                          sp.AgregarParametro("usr_usrid", ds.Tables[0].Rows[i]("usr_usrid"));
                          sp.AgregarParametro("usr_nombre", ds.Tables[0].Rows[i]("usr_nombre"));
                          sp.AgregarParametro("usr_login", ds.Tables[0].Rows[i]("usr_login"));
                          sp.AgregarParametro("usr_password", ds.Tables[0].Rows[i]("usr_password"));
                          sp.AgregarParametro("usr_mail", ds.Tables[0].Rows[i]("usr_mail"));
                          sp.AgregarParametro("usr_tipquest", ds.Tables[0].Rows[i]("usr_tipquest"));
                          sp.AgregarParametro("usr_answer", ds.Tables[0].Rows[i]("usr_answer"));
                          sp.AgregarParametro("usr_sucid", ds.Tables[0].Rows[i]("usr_sucid"));
                          sp.AgregarParametro("usr_prfid", ds.Tables[0].Rows[i]("usr_prfid"));
                          sp.AgregarParametro("usr_permisos", ds.Tables[0].Rows[i]("usr_permisos"));
                          sp.AgregarParametro("usr_campass", ds.Tables[0].Rows[i]("usr_campass"));
                          sp.AgregarParametro("usr_feccambio", fec);
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Usuarios_Sucursal(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Usuarios_Sucursal");
                          sp.AgregarParametro("uss_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("uss_usrid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("uss_sucid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("uss_default", ds.Tables[0].Rows[i](3));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Clasificacion_CpbtDoc(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Clasificacion_CpbtDoc");
                          sp.AgregarParametro("clb_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("clb_clbid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("clb_codigo", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("clb_tipcpbtdoc", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("clb_tipprod", ds.Tables[0].Rows[i](4));
                          sp.AgregarParametro("clb_costos", ds.Tables[0].Rows[i](5));
                          sp.AgregarParametro("clb_selcpbt", ds.Tables[0].Rows[i](6));
                          sp.AgregarParametro("clb_cartcli", ds.Tables[0].Rows[i](7));
                          sp.AgregarParametro("clb_contable", ds.Tables[0].Rows[i](8));
                          sp.AgregarParametro("clb_selapl", ds.Tables[0].Rows[i](9));
                          sp.AgregarParametro("clb_aplica", ds.Tables[0].Rows[i](10));
                          sp.AgregarParametro("clb_cptoctbl", ds.Tables[0].Rows[i](11));
                          sp.AgregarParametro("clb_findeuda", ds.Tables[0].Rows[i](12));
                          sp.AgregarParametro("clb_cancela", ds.Tables[0].Rows[i](13));
                          sp.AgregarParametro("clb_libcompra", ds.Tables[0].Rows[i](14));
                          sp.AgregarParametro("clb_cambiodoc", ds.Tables[0].Rows[i](15));
                          sp.AgregarParametro("clb_remesa", ds.Tables[0].Rows[i](16));
                          sp.AgregarParametro("clb_tipsel", ds.Tables[0].Rows[i](17));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Empresa_Sucursal(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Empresa_Sucursal");
                          sp.AgregarParametro("esu_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("esu_sucid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("esu_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("esu_comid", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("esu_direccion", ds.Tables[0].Rows[i](4));
                          sp.AgregarParametro("esu_telefono", ds.Tables[0].Rows[i](5));
                          sp.AgregarParametro("esu_fax", ds.Tables[0].Rows[i](6));
                          sp.AgregarParametro("esu_mail", ds.Tables[0].Rows[i](7));
                          sp.AgregarParametro("esu_css", ds.Tables[0].Rows[i](8));
                          sp.AgregarParametro("esu_matriz", ds.Tables[0].Rows[i](9));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Pais(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Pais");
                          sp.AgregarParametro("id", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("nombre", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("codtel", ds.Tables[0].Rows[i](2));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Region(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Region");
                          sp.AgregarParametro("reg_paiid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("reg_regid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("reg_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("reg_orden", ds.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Ciudad(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Ciudad");
                          sp.AgregarParametro("ciu_regid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("ciu_ciuid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("ciu_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("ciu_codarea", ds.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Comuna(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Comuna");
                          sp.AgregarParametro("com_ciuid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("com_comid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("com_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("com_codpost", ds.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Perfil_Menu(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Perfiles_Menu");
                          sp.AgregarParametro("pfm_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("pfm_prfid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("pfm_trvid", ds.Tables[0].Rows[i](2));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Idiomas");
                          sp.AgregarParametro("idi_idid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("idi_nombre", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("idi_idisrc", ds.Tables[0].Rows[i](2));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Materia_Judicial(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Materia_Judicial");
                          sp.AgregarParametro("esj_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("esj_esjid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("esj_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("esj_orden", ds.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Materia_Judicial_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Materia_Judicial_Idiomas");
                          sp.AgregarParametro("mji_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("mji_esjid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("mji_idid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("mji_nombre", ds.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_CpbtDoc(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_CpbtDoc");
                          sp.AgregarParametro("tpc_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tpc_tpcid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tpc_clbid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tpc_nombre", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("tpc_talonario", ds.Tables[0].Rows[i](4));
                          sp.AgregarParametro("tpc_ultnum", ds.Tables[0].Rows[i](5));
                          sp.AgregarParametro("tpc_lineas", ds.Tables[0].Rows[i](6));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_CpbtDoc_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_CpbtDoc_Idiomas");
                          sp.AgregarParametro("tci_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tci_tpcid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tci_idid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tci_nombre", ds.Tables[0].Rows[i](3));
                   
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Imagenes_CpbtDoc(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
 
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Imagenes_Cpbtdoc");
                          sp.AgregarParametro("tpc_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tpc_tpcid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tpc_nombre", ds.Tables[0].Rows[i](2));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Imagenes_CpbtDoc_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Imagenes_CpbtDoc_Idiomas");
                          sp.AgregarParametro("tpc_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tpc_tpcid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tpi_idid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tpc_nombre", ds.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Reportes(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Reportes");
                          sp.AgregarParametro("rpt_trvid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("rpt_rptid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("rpt_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("rpt_tipo", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("rpt_cartera", DBNull.Value);
                          sp.AgregarParametro("rpt_tipcart", DBNull.Value);
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Reportes_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Reportes_Idiomas");
                          sp.AgregarParametro("rti_trvid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("rti_rptid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("rti_idid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("rti_nombre", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("rti_fisico", ds.Tables[0].Rows[i](4));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Cpbtdoc_Report(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_CpbtDoc_Report");
                          sp.AgregarParametro("trc_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("trc_tpcid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("trc_trcid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("trc_reporte", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("trc_nombre", ds.Tables[0].Rows[i](4));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Insumo(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Insumos");
                          sp.AgregarParametro("tpi_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tpi_tipid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tpi_nombre", ds.Tables[0].Rows[i](2));
                  
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Insumo_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Insumos_Idiomas");
                          sp.AgregarParametro("tii_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tii_tipid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tii_idid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tii_nombre", ds.Tables[0].Rows[i](3));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool SuperCategorias(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_SuperCategorias");
                          sp.AgregarParametro("spc_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("spc_spcid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("spc_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("spc_orden", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("spc_utilizacion", ds.Tables[0].Rows[i](4));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool SuperCategorias_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_SuperCategorias_Idiomas");
                          sp.AgregarParametro("sci_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("sci_spcid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("sci_idiid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("sci_nombre", ds.Tables[0].Rows[i](3));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Categorias(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Categorias");
                          sp.AgregarParametro("cat_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("cat_catid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("cat_nombre", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("cat_utilizacion", ds.Tables[0].Rows[i](3));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Categorias_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Categorias_Idiomas");
                          sp.AgregarParametro("cai_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("cai_catid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("cai_idiid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("cai_nombre", ds.Tables[0].Rows[i](3));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Supcat_Categorias(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_SupCat_Categorias");
                          sp.AgregarParametro("scc_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("scc_spcid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("scc_catid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("scc_orden", ds.Tables[0].Rows[i](3));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Periodos_Contables(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Periodos_Contables");
                          sp.AgregarParametro("pec_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("pec_anio", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("pec_habilitado", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("pec_finalizado", ds.Tables[0].Rows[i](3));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Periodos_Contables_Meses(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                  
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Periodos_Contables_Meses");
                          sp.AgregarParametro("pcm_codemp", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("pcm_anio", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("pcm_mes", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("pcm_inicio", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("pcm_fin", ds.Tables[0].Rows[i](4));
                          sp.AgregarParametro("pcm_apeini", ds.Tables[0].Rows[i](5));
                          sp.AgregarParametro("pcm_apefin", ds.Tables[0].Rows[i](6));
                          sp.AgregarParametro("pcm_ingini", ds.Tables[0].Rows[i](7));
                          sp.AgregarParametro("pcm_ingfin", ds.Tables[0].Rows[i](8));
                          sp.AgregarParametro("pcm_egreini", ds.Tables[0].Rows[i](9));
                          sp.AgregarParametro("pcm_egrefin", ds.Tables[0].Rows[i](10));
                          sp.AgregarParametro("pcm_trasini", ds.Tables[0].Rows[i](11));
                          sp.AgregarParametro("pcm_trasfin", ds.Tables[0].Rows[i](12));
                          sp.AgregarParametro("pcm_habilitado", ds.Tables[0].Rows[i](13));
                          sp.AgregarParametro("pcm_finalizado", ds.Tables[0].Rows[i](14));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
 
 
 
 
 
              private bool Pais() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Pais");
                          sp.AgregarParametro("id", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("nombre", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("codtel", dsOri.Tables[0].Rows[i](2));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Region() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Region");
                          sp.AgregarParametro("reg_paiid", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("reg_regid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("reg_nombre", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("reg_orden", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Ciudad() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Ciudad");
                          sp.AgregarParametro("ciu_regid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("ciu_ciuid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("ciu_nombre", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("ciu_codarea", dsOri.Tables[0].Rows[i](4));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Comuna() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Comuna");
                          sp.AgregarParametro("com_ciuid", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("com_comid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("com_nombre", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("com_codpost", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Idiomas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Idiomas");
                          sp.AgregarParametro("idi_idid", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("idi_nombre", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("idi_idisrc", "");
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Empresa_Sucursal() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insert_Empresa_Sucursal");
                          sp.AgregarParametro("esu_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("esu_sucid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("esu_nombre", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("esu_comid", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("esu_direccion", dsOri.Tables[0].Rows[i](4));
                          sp.AgregarParametro("esu_telefono", dsOri.Tables[0].Rows[i](5));
                          sp.AgregarParametro("esu_fax", dsOri.Tables[0].Rows[i](6));
                          sp.AgregarParametro("esu_mail", dsOri.Tables[0].Rows[i](7));
                          sp.AgregarParametro("esu_css", dsOri.Tables[0].Rows[i](8));
                          sp.AgregarParametro("esu_matriz", "S");
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Monedas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Monedas");
                          sp.AgregarParametro("mon_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("mon_codmon", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("mon_nombre", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("mon_simbolo", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("mon_default", "N");
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Monedas_Valores() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Monedas_Valores");
                          sp.AgregarParametro("mnv_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("mnv_codmon", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("mnv_fecha", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("mnv_valor", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Cuentas_Padres() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Cuentas_Padres");
                          sp.AgregarParametro("ctp_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("ctp_ctpid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("ctp_codigo", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("ctp_nombre", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("ctp_agrupa", dsOri.Tables[0].Rows[i](4));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Cuentas_Padres_Idiomas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Cuentas_Padres_Idiomas");
                          sp.AgregarParametro("cpi_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("cpi_ctpid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("cpi_idid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("cpi_nombre", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Plan_Cuentas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Plan_Cuentas");
                          sp.AgregarParametro("pct_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("pct_pctid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("pct_codigo", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("pct_nombre", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("pct_ctpid", dsOri.Tables[0].Rows[i](4));
                          sp.AgregarParametro("pct_ccs", dsOri.Tables[0].Rows[i](5));
                          sp.AgregarParametro("pct_activos", dsOri.Tables[0].Rows[i](6));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Plan_Cuentas_Idiomas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Plan_Cuentas_Idiomas");
                          sp.AgregarParametro("pci_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("pci_pctid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("pci_idid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("pci_nombre", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Cargos() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Cargos");
                          sp.AgregarParametro("car_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("car_carid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("car_nombre", dsOri.Tables[0].Rows[i](2));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Cargos_Idiomas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Cargos_Idiomas");
                          sp.AgregarParametro("car_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("car_carid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("cai_idid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("cai_nombre", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Estados_Empleados() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Estados_Empleado");
                          sp.AgregarParametro("eem_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("eem_eemid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("eem_nombre", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("eem_accion", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Estados_Empleados_Idiomas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Estados_Empleado_Idiomas");
                          sp.AgregarParametro("eei_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("eei_eemid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("eei_idid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("eei_nombre", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Contrato() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Contrato");
                          sp.AgregarParametro("tic_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tic_ticid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tic_nombre", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tic_duracion", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("tic_indefinido", dsOri.Tables[0].Rows[i](4));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Contrato_Idiomas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Contrato_Idiomas");
                          sp.AgregarParametro("tci_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tci_ticid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tci_idid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tci_nombre", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Afp() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_AFP");
                          sp.AgregarParametro("afp_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("afp_afpid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("afp_rut", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("afp_nombre", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("afp_pctid", dsOri.Tables[0].Rows[i](4));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Caja_Compensacion() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Caja_Compensacion");
                          sp.AgregarParametro("cjc_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("cjc_cjcid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("cjc_rut", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("cjc_nombre", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("cjc_pctid", dsOri.Tables[0].Rows[i](4));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Isapres() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Isapres");
                          sp.AgregarParametro("isp_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("isp_ispid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("isp_rut", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("isp_nombre", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("isp_pctid", dsOri.Tables[0].Rows[i](4));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Usuarios() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  Horus.Usuarios UsuH = new Horus.Usuarios();
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      strSql = "SELECT usuarios.usu_codemp,   ";
                      strSql = strSql + " usuarios.usu_usuid,   ";
                      strSql = strSql + " usuarios.usu_nombre,   ";
                      strSql = strSql + " usuarios.usu_usuario,   ";
                      strSql = strSql + " usuarios.usu_password,   ";
                      strSql = strSql + " usuarios.usu_fecing,   ";
                      strSql = strSql + " usuarios.usu_logok,   ";
                      strSql = strSql + "  usuarios.usu_logbad,   ";
                      strSql = strSql + "  getdate(),   ";
                      strSql = strSql + "  usuarios.usu_fecbloqueo,   ";
                      strSql = strSql + "  usuarios_anexo.usa_mail,   ";
                      strSql = strSql + "  1,   ";
                      strSql = strSql + "  usuarios_anexo.usa_respuesta,  ";
                      strSql = strSql + "   usuarios.usu_sucid,   ";
                      strSql = strSql + "  2,   ";
                      strSql = strSql + "  1,   ";
                      strSql = strSql + "     Usuarios.usu_estado";
                      strSql = strSql + " FROM usuarios,   ";
                      strSql = strSql + "           usuarios_anexo";
                      strSql = strSql + " WHERE ( usuarios_anexo.usa_codemp = usuarios.usu_codemp ) and  ";
                      strSql = strSql + "     ( usuarios_anexo.usa_usuid = usuarios.usu_usuid ) and usu_usuid > 1 ";
                      dsOri = Hds.ConsultaBDOri(strSql);
                
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Usuarios");
                          sp.AgregarParametro("usr_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("usr_usrid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("usr_nombre", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("usr_login", UsuH.Encripta(LCase(dsOri.Tables[0].Rows[i](3))));
                          sp.AgregarParametro("usr_password", UsuH.Encripta(LCase(dsOri.Tables[0].Rows[i](4))));
                          sp.AgregarParametro("usr_mail", dsOri.Tables[0].Rows[i](10));
                          sp.AgregarParametro("usr_tipquest", dsOri.Tables[0].Rows[i](11));
                          sp.AgregarParametro("usr_answer", dsOri.Tables[0].Rows[i](12));
                          sp.AgregarParametro("usr_sucid", dsOri.Tables[0].Rows[i](13));
                          sp.AgregarParametro("usr_prfid", dsOri.Tables[0].Rows[i](14));
                          sp.AgregarParametro("usr_permisos", dsOri.Tables[0].Rows[i](15));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Usuarios_Sucursal() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Usuarios_Sucursal");
                          sp.AgregarParametro("uss_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("uss_usrid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("uss_sucid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("uss_default", "S");
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Empleados() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "SELECT empleados.emp_codemp,   ";
                      strSql = strSql + "      empleados.emp_emplid,   ";
                      strSql = strSql + "     empleados.emp_rut,   ";
                      strSql = strSql + "    empleados.emp_nombre, ";
                      strSql = strSql + "    empleados.emp_apepat,  ";
                      strSql = strSql + "    empleados.emp_apemat,  ";
                      strSql = strSql + "    empleados.emp_eemid,  ";
                      strSql = strSql + "    empleados.emp_comid,  ";
                      strSql = strSql + "    empleados.emp_direccion,  ";
                      strSql = strSql + "    empleados.emp_telefono,  ";
                      strSql = strSql + "    empleados.emp_celular,   ";
                      strSql = strSql + "    empleados.emp_mail,   ";
                      strSql = strSql + "   empleados.emp_fecing,   ";
                      strSql = strSql + "   empleados.emp_fecfin,  ";
                      strSql = strSql + "    empleados.emp_sucid,   ";
                      strSql = strSql + "   usuarios_empleado.use_usuid,  ";
                      strSql = strSql + "   empleados.emp_huella,   ";
                      strSql = strSql + "          Empleados.emp_digito ";
                      strSql = strSql + "  FROM {oj usuarios_empleado RIGHT OUTER JOIN empleados ON usuarios_empleado.use_codemp = empleados.emp_codemp AND usuarios_empleado.use_emplid = empleados.emp_emplid}";
 
 
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Empleados");
                          sp.AgregarParametro("epl_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("epl_emplid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("epl_rut", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("epl_nombre", UCase(dsOri.Tables[0].Rows[i](3)));
                          sp.AgregarParametro("epl_apepat", UCase(dsOri.Tables[0].Rows[i](4)));
                          sp.AgregarParametro("epl_apemat", UCase(dsOri.Tables[0].Rows[i](5)));
                          sp.AgregarParametro("epl_eemid", dsOri.Tables[0].Rows[i](6));
                          sp.AgregarParametro("epl_comid", dsOri.Tables[0].Rows[i](7));
                          sp.AgregarParametro("epl_direccion", dsOri.Tables[0].Rows[i](8));
                          sp.AgregarParametro("epl_telefono", dsOri.Tables[0].Rows[i](9));
                          sp.AgregarParametro("epl_celular", dsOri.Tables[0].Rows[i](10));
                          sp.AgregarParametro("epl_mail", LCase(dsOri.Tables[0].Rows[i](11)));
                          sp.AgregarParametro("epl_fecing", dsOri.Tables[0].Rows[i](12));
                          sp.AgregarParametro("epl_fecfin", dsOri.Tables[0].Rows[i](13));
                          sp.AgregarParametro("epl_sucid", dsOri.Tables[0].Rows[i](14));
                          sp.AgregarParametro("epl_usrid", dsOri.Tables[0].Rows[i](15));
                          sp.AgregarParametro("epl_huella", dsOri.Tables[0].Rows[i](16));
                          sp.AgregarParametro("epl_digito", dsOri.Tables[0].Rows[i](17));
 
                 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                 
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Empleados_Datos_Contrato() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Empleados_Datos_Contrato");
                          sp.AgregarParametro("edc_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("edc_emplid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("edc_carid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("edc_ticid", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("edc_afpid", dsOri.Tables[0].Rows[i](4));
                          sp.AgregarParametro("edc_afp", dsOri.Tables[0].Rows[i](5));
                          sp.AgregarParametro("edc_afc", dsOri.Tables[0].Rows[i](6));
                          sp.AgregarParametro("edc_ispid", dsOri.Tables[0].Rows[i](7));
                          sp.AgregarParametro("edc_isapre", dsOri.Tables[0].Rows[i](8));
                          sp.AgregarParametro("edc_ispmon", dsOri.Tables[0].Rows[i](9));
                          sp.AgregarParametro("edc_codmon", dsOri.Tables[0].Rows[i](10));
                          sp.AgregarParametro("edc_valmon", dsOri.Tables[0].Rows[i](11));
                          sp.AgregarParametro("edc_cjcid", dsOri.Tables[0].Rows[i](12));
                          sp.AgregarParametro("edc_caja", dsOri.Tables[0].Rows[i](13));
                          sp.AgregarParametro("edc_fecing", dsOri.Tables[0].Rows[i](14));
                          sp.AgregarParametro("edc_fecfin", dsOri.Tables[0].Rows[i](15));
                          sp.AgregarParametro("edc_sueldobase", dsOri.Tables[0].Rows[i](16));
                          sp.AgregarParametro("edc_horassemana", dsOri.Tables[0].Rows[i](17));
                          sp.AgregarParametro("edc_horasdiarias", dsOri.Tables[0].Rows[i](18));
                          sp.AgregarParametro("edc_horaent", dsOri.Tables[0].Rows[i](19));
                          sp.AgregarParametro("edc_horasal", dsOri.Tables[0].Rows[i](20));
                          sp.AgregarParametro("edc_diatra1", dsOri.Tables[0].Rows[i](21));
                          sp.AgregarParametro("edc_diatra2", dsOri.Tables[0].Rows[i](22));
                          sp.AgregarParametro("edc_diatra3", dsOri.Tables[0].Rows[i](23));
                          sp.AgregarParametro("edc_diatra4", dsOri.Tables[0].Rows[i](24));
                          sp.AgregarParametro("edc_diatra5", dsOri.Tables[0].Rows[i](25));
                          sp.AgregarParametro("edc_diatra6", dsOri.Tables[0].Rows[i](26));
                          sp.AgregarParametro("edc_diatra7", dsOri.Tables[0].Rows[i](27));
                          sp.AgregarParametro("edc_colacion", dsOri.Tables[0].Rows[i](28));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Empleados_Foto() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select emp_codemp, emp_emplid, emp_foto from empleados";
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
 
                          if( IsDBNull(dsOri.Tables[0].Rows[i](2)) == false ){
                              Horus.StoredProcedure sp = new Horus.StoredProcedure("Update_Empleados_Foto");
                              sp.AgregarParametro("epl_codemp", dsOri.Tables[0].Rows[i](0));
                              sp.AgregarParametro("epl_emplid", dsOri.Tables[0].Rows[i](1));
                              sp.AgregarParametro("epl_foto", dsOri.Tables[0].Rows[i](2));
 
 
                              err = sp.EjecutarProcedimiento(conn, myTrans);
 
                              if( err < 0 ){
                                  i = dsOri.Tables[0].Rows.Count + 1;
                              }
 
                          }
 
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipo_Asistencia() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Asistencia");
                          sp.AgregarParametro("tia_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tia_tipoid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tia_nombre", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tia_tipo", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipo_Asistencia_Idiomas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Asistencia_Idiomas");
                          sp.AgregarParametro("tai_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tai_tipoid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tai_idid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tai_nombre", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Empleado_Asistencia() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Empleado_Asistencia");
                          sp.AgregarParametro("epa_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("epa_sucid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("epa_emplid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("epa_dia", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Empleado_Asistencia_Horas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Empleado_Asistencia_Horas");
                          sp.AgregarParametro("eah_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("eah_sucid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("eah_emplid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("eah_dia", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("eah_item", dsOri.Tables[0].Rows[i](4));
                          sp.AgregarParametro("eah_tipoid", dsOri.Tables[0].Rows[i](5));
                          sp.AgregarParametro("eah_entrada", dsOri.Tables[0].Rows[i](6));
                          sp.AgregarParametro("eah_salida", dsOri.Tables[0].Rows[i](7));
                          sp.AgregarParametro("eah_authora", dsOri.Tables[0].Rows[i](8));
                          sp.AgregarParametro("eah_pagsueldo", dsOri.Tables[0].Rows[i](9));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Provcli() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Provcli");
                          sp.AgregarParametro("tpc_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tpc_tpcid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tpc_nombre", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tpc_agrupa", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Provcli_Idiomas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Provcli_Idiomas");
                          sp.AgregarParametro("tpi_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tpi_tpcid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tpi_idid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tpi_nombre", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Giros() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Giros");
                          sp.AgregarParametro("gir_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("gir_girid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("gir_nombre", dsOri.Tables[0].Rows[i](2));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Giros_Idiomas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Giros_Idiomas");
                          sp.AgregarParametro("gii_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("gii_girid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("gii_idid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("gii_nombre", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Contacto() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Contacto");
                          sp.AgregarParametro("tic_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tic_ticid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tic_nombre", UCase(dsOri.Tables[0].Rows[i](2)));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Tipos_Contacto_Idiomas() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Tipos_Contacto_Idiomas");
                          sp.AgregarParametro("tci_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("tci_ticid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("tci_idid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("tci_nombre", UCase(dsOri.Tables[0].Rows[i](3)));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool ProvCli() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_ProvCli2");
                          sp.AgregarParametro("pcl_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("pcl_pclid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("pcl_tpcid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("pcl_rut", UCase(dsOri.Tables[0].Rows[i](3)));
                          sp.AgregarParametro("pcl_nombre", UCase(dsOri.Tables[0].Rows[i](4)));
                          sp.AgregarParametro("pcl_apepat", UCase(dsOri.Tables[0].Rows[i](5)));
                          if( IsDBNull(dsOri.Tables[0].Rows[i](6)) ){
                              sp.AgregarParametro("pcl_apemat", "");
                          }else{
                              sp.AgregarParametro("pcl_apemat", UCase(dsOri.Tables[0].Rows[i](6)));
                          }
                          sp.AgregarParametro("pcl_nomfant", UCase(dsOri.Tables[0].Rows[i](7)));
                          sp.AgregarParametro("pcl_girid", dsOri.Tables[0].Rows[i](8));
                          sp.AgregarParametro("pcl_fecing", dsOri.Tables[0].Rows[i](10));
                          sp.AgregarParametro("pcl_estado", dsOri.Tables[0].Rows[i](11));
                          sp.AgregarParametro("pcl_replegal", dsOri.Tables[0].Rows[i](13));
                          sp.AgregarParametro("pcl_rutlegal", dsOri.Tables[0].Rows[i](14));
                          sp.AgregarParametro("pcl_tipcli", dsOri.Tables[0].Rows[i](15));
                          if( IsDBNull(dsOri.Tables[0].Rows[i](17)) ){
                              sp.AgregarParametro("pcl_web", "N");
                          }else{
                              sp.AgregarParametro("pcl_web", dsOri.Tables[0].Rows[i](17));
                          }
                          if( IsDBNull(dsOri.Tables[0].Rows[i](18)) ){
                              sp.AgregarParametro("pcl_comentario", "");
                          }else{
                              sp.AgregarParametro("pcl_comentario", dsOri.Tables[0].Rows[i](18));
                          }
                          sp.AgregarParametro("pcl_tipcart", dsOri.Tables[0].Rows[i](19));
                          sp.AgregarParametro("pcl_transportista", "N");
                          sp.AgregarParametro("pcl_usrid", DBNull.Value);
 
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Bancos() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Bancos");
                          sp.AgregarParametro("bco_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("bco_bcoid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("bco_rut", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("bco_nombre", dsOri.Tables[0].Rows[i](3));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool ProvCli_Sucursal() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_ProvCli_Sucursal");
                          sp.AgregarParametro("pcs_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("pcs_pclid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("pcs_pcsid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("pcs_nombre", UCase(dsOri.Tables[0].Rows[i](3)));
                          sp.AgregarParametro("pcs_comid", dsOri.Tables[0].Rows[i](4));
                          sp.AgregarParametro("pcs_direccion", UCase(dsOri.Tables[0].Rows[i](5)));
                          sp.AgregarParametro("pcs_telefono", dsOri.Tables[0].Rows[i](6));
                          sp.AgregarParametro("pcs_fax", dsOri.Tables[0].Rows[i](7));
                          sp.AgregarParametro("pcs_mail", LCase(dsOri.Tables[0].Rows[i](8)));
                          sp.AgregarParametro("pcs_casamatriz", dsOri.Tables[0].Rows[i](9));
                          sp.AgregarParametro("pcs_bcoid", dsOri.Tables[0].Rows[i](10));
                          sp.AgregarParametro("pcs_tipcta", dsOri.Tables[0].Rows[i](11));
                          sp.AgregarParametro("pcs_numcta", dsOri.Tables[0].Rows[i](12));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool ProvCli_Impuestos() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_ProvCli_Impuestos");
                          sp.AgregarParametro("pci_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("pci_pclid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("pci_iptid", dsOri.Tables[0].Rows[i](2));
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool ProvCli_Sucursal_Contacto() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_ProvCli_Sucursal_Contacto");
                          sp.AgregarParametro("psc_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("psc_pclid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("psc_pcsid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("psc_pscid", dsOri.Tables[0].Rows[i](3));
                          sp.AgregarParametro("psc_ticid", dsOri.Tables[0].Rows[i](4));
                          sp.AgregarParametro("psc_nombre", UCase(dsOri.Tables[0].Rows[i](5)));
                          sp.AgregarParametro("psc_telefono", dsOri.Tables[0].Rows[i](6));
                          sp.AgregarParametro("psc_anexo", dsOri.Tables[0].Rows[i](7));
                          sp.AgregarParametro("psc_fax", dsOri.Tables[0].Rows[i](8));
                          sp.AgregarParametro("psc_celular", dsOri.Tables[0].Rows[i](9));
                          sp.AgregarParametro("psc_mail", LCase(dsOri.Tables[0].Rows[i](10)));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool ProvCli_Codigo_Carga() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
                      strSql = "select * from " + Tabla;
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_ProvCli_Codigo_Carga");
                          sp.AgregarParametro("pcc_codemp", dsOri.Tables[0].Rows[i](0));
                          sp.AgregarParametro("pcc_pclid", dsOri.Tables[0].Rows[i](1));
                          sp.AgregarParametro("pcc_codid", dsOri.Tables[0].Rows[i](2));
                          sp.AgregarParametro("pcc_codigo", UCase(dsOri.Tables[0].Rows[i](3)));
                          sp.AgregarParametro("pcc_nombre", UCase(dsOri.Tables[0].Rows[i](4)));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = dsOri.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Cartera_Clientes() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
                  int ctc = 0;
                  bool ok;
                  DataSet ds = new DataSet();
                  string[] ArrApe;
                  string pasTel = "";
                  int p;
                  string telS;
                  string[] ArrNum = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
                  int x;
                  string[] ArrMail;
 
                  try{
 
                      strSql = " SELECT DISTINCT cartera_clientes.ctc_codemp,   ";
                      strSql = strSql + " cartera_clientes.ctc_pclid,   ";
                      strSql = strSql + "  cartera_clientes.ctc_ctcid,   ";
                      strSql = strSql + "  cartera_clientes.ctc_rut,   ";
                      strSql = strSql + "  cartera_clientes.ctc_nombre,   ";
                      strSql = strSql + "  cartera_clientes.ctc_comid,   ";
                      strSql = strSql + "  cartera_clientes.ctc_direccion,   ";
                      strSql = strSql + "  cartera_clientes.ctc_telefono,   ";
                      strSql = strSql + "  cartera_clientes.ctc_fax,   ";
                      strSql = strSql + "  cartera_clientes.ctc_email,   ";
                      strSql = strSql + "  cartera_clientes.ctc_gestion,   ";
                      strSql = strSql + "  cartera_clientes.ctc_estid,   ";
                      strSql = strSql + "  cartera_clientes.ctc_codsap,   ";
                      strSql = strSql + "  cartera_clientes.ctc_celular,   ";
                      strSql = strSql + "  cartera_clientes.ctc_partemp,   ";
                      strSql = strSql + "       Cartera_Clientes.ctc_fecing";
                      strSql = strSql + " FROM Cartera_Clientes";
                      //strSql = strSql + " WHERE cartera_clientes.ctc_rut in (  SELECT cartera_clientes.ctc_rut  "
                      //strSql = strSql + "                    FROM cartera_clientes,   "
                      //strSql = strSql + " cartera_clientes_cpbt_doc"
                      //strSql = strSql + " WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = cartera_clientes.ctc_codemp ) and  "
                      //strSql = strSql + " ( cartera_clientes_cpbt_doc.ccb_pclid = cartera_clientes.ctc_pclid ) and  "
                      //strSql = strSql + " ( cartera_clientes_cpbt_doc.ccb_ctcid = cartera_clientes.ctc_ctcid )  ) and ctc_rut in (select numero from paso)   "
 
 
                      //strSql = "select * from " + Tabla
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 1; i < dsOri.Tables[0].Rows.Count; i++){
                          // For i = 50000 To 80000
                          ok = func.Valida_Rut(UCase(dsOri.Tables[0].Rows[i](3)));
 
 
                          if( ok == true ){
                              //-----------------------Hago el Insert de los Deudores-----------------------------
                              try{
                                  strSql = "select count(ctc_ctcid) from deudores where ctc_numero=" + Left(dsOri.Tables[0].Rows[i](3), Len(dsOri.Tables[0].Rows[i](3)) - 1);
                                  ds = Hds.ConsultaBD(conn, myTrans, strSql);
                              }catch(Exception ex){
                                  break;
                              }
 
 
                              if( ds.Tables[0].Rows[0](0) == 0 ){
                                  try{
                                      strSql = "select count(ctc_ctcid) from deudores where ctc_nomfant like '%" + Trim(Replace(UCase(dsOri.Tables[0].Rows[i](4)), "'", \")) + "%' and ctc_numero=" + Left(dsOri.Tables[0].Rows[i](3), Len(dsOri.Tables[0].Rows[i](3)) - 1);
                                      ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                  }catch(Exception ex){
                                      break;
                                  }
 
 
                                  if( ds.Tables[0].Rows[0](0) == 0 ){
                                      ctc = ctc + 1;
                                      Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Deudores");
                                      sp.AgregarParametro("ctc_codemp", dsOri.Tables[0].Rows[i](0));
                                      sp.AgregarParametro("ctc_ctcid", ctc);
                                      sp.AgregarParametro("ctc_rut", dsOri.Tables[0].Rows[i](3));
                                      sp.AgregarParametro("ctc_numero", Left(dsOri.Tables[0].Rows[i](3), Len(dsOri.Tables[0].Rows[i](3)) - 1));
                                      sp.AgregarParametro("ctc_digito", Right(dsOri.Tables[0].Rows[i](3), 1));
                                      sp.AgregarParametro("ctc_nombre", Replace(UCase(dsOri.Tables[0].Rows[i](4)), "'", ""));
                                      ArrApe = Split(Replace(dsOri.Tables[0].Rows[i](4), "'", ""));
                                      if( ArrApe.Length >= 3 ){
                                          sp.AgregarParametro("ctc_apepat", UCase(ArrApe[2]));
                                          if( ArrApe.Length == 4 ){
                                              sp.AgregarParametro("ctc_apemat", UCase(ArrApe[3]));
                                          }else{
                                              sp.AgregarParametro("ctc_apemat", "");
                                          }
                                      }else{
                                          sp.AgregarParametro("ctc_apepat", "");
                                          sp.AgregarParametro("ctc_apemat", "");
                                      }
                                      sp.AgregarParametro("ctc_nomfant", UCase(dsOri.Tables[0].Rows[i](4)));
                                      sp.AgregarParametro("ctc_comid", dsOri.Tables[0].Rows[i](5));
                                      sp.AgregarParametro("ctc_direccion", Replace(UCase(dsOri.Tables[0].Rows[i](6)), "'", ""));
                                      if( IsDBNull(dsOri.Tables[0].Rows[i](14)) ){
                                          sp.AgregarParametro("ctc_partemp", "P");
                                      }else{
                                          sp.AgregarParametro("ctc_partemp", dsOri.Tables[0].Rows[i](14));
                                      }
 
                                      if( IsDBNull(dsOri.Tables[0].Rows[i](15)) ){
                                          sp.AgregarParametro("ctc_fecing", Today());
                                      }else{
                                          sp.AgregarParametro("ctc_fecing", dsOri.Tables[0].Rows[i](15));
                                      }
                                      sp.AgregarParametro("ctc_socid", DBNull.Value);
                                      sp.AgregarParametro("ctc_quiebra", "N");
                                      sp.AgregarParametro("ctc_nacext", "N");
                          
 
                                      err = sp.EjecutarProcedimiento(conn, myTrans);
 
                                      if( err < 0 ){
                                          break;
                                      }
 
                                      //---------------------Telefono
 
                                      try{
                                          telS = Trim(dsOri.Tables[0].Rows[i](7));
                                      }catch(Exception ex){
                                          telS = "";
                                      }
 
                                      pasTel = "";
 
                                      for(p = 1; p <= telS.Length; p++){
                                          try{
                                              x = Mid(telS, p, 1);
                                              pasTel = pasTel + Mid(telS, p, 1);
                                          }catch(Exception ex){
                                              if( Len(Trim(pasTel)) >= 5 ){
                                                  strSql = "select count(ddt_numero) from deudores_telefonos where ddt_numero=" + pasTel + " and ddt_ctcid=" + ctc.ToString();
                                                  ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                                  if( ds.Tables[0].Rows[0](0) == 0 ){
                                                      Horus.StoredProcedure sp3 = new Horus.StoredProcedure("Insertar_Deudores_Telefonos");
                                                      sp3.AgregarParametro("ddt_codemp", dsOri.Tables[0].Rows[i](0));
                                                      sp3.AgregarParametro("ddt_ctcid", ctc);
                                                      sp3.AgregarParametro("ddt_numero", pasTel);
                                                      if( Len(Trim(pasTel)) == 8 ){
                                                          sp3.AgregarParametro("ddt_tipo", "M");
                                                      }else{
                                                          sp3.AgregarParametro("ddt_tipo", "C");
                                                      }
                                                      sp3.AgregarParametro("ddt_estado", "A");
                                                      err = sp3.EjecutarProcedimiento(conn, myTrans);
 
                                                      pasTel = "";
 
                                                  }else{
                                                      pasTel = "";
                                                  }
                                              }
                                              pasTel = "";
                                          }
                                      }
 
 
                                      try{
                                          x = pasTel;
 
                                          if( Len(Trim(pasTel)) >= 5 ){
                                              strSql = "select count(ddt_numero) from deudores_telefonos where ddt_numero=" + pasTel + " and ddt_ctcid=" + ctc.ToString();
                                              ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                              if( ds.Tables[0].Rows[0](0) == 0 ){
                                                  Horus.StoredProcedure sp2 = new Horus.StoredProcedure("Insertar_Deudores_Telefonos");
                                                  sp2.AgregarParametro("ddt_codemp", dsOri.Tables[0].Rows[i](0));
                                                  sp2.AgregarParametro("ddt_ctcid", ctc);
                                                  sp2.AgregarParametro("ddt_numero", pasTel);
                                                  if( Len(Trim(pasTel)) == 8 ){
                                                      sp2.AgregarParametro("ddt_tipo", "M");
                                                  }else{
                                                      sp2.AgregarParametro("ddt_tipo", "C");
                                                  }
                                                  sp2.AgregarParametro("ddt_estado", "A");
                                                  err = sp2.EjecutarProcedimiento(conn, myTrans);
                                                  pasTel = "";
 
                                              }else{
                                                  pasTel = "";
                                              }
                                          }else{
                                              pasTel = "";
                                          }
                                      }catch(Exception ex){
                                          pasTel = "";
                                      }
 
                                      //-----------------Fax
 
                                      try{
                                          telS = Trim(dsOri.Tables[0].Rows[i](8));
                                      }catch(Exception ex){
                                          telS = "";
                                      }
 
                                      pasTel = "";
 
                                      for(p = 1; p <= telS.Length; p++){
                                          try{
                                              x = Mid(telS, p, 1);
                                              pasTel = pasTel + Mid(telS, p, 1);
                                          }catch(Exception ex){
                                              if( Len(Trim(pasTel)) >= 5 ){
                                                  strSql = "select count(ddt_numero) from deudores_telefonos where ddt_numero=" + pasTel + " and ddt_ctcid=" + ctc.ToString();
                                                  ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                                  if( ds.Tables[0].Rows[0](0) == 0 ){
                                                      Horus.StoredProcedure sp3 = new Horus.StoredProcedure("Insertar_Deudores_Telefonos");
                                                      sp3.AgregarParametro("ddt_codemp", dsOri.Tables[0].Rows[i](0));
                                                      sp3.AgregarParametro("ddt_ctcid", ctc);
                                                      sp3.AgregarParametro("ddt_numero", pasTel);
                                                      if( Len(Trim(pasTel)) == 8 ){
                                                          sp3.AgregarParametro("ddt_tipo", "M");
                                                      }else{
                                                          sp3.AgregarParametro("ddt_tipo", "C");
                                                      }
                                                      sp3.AgregarParametro("ddt_estado", "A");
                                                      err = sp3.EjecutarProcedimiento(conn, myTrans);
 
                                                      pasTel = "";
 
                                                  }else{
                                                      pasTel = "";
                                                  }
                                              }
                                              pasTel = "";
                                          }
                                      }
 
 
                                      try{
                                          x = pasTel;
 
                                          if( Len(Trim(pasTel)) >= 5 ){
                                              strSql = "select count(ddt_numero) from deudores_telefonos where ddt_numero=" + pasTel + " and ddt_ctcid=" + ctc.ToString();
                                              ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                              if( ds.Tables[0].Rows[0](0) == 0 ){
                                                  Horus.StoredProcedure sp2 = new Horus.StoredProcedure("Insertar_Deudores_Telefonos");
                                                  sp2.AgregarParametro("ddt_codemp", dsOri.Tables[0].Rows[i](0));
                                                  sp2.AgregarParametro("ddt_ctcid", ctc);
                                                  sp2.AgregarParametro("ddt_numero", pasTel);
                                                  if( Len(Trim(pasTel)) == 8 ){
                                                      sp2.AgregarParametro("ddt_tipo", "M");
                                                  }else{
                                                      sp2.AgregarParametro("ddt_tipo", "C");
                                                  }
                                                  sp2.AgregarParametro("ddt_estado", "A");
                                                  err = sp2.EjecutarProcedimiento(conn, myTrans);
                                                  pasTel = "";
 
                                              }else{
                                                  pasTel = "";
                                              }
                                          }else{
                                              pasTel = "";
                                          }
                                      }catch(Exception ex){
                                          pasTel = "";
                                      }
 
                                      //-----------------Celular
 
                                      try{
                                          telS = Trim(dsOri.Tables[0].Rows[i](13));
                                      }catch(Exception ex){
                                          telS = "";
                                      }
 
                                      pasTel = "";
 
                                      for(p = 1; p <= telS.Length; p++){
                                          try{
                                              x = Mid(telS, p, 1);
                                              pasTel = pasTel + Mid(telS, p, 1);
                                          }catch(Exception ex){
                                              if( Len(Trim(pasTel)) >= 5 ){
                                                  strSql = "select count(ddt_numero) from deudores_telefonos where ddt_numero=" + pasTel + " and ddt_ctcid=" + ctc.ToString();
                                                  ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                                  if( ds.Tables[0].Rows[0](0) == 0 ){
                                                      Horus.StoredProcedure sp3 = new Horus.StoredProcedure("Insertar_Deudores_Telefonos");
                                                      sp3.AgregarParametro("ddt_codemp", dsOri.Tables[0].Rows[i](0));
                                                      sp3.AgregarParametro("ddt_ctcid", ctc);
                                                      sp3.AgregarParametro("ddt_numero", pasTel);
                                                      if( Len(Trim(pasTel)) == 8 ){
                                                          sp3.AgregarParametro("ddt_tipo", "M");
                                                      }else{
                                                          sp3.AgregarParametro("ddt_tipo", "C");
                                                      }
                                                      sp3.AgregarParametro("ddt_estado", "A");
                                                      err = sp3.EjecutarProcedimiento(conn, myTrans);
 
                                                      pasTel = "";
 
                                                  }else{
                                                      pasTel = "";
                                                  }
                                              }
                                              pasTel = "";
                                          }
                                      }
 
 
                                      try{
                                          x = pasTel;
 
                                          if( Len(Trim(pasTel)) >= 5 ){
                                              strSql = "select count(ddt_numero) from deudores_telefonos where ddt_numero=" + pasTel + " and ddt_ctcid=" + ctc.ToString();
                                              ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                              if( ds.Tables[0].Rows[0](0) == 0 ){
                                                  Horus.StoredProcedure sp2 = new Horus.StoredProcedure("Insertar_Deudores_Telefonos");
                                                  sp2.AgregarParametro("ddt_codemp", dsOri.Tables[0].Rows[i](0));
                                                  sp2.AgregarParametro("ddt_ctcid", ctc);
                                                  sp2.AgregarParametro("ddt_numero", pasTel);
                                                  if( Len(Trim(pasTel)) == 8 ){
                                                      sp2.AgregarParametro("ddt_tipo", "M");
                                                  }else{
                                                      sp2.AgregarParametro("ddt_tipo", "C");
                                                  }
                                                  sp2.AgregarParametro("ddt_estado", "A");
                                                  err = sp2.EjecutarProcedimiento(conn, myTrans);
                                                  pasTel = "";
 
                                              }else{
                                                  pasTel = "";
                                              }
                                          }else{
                                              pasTel = "";
                                          }
                                      }catch(Exception ex){
                                          pasTel = "";
                                      }
 
                                      //-----------------Celular
 
                                      try{
                                          ArrMail = Split(dsOri.Tables[0].Rows[i](13));
                                      }catch(Exception ex){
                                          ArrMail = Split("", "");
                                      }
 
                                      for(p = 0; p < ArrMail.Length; p++){
                                          try{
                                              ok = func.ValidaMail(Trim(ArrMail[p]));
 
                                              strSql = "select count(ddm_mail) from deudores_mail where ddm_mail='" + Trim(ArrMail[p]) + "'" + " and ddm_ctcid=" + ctc.ToString();
                                              ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                              if( ds.Tables[0].Rows[0](0) == 0 & ok == true ){
                                                  Horus.StoredProcedure sp3 = new Horus.StoredProcedure("Insertar_Deudores_Mail");
                                                  sp3.AgregarParametro("ddm_codemp", dsOri.Tables[0].Rows[i](0));
                                                  sp3.AgregarParametro("ddm_ctcid", ctc);
                                                  sp3.AgregarParametro("ddm_mail", LCase(Trim(ArrMail[p])));
                                                  sp3.AgregarParametro("ddm_tipo", "P");
 
                                                  err = sp3.EjecutarProcedimiento(conn, myTrans);
                                              }
                                          }catch(Exception ex){
 
                                          }
                                      }
 
 
                                  }
                              }
 
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Cartera_Clientes_Contacto() {
                  DataSet dsOri = new DataSet();
                  string strSql;
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
                  int ctc = 0;
                  bool ok;
                  DataSet ds = new DataSet();
                  string pasTel = "";
                  int p;
                  string telS;
                  string[] ArrNum = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
                  int x;
                  string[] ArrMail;
                  int cont;
                  bool ins = true;
 
 
                  try{
 
                      strSql = "     SELECT cartera_clientes_contacto.cct_codemp,   ";
                      strSql = strSql + "  cartera_clientes_contacto.cct_pclid,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_ctcid,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_cctid,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_ticid,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_nombre,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_telefono,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_fax,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_mail,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_celular,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_comid,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_direccion,   ";
                      strSql = strSql + " cartera_clientes_contacto.cct_mailmasivo,   ";
                      strSql = strSql + " Cartera_Clientes.ctc_rut";
                      strSql = strSql + " FROM cartera_clientes,   ";
                      strSql = strSql + " Cartera_Clientes_Contacto";
                      strSql = strSql + " WHERE  cartera_clientes_contacto.cct_codemp = cartera_clientes.ctc_codemp  and  ";
                      strSql = strSql + " cartera_clientes_contacto.cct_pclid = cartera_clientes.ctc_pclid  and  ";
                      strSql = strSql + " Cartera_Clientes_Contacto.cct_ctcid = Cartera_Clientes.ctc_ctcid";
 
                
                      dsOri = Hds.ConsultaBDOri(strSql);
 
                      for(i = 0; i < dsOri.Tables[0].Rows.Count; i++){
                          ins = true;
 
                          if( i == 4361 ){
                              i = i;
                          }
 
                          try{
                              strSql = "select ctc_ctcid from deudores where ctc_numero=" + Left(dsOri.Tables[0].Rows[i](13), Len(dsOri.Tables[0].Rows[i](13)) - 1);
                              ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                              if( ds.Tables[0].Rows.Count > 0 ){
                                  ctc = ds.Tables[0].Rows[0](0);
                              }else{
                                  ctc = 0;
                              }
                          }catch(Exception ex){
                              ctc = 0;
                              break;
                          }
 
                          if( ctc > 0 ){
                              strSql = "select IsNull(Max(ddc_ddcid)+1, 1) from deudores_contactos where ddc_ctcid =" + ctc.ToString();
                              ds = Hds.ConsultaBD(conn, myTrans, strSql);
                              cont = ds.Tables[0].Rows[0](0);
 
                              strSql = "select count(ddc_ddcid) from deudores_contactos where ddc_ctcid=" + ctc.ToString() + " and ddc_nombre like '%" + Trim(Replace(UCase(dsOri.Tables[0].Rows[i](5)), "'", \")) + "%'";
                              ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                              if( ds.Tables[0].Rows[0](0) > 0 ){
                                  strSql = "select ddc_ddcid from deudores_contactos where ddc_ctcid=" + ctc.ToString() + " and ddc_nombre like '%" + Trim(Replace(UCase(dsOri.Tables[0].Rows[i](5)), "'", \")) + "%'";
                                  ds = Hds.ConsultaBD(conn, myTrans, strSql);
                                  cont = ds.Tables[0].Rows[0](0);
                                  ins = false;
                              }
 
                              if( ins == true ){
                                  Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Deudores_Contactos");
                                  sp.AgregarParametro("ddc_codemp", dsOri.Tables[0].Rows[i](0));
                                  sp.AgregarParametro("ddc_ctcid", ctc);
                                  sp.AgregarParametro("ddc_ddcid", cont);
                                  sp.AgregarParametro("ddc_ticid", dsOri.Tables[0].Rows[i](4));
                                  sp.AgregarParametro("ddc_nombre", Replace(UCase(dsOri.Tables[0].Rows[i](5)), "'", ""));
                                  if( IsDBNull(dsOri.Tables[0].Rows[i](10)) ){
                                      sp.AgregarParametro("ddc_comid", DBNull.Value);
                                      sp.AgregarParametro("ddc_direccion", "");
                                  }else{
                                      sp.AgregarParametro("ddc_comid", dsOri.Tables[0].Rows[i](10));
                                      sp.AgregarParametro("ddc_direccion", UCase(dsOri.Tables[0].Rows[i](11)));
                                  }
                                  sp.AgregarParametro("ddc_fecing", Today());
 
                                  err = sp.EjecutarProcedimiento(conn, myTrans);
 
                                  if( err < 0 ){
                                      break;
                                  }
                              }
 
                              //---------------------Telefono
 
                              try{
                                  telS = Trim(dsOri.Tables[0].Rows[i](6));
                              }catch(Exception ex){
                                  telS = "";
                              }
 
                              pasTel = "";
 
                              for(p = 1; p <= telS.Length; p++){
                                  try{
                                      x = Mid(telS, p, 1);
                                      pasTel = pasTel + Mid(telS, p, 1);
                                  }catch(Exception ex){
                                      if( Len(Trim(pasTel)) >= 5 ){
                                          strSql = "select count(dct_numero) from deudores_contactos_telefonos where dct_numero=" + pasTel + " and dct_ctcid=" + ctc.ToString() + " and dct_ddcid =" + cont.ToString();
                                          ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                          if( ds.Tables[0].Rows[0](0) == 0 ){
                                              Horus.StoredProcedure sp3 = new Horus.StoredProcedure("Insertar_Deudores_Contactos_Telefonos");
                                              sp3.AgregarParametro("dct_codemp", dsOri.Tables[0].Rows[i](0));
                                              sp3.AgregarParametro("dct_ctcid", ctc);
                                              sp3.AgregarParametro("dct_ddcid", cont);
                                              sp3.AgregarParametro("dct_numero", pasTel);
                                              if( Len(Trim(pasTel)) == 8 ){
                                                  sp3.AgregarParametro("dct_tipo", "M");
                                              }else{
                                                  sp3.AgregarParametro("dct_tipo", "C");
                                              }
                                              sp3.AgregarParametro("dct_estado", "A");
                                              err = sp3.EjecutarProcedimiento(conn, myTrans);
 
                                              pasTel = "";
 
                                          }else{
                                              pasTel = "";
                                          }
                                      }
                                      pasTel = "";
                                  }
                              }
 
 
                              try{
                                  x = pasTel;
 
                                  if( Len(Trim(pasTel)) >= 5 ){
                                      strSql = "select count(dct_numero) from deudores_contactos_telefonos where dct_numero=" + pasTel + " and dct_ctcid=" + ctc.ToString() + " and dct_ddcid =" + cont.ToString();
                                      ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                      if( ds.Tables[0].Rows[0](0) == 0 ){
                                          Horus.StoredProcedure sp2 = new Horus.StoredProcedure("Insertar_Deudores_Contactos_Telefonos");
                                          sp2.AgregarParametro("dct_codemp", dsOri.Tables[0].Rows[i](0));
                                          sp2.AgregarParametro("dct_ctcid", ctc);
                                          sp2.AgregarParametro("dct_ddcid", cont);
                                          sp2.AgregarParametro("dct_numero", pasTel);
                                          if( Len(Trim(pasTel)) == 8 ){
                                              sp2.AgregarParametro("dct_tipo", "M");
                                          }else{
                                              sp2.AgregarParametro("dct_tipo", "C");
                                          }
                                          sp2.AgregarParametro("dct_estado", "A");
                                          err = sp2.EjecutarProcedimiento(conn, myTrans);
                                          pasTel = "";
 
                                      }else{
                                          pasTel = "";
                                      }
                                  }else{
                                      pasTel = "";
                                  }
                              }catch(Exception ex){
                                  pasTel = "";
                              }
 
 
                              //---------------------Fax
 
                              try{
                                  telS = Trim(dsOri.Tables[0].Rows[i](9));
                              }catch(Exception ex){
                                  telS = "";
                              }
 
                              pasTel = "";
 
                              for(p = 1; p <= telS.Length; p++){
                                  try{
                                      x = Mid(telS, p, 1);
                                      pasTel = pasTel + Mid(telS, p, 1);
                                  }catch(Exception ex){
                                      if( Len(Trim(pasTel)) >= 5 ){
                                          strSql = "select count(dct_numero) from deudores_contactos_telefonos where dct_numero=" + pasTel + " and dct_ctcid=" + ctc.ToString() + " and dct_ddcid =" + cont.ToString();
                                          ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                          if( ds.Tables[0].Rows[0](0) == 0 ){
                                              Horus.StoredProcedure sp3 = new Horus.StoredProcedure("Insertar_Deudores_Contactos_Telefonos");
                                              sp3.AgregarParametro("dct_codemp", dsOri.Tables[0].Rows[i](0));
                                              sp3.AgregarParametro("dct_ctcid", ctc);
                                              sp3.AgregarParametro("dct_ddcid", cont);
                                              sp3.AgregarParametro("dct_numero", pasTel);
                                              if( Len(Trim(pasTel)) == 8 ){
                                                  sp3.AgregarParametro("dct_tipo", "M");
                                              }else{
                                                  sp3.AgregarParametro("dct_tipo", "C");
                                              }
                                              sp3.AgregarParametro("dct_estado", "A");
                                              err = sp3.EjecutarProcedimiento(conn, myTrans);
 
                                              pasTel = "";
 
                                          }else{
                                              pasTel = "";
                                          }
                                      }
                                      pasTel = "";
                                  }
                              }
 
 
                              try{
                                  x = pasTel;
 
                                  if( Len(Trim(pasTel)) >= 5 ){
                                      strSql = "select count(dct_numero) from deudores_contactos_telefonos where dct_numero=" + pasTel + " and dct_ctcid=" + ctc.ToString() + " and dct_ddcid =" + cont.ToString();
                                      ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                      if( ds.Tables[0].Rows[0](0) == 0 ){
                                          Horus.StoredProcedure sp2 = new Horus.StoredProcedure("Insertar_Deudores_Contactos_Telefonos");
                                          sp2.AgregarParametro("dct_codemp", dsOri.Tables[0].Rows[i](0));
                                          sp2.AgregarParametro("dct_ctcid", ctc);
                                          sp2.AgregarParametro("dct_ddcid", cont);
                                          sp2.AgregarParametro("dct_numero", pasTel);
                                          if( Len(Trim(pasTel)) == 8 ){
                                              sp2.AgregarParametro("dct_tipo", "M");
                                          }else{
                                              sp2.AgregarParametro("dct_tipo", "C");
                                          }
                                          sp2.AgregarParametro("dct_estado", "A");
                                          err = sp2.EjecutarProcedimiento(conn, myTrans);
                                          pasTel = "";
 
                                      }else{
                                          pasTel = "";
                                      }
                                  }else{
                                      pasTel = "";
                                  }
                              }catch(Exception ex){
                                  pasTel = "";
                              }
 
 
                              //---------------------Celular
 
                              try{
                                  telS = Trim(dsOri.Tables[0].Rows[i](7));
                              }catch(Exception ex){
                                  telS = "";
                              }
 
                              pasTel = "";
 
                              for(p = 1; p <= telS.Length; p++){
                                  try{
                                      x = Mid(telS, p, 1);
                                      pasTel = pasTel + Mid(telS, p, 1);
                                  }catch(Exception ex){
                                      if( Len(Trim(pasTel)) >= 5 ){
                                          strSql = "select count(dct_numero) from deudores_contactos_telefonos where dct_numero=" + pasTel + " and dct_ctcid=" + ctc.ToString() + " and dct_ddcid =" + cont.ToString();
                                          ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                          if( ds.Tables[0].Rows[0](0) == 0 ){
                                              Horus.StoredProcedure sp3 = new Horus.StoredProcedure("Insertar_Deudores_Contactos_Telefonos");
                                              sp3.AgregarParametro("dct_codemp", dsOri.Tables[0].Rows[i](0));
                                              sp3.AgregarParametro("dct_ctcid", ctc);
                                              sp3.AgregarParametro("dct_ddcid", cont);
                                              sp3.AgregarParametro("dct_numero", pasTel);
                                              if( Len(Trim(pasTel)) == 8 ){
                                                  sp3.AgregarParametro("dct_tipo", "M");
                                              }else{
                                                  sp3.AgregarParametro("dct_tipo", "C");
                                              }
                                              sp3.AgregarParametro("dct_estado", "A");
                                              err = sp3.EjecutarProcedimiento(conn, myTrans);
 
                                              pasTel = "";
 
                                          }else{
                                              pasTel = "";
                                          }
                                      }
                                      pasTel = "";
                                  }
                              }
 
 
                              try{
                                  x = pasTel;
 
                                  if( Len(Trim(pasTel)) >= 5 ){
                                      strSql = "select count(dct_numero) from deudores_contactos_telefonos where dct_numero=" + pasTel + " and dct_ctcid=" + ctc.ToString() + " and dct_ddcid =" + cont.ToString();
                                      ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                      if( ds.Tables[0].Rows[0](0) == 0 ){
                                          Horus.StoredProcedure sp2 = new Horus.StoredProcedure("Insertar_Deudores_Contactos_Telefonos");
                                          sp2.AgregarParametro("dct_codemp", dsOri.Tables[0].Rows[i](0));
                                          sp2.AgregarParametro("dct_ctcid", ctc);
                                          sp2.AgregarParametro("dct_ddcid", cont);
                                          sp2.AgregarParametro("dct_numero", pasTel);
                                          if( Len(Trim(pasTel)) == 8 ){
                                              sp2.AgregarParametro("dct_tipo", "M");
                                          }else{
                                              sp2.AgregarParametro("dct_tipo", "C");
                                          }
                                          sp2.AgregarParametro("dct_estado", "A");
                                          err = sp2.EjecutarProcedimiento(conn, myTrans);
                                          pasTel = "";
 
                                      }else{
                                          pasTel = "";
                                      }
                                  }else{
                                      pasTel = "";
                                  }
                              }catch(Exception ex){
                                  pasTel = "";
                              }
 
                              //----------------Mail
 
                              try{
                                  ArrMail = Split(dsOri.Tables[0].Rows[i](8));
                              }catch(Exception ex){
                                  ArrMail = Split("", "");
                              }
 
                              for(p = 0; p < ArrMail.Length; p++){
                                  try{
                                      ok = func.ValidaMail(Trim(ArrMail[p]));
 
                                      strSql = "select count(dcm_mail) from deudores_contactos_mail where dcm_mail='" + Trim(ArrMail[p]) + "'" + " and dcm_ctcid=" + ctc.ToString() + " and dcm_ddcid=" + cont.ToString();
                                      ds = Hds.ConsultaBD(conn, myTrans, strSql);
 
                                      if( ds.Tables[0].Rows[0](0) == 0 & ok == true ){
                                          Horus.StoredProcedure sp3 = new Horus.StoredProcedure("Insertar_Deudores_Contactos_Mail");
                                          sp3.AgregarParametro("dcm_codemp", dsOri.Tables[0].Rows[i](0));
                                          sp3.AgregarParametro("dcm_ctcid", ctc);
                                          sp3.AgregarParametro("dcm_ddcid", cont);
                                          sp3.AgregarParametro("dcm_mail", LCase(Trim(ArrMail[p])));
                                          sp3.AgregarParametro("dcm_tipo", "P");
 
                                          err = sp3.EjecutarProcedimiento(conn, myTrans);
                                      }
                                  }catch(Exception ex){
 
                                  }
                              }
                          }
 
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
 
              private bool Help(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
 
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Help");
                          sp.AgregarParametro("hlp_trvid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("hlp_hlpid", ds.Tables[0].Rows[i](1));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
 
              private bool Help_Idiomas(DataSet ds) {
                  DataSet dsOri = new DataSet();
                  int i;
                  Horus.Conexion conn = new Horus.Conexion();
                  int err;
                  conn.SQLConn.Open();
                  SqlTransaction myTrans = conn.SQLConn.BeginTransaction();
 
                  try{
 
 
                      for(i = 0; i < ds.Tables[0].Rows.Count; i++){
                          Horus.StoredProcedure sp = new Horus.StoredProcedure("Insertar_Help_Idiomas");
                          sp.AgregarParametro("hpi_trvid", ds.Tables[0].Rows[i](0));
                          sp.AgregarParametro("hpi_hlpid", ds.Tables[0].Rows[i](1));
                          sp.AgregarParametro("hpi_idid", ds.Tables[0].Rows[i](2));
                          sp.AgregarParametro("hpi_nombre", ds.Tables[0].Rows[i](3));
                          sp.AgregarParametro("hpi_archivo", ds.Tables[0].Rows[i](4));
 
 
                          err = sp.EjecutarProcedimiento(conn, myTrans);
 
                          if( err < 0 ){
                              i = ds.Tables[0].Rows.Count + 1;
                          }
                      }
 
                      if( err < 0 ){
                          myTrans.Rollback();
                          conn.SQLConn.Close();
                          return false;
                      }
                      myTrans.Commit();
                      conn.SQLConn.Close();
 
 
                  }catch(Exception ex){
                      myTrans.Rollback();
                      conn.SQLConn.Close();
 
                      return false;
                  }
 
                  return true;
 
 
 
              }  
       */
    }
 
  
}
