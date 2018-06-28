

Create Procedure Trae_Reporte_CpbtDoc(@pcl_codemp integer, @cbc_tpcid integer, @cbc_numero numeric(15), @idi_idid integer) as
  SELECT provcli.pcl_rut,   
         provcli.pcl_nombre,   
         provcli.pcl_apepat,   
         provcli.pcl_apemat,   
         provcli.pcl_nomfant,   
         giros_idiomas.gii_nombre,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         view_cabecera_comprobantes.cbc_fecvenc,   
         view_cabecera_comprobantes.cbc_fecent,   
         view_cabecera_comprobantes.mon_nombre,   
         view_cabecera_comprobantes.cbc_tipcambio,   
         view_cabecera_comprobantes.cbc_porcdesc,   
         view_cabecera_comprobantes.cbc_glosa,   
         view_cabecera_comprobantes.fpi_nombre,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numero,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_neto,   
         view_cabecera_comprobantes.cbc_impuestos,   
         view_cabecera_comprobantes.cbc_retenido,   
         view_cabecera_comprobantes.cbc_descuentos,   
         view_cabecera_comprobantes.cbc_final,   
         view_cabecera_comprobantes.cbc_ordcomp,   
         view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         view_datos_geograficos.com_codpost,   
         provcli_sucursal.pcs_direccion,   
         provcli_sucursal.pcs_telefono,   
         provcli_sucursal.pcs_fax,   
         provcli_sucursal.pcs_mail,   
         productos.pdt_codfisico,   
         productos.pdt_nombre,   
         detalle_comprobantes.dcc_precio,   
         detalle_comprobantes.dcc_cantidad,   
         detalle_comprobantes.dcc_neto,   
         detalle_comprobantes.dcc_impuesto,   
         detalle_comprobantes.dcc_retenido,   
         detalle_comprobantes.dcc_total,   
         detalle_comprobantes.dcc_porcfact,   
         detalle_comprobantes.dcc_porchon,   
         detalle_comprobantes.dcc_numserie,   
         detalle_comprobantes.dcc_numserieprov,   
         detalle_comprobantes.dcc_comentario,   
         view_cabecera_comprobantes.cbc_tpcid,
         pcs_nombre,
        pcs_codigo,
        cbt_estado  
    FROM provcli,   
         provcli_sucursal,   
         view_datos_geograficos,   
         giros_idiomas,   
         detalle_comprobantes,   
         view_cabecera_comprobantes,   
         productos  
   WHERE ( provcli_sucursal.pcs_codemp = provcli.pcl_codemp ) and  
         ( provcli_sucursal.pcs_pclid = provcli.pcl_pclid ) and  
         ( provcli_sucursal.pcs_pcsid = cbc_pcsid ) and  
         ( provcli.pcl_codemp = giros_idiomas.gii_codemp ) and  
         ( provcli.pcl_girid = giros_idiomas.gii_girid ) and  
         ( provcli_sucursal.pcs_comid = view_datos_geograficos.com_comid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = provcli.pcl_codemp ) and  
         ( view_cabecera_comprobantes.cbc_pclid = provcli.pcl_pclid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_sucid = detalle_comprobantes.dcc_sucid ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = detalle_comprobantes.dcc_tpcid ) and  
         ( view_cabecera_comprobantes.cbc_numero = detalle_comprobantes.dcc_numero ) and  
         ( productos.pdt_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( productos.pdt_prodid = detalle_comprobantes.dcc_prodid ) and  
         ( view_cabecera_comprobantes.idi_idid = giros_idiomas.gii_idid ) and  
         ( ( provcli.pcl_codemp = @pcl_codemp ) AND  
         ( view_cabecera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( view_cabecera_comprobantes.cbc_numero = @cbc_numero ) AND  
         ( view_cabecera_comprobantes.cbt_estado in ( 'A','F','B','E' ) ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid )   
         )
