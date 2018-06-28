﻿using Mvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Dimol.Carteras.Models;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using Dimol.dto;
using Dimol.Carteras.bcp;
using System.Globalization;
using System.Threading;

namespace Dimol.Carteras.Controllers
{
    public class MantenedorController : Controller
    {
        #region "Sesion"
        UserSession objSession = new UserSession()
        {
            Cargo = "Cargo",
            CodigoEmpresa = 1,
            CodigoSucursal = 1,
            EmplId = 0,
            Idioma = 1,
            IpPc = "1",
            IpRed = "2",
            Menu = "<div id='cssmenu' style='position: absolute;z-index: 99999;'><ul><li class='has-sub'><a href='#' title='s'><span>Carteras</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Acciones</span></a><ul><li class='last'><a href='~/Carteras/acciones.aspx?TitPag=Acciones' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Campaña</span></a><ul><li class='last'><a href='~/Carteras/cartera_clientes_campana.aspx?TitPag=Campaña Cliente&Tipo=V&RepPag=136' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Codigo Carga</span></a><ul><li class='last'><a href='~/Carteras/provcli_codigo_carga.aspx?TitPag=Codigo Carga' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Comprobantes</span></a><ul><li><a href='~/Carteras/cabecera_comprobantes.aspx?TitPag=Comprobantes&Tipo=CC&PreJud=P&RepPag=176&Help=176' title='s'><span>ABM</span></a></li><li><a href='~/Carteras/cabecera_comprobantes_aceptar.aspx?TitPag=Aceptar Comprobantes&Tipo=CC&PreJud=P' title='s'><span>Aceptar</span></a></li><li class='last'><a href='~/Carteras/cabecera_comprobantes_buscar.aspx?TitPag=Buscar Comprobantes&Tipo=CC&PreJud=P&RepPag=176' title='s'><span>Buscar</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Controles</span></a><ul><li><a href='~/Carteras/control_gestiones_online.aspx?TitPag=Gestiones y Llamadas On-Line' title='s'><span>Gestiones y Llamadas On-Line</span></a></li><li class='last'><a href='~/Carteras/control_llamadas.aspx?TitPag=Control Llamadas' title='s'><span>Llamadas</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Deudores</span></a><ul><li><a href='~/Carteras/deudores.aspx?TitPag=Deudores' title='s'><span>ABM</span></a></li><li><a href='~/Carteras/deudores_buscar.aspx?TitPag=Buscar Deudores' title='s'><span>Buscar</span></a></li><li class='has-sub'><a href='#' title='s'><span>Cpbt/Doc</span></a><ul><li><a href='~/Carteras/deudores_cpbtdoc.aspx?TitPag=Comprobantes/Documentos Deudores' title='s'><span>ABM</span></a></li><li><a href='~/Carteras/aprobacion_carga.aspx?TitPag=Aprobacion Carga' title='s'><span>Aprobacion Carga</span></a></li><li><a href='~/Carteras/deudores_cpbtdoc_buscar.aspx?TitPag=Buscar Cpbt/Doc' title='s'><span>Buscar</span></a></li><li class='has-sub'><a href='#' title='s'><span>Procesos Masivos</span></a><ul><li><a href='~/Carteras/deudores_cpbtdoc_carga_masiva_anular.aspx?TitPag=Carga Masiva Anular' title='s'><span>Anular Carga</span></a></li><li><a href='~/Carteras/deudores_cpbtdoc_carga_masiva.aspx?TitPag=Carga Masiva' title='s'><span>Cargar</span></a></li><li class='has-sub'><a href='#' title='s'><span>Cargar Pagos</span></a><ul><li><a href='~/Carteras/deudores_cpbtDoc_pagos.aspx?TitPag=Cargar Pagos' title='s'><span>ABM</span></a></li><li class='last'><a href='~/Carteras/deudores_cpbtdoc_pagos_clientes.aspx?TitPag=Ingresar Pagos Clientes' title='s'><span>Especial Clientes</span></a></li></ul></li><li class='last'><a href='~/Carteras/deudores_cpbtdoc_carga_cliente.aspx?TitPag=Cargas Clientes' title='s'><span>Clientes</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Documentos</span></a><ul><li class='last'><a href='~/Carteras/deudores_documentos.aspx?TitPag=Tipos Imagenes Documentos' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Documentos</span></a><ul><li class='last'><a href='~/Carteras/tipos_documentos_deudor.aspx?TitPag=Tipos Documentos Deudor' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Imagenes</span></a><ul><li class='last'><a href='~/Carteras/tipos_imagenes_cpbtdoc.aspx?TitPag=Tipos Imagenes Documentos' title='s'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Estados Cartera</span></a><ul><li><a href='~/Carteras/estados_cartera.aspx?TitPag=Estados Cartera Judicial&Tipo=J' title='s'><span>Judicial</span></a></li><li class='has-sub'><a href='#' title='s'><span>Maestra Clientes</span></a><ul><li class='last'><a href='~/Carteras/maestra_provcli_estados.aspx?TitPag=Maestra Estados Cliente' title='s'><span>ABM</span></a></li></ul></li><li class='last'><a href='~/Carteras/estados_cartera.aspx?TitPag=Estados Cartera PreJudicial&Tipo=P' title='s'><span>PreJudicial</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Gestionar Cartera</span></a><ul><li><a href='~/Carteras/gestionar_cartera.aspx?TitPag=Gestionar Cartera&Tipo=V&RepPag=132&Help=132' title='s'><span>ABM</span></a></li><li class='last'><a href='~/Carteras/gestor_anula_restriccion.aspx?TitPag=Anular Restriccion Gestor' title='s'><span>Anular Restriccion Gestor</span></a></li></ul></li><li class='has-sub'><a href='#' title=''><span>Motivo Cobranza</span></a><ul><li class='last'><a href='~/Carteras/motivo_cobranza.aspx?TitPag=Motivo Cobranza' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Multimedia</span></a><ul><li><a href='~/Carteras/email.aspx?TitPag=E-Mail&RepPag=241' title='s'><span>E-Mail</span></a></li><li><a href='~/Carteras/ivr.aspx?TitPag=IVR' title='s'><span>IVR</span></a></li><li class='last'><a href='~/Carteras/sms.aspx?TitPag=SMS' title='s'><span>SMS</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Opciones Gestores</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Anexar Gestor</span></a><ul><li class='last'><a href='~/Carteras/gestor_anexo.aspx?TitPag=Anexar Gestor' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Cartera</span></a><ul><li><a href='~/Carteras/gestor_cartera_asignar.aspx?TitPag=Asignar Cartera Gestor' title='s'><span>Asignar</span></a></li><li class='last'><a href='~/Carteras/gestor_cartera_reasignar.aspx?TitPag=Reasignar Cartera Gestor' title='s'><span>Reasignar</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Gestores</span></a><ul><li class='last'><a href='~/Carteras/gestor.aspx?TitPag=Gestor' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Grupos Cobranza</span></a><ul><li class='last'><a href='~/Carteras/grupos_cobranza.aspx?TitPag=Grupos Cobranza' title='s'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Reportes</span></a><ul><li><a href='~/Carteras/reportes_cierre_mes.aspx?TitPag=Reportes Cierre Mes' title='s'><span>Cierre Mes</span></a></li><li><a href='~/Carteras/dinamicos_prejudicial.aspx?TitPag=Dinamicos Prejudicial' title='s'><span>Dinamicos</span></a></li><li><a href='~/Carteras/reportes_carteras.aspx?TitPag=Reportes Gestores&PreJud=P&RepPag=256' title='s'><span>Gestores</span></a></li><li class='last'><a href='~/Carteras/reportes_carteras.aspx?TitPag=Reportes Carteras Clientes&PreJud=P&RepPag=138' title='s'><span>Predefinidos</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Sub-Carteras</span></a><ul><li><a href='~/Carteras/subcarteras.aspx?TitPag=Sub-Carteras' title='s'><span>ABM</span></a></li><li class='last'><a href='~/Carteras/subcarteras_buscar.aspx?Titpag=Buscar Sub-Carteras' title='s'><span>Buscar</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Castigos</span></a><ul><li class='last'><a href='~/Carteras/tipos_motivos_castigos.aspx?TitPag=Tipos Motivos Castigos' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Retiros/Entregas</span></a><ul><li class='last'><a href='~/Carteras/tipos_retiro_entrega.aspx?TitPag=Tipos Retiros/Entregas' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Control Gestion</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Carteras</span></a><ul><li><a href='~/Control Gestion/analitico_carteras.aspx?TitPag=Analitico de Carteras' title='s'><span>Analitico Carteras</span></a></li><li class='has-sub'><a href='#' title='s'><span>Cambios Estados Masivos</span></a><ul><li class='last'><a href='~/Control Gestion/cambio_estado_masivo.aspx?TitPag=Cambiar estados masivos' title='s'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Gestores</span></a><ul><li class='last'><a href='~/Control Gestion/productividad_gestor.aspx?TitPag=Productividad Gestor' title='s'><span>Productividad</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Reportes</span></a><ul><li class='last'><a href='~/Control Gestion/reportes_carteras.aspx?TitPag=Reportes Control Gestion&PreJud=P&RepPag=346' title='s'><span>Predefinidos</span></a></li></ul></li></ul></li></ul></li><li class='has-sub'><a href='#' title='Compras, Insumos y Productos'><span>Compra</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Compras</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Categorizacion</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Asociar Super y Categorias</span></a><ul><li class='last'><a href='~/ProvCli/supcat_categorias.aspx?TitPag=Asociar Super y Categorias&Uti=1' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Categorias</span></a><ul><li class='last'><a href='~/ProvCli/categorias.aspx?TitPag=Categorias&Uti=1' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Super Categoria</span></a><ul><li class='last'><a href='~/ProvCli/supercategorias.aspx?TitPag=Super Categorias&Uti=1' title='s'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Comprobantes</span></a><ul><li><a href='~/Carteras/cabecera_comprobantes.aspx?TitPag=Comprobantes&Tipo=C&PreJud=N&RepPag=203&Help=203' title='s'><span>ABM</span></a></li><li><a href='~/Carteras/cabecera_comprobantes_aceptar.aspx?TitPag=Aceptar Comprobantes&Tipo=C&PreJud=N' title='s'><span>Aceptar</span></a></li><li class='last'><a href='~/Carteras/cabecera_comprobantes_buscar.aspx?TitPag=Buscar Comprobantes&Tipo=C&PreJud=N&RepPag=203' title='s'><span>Buscar</span></a></li></ul></li><li><a href='~/ProvCli/giros.aspx?TitPag=Giros' title='s'><span>Giros</span></a></li><li class='has-sub'><a href='#' title='s'><span>Proveedor</span></a><ul><li><a href='~/ProvCli/provcli.aspx?TitPag=Proveedor&Tipo=C' title='s'><span>ABM</span></a></li><li class='last'><a href='~/ProvCli/provcli_buscar.aspx?TitPag=Buscar Proveedor&Tipo=C' title='s'><span>Buscar</span></a></li></ul></li><li><a href='~/ProvCli/tipos_contacto.aspx?TitPag=Tipos de Contacto' title='s'><span>Tipos Contacto</span></a></li><li class='last'><a href='~/ProvCli/tipos_provcli.aspx?TitPag=Tipos Proveedor/Cliente' title='s'><span>Tipos Proveedor/Clientes</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Insumos</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Insumos</span></a><ul><li><a href='~/Insumos/insumos.aspx?TitPag=Insumos' title='s'><span>ABM</span></a></li><li><a href='~/Insumos/insumos_buscar.aspx?TitPag=Buscar Insumos' title='s'><span>Buscar</span></a></li><li class='has-sub'><a href='#' title='s'><span>Maestra Insumos</span></a><ul><li class='last'><a href='~/Insumos/maestra_insumos.aspx?TitPag=Maestra Insumos&Tipo=C' title='s'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Especificaciones</span></a><ul><li class='last'><a href='~/Insumos/tipos_especificaciones.aspx?TitPag=Tipos Especificaciones' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Insumos</span></a><ul><li class='last'><a href='~/Insumos/tipos_insumo.aspx?TitPag=Tipos Insumos' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Unidades Medida</span></a><ul><li class='last'><a href='~/Insumos/unidades_medida.aspx?TitPag=Unidades Medida' title='s'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Productos</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Productos</span></a><ul><li><a href='~/Productos/productos.aspx?TitPag=Productos' title='s'><span>ABM</span></a></li><li class='last'><a href='~/Productos/productos_buscar.aspx?TitPag=Buscar Productos' title='s'><span>Buscar</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Especificaciones</span></a><ul><li class='last'><a href='~/Insumos/tipos_especificaciones.aspx?TitPag=Tipos Especificaciones' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Unidades Medida</span></a><ul><li class='last'><a href='~/Insumos/unidades_medida.aspx?TitPag=Unidades Medida' title='s'><span>ABM</span></a></li></ul></li></ul></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Contabilidad</span></a><ul><li class='has-sub'><a href='#' title=''><span>Asientos Contables</span></a><ul><li><a href='~/Contabilidad/asientos_contables.aspx?TitPag=Asientos Contables&RepPag=53' title='s'><span>ABM</span></a></li><li><a href='~/Contabilidad/Analitico_Cuentas.aspx?TitPag=Analitico Cuentas' title='s'><span>Analitico Cuentas</span></a></li><li class='last'><a href='~/Contabilidad/asientos_contables_buscar.aspx?TitPag=Buscar Asientos Contables' title='a'><span>Buscar</span></a></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Centros de Costos</span></a><ul><li class='last'><a href='~/Contabilidad/centro_costos.aspx?TitPag=Centros Costos' title='abm.gif'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Comprobantes</span></a><ul><li><a href='~/Contabilidad/clasificacion_cpbtdoc.aspx?TitPag=Clasificacion Comprobantes/Documentos&help=56' title=''><span>Clasificacion</span></a></li><li class='has-sub'><a href='#' title='s'><span>Formas de Pago</span></a><ul><li class='last'><a href='~/Contabilidad/formas_pago.aspx?TitPag=Formas de Pago' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Comprobantes</span></a><ul><li><a href='~/Contabilidad/tipos_cpbtdoc.aspx?TitPag=Tipos Comprobrantes&Cpbt=S' title='s'><span>ABM</span></a></li><li><a href='~/Contabilidad/tipos_cpbtdoc_talonario.aspx?TitPag=Talonario&Cpbt=S' title='s'><span>Talonarios</span></a></li><li class='last'><a href='~/Contabilidad/tipos_cpbtdoc_report.aspx?TitPag=Tipos Reporte&Cpbt=S' title='s'><span>Tipos Reporte</span></a></li></ul></li><li class='last'><a href='~/Contabilidad/tipos_cpbtdoc.aspx?TitPag=Tipos Documentos&Cpbt=N' title='S'><span>Tipos Documentos</span></a></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Cuentas Contables</span></a><ul><li><a href='~/Contabilidad/cuentas_padres.aspx?TitPag=Cuentas Padres' title='abm.gif'><span>Padres</span></a></li><li class='last'><a href='~/Contabilidad/plan_cuentas.aspx?TitPag=Plan de Cuentas&RepPag=44' title='abm.gif'><span>Plan de Cuenta</span></a></li></ul></li><li class='has-sub'><a href='#' title='a'><span>Impuestos</span></a><ul><li class='last'><a href='~/Contabilidad/impuestos.aspx?TitPag=Impuestos' title='a'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Monedas Valores</span></a><ul><li class='last'><a href='~/Contabilidad/monedas_valores.aspx?TitPag=Monedas Valores' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Periodos Contables</span></a><ul><li class='has-sub'><a href='#' title='abm.gif'><span>Años</span></a><ul><li class='last'><a href='~/Contabilidad/periodos_contables.aspx?TitPag=Periodos Contables' title='a'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='abm'><span>Meses</span></a><ul><li class='last'><a href='~/Contabilidad/periodos_contables_meses.aspx?TitPag=Periodos Contables Meses' title='a'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Reportes</span></a><ul><li class='last'><a href='~/Contabilidad/reportes_contables.aspx?TitPag=Reportes Contables&Tipo=A&RepPag=272' title='s'><span>Definidos</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Finanzas</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Comisiones</span></a><ul><li class='last'><a href='~/Finanzas/comisiones.aspx?TitPag=Calculo Comisiones&Tipo=G' title='s'><span>Gestores</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Contrato Carteras</span></a><ul><li><a href='~/Finanzas/contratos_cartera.aspx?TitPag=Contratos Clientes Carteras' title='s'><span>ABM</span></a></li><li class='has-sub'><a href='#' title='s'><span>Clausulas</span></a><ul><li class='last'><a href='~/Finanzas/clausulas_contcart.aspx?TitPag=Clausulas Contratos Cartera' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Maxima Convencional</span></a><ul><li class='last'><a href='~/Finanzas/maxima_convencional.aspx?TitPag=Maxima Convencional' title='s'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Reportes</span></a><ul><li class='last'><a href='~/Finanzas/reportes_finanzas.aspx?TitPag=Reportes Finanzas&PreJud=J&RepPag=255' title='s'><span>Definidos</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Judicial</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Comprobantes</span></a><ul><li><a href='~/Carteras/cabecera_comprobantes.aspx?TitPag=Comprobantes&Tipo=CC&PreJud=J&RepPag=235&Help=176' title='s'><span>ABM</span></a></li><li><a href='~/Carteras/cabecera_comprobantes_aceptar.aspx?TitPag=Aceptar Comprobantes&Tipo=CC&PreJud=J' title='s'><span>Aceptar</span></a></li><li class='last'><a href='~/Carteras/cabecera_comprobantes_buscar.aspx?TitPag=Buscar Comprobantes&Tipo=CC&PreJud=J&RepPag=235' title='s'><span>Buscar</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Entes Judiciales</span></a><ul><li><a href='~/Judicial/entes_judicial.aspx?TitPag=Entes Judiciales' title='s'><span>ABM</span></a></li><li class='last'><a href='~/Judicial/reasignar_entes.aspx?TitPag=Reasignar Casos Entes Judiciales' title='s'><span>Reasignar</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Estados</span></a><ul><li class='last'><a href='~/Carteras/estados_cartera.aspx?TitPag=Estados Cartera Judicial&Tipo=J' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Gestionar Cartera</span></a><ul><li><a href='~/Carteras/gestionar_cartera.aspx?TitPag=Gestionar Cartera&Tipo=J&RepPag=132&Help=164' title='s'><span>ABM</span></a></li><li class='last'><a href='~/Judicial/traspasos_judiciales.aspx?TitPag=Traspasos Judiciales&RepPag=242&Help=242' title='s'><span>Traspasos Judiciales</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Mandatos</span></a><ul><li class='last'><a href='~/Judicial/mandatos.aspx?TitPag=Mandatos' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Materia Judicial</span></a><ul><li class='last'><a href='~/Judicial/materia_judicial.aspx?TitPag=Materia Judicial' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Materia/Estados</span></a><ul><li class='last'><a href='~/Judicial/materia_estados.aspx?TitPag=Materia/Estados&Tipo=J' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Notarias</span></a><ul><li class='last'><a href='~/Judicial/notarias.aspx?TitPag=Notarias' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Reportes</span></a><ul><li><a href='~/Judicial/Dinamicos_Judicial.aspx?TitPag=Dinamicos Judicial' title='s'><span>Dinamicos</span></a></li><li class='last'><a href='~/Carteras/reportes_carteras.aspx?TitPag=Reportes Judiciales&PreJud=J&RepPag=165' title='s'><span>Estandar</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>ROL</span></a><ul><li><a href='~/Judicial/rol.aspx?TitPag=ROL&RepPag=158&Help=158' title='s'><span>ABM</span></a></li><li class='last'><a href='~/Judicial/rol_buscar.aspx?TitPag=ROL Buscar' title='s'><span>Buscar</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Castigos</span></a><ul><li class='last'><a href='~/Carteras/tipos_motivos_castigos.aspx?TitPag=Tipos Motivos Castigos' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Causa</span></a><ul><li class='last'><a href='~/Judicial/tipos_causa.aspx?TitPag=Tipos Causa' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tipos Informe</span></a><ul><li class='last'><a href='~/Judicial/tipos_informes.aspx?Tipos Informes' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tribunales</span></a><ul><li><a href='~/Judicial/tribunales.aspx?TitPag=Tribunales' title='s'><span>ABM</span></a></li><li class='has-sub'><a href='#' title='s'><span>Tipos Tribunales</span></a><ul><li class='last'><a href='~/Judicial/tipos_tribunales.aspx?TitPag=Tipos Tribunales' title='s'><span>ABM</span></a></li></ul></li></ul></li></ul></li><li class='has-sub'><a href='#' title='Herramientas de Configuración del Sistema'><span>Sistema</span></a><ul><li class='has-sub'><a href='#' title='empresa.gif'><span>Empresa</span></a><ul><li class='last'><a href='~/Empresa/empresa.aspx?TitPag=Empresa' title='abm.gif'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='mundo.gif'><span>Parametros</span></a><ul><li class='has-sub'><a href='#' title='mundo.gif'><span>Geograficos</span></a><ul><li><a href='~/Parametros/ciudad.aspx?TitPag=Ciudad' title='mundo_det.gif'><span>Ciudad</span></a></li><li><a href='~/Parametros/comuna.aspx?TitPag=Comuna' title='mundo_det.gif'><span>Comuna</span></a></li><li><a href='~/Parametros/pais.aspx?TitPag=Pais' title='mundo_det.gif'><span>Pais</span></a></li><li class='last'><a href='~/Parametros/region.aspx?TitPag=Region' title='mundo_det.gif'><span>Region</span></a></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Idiomas</span></a><ul><li class='last'><a href='~/Parametros/idiomas.aspx?TitPag=Idiomas' title='abm.gif'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='monedas.gif'><span>Monedas</span></a><ul><li class='last'><a href='~/Parametros/monedas.aspx?TitPag=Monedas' title='abm.gif'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Personal</span></a><ul><li class='has-sub'><a href='#' title='abm.gif'><span>Asistencia</span></a><ul><li><a href='~/Personal/empleados_asistencia.aspx?TitPag=Control Asistencia&RepPag=37' title='abm.gif'><span>Control Asistencia</span></a></li><li><a href='~/Personal/dias_festivos.aspx?TitPag=Dias Festivos' title='abm.gif'><span>Dias Festivos</span></a></li><li class='last'><a href='~/Personal/tipos_asistencia.aspx?TitPag=Tipos Asistencia' title='abm.gif'><span>Tipos Asistencia</span></a></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Empleados</span></a><ul><li><a href='~/Personal/empleados.aspx?TitPag=Empleados' title='abm.gif'><span>ABM</span></a></li><li class='last'><a href='~/Personal/empleados_buscar.aspx?TitPag=Buscar Empleados' title='abm.gif'><span>Buscar</span></a></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Parametros</span></a><ul><li><a href='~/Personal/cargos.aspx?TitPag=Cargos' title='abm.gif'><span>Cargos</span></a></li><li><a href='~/Personal/estados_empleado.aspx?TitPag=Estados Empleado' title='abm.gif'><span>Estados Empleado</span></a></li><li><a href='~/Personal/tipos_contrato.aspx?TitPag=Tipos de Contrato' title='abm.gif'><span>Tipos de Contrato</span></a></li><li class='last'><a href='~/Personal/tipos_doccont.aspx?TitPag=Tipos Documentos Contrato' title='abm.gif'><span>Tipos Documentos Contrato</span></a></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Seguridad Social</span></a><ul><li><a href='~/Personal/afp.aspx?TitPag=AFP' title='abm.gif'><span>AFP</span></a></li><li><a href='~/Personal/caja_compensacion.aspx?TitPag=Caja Compensacion' title='abm.gif'><span>Caja Compensacion</span></a></li><li class='last'><a href='~/Personal/isapres.aspx?TitPag=Isapres' title='abm.gif'><span>Isapres</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='nivel.gif'><span>Seguridad</span></a><ul><li class='has-sub'><a href='#' title='abm.gif'><span>Perfiles</span></a><ul><li><a href='~/Seguridad/perfiles.aspx?TitPag=Perfiles' title='abm.gif'><span>ABM</span></a></li><li class='has-sub'><a href='#' title='abm.gif'><span>Comprobantes</span></a><ul><li class='last'><a href='~/Seguridad/perfiles_comprobantes.aspx?TitPag=Perfiles Comprobantes' title='abm.gif'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Estados Cobranza</span></a><ul><li class='last'><a href='~/Seguridad/perfiles_estados.aspx?TitPag=Perfiles Estados Cobranza' title='abm.gif'><span>ABM</span></a></li></ul></li><li class='last'><a href='~/Seguridad/perfiles_menu.aspx?TitPag=Perfiles Menu' title='abm.gif'><span>Menu</span></a></li></ul></li><li class='has-sub'><a href='#' title='usuarios.gif'><span>Usuarios</span></a><ul><li><a href='~/Seguridad/usuarios.aspx?TitPag=Usuarios' title='abm.gif'><span>ABM</span></a></li><li class='last'><a href='~/Seguridad/usuarios_buscar.aspx?TitPag=Buscar Usuario' title='buscar.gif'><span>Buscar</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='abm.gif'><span>Utilitarios</span></a><ul><li><a href='~/Utilitarios/exportar.aspx?TitPag=Exportar' title='s'><span>Exportar tablas Basicas</span></a></li><li><a href='~/Utilitarios/importar.aspx?TitPag=Importar desde antiguo Horus' title='abm.gif'><span>Importar desde Antiguo Horus</span></a></li><li class='last'><a href='~/Utilitarios/importar_datos_exportados.aspx?TitPag=Importar desde Archivos Respaldados' title='s'><span>Importar desde Archivos Respaldados</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Web</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Eventos</span></a><ul><li class='last'><a href='~/Web/eventos.aspx?TitPag=Eventos' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Noticias</span></a><ul><li class='last'><a href='~/Web/noticias.aspx?TitPag=Noticias' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Servicios</span></a><ul><li class='last'><a href='~/Web/servicios.aspx?TitPag=Servicios' title='s'><span>ABM</span></a></li></ul></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Tesoreria</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Bancos</span></a><ul><li class='last'><a href='~/Tesoreria/bancos.aspx?TitPag=Bancos' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Consulta Pagos</span></a><ul><li><a href='~/Tesoreria/consulta_pagos.aspx?TitPag=Consulta Pagos- Cartera Clientes&Tipo=CC' title='s'><span>Cartera Clientes</span></a></li><li class='last'><a href='~/Tesoreria/consulta_pagos_provcli.aspx?TitPag=Consulta Pagos- Cliente/Proveedor&Tipo=A' title='s'><span>Cliente/Proveedor</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Documentos Caja</span></a><ul><li><a href='~/Tesoreria/documentos_diarios.aspx?TitPag=Documentos Caja&RepPag=169&Help=169' title='s'><span>ABM</span></a></li><li class='has-sub'><a href='#' title='s'><span>Anular Pagos</span></a><ul><li class='last'><a href='~/Tesoreria/aplicaciones_anular.aspx?TitPagAnular Pagos&Help=201' title='s'><span>ABM</span></a></li></ul></li><li><a href='~/Tesoreria/documentos_diarios_buscar.aspx?TitPag=Buscar Documentos Caja' title='s'><span>Buscar</span></a></li><li class='has-sub'><a href='#' title='s'><span>Modificar Pagos</span></a><ul><li class='last'><a href='~/Tesoreria/Aplicaciones_Modificar.aspx?titPag=Modificar Pagos' title='s'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Empresa Bancos</span></a><ul><li class='last'><a href='~/Tesoreria/empresa_bancos.aspx?TitPag=Empresa Bancos' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Estado Documentos</span></a><ul><li class='last'><a href='~/Tesoreria/estados_documentos_diarios.aspx?TitPag=Estado Documentos' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Reportes</span></a><ul><li class='last'><a href='~/Tesoreria/reportes_tesoreria.aspx?TitPag=Reportes Tesoreria&RepPag=268' title='s'><span>Definidos</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Ventas</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Categorizacion</span></a><ul><li class='has-sub'><a href='#' title='s'><span>Asociar Super y Categorias</span></a><ul><li class='last'><a href='~/ProvCli/supcat_categorias.aspx?TitPag=Asociar Super y Categorias&Uti=2' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Categorias</span></a><ul><li class='last'><a href='~/ProvCli/categorias.aspx?TitPag=Categorias&Uti=2' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Super Categoria</span></a><ul><li class='last'><a href='~/ProvCli/supercategorias.aspx?TitPag=Super Categorias&Uti=2' title='s'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Cliente</span></a><ul><li><a href='~/ProvCli/provcli.aspx?TitPag=Cliente&Tipo=V' title='s'><span>ABM</span></a></li><li><a href='~/ProvCli/provcli_buscar.aspx?TitPag=Buscar Cliente&Tipo=V' title='s'><span>Buscar</span></a></li><li class='has-sub'><a href='#' title='s'><span>Datos Despacho</span></a><ul><li class='last'><a href='~/ProvCli/provcli_despachos.aspx?TitPag=Datos Despacho' title='s'><span>ABM</span></a></li></ul></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Comprobantes</span></a><ul><li><a href='~/Carteras/cabecera_comprobantes.aspx?TitPag=Comprobantes&Tipo=V&PreJud=N&RepPag=235&Help=235' title='s'><span>ABM</span></a></li><li><a href='~/Carteras/cabecera_comprobantes_aceptar.aspx?TitPag=Aceptar Comprobantes&Tipo=V&PreJud=N' title='s'><span>Aceptar</span></a></li><li><a href='~/Carteras/cabecera_comprobantes_buscar.aspx?TitPag=Buscar Comprobantes&Tipo=V&PreJud=N&RepPag=235' title='s'><span>Buscar</span></a></li><li><a href='~/Carteras/cabecera_comprobantes_importar.aspx?TitPag=Importar' title='s'><span>Importar</span></a></li><li class='last'><a href='~/Carteras/Cabecera_Comprobantes_ReImputar.aspx?TitPag=Re-Emison Comprobante&Tipo=V' title='s'><span>Re-Emision</span></a></li></ul></li><li><a href='~/ProvCli/giros.aspx?TitPag=Giros' title='s'><span>Giros</span></a></li><li class='has-sub'><a href='#' title='s'><span>Listas de Precios</span></a><ul><li class='last'><a href='~/ProvCli/listas_precios.aspx?TitPag=Listas de Precios&RepPag=233' title='s'><span>ABM</span></a></li></ul></li><li class='has-sub'><a href='#' title='s'><span>Reportes</span></a><ul><li class='last'><a href='~/ProvCli/reportes_provcli.aspx?TitPag=Reportes&RepPag=315&Tipo=V' title='s'><span>Definidos</span></a></li></ul></li><li><a href='~/ProvCli/tipos_contacto.aspx?TitPag=Tipos de Contacto' title='s'><span>Tipos Contacto</span></a></li><li><a href='~/ProvCli/tipos_provcli.aspx?TitPag=Tipos Proveedor/Cliente' title='s'><span>Tipos Proveedor/Clientes</span></a></li><li class='has-sub'><a href='#' title='s'><span>Traspasos Area Cobranza</span></a><ul><li class='last'><a href='~/Carteras/Cabecera_Comprobantes_Cobranza.aspx?TitPag=Traspasar Area Cobranza&Tipo=V' title='s'><span>Traspasar</span></a></li></ul></li></ul></li></ul></div>",
            Nombre = "Dummy",
            NombreEmpresa = "Dummy Enterpise",
            PclId = 0,
            Permisos = 4,
            PrfId = 1,
            User = "Test",
            UserId = 293
        };

        #endregion

        //UserSession objSession = new UserSession();

        public void ActivarSession()
        {
            //this.objSession = (UserSession)Session["Usuario"];
            ViewBag.Menu = objSession.Menu;
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            ViewBag.UserName = textInfo.ToTitleCase(objSession.Nombre.ToLower());
            ViewBag.Cargo = textInfo.ToTitleCase(objSession.Cargo.ToLower());
        }

        //
        // GET: /Mantenedor/

        public ActionResult Index()
        {
            //objSession.CodigoEmpresa = 1;
            Session["Usuario"] = objSession;
            GridModel model = new GridModel();
            model.GridSelect = "1:USA;2:England;3:France;4:Germany";
            ViewBag.Add = false;
            ViewBag.Del = true;
            ViewBag.Edit = false;
            ViewBag.Menu = objSession.Menu;
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            ViewBag.UserName = textInfo.ToTitleCase(objSession.Nombre.ToLower());
            ViewBag.Cargo = textInfo.ToTitleCase(objSession.Cargo.ToLower());
            return View(model);
        }

        public ActionResult Acciones()
        {
            ActivarSession();
            GridModel model = new GridModel();
            model.GridSelect = "Acciones";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            ViewBag.Agrupa = bcp.Accion.ListarGrupoAcciones(objSession.Idioma);
            return View(model);
        }

        public ActionResult MotivoCobranza()
        {
            GridModel model = new GridModel();
            model.GridSelect = "MotivoCobrnaza";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        public ActionResult CodigoCarga()
        {
            GridModel model = new GridModel();
            model.GridSelect = "CodigoCarga";
            ViewBag.Add = true;
            ViewBag.Del = true;
            ViewBag.Edit = true;
            return View(model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(GridModel model)
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<MotivoCobranzaModel> lstMotivo = serializer.Deserialize<List<MotivoCobranzaModel>>(model.GridData);

                // Process returned data here
            }
            catch
            {
            }
            return RedirectToAction("Index");
        }
        #region "Motivo Cobranza"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetMotivoCobranza(GridSettings gridSettings)
        {
            // create json data
            int totalRecords = 1000;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<MotivoCobranzaModel> list = new List<MotivoCobranzaModel>();
            for (int iX = startRow; iX < endRow; iX++)
            {
                MotivoCobranzaModel motivo = new MotivoCobranzaModel();
                motivo.Id = iX;
                motivo.Nombre = "Line# " + iX.ToString();
                list.Add(motivo);
            }

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from MotivoCobranzaModel item in list
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.Nombre
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperMotivoCobranza(bcp.MotivoCobranza model, string oper, int? id)
        {
            model.OperMotivoCobranza(oper, id, objSession);
            return Json("OK");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditMotivoCobranza(FormCollection form)
        {
            string lll = form.ToString();
            return Json(lll);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DelMotivoCobranza(FormCollection form)
        {
            string lll = form.ToString();
            return Json(lll);
        }
        #endregion

        #region "Acciones"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetAcciones(GridSettings gridSettings)
        {
            // create json data 
            bcp.Accion bcpAccion = new Accion();
            List<dto.Accion> lstAccion = bcpAccion.ListarAccionesGrilla(1, 1, gridSettings.where.groupOp, gridSettings.sortColumn.ToString(), gridSettings.sortOrder.ToString(), gridSettings.pageIndex, gridSettings.pageSize);
            
            int totalRecords = lstAccion.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;
           

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from dto.Accion item in lstAccion
                    select new
                    {
                        id = item.IdAccion,
                        cell = new object[]
                        {
                            item.Codemp,
                            item.Idioma,
                            item.IdAccion,
                            item.Nombre,
                            item.Agrupa
                        }
                    }
                ).ToArray()
            };



            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult OperAccion(bcp.Accion model, string oper, int? id)
        {
            model.OperAccion( oper, id, objSession);
            return Json("OK");
        }
        #endregion

        #region "Código de Carga"
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetCodigoCarga(GridSettings gridSettings)
        {
            // create json data
            int totalRecords = 1000;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)gridSettings.pageSize);
            int startRow = (gridSettings.pageIndex - 1) * gridSettings.pageSize;
            int endRow = startRow + gridSettings.pageSize;

            List<CodigoCargaModel> list = new List<CodigoCargaModel>();
            for (int iX = startRow; iX < endRow; iX++)
            {
                CodigoCargaModel codigo = new CodigoCargaModel();
                codigo.IdCliente = iX;
                codigo.CodigoCarga = "Codigo# " + iX.ToString();
                codigo.Descripcion = "Line# " + iX.ToString();
                codigo.NombreCliente = "Cliente#" + iX.ToString();
                codigo.Id = iX;
                list.Add(codigo);
            }

            var jsonData = new
            {
                total = totalPages,
                page = gridSettings.pageIndex,
                records = totalRecords,

                rows =
                (
                    from CodigoCargaModel item in list
                    select new
                    {
                        id = item.Id,
                        cell = new object[]
                        {
                            item.Id,
                            item.IdCliente,
                            item.NombreCliente,
                            item.CodigoCarga,
                            item.Descripcion
                        }
                    }
                ).ToArray()
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        
        #endregion


        public ActionResult ExportToExcel(GridSettings gridSettings)
        {
            var products = new System.Data.DataTable("teste");
            products.Columns.Add("col1", typeof(int));
            products.Columns.Add("col2", typeof(string));

            products.Rows.Add(1, "product 1");
            products.Rows.Add(2, "product 2");
            products.Rows.Add(3, "product 3");
            products.Rows.Add(4, "product 4");
            products.Rows.Add(5, "product 5");
            products.Rows.Add(6, "product 6");
            products.Rows.Add(7, "product 7");

            bcp.Accion objAccion = new Accion();


            var grid = new GridView();
            grid.DataSource = objAccion.ExportarExcel(1, 1, gridSettings.where.groupOp, "Nombre", gridSettings.sortOrder.ToString(), 1, 100000000); ;
            grid.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=MyExcelFile.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return View("MyView");
        }
    }
}

