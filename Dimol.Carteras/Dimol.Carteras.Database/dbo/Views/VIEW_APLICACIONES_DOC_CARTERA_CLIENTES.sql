
/*==============================================================*/
/* View: VIEW_APLICACIONES_DOC_CARTERA_CLIENTES                 */
/*==============================================================*/
create view VIEW_APLICACIONES_DOC_CARTERA_CLIENTES as
SELECT DISTINCT aplicaciones.apl_codemp,   
         aplicaciones.apl_sucid,   
         aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_numapl,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_tipo,   
         aplicaciones.apl_accion,   
         aplicaciones.apl_fecing, 
         view_documentos_diarios.ddi_tpcid,  
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.ddi_tipcambio,   
         view_documentos_diarios.mon_nombre,   
         view_documentos_diarios.ddi_pclid,   
         view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.ddi_ctcid,   
         view_documentos_diarios.ctc_rut,   
         view_documentos_diarios.ctc_nomfant,   
         view_documentos_diarios.ddi_emplid,   
         view_documentos_diarios.epl_rut,   
         view_documentos_diarios.epl_nombre,   
         view_documentos_diarios.epl_apepat,   
         view_documentos_diarios.ddi_custodia,   
         view_documentos_diarios.ddi_docemp,   
         view_documentos_diarios.ddi_pagdir,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre as tci_nombreP,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_pclid,   
         cartera_clientes_documentos_cpbt_doc.pcl_rut as pcl_rutP,   
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant as pcl_nomfantP ,   
         cartera_clientes_documentos_cpbt_doc.ccb_ctcid as ccb_ctcidP ,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut as ctc_rutP,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant as ctc_nomfantP,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc, 
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,
         cartera_clientes_documentos_cpbt_doc.ccb_tipcart,
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,
         cartera_clientes_documentos_cpbt_doc.ccb_monto,
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,
         cartera_clientes_documentos_cpbt_doc.ccb_numesp,
         'N' as gastjud,   
         aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_codid,   
         cartera_clientes_documentos_cpbt_doc.pcc_codigo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         usuarios.usr_usrid,   
         usuarios.usr_nombre,   
         idiomas.idi_idid,
         aplicaciones_items.api_item,
         cartera_clientes_documentos_cpbt_doc.ccb_ccbid,  
        cartera_clientes_documentos_cpbt_doc.ccb_ctcid
    FROM aplicaciones,   
         aplicaciones_items,   
         view_documentos_diarios,   
         idiomas,   
         usuarios,   
         cartera_clientes_documentos_cpbt_doc  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = view_documentos_diarios.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = view_documentos_diarios.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc = view_documentos_diarios.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc = view_documentos_diarios.ddi_numdoc ) and  
         ( aplicaciones.apl_codemp = usuarios.usr_codemp ) and  
         ( aplicaciones.apl_usrid = usuarios.usr_usrid ) and  
         ( view_documentos_diarios.edi_idiid = idiomas.idi_idid ) and  
         ( view_documentos_diarios.tci_idid = idiomas.idi_idid ) and  
         ( aplicaciones_items.api_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( aplicaciones_items.api_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( aplicaciones_items.api_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( aplicaciones_items.api_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid )
