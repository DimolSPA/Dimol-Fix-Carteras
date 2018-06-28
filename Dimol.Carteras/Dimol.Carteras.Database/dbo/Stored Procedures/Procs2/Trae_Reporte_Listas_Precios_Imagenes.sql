

Create Procedure Trae_Reporte_Listas_Precios_Imagenes(@ltp_codemp integer, @ltp_ltpid integer, @idi_idid integer) as
  SELECT listas_precios.ltp_ltpid,   
         listas_precios.ltp_nombre,   
         listas_precios.ltp_tipo,   
         listas_precios.ltp_vigencia,   
         listas_precios.ltp_desde,   
         listas_precios.ltp_hasta,   
         listas_precios.ltp_descuento,   
         listas_precios.ltp_gastjud,   
         productos.pdt_codfisico,   
         productos.pdt_nombre,   
         listas_precios_productos.lpp_prereal,   
         listas_precios_productos.lpp_precio,   
         supercategorias_idioma.sci_nombre,   
         categorias_idiomas.cai_nombre,   
         supcat_categorias.scc_orden,
         mon_nombre,
         pdi_imagen  
    FROM listas_precios,   
         listas_precios_productos,   
         productos,   
         supercategorias_idioma,   
         categorias_idiomas,   
         supcat_categorias,   
         idiomas, 
         monedas, productos_imagenes    
   WHERE ( listas_precios_productos.lpp_codemp = listas_precios.ltp_codemp ) and  
         ( listas_precios_productos.lpp_ltpid = listas_precios.ltp_ltpid ) and  
         ( productos.pdt_codemp = listas_precios_productos.lpp_codemp ) and  
         ( productos.pdt_prodid = listas_precios_productos.lpp_prodid ) and  
         ( productos.pdt_codemp = pdi_codemp ) and  
         ( productos.pdt_prodid = pdi_prodid ) and  
         ( supercategorias_idioma.sci_idiid = idiomas.idi_idid ) and  
         ( categorias_idiomas.cai_idiid = idiomas.idi_idid ) and  
         ( supcat_categorias.scc_codemp = supercategorias_idioma.sci_codemp ) and  
         ( supcat_categorias.scc_spcid = supercategorias_idioma.sci_spcid ) and  
         ( supcat_categorias.scc_codemp = categorias_idiomas.cai_codemp ) and  
         ( supcat_categorias.scc_catid = categorias_idiomas.cai_catid ) and  
         ( productos.pdt_codemp = categorias_idiomas.cai_codemp ) and  
         ( productos.pdt_catid = categorias_idiomas.cai_catid ) and  
         ( productos.pdt_spcid =  supercategorias_idioma.sci_spcid ) and  
         (  listas_precios.ltp_codemp = mon_codemp) and
         (  listas_precios.ltp_codmon = mon_codmon) and
         ( ( listas_precios.ltp_codemp = @ltp_codemp ) AND  
         ( listas_precios.ltp_ltpid = @ltp_ltpid ) AND  
         ( idiomas.idi_idid = @idi_idid and pdi_default = 'S' )   
         )   
ORDER BY supcat_categorias.scc_orden ASC,   
         productos.pdt_codfisico ASC
