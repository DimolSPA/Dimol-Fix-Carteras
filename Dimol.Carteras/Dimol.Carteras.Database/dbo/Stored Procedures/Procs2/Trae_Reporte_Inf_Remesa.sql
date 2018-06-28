

Create Procedure Trae_Reporte_Inf_Remesa(@dcc_codemp integer, @dcc_sucid integer, @dcc_tpcid integer, @dcc_numero numeric(15), @idi_idid integer) as
 SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         detalle_comprobantes.dcc_porcfact,   
         detalle_comprobantes.dcc_numero,   
         aplicaciones_items.api_numapl,   
         aplicaciones_items.api_item,   
         cartera_clientes_documentos_cpbt_doc.ccb_numesp,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         documentos_diarios.ddi_tipcambio,   
         cabacera_comprobantes.cbc_feccpbt,
         ctc_nomfant,
         apl_fecapl,
         ctc_intcli,
         ctc_honcli,
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,
         cabacera_comprobantes.cbc_glosa,
         ddi_numcta,
         tipos_cpbtdoc_idiomas.tci_nombre    as TipDocDia,
         cbc_glosa,
         cbc_numero, cbc_numprovcli
    FROM detalle_comprobantes,   
         aplicaciones_items,   
         cartera_clientes_documentos_cpbt_doc,   
         idiomas,   
         documentos_diarios,   
         cabacera_comprobantes,
         aplicaciones,
         contratos_clientes,
         tipos_cpbtdoc_idiomas  
   WHERE ( aplicaciones_items.api_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( aplicaciones_items.api_sucid = detalle_comprobantes.dcc_sucid ) and  
         ( aplicaciones_items.api_anio = detalle_comprobantes.dcc_anio ) and  
         ( aplicaciones_items.api_numapl = detalle_comprobantes.dcc_numapl ) and  
         ( aplicaciones_items.api_item = detalle_comprobantes.dcc_itemapl ) and  
         ( aplicaciones_items.api_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( aplicaciones_items.api_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( aplicaciones_items.api_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( aplicaciones_items.api_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( documentos_diarios.ddi_codemp = aplicaciones_items.api_codemp ) and  
         ( documentos_diarios.ddi_sucid = aplicaciones_items.api_sucid ) and  
         ( documentos_diarios.ddi_anio = aplicaciones_items.api_aniodoc ) and  
         ( documentos_diarios.ddi_numdoc = aplicaciones_items.api_numdoc ) and  

         ( documentos_diarios.ddi_codemp = tipos_cpbtdoc_idiomas.tci_codemp ) and  
         ( documentos_diarios.ddi_tpcid = tipos_cpbtdoc_idiomas.tci_tpcid ) and  

         ( detalle_comprobantes.dcc_codemp = cabacera_comprobantes.cbc_codemp ) and  
         ( detalle_comprobantes.dcc_sucid = cabacera_comprobantes.cbc_sucid ) and  
         ( detalle_comprobantes.dcc_tpcid = cabacera_comprobantes.cbc_tpcid ) and  
         ( detalle_comprobantes.dcc_numero = cabacera_comprobantes.cbc_numero ) and  

         ( apl_codemp = api_codemp ) and  
         ( apl_sucid = api_sucid ) and  
         ( apl_anio = api_anio ) and  
         ( apl_numapl = api_numapl ) and  

         ( ccb_codemp = contratos_clientes.ctc_codemp ) and  
         ( ccb_pclid = contratos_clientes.ctc_pclid ) and  
         ( ccb_cctid = contratos_clientes.ctc_cctid ) and  

         ( ( detalle_comprobantes.dcc_codemp = @dcc_codemp ) AND  
         ( detalle_comprobantes.dcc_sucid = @dcc_sucid ) AND  
         ( detalle_comprobantes.dcc_tpcid = @dcc_tpcid ) AND  
         ( detalle_comprobantes.dcc_numero = @dcc_numero ) AND  
         ( idiomas.idi_idid = @idi_idid and tipos_cpbtdoc_idiomas.tci_idid = @idi_idid  )   
         )
