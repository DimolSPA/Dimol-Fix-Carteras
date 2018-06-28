

Create Procedure Trae_Reporte_CpbtDoc_NC(@pcl_codemp integer, @cbc_tpcid integer, @cbc_numero numeric(15), @idi_idid integer) as
SELECT provcli.pcl_rut,   
         provcli.pcl_nombre,   
         provcli.pcl_apepat,   
         provcli.pcl_apemat,   
         provcli.pcl_nomfant,   
         giros_idiomas.gii_nombre,   
         a.cbc_feccpbt,   
         a.cbc_fecvenc,   
         a.cbc_fecent,   
         a.mon_nombre,   
         a.cbc_tipcambio,   
         a.cbc_porcdesc,   
         a.cbc_glosa,   
         a.fpi_nombre,   
         a.tci_nombre,   
         a.cbc_numero,   
         a.cbc_numprovcli,   
         a.cbc_neto,   
         a.cbc_impuestos,   
         a.cbc_retenido,   
         a.cbc_descuentos,   
         a.cbc_final,   
         a.cbc_ordcomp,   
         pai_nombre,   
         reg_nombre,   
         ciu_nombre,   
         com_nombre,   
         com_codpost,   
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
         a.cbc_tpcid,
         pcs_nombre,
        pcs_codigo,
        b.tci_nombre TipPad,
        b.cbc_numprovcli NomPad,
        b.cbc_feccpbt    FecPad,
        a.cbt_estado
    FROM provcli,   
         provcli_sucursal,   
         view_datos_geograficos,   
         giros_idiomas,   
         detalle_comprobantes,   
         view_cabecera_comprobantes a,   
         productos,
         view_cabecera_comprobantes b 
   WHERE ( provcli_sucursal.pcs_codemp = provcli.pcl_codemp ) and  
         ( provcli_sucursal.pcs_pclid = provcli.pcl_pclid ) and  
         ( provcli_sucursal.pcs_pcsid = a.cbc_pcsid ) and  
         ( provcli.pcl_codemp = giros_idiomas.gii_codemp ) and  
         ( provcli.pcl_girid = giros_idiomas.gii_girid ) and  
         ( provcli_sucursal.pcs_comid = view_datos_geograficos.com_comid ) and  
         ( a.cbc_codemp = provcli.pcl_codemp ) and  
         ( a.cbc_pclid = provcli.pcl_pclid ) and  
         ( a.cbc_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( a.cbc_sucid = detalle_comprobantes.dcc_sucid ) and  
         ( a.cbc_tpcid = detalle_comprobantes.dcc_tpcid ) and  
         ( a.cbc_numero = detalle_comprobantes.dcc_numero ) and  
         ( productos.pdt_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( productos.pdt_prodid = detalle_comprobantes.dcc_prodid ) and  
         ( dcc_codemp  = b.cbc_codemp ) and 
         ( dcc_tpcidpad = b.cbc_tpcid ) and
         ( dcc_numeropad = b.cbc_numero ) and
         ( a.idi_idid = giros_idiomas.gii_idid ) and  
         ( ( provcli.pcl_codemp = @pcl_codemp) AND  
         ( a.cbc_tpcid = @cbc_tpcid ) AND  
         ( a.cbc_numero = @cbc_numero ) AND  
         ( a.cbt_estado in ( 'A','F','B','E' ) ) AND  
         ( a.idi_idid =@idi_idid )   
         )
