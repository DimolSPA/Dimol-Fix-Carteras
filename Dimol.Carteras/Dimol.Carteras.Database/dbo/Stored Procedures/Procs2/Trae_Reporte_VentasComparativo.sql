

Create Procedure Trae_Reporte_VentasComparativo(@cbc_codemp integer, @idi_idid integer, @desde datetime, @hasta datetime) as
 SELECT view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_tipcambio,   
         view_cabecera_comprobantes.cbc_anio,   
         view_cabecera_comprobantes.cbc_mes,   
         view_cabecera_comprobantes.cbc_final,   
         clasificacion_cpbtdoc_contable.cct_debhab  
    FROM view_cabecera_comprobantes,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc_contable  
   WHERE ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc_contable.cct_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc_contable.cct_clbid ) and  
         ( ( view_cabecera_comprobantes.cbc_codemp =@cbc_codemp ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid  ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc = 'V' ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_contable = 'S' ) AND  
         ( view_cabecera_comprobantes.cbt_estado <> 'X' and  view_cabecera_comprobantes.cbc_feccpbt between @desde and @hasta )   
         )
