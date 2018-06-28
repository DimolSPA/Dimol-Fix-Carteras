

Create Procedure Trae_Reporte_FacturasPendientes(@cbc_codemp integer, @clb_tipcpbtdoc char(1), @idi_idid integer) as
SELECT view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         provcli_sucursal.pcs_nombre,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.fpi_nombre,   
         view_cabecera_comprobantes.cbt_estado,   
         view_cabecera_comprobantes.cbc_final,   
         view_cabecera_comprobantes.cbc_saldo,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         view_cabecera_comprobantes.cbc_fecvenc,   
         clasificacion_cpbtdoc.clb_tipcpbtdoc,
         cbc_numprovcli,
         cbc_numero,
         cbc_ordcomp,
         view_cabecera_comprobantes.cbc_tipcambio   
    FROM view_cabecera_comprobantes,   
         provcli_sucursal,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc  
   WHERE ( view_cabecera_comprobantes.cbc_codemp = provcli_sucursal.pcs_codemp ) and  
         ( view_cabecera_comprobantes.cbc_pclid = provcli_sucursal.pcs_pclid ) and  
         ( view_cabecera_comprobantes.cbc_pcsid = provcli_sucursal.pcs_pcsid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc.clb_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc.clb_clbid ) and  
         ( ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( view_cabecera_comprobantes.cbt_estado in ( 'A','B' ) ) AND  
         ( view_cabecera_comprobantes.cbc_saldo > 0 ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc =@clb_tipcpbtdoc ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_contable = 'S' ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid )   
         )
