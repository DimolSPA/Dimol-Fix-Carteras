

Create Procedure Trae_Reporte_Remesa_Pendiente(@apl_codemp integer, @apl_sucid integer, @ccb_pclid integer, @edi_idiid integer) as
  SELECT aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_accion,   
         view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.ddi_custodia,   
         view_documentos_diarios.ddi_docemp,   
         view_documentos_diarios.ddi_pagdir,   
         view_documentos_diarios.mon_nombre,   
         view_documentos_diarios.ddi_tipcambio,   
         view_documentos_diarios.ddi_monto,   
         view_documentos_diarios.ddi_saldo,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,
         ctc_intcli,
         ctc_honcli,
         view_documentos_diarios.ddi_docemp     
    FROM aplicaciones,   
         aplicaciones_items,   
         view_documentos_diarios,   
         cartera_clientes_documentos_cpbt_doc,   
         view_tipos_cpbtdoc_clasificacion,
         contratos_clientes  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = view_documentos_diarios.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = view_documentos_diarios.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc = view_documentos_diarios.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc = view_documentos_diarios.ddi_numdoc ) and  
         ( aplicaciones_items.api_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( aplicaciones_items.api_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( aplicaciones_items.api_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( aplicaciones_items.api_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( view_documentos_diarios.ddi_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_documentos_diarios.ddi_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_documentos_diarios.edi_idiid = cartera_clientes_documentos_cpbt_doc.tci_idid ) and  
         ( view_documentos_diarios.edi_idiid = cartera_clientes_documentos_cpbt_doc.eci_idid ) and  
         ( view_documentos_diarios.edi_idiid = cartera_clientes_documentos_cpbt_doc.mci_idid ) and  

         ( ccb_codemp = ctc_codemp ) and  
         ( ccb_pclid = ctc_pclid) and  
         ( ccb_cctid = ctc_cctid ) and  

         ( ( aplicaciones.apl_codemp = @apl_codemp ) AND  
         ( aplicaciones.apl_sucid = @apl_sucid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_remesa = 'S' and api_remesa = 'N') AND  
         ( convert(varchar, api_anio) + '_' + convert(varchar, api_numapl) + '_' + convert(varchar, api_item) not in (  SELECT convert(varchar, dcc_anio) + '_' + convert(varchar, dcc_numapl) + '_' + convert(varchar, dcc_itemapl)  
                                                                                                                          FROM detalle_comprobantes,   
                                                                                                                               view_tipos_cpbtdoc_clasificacion,
																								cabacera_comprobantes  
                                                                                                                         WHERE ( detalle_comprobantes.dcc_codemp =cbc_codemp ) and  
																										( detalle_comprobantes.dcc_sucid =cbc_sucid ) and  
																										( detalle_comprobantes.dcc_tpcid =cbc_tpcid ) and  
																										( detalle_comprobantes.dcc_numero =cbc_numero ) and  
																								 ( detalle_comprobantes.dcc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
                                                                                                                               ( detalle_comprobantes.dcc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
                                                                                                                               ( ( detalle_comprobantes.dcc_codemp = @apl_codemp ) AND  
                                                                                                                               ( detalle_comprobantes.dcc_sucid = @apl_sucid ) AND  
                                                                                                                               ( view_tipos_cpbtdoc_clasificacion.clb_selapl = 'S' and cbc_pclid=@ccb_pclid )   
                                                                                                                               )  )) AND  
         ( view_documentos_diarios.edi_idiid = @edi_idiid )   
         )
