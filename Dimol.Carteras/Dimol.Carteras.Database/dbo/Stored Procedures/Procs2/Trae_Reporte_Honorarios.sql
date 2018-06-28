

Create Procedure Trae_Reporte_Honorarios(@cbc_codemp integer, @desde datetime, @hasta datetime, @idi_idid integer) as
 SELECT DISTINCT view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         asientos_contables.ast_fecperiodo,   
         view_cabecera_comprobantes.cbc_neto,   
         view_cabecera_comprobantes.cbc_retenido,   
         view_cabecera_comprobantes.cbc_final,   
         view_cabecera_comprobantes.mon_nombre,   
         view_cabecera_comprobantes.cbt_estado,   
         provcli_sucursal.pcs_direccion,   
         view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         view_datos_geograficos.com_codpost  
    FROM view_cabecera_comprobantes,   
         asientos_contables,   
         asientos_contables_detalle,   
         asientos_contables_cpbtdoc_apl,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc_contable,   
         provcli_sucursal,   
         view_datos_geograficos  
   WHERE ( asientos_contables_detalle.acd_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_detalle.acd_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_detalle.acd_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_detalle.acd_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numero = asientos_contables.ast_numero ) and  
         ( view_cabecera_comprobantes.cbc_codemp = asientos_contables_cpbtdoc_apl.ada_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = asientos_contables_cpbtdoc_apl.ada_tpcid ) and  
         ( view_cabecera_comprobantes.cbc_numero = asientos_contables_cpbtdoc_apl.ada_numcpbt ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc_contable.cct_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc_contable.cct_clbid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = provcli_sucursal.pcs_codemp ) and  
         ( view_cabecera_comprobantes.cbc_pclid = provcli_sucursal.pcs_pclid ) and  
         ( view_cabecera_comprobantes.cbc_pcsid = provcli_sucursal.pcs_pcsid ) and  
         ( provcli_sucursal.pcs_comid = view_datos_geograficos.com_comid ) and  
         ( ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( clasificacion_cpbtdoc_contable.cct_honorarios = 'S' ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid ) and asientos_contables.ast_estado in('V','P')  )
