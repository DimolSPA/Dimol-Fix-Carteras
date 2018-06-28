

Create Procedure Trae_Reporte_GasPend_Fact(@cbc_codemp integer) as  
  SELECT DISTINCT view_cabecera_comprobantes.cbc_tpcid,   
         view_cabecera_comprobantes.cbc_numero,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_neto,   
         view_cabecera_comprobantes.cbc_impuestos,   
         view_cabecera_comprobantes.cbc_retenido,   
         view_cabecera_comprobantes.cbc_final,   
         view_cabecera_comprobantes.cbc_exento,   
         insumos.ins_codigo,   
         insumos.ins_nombre,   
         detalle_comprobantes.dcc_prereal,   
         detalle_comprobantes.dcc_precio,   
         detalle_comprobantes.dcc_impuesto,   
         detalle_comprobantes.dcc_retenido,   
         subcarteras.sbc_rut,   
         subcarteras.sbc_nombre,   
         rol.rol_numero,   
         detalle_comprobantes_rol.dcr_monto,   
         detalle_comprobantes.dcc_pclid,   
         rol.rol_pclid,   
         deudores.ctc_numero,   
         deudores.ctc_digito,   
         deudores.ctc_nomfant,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         view_cabecera_comprobantes.cbc_fecvenc,
         cbc_codemp     
         into #DocPen  
    FROM {oj detalle_comprobantes LEFT OUTER JOIN detalle_comprobantes_rol ON detalle_comprobantes.dcc_codemp = detalle_comprobantes_rol.dcr_codemp AND detalle_comprobantes.dcc_sucid = detalle_comprobantes_rol.dcr_sucid AND detalle_comprobantes.dcc_tpcid = detalle_comprobantes_rol.dcr_tpcid AND detalle_comprobantes.dcc_numero = detalle_comprobantes_rol.dcr_numero AND detalle_comprobantes.dcc_item = detalle_comprobantes_rol.dcr_item LEFT OUTER JOIN rol ON detalle_comprobantes_rol.dcr_codemp =
 rol.rol_codemp AND detalle_comprobantes_rol.dcr_rolid = rol.rol_rolid LEFT OUTER JOIN rol_documentos ON rol.rol_codemp = rol_documentos.rdc_codemp AND rol.rol_rolid = rol_documentos.rdc_rolid}, {oj cartera_clientes_cpbt_doc LEFT OUTER JOIN subcarteras ON cartera_clientes_cpbt_doc.ccb_codemp = subcarteras.sbc_codemp AND cartera_clientes_cpbt_doc.ccb_sbcid = subcarteras.sbc_sbcid},   
         view_cabecera_comprobantes,   
         insumos,   
         deudores  
   WHERE ( view_cabecera_comprobantes.cbc_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_sucid = detalle_comprobantes.dcc_sucid ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = detalle_comprobantes.dcc_tpcid ) and  
         ( view_cabecera_comprobantes.cbc_numero = detalle_comprobantes.dcc_numero ) and  
         ( insumos.ins_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( insumos.ins_insid = detalle_comprobantes.dcc_insid ) and  
         ( rol_documentos.rdc_codemp = cartera_clientes_cpbt_doc.ccb_codemp ) and  
         ( rol_documentos.rdc_pclid = cartera_clientes_cpbt_doc.ccb_pclid ) and  
         ( rol_documentos.rdc_ctcid = cartera_clientes_cpbt_doc.ccb_ctcid ) and  
         ( rol_documentos.rdc_ccbid = cartera_clientes_cpbt_doc.ccb_ccbid ) and  
         ( cartera_clientes_cpbt_doc.ccb_codemp = deudores.ctc_codemp ) and  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = deudores.ctc_ctcid ) and  
         ( ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( insumos.ins_impcli = 'S' ) AND  
         ( view_cabecera_comprobantes.cbt_estado in ( 'A','F','B' ) ) AND  
         ( convert(varchar, cbc_tpcid) + '_' + convert(varchar, cbc_numero) not in (  SELECT convert(varchar, dcc_tpcidpad) + '_' + convert(varchar, dcc_numeropad)  
                                                                                        FROM detalle_comprobantes  
                                                                                       WHERE ( detalle_comprobantes.dcc_codemp = @cbc_codemp ) AND  
                                                                                             ( detalle_comprobantes.dcc_tpcidpad is not null )   
                                                                                              )) )   

select #DocPen.*, provcli.pcl_rut RutCli, provcli.pcl_nomfant NomCli
from #DocPen, provcli
where cbc_codemp = provcli.pcl_codemp and
      dcc_pclid = provcli.pcl_pclid

union

select #DocPen.*, provcli.pcl_rut RutCli, provcli.pcl_nomfant NomCli
from #DocPen, provcli
where cbc_codemp = provcli.pcl_codemp and
      rol_pclid = provcli.pcl_pclid
