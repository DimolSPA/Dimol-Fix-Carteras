
/*==============================================================*/
/* View: VIEW_APLICACIONES_CPBT_CPBT                            */
/*==============================================================*/
create view VIEW_APLICACIONES_CPBT_CPBT as
  SELECT DISTINCT aplicaciones.apl_codemp,   
         aplicaciones.apl_sucid,   
         aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_numapl,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_tipo,   
         aplicaciones.apl_accion,   
         aplicaciones.apl_fecing,   
         view_cabecera_comprobantes_a.cbc_tpcid,   
         view_cabecera_comprobantes_a.tci_nombre,   
         view_cabecera_comprobantes_a.cbc_numprovcli,   
         view_cabecera_comprobantes_a.cbc_pclid,   
         view_cabecera_comprobantes_a.pcl_rut,   
         view_cabecera_comprobantes_a.pcl_nomfant,   
         view_cabecera_comprobantes_a.cbc_tipcambio,
         view_cabecera_comprobantes_a.mon_nombre,   
         view_cabecera_comprobantes_b.tci_nombre as tci_nombreP,   
         view_cabecera_comprobantes_b.cbc_numprovcli as cbc_numprovcliP,   
         view_cabecera_comprobantes_b.pcl_rut as pcl_rutP,   
         view_cabecera_comprobantes_b.pcl_nomfant as pcl_nomfantP,   
         view_cabecera_comprobantes_b.idi_idid,   
         usuarios.usr_usrid,   
         usuarios.usr_nombre,
         view_cabecera_comprobantes_b.cbc_final,
         api_capital 
    FROM aplicaciones,   
         aplicaciones_items,   
         view_cabecera_comprobantes view_cabecera_comprobantes_a,   
         view_cabecera_comprobantes view_cabecera_comprobantes_b,   
         usuarios  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = view_cabecera_comprobantes_a.cbc_codemp ) and  
         ( aplicaciones_items.api_sucid = view_cabecera_comprobantes_a.cbc_sucid ) and  
         ( aplicaciones_items.api_tpcid = view_cabecera_comprobantes_a.cbc_tpcid ) and  
         ( aplicaciones_items.api_numero = view_cabecera_comprobantes_a.cbc_numero ) and  
         ( aplicaciones_items.api_codemp = view_cabecera_comprobantes_b.cbc_codemp ) and  
         ( aplicaciones_items.api_sucid = view_cabecera_comprobantes_b.cbc_sucid ) and  
         ( aplicaciones_items.api_tpcid2 = view_cabecera_comprobantes_b.cbc_tpcid ) and  
         ( aplicaciones_items.api_numero2 = view_cabecera_comprobantes_b.cbc_numero ) and  
         ( aplicaciones.apl_codemp = usuarios.usr_codemp ) and  
         ( aplicaciones.apl_usrid = usuarios.usr_usrid )
