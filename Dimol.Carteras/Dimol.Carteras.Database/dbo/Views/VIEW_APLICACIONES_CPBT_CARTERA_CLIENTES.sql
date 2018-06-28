
/*==============================================================*/
/* View: VIEW_APLICACIONES_CPBT_CARTERA_CLIENTES                */
/*==============================================================*/
create view VIEW_APLICACIONES_CPBT_CARTERA_CLIENTES as
SELECT DISTINCT aplicaciones.apl_codemp,   
         aplicaciones.apl_sucid,   
         aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_numapl,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_tipo,   
         aplicaciones.apl_accion,   
         aplicaciones.apl_fecing,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_pclid,   
         view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.cbc_tipcambio,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre as tci_nombreP ,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_ctcid,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ccb_numesp,   
         cartera_clientes_documentos_cpbt_doc.ccb_numagrupa,   
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_codigo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_codid,   
         cartera_clientes_documentos_cpbt_doc.ccb_tipcart,  
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,  
         cartera_clientes_documentos_cpbt_doc.ccb_monto,  
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,  
         usuarios.usr_usrid,   
         usuarios.usr_nombre,   
         view_cabecera_comprobantes.cbc_tpcid,
         view_cabecera_comprobantes.cbt_gastjud,
          aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,
  cartera_clientes_documentos_cpbt_doc.ccb_ccbid   
    FROM aplicaciones,   
         aplicaciones_items,   
         idiomas,   
         usuarios,   
         view_cabecera_comprobantes,   
         cartera_clientes_documentos_cpbt_doc  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones.apl_codemp = usuarios.usr_codemp ) and  
         ( aplicaciones.apl_usrid = usuarios.usr_usrid ) and  
         ( aplicaciones_items.api_codemp = view_cabecera_comprobantes.cbc_codemp ) and  
         ( aplicaciones_items.api_codemp = view_cabecera_comprobantes.cbc_sucid ) and  
         ( aplicaciones_items.api_tpcid = view_cabecera_comprobantes.cbc_tpcid ) and  
         ( aplicaciones_items.api_numero = view_cabecera_comprobantes.cbc_numero ) and  
         ( aplicaciones_items.api_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( aplicaciones_items.api_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( aplicaciones_items.api_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( aplicaciones_items.api_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( view_cabecera_comprobantes.idi_idid = idiomas.idi_idid )
