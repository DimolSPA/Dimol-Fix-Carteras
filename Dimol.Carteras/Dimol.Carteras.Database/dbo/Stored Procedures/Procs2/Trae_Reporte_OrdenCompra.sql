

Create Procedure Trae_Reporte_OrdenCompra(@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @cbc_numero integer, @idi_idid integer) as
SELECT view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_cabecera_comprobantes.fpi_nombre,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numero,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         view_cabecera_comprobantes.cbc_fecvenc,   
         view_cabecera_comprobantes.cbc_glosa,   
         view_cabecera_comprobantes.cbc_neto,   
         view_cabecera_comprobantes.cbc_impuestos,   
         view_cabecera_comprobantes.cbc_retenido,   
         view_cabecera_comprobantes.cbc_descuentos,   
         view_cabecera_comprobantes.cbc_porcdesc,   
         view_cabecera_comprobantes.cbc_final,   
         view_cabecera_comprobantes.cbc_saldo,   
         view_cabecera_comprobantes.mon_nombre,   
         view_cabecera_comprobantes.cbc_tipcambio,   
         maestra_insumos_provcli.mip_codigo,   
         maestra_insumos_provcli.mip_nombre,   
         detalle_comprobantes.dcc_cantidad,   
         detalle_comprobantes.dcc_saldo,   
         detalle_comprobantes.dcc_neto,   
         detalle_comprobantes.dcc_impuesto,   
         detalle_comprobantes.dcc_retenido,   
         detalle_comprobantes.dcc_total,   
         view_cabecera_comprobantes.cbt_estado,
       detalle_comprobantes.dcc_prereal,   
         detalle_comprobantes.dcc_precio,
   view_cabecera_comprobantes.cbc_numprovcli      
    FROM view_cabecera_comprobantes,   
         detalle_comprobantes,   
         maestra_insumos_provcli  
   WHERE ( view_cabecera_comprobantes.cbc_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_sucid = detalle_comprobantes.dcc_sucid ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = detalle_comprobantes.dcc_tpcid ) and  
         ( view_cabecera_comprobantes.cbc_numero = detalle_comprobantes.dcc_numero ) and  
         ( detalle_comprobantes.dcc_codemp = maestra_insumos_provcli.mip_codemp ) and  
         ( detalle_comprobantes.dcc_insid = maestra_insumos_provcli.mip_insid ) and  
         ( view_cabecera_comprobantes.cbc_pclid = maestra_insumos_provcli.mip_pclid ) and  
         ( ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( view_cabecera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( view_cabecera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid ) AND  
         ( view_cabecera_comprobantes.cbc_numero = @cbc_numero ) AND  
         ( view_cabecera_comprobantes.cbt_estado in ( 'A','F' ) )   
         )
