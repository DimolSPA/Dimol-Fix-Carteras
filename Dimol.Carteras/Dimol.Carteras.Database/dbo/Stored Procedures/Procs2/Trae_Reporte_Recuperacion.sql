

Create Procedure Trae_Reporte_Recuperacion(@apl_codemp integer, @apl_desde datetime, @apl_hasta datetime, @ccb_tipcart integer) as
    SELECT view_aplicaciones_doc_cartera_clientes.pcl_rut Rut,    
view_aplicaciones_doc_cartera_clientes.pcl_nomfant Cliente,    
view_aplicaciones_doc_cartera_clientes.tci_nombre Comprobante,    
view_aplicaciones_doc_cartera_clientes.ddi_numcta Numero,    
view_aplicaciones_doc_cartera_clientes.mon_nombre Moneda,    
view_aplicaciones_doc_cartera_clientes.ddi_tipcambio TipCambio,    
case view_aplicaciones_doc_cartera_clientes.ddi_pagdir when 'S' then 'S' else 'N' end PagDir,    
case view_tipos_cpbtdoc_clasificacion.clb_remesa when 'S' then 'S' else 'N' end Comisionable,    
view_aplicaciones_doc_cartera_clientes.ctc_rutp  RutDeudor,    
view_aplicaciones_doc_cartera_clientes.ctc_numero  NumRut,    
view_aplicaciones_doc_cartera_clientes.ctc_digito  DigVer,    
view_aplicaciones_doc_cartera_clientes.ctc_nomfantp Deudor ,    
view_aplicaciones_doc_cartera_clientes.tci_nombrep Comprobante,    
view_aplicaciones_doc_cartera_clientes.ccb_numero Numero,    
view_aplicaciones_doc_cartera_clientes.ccb_fecdoc FecDoc,    
view_aplicaciones_doc_cartera_clientes.ccb_fecvenc FecVenc,    
view_aplicaciones_doc_cartera_clientes.ccb_fecing FecAsig,    
case view_aplicaciones_doc_cartera_clientes.gastjud when 'J' then 'S' else 'N' end GastJud,   
view_aplicaciones_doc_cartera_clientes.api_capital * (apl_accion * -1) Capital ,    
view_aplicaciones_doc_cartera_clientes.api_interes * (apl_accion * -1) Interes ,    
view_aplicaciones_doc_cartera_clientes.api_honorario * (apl_accion * -1) Honorario ,    
view_aplicaciones_doc_cartera_clientes.api_gastpre * (apl_accion * -1) GastPre ,    
view_aplicaciones_doc_cartera_clientes.api_gastjud * (apl_accion * -1) GastJud ,    
view_aplicaciones_doc_cartera_clientes.sbc_rut RutAse,    
view_aplicaciones_doc_cartera_clientes.sbc_nombre Asegurado,    
view_aplicaciones_doc_cartera_clientes.pcc_codigo CodCarg,    
view_aplicaciones_doc_cartera_clientes.pcc_nombre Carga,    
gestor.ges_nombre Gestor,    
view_aplicaciones_doc_cartera_clientes.usr_nombre Usuario, 
view_aplicaciones_doc_cartera_clientes.apl_fecapl FecCanc, 
datepart(year,view_aplicaciones_doc_cartera_clientes.apl_fecapl) AnioCanc, 
datepart(Month,view_aplicaciones_doc_cartera_clientes.apl_fecapl) MesCanc 
FROM {oj view_aplicaciones_doc_cartera_clientes LEFT OUTER JOIN gestor ON view_aplicaciones_doc_cartera_clientes.apl_codemp = gestor.ges_codemp AND view_aplicaciones_doc_cartera_clientes.apl_sucid = gestor.ges_sucid AND view_aplicaciones_doc_cartera_clientes.api_gesid = gestor.ges_gesid},    
view_tipos_cpbtdoc_clasificacion 
WHERE  view_aplicaciones_doc_cartera_clientes.apl_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp  and   
 view_aplicaciones_doc_cartera_clientes.ddi_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid and 
apl_fecapl >=@apl_desde and apl_fecapl <=@apl_hasta and 
ccb_tipcart =@ccb_tipcart and
apl_codemp =@apl_codemp
Union 
SELECT 
view_aplicaciones_cpbt_cartera_clientes.pcl_rut,    
view_aplicaciones_cpbt_cartera_clientes.pcl_nomfant,    
view_aplicaciones_cpbt_cartera_clientes.tci_nombre,    
view_aplicaciones_cpbt_cartera_clientes.cbc_numprovcli,    
view_aplicaciones_cpbt_cartera_clientes.mon_nombre,    
view_aplicaciones_cpbt_cartera_clientes.cbc_tipcambio,    'N' as PagDir,    
case view_tipos_cpbtdoc_clasificacion.clb_remesa when 'S' then 'S' else 'N' end Comisionable,    
view_aplicaciones_cpbt_cartera_clientes.ctc_rut,    
view_aplicaciones_cpbt_cartera_clientes.ctc_numero,    
view_aplicaciones_cpbt_cartera_clientes.ctc_digito,    
view_aplicaciones_cpbt_cartera_clientes.ctc_nomfant,   
view_aplicaciones_cpbt_cartera_clientes.tci_nombrep,    
view_aplicaciones_cpbt_cartera_clientes.ccb_numero,    
view_aplicaciones_cpbt_cartera_clientes.ccb_fecdoc,    
view_aplicaciones_cpbt_cartera_clientes.ccb_fecvenc,    
view_aplicaciones_cpbt_cartera_clientes.ccb_fecing,    
case view_aplicaciones_cpbt_cartera_clientes.cbt_gastjud when 'J' then 'S' else 'N' end GastJud,    
view_aplicaciones_cpbt_cartera_clientes.api_capital * (apl_accion * -1) Capital ,    
view_aplicaciones_cpbt_cartera_clientes.api_interes * (apl_accion * -1) Interes ,    
view_aplicaciones_cpbt_cartera_clientes.api_honorario * (apl_accion * -1) Honorario ,    
view_aplicaciones_cpbt_cartera_clientes.api_gastpre * (apl_accion * -1) GastPre ,   
 view_aplicaciones_cpbt_cartera_clientes.api_gastjud * (apl_accion * -1) GastJud ,    
view_aplicaciones_cpbt_cartera_clientes.sbc_rut,    
view_aplicaciones_cpbt_cartera_clientes.sbc_nombre,    
view_aplicaciones_cpbt_cartera_clientes.pcc_codigo,    
view_aplicaciones_cpbt_cartera_clientes.pcc_nombre,    
gestor.ges_nombre,    
view_aplicaciones_cpbt_cartera_clientes.usr_nombre, 
view_aplicaciones_cpbt_cartera_clientes.apl_fecapl, datepart(year,view_aplicaciones_cpbt_cartera_clientes.apl_fecapl) AnioCanc, 
datepart(Month,view_aplicaciones_cpbt_cartera_clientes.apl_fecapl) MesCanc 
FROM {oj view_aplicaciones_cpbt_cartera_clientes LEFT OUTER JOIN gestor ON view_aplicaciones_cpbt_cartera_clientes.apl_codemp = gestor.ges_codemp AND view_aplicaciones_cpbt_cartera_clientes.apl_sucid = gestor.ges_sucid AND view_aplicaciones_cpbt_cartera_clientes.api_gesid = gestor.ges_gesid},    
view_tipos_cpbtdoc_clasificacion 
WHERE  view_aplicaciones_cpbt_cartera_clientes.apl_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp  and    
view_aplicaciones_cpbt_cartera_clientes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid   and 
apl_fecapl >=@apl_desde and apl_fecapl <=@apl_hasta and 
ccb_tipcart =@ccb_tipcart and
apl_codemp = @apl_codemp
order by FecCanc
