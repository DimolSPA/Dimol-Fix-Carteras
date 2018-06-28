

Create Procedure Trae_Reporte_Flujo_Caja(@cbc_codemp integer, @idi_idid integer) as
  SELECT view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_saldo,   
         view_cabecera_comprobantes.cbc_tipcambio,   
         view_cabecera_comprobantes.cbc_fecvenc,   
         view_tipos_cpbtdoc_clasificacion.clb_cptoctbl,   
         clasificacion_cpbtdoc_contable.cct_debhab,   
         view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc
    FROM view_cabecera_comprobantes,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc_contable  
   WHERE ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc_contable.cct_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc_contable.cct_clbid ) and  
         ( ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_contable = 'S' ) AND  
         ( view_cabecera_comprobantes.cbt_estado in ( 'A','B' ) )  and cbc_saldo > 0 and idi_idid = @idi_idid )
   UNION   
  SELECT view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.ddi_saldo,   
         view_documentos_diarios.ddi_tipcambio,   
         view_documentos_diarios.ddi_fecvenc,   
         view_tipos_cpbtdoc_clasificacion.clb_cptoctbl,   
         clasificacion_cpbtdoc_contable.cct_debhab,
          view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc
    FROM view_documentos_diarios,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc_contable,   
         estados_documentos_diarios  
   WHERE ( view_documentos_diarios.ddi_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_documentos_diarios.ddi_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc_contable.cct_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc_contable.cct_clbid ) and  
         ( view_documentos_diarios.ddi_codemp = estados_documentos_diarios.edc_codemp ) and  
         ( view_documentos_diarios.ddi_edcid = estados_documentos_diarios.edc_edcid ) and  
         ( ( view_documentos_diarios.ddi_codemp = @cbc_codemp ) AND  
         ( estados_documentos_diarios.edc_estado not in ( 3,4 ) ) AND  
         ( view_documentos_diarios.tci_idid = @idi_idid  and ddi_saldo > 0 )
         )
