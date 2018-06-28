

Create Procedure Trae_Reporte_Resumen_VentaComp(@cbc_codemp integer, @cbc_sucid integer, @cbc_desde datetime, @cbc_hasta datetime, @clb_tipcpbtdoc char(1), @idi_idid integer) as
  SELECT view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_neto,   
         view_cabecera_comprobantes.cbc_impuestos,   
         view_cabecera_comprobantes.cbc_retenido,   
         view_cabecera_comprobantes.cbc_descuentos,   
         view_cabecera_comprobantes.cbc_final,   
         view_cabecera_comprobantes.cbc_saldo,   
         view_cabecera_comprobantes.cbc_exento,   
         clasificacion_cpbtdoc_contable.cct_debhab,   
         datepart(year, cbc_feccpbt) as anio,   
         datepart(month, cbc_feccpbt) as mes,
          clb_contable 
    FROM view_cabecera_comprobantes,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc_contable  
   WHERE ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc_contable.cct_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc_contable.cct_clbid ) and  
         ( ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( view_cabecera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( view_cabecera_comprobantes.cbc_feccpbt >= @cbc_desde ) AND  
         ( view_cabecera_comprobantes.cbc_feccpbt <=@cbc_hasta) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc = @clb_tipcpbtdoc ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid and clb_contable = 'S' and clb_libcompra <> 0   )   
         )
