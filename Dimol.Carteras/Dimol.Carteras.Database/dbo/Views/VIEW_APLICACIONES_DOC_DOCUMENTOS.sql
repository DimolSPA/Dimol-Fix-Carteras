
/*==============================================================*/
/* View: VIEW_APLICACIONES_DOC_DOCUMENTOS                       */
/*==============================================================*/
create view VIEW_APLICACIONES_DOC_DOCUMENTOS as
  SELECT DISTINCT aplicaciones.apl_codemp,   
         aplicaciones.apl_sucid,   
         aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_numapl,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_tipo,   
         aplicaciones.apl_accion,   
         aplicaciones.apl_fecing,   
         view_documentos_diarios_a.tci_nombre,   
         view_documentos_diarios_a.ddi_numcta,   
         view_documentos_diarios_a.ddi_tipcambio,   
         view_documentos_diarios_a.mon_nombre,   
         view_documentos_diarios_a.ddi_pclid,   
         view_documentos_diarios_a.pcl_rut,   
         view_documentos_diarios_a.pcl_nomfant,   
         view_documentos_diarios_a.ddi_ctcid,   
         view_documentos_diarios_a.ctc_rut,   
         view_documentos_diarios_a.ctc_nomfant,   
         view_documentos_diarios_a.ddi_emplid,   
         view_documentos_diarios_a.epl_rut,   
         view_documentos_diarios_a.epl_nombre,   
         view_documentos_diarios_a.epl_apepat,   
         view_documentos_diarios_a.ddi_custodia,   
         view_documentos_diarios_a.ddi_docemp,   
         view_documentos_diarios_a.ddi_pagdir,   
         'N' as gastjud,   
         aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,   
         view_documentos_diarios_b.tci_nombre as tci_nombreP ,   
         view_documentos_diarios_b.ddi_numcta as ddi_numctaP,   
         view_documentos_diarios_b.ddi_pclid as ddi_pclidP,   
         view_documentos_diarios_b.pcl_rut as pcl_rutP,   
         view_documentos_diarios_b.pcl_nomfant as pcl_nomfantP,   
         view_documentos_diarios_b.ddi_ctcid as ddi_ctcidP,   
         view_documentos_diarios_b.ctc_rut as ctc_rutP,   
         view_documentos_diarios_b.ctc_nomfant as ctc_nomfantP,   
         view_documentos_diarios_b.ddi_emplid as ddi_emplidP,   
         view_documentos_diarios_b.epl_rut as epl_rutP,   
         view_documentos_diarios_b.epl_nombre as epl_nombreP,   
         view_documentos_diarios_b.epl_apepat   as epl_apepatP
    FROM aplicaciones,   
         aplicaciones_items,   
         view_documentos_diarios view_documentos_diarios_a,   
         idiomas,   
         usuarios,   
         view_documentos_diarios view_documentos_diarios_b  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = view_documentos_diarios_a.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = view_documentos_diarios_a.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc = view_documentos_diarios_a.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc = view_documentos_diarios_a.ddi_numdoc ) and  
         ( aplicaciones.apl_codemp = usuarios.usr_codemp ) and  
         ( aplicaciones.apl_usrid = usuarios.usr_usrid ) and  
         ( view_documentos_diarios_a.edi_idiid = idiomas.idi_idid ) and  
         ( view_documentos_diarios_a.tci_idid = idiomas.idi_idid ) and  
         ( aplicaciones_items.api_codemp = view_documentos_diarios_b.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = view_documentos_diarios_b.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc2 = view_documentos_diarios_b.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc2 = view_documentos_diarios_b.ddi_numdoc )
