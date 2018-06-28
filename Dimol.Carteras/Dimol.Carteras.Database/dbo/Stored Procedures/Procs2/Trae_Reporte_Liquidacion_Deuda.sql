

Create Procedure Trae_Reporte_Liquidacion_Deuda(@apl_codemp integer, @apl_sucid integer, @ccb_ctcid integer, @apl_fecapl datetime, @idi_idid integer) as
  SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones.apl_fecapl,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.bco_nombre,   
         view_documentos_diarios.mon_nombre,   
         view_documentos_diarios.ddi_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_codemp,   
         cartera_clientes_documentos_cpbt_doc.ccb_pclid,   
         cartera_clientes_documentos_cpbt_doc.ccb_ctcid,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastjud,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastotro,   
         cartera_clientes_documentos_cpbt_doc.ccb_intereses,   
         cartera_clientes_documentos_cpbt_doc.ccb_honorarios,
         pai_nombre,
         reg_nombre,
         ciu_nombre,
         com_nombre,
         com_codpost,
         ctc_direccion,
         ddi_tipcambio,
         api_anio,
         api_numapl,
         ccb_codmon,
         ddi_numcta,
         ges_nombre,
         ddi_rutpag,
         ddi_nompag,
         apl_accion       
    FROM aplicaciones,   
         aplicaciones_items,   
         cartera_clientes_documentos_cpbt_doc,   
         view_documentos_diarios,   
         tipos_cpbtdoc,   
         clasificacion_cpbtdoc,   
         idiomas,
         view_datos_geograficos, 
         gestor  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = view_documentos_diarios.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = view_documentos_diarios.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc = view_documentos_diarios.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc = view_documentos_diarios.ddi_numdoc ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = aplicaciones_items.api_codemp ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = aplicaciones_items.api_pclid ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = aplicaciones_items.api_ctcid ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ccbid = aplicaciones_items.api_ccbid ) and  
         ( view_documentos_diarios.ddi_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( view_documentos_diarios.ddi_tpcid = tipos_cpbtdoc.tpc_tpcid ) and  
         ( clasificacion_cpbtdoc.clb_codemp = tipos_cpbtdoc.tpc_codemp ) and  
         ( clasificacion_cpbtdoc.clb_clbid = tipos_cpbtdoc.tpc_clbid ) and  
         ( view_documentos_diarios.tci_idid = idiomas.idi_idid ) and  
         ( view_documentos_diarios.edi_idiid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( ctc_comid = com_comid ) and  
         ( api_codemp = ges_codemp ) and  
         ( api_sucid = ges_sucid ) and  
         ( api_gesid = ges_gesid ) and  
         ( ( aplicaciones.apl_codemp = @apl_codemp ) AND  
         ( aplicaciones.apl_sucid = @apl_sucid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( idiomas.idi_idid =@idi_idid  and clb_cptoctbl = 'I' and datepart(year,apl_fecapl) = datepart(year, @apl_fecapl)   and datepart(month,apl_fecapl) = datepart(month, @apl_fecapl)  and datepart(day,apl_fecapl) = datepart(day, @apl_fecapl)  and eci_estid > 1)   
         )   
ORDER BY aplicaciones.apl_fecapl DESC
