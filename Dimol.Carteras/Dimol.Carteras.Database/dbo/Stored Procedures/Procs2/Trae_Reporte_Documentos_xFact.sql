

Create Procedure Trae_Reporte_Documentos_xFact(@cbc_codemp integer, @cbc_pclid integer, @idi_idid integer) as
  SELECT view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         view_cabecera_comprobantes.cbc_fecvenc,   
         view_cabecera_comprobantes.cbt_estado,   
         view_cabecera_comprobantes.mon_nombre,   
         view_cabecera_comprobantes.cbc_final,   
         view_cabecera_comprobantes.cbc_saldo,   
         view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc  
    FROM view_cabecera_comprobantes,   
         view_tipos_cpbtdoc_clasificacion  
   WHERE ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( view_cabecera_comprobantes.cbc_pclid = @cbc_pclid ) AND  
         ( view_cabecera_comprobantes.cbt_estado in ( 'E','A' ) ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_contable = 'N' ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc = 'V' ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid )   
         )
