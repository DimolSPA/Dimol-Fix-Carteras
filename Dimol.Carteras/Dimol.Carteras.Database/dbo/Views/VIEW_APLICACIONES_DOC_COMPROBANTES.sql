
/*==============================================================*/
/* View: VIEW_APLICACIONES_DOC_COMPROBANTES                     */
/*==============================================================*/
create view VIEW_APLICACIONES_DOC_COMPROBANTES as
SELECT aplicaciones.apl_codemp,   
         aplicaciones.apl_sucid,   
         aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_numapl,   
         aplicaciones_items.api_item,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_tipo,   
         aplicaciones.apl_accion,   
         aplicaciones.apl_fecing,   
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
         view_cabecera_comprobantes.tci_nombre as tci_nombreP,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_pclid,   
         view_cabecera_comprobantes.pcl_rut as pcl_rutP,   
         view_cabecera_comprobantes.pcl_nomfant as pcl_nomfantP,   
         view_cabecera_comprobantes.cbt_gastjud,   
         aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,   
         usuarios.usr_usrid,   
         usuarios.usr_nombre,
         view_documentos_diarios.ddi_tpcid,     
         view_documentos_diarios.ctc_numero,
         view_documentos_diarios.ctc_digito,
         view_cabecera_comprobantes.cbc_final     
    FROM aplicaciones,   
         aplicaciones_items,   
         view_documentos_diarios,   
         view_cabecera_comprobantes,   
         idiomas,   
         usuarios  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = view_documentos_diarios.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = view_documentos_diarios.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc = view_documentos_diarios.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc = view_documentos_diarios.ddi_numdoc ) and  
         ( aplicaciones_items.api_codemp = view_cabecera_comprobantes.cbc_codemp ) and  
         ( aplicaciones_items.api_sucid = view_cabecera_comprobantes.cbc_sucid ) and  
         ( aplicaciones_items.api_tpcid = view_cabecera_comprobantes.cbc_tpcid ) and  
         ( aplicaciones_items.api_numero = view_cabecera_comprobantes.cbc_numero ) and  
         ( aplicaciones.apl_codemp = usuarios.usr_codemp ) and  
         ( aplicaciones.apl_usrid = usuarios.usr_usrid ) and  
         ( view_documentos_diarios.edi_idiid = idiomas.idi_idid ) and  
         ( view_documentos_diarios.tci_idid = idiomas.idi_idid and view_cabecera_comprobantes.idi_idid =idiomas.idi_idid  )
