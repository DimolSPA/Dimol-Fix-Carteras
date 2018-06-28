

Create Procedure Trae_Reporte_Inventario_Valorizado(@pdt_codemp integer, @idi_idid integer, @pdm_codmon integer) as
 SELECT productos.pdt_codfisico,   
         productos.pdt_nombre,   
         productos_moneda.pdm_precio,   
         insumos.ins_costo,   
         insumos.ins_costo_prom,   
         productos_stock.pst_stock_merma,   
         productos_stock.pst_stock_total,   
         supercategorias_idioma.sci_nombre,   
         supercategorias.spc_orden,   
         categorias_idiomas.cai_nombre,   
         supcat_categorias.scc_orden,   
         productos_moneda.pdm_codmon  
  FROM productos,   
         productos_moneda,   
         insumos,   
         supcat_categorias,   
         supercategorias_idioma,   
         idiomas,   
         categorias_idiomas,   
         productos_stock,   
         supercategorias  
   WHERE ( productos_moneda.pdm_codemp = productos.pdt_codemp ) and  
         ( productos_moneda.pdm_prodid = productos.pdt_prodid ) and  
         ( insumos.ins_codemp = productos.pdt_codemp ) and  
         ( insumos.ins_insid = productos.pdt_insid ) and  
         ( productos.pdt_codemp = supcat_categorias.scc_codemp ) and  
         ( productos.pdt_catid = supcat_categorias.scc_catid ) and  
         ( productos.pdt_spcid = supcat_categorias.scc_spcid ) and  
         ( productos.pdt_codemp = supercategorias_idioma.sci_codemp ) and  
         ( productos.pdt_spcid = supercategorias_idioma.sci_spcid ) and  
         ( productos.pdt_codemp = categorias_idiomas.cai_codemp ) and  
         ( productos.pdt_catid = categorias_idiomas.cai_catid ) and  
         ( supercategorias_idioma.sci_idiid = idiomas.idi_idid ) and  
         ( categorias_idiomas.cai_idiid = idiomas.idi_idid ) and  
         ( productos_stock.pst_codemp = productos.pdt_codemp ) and  
         ( productos_stock.pst_prodid = productos.pdt_prodid ) and  
         ( supercategorias_idioma.sci_codemp = supercategorias.spc_codemp ) and  
         ( supercategorias_idioma.sci_spcid = supercategorias.spc_spcid ) and  
         ( ( productos.pdt_codemp = @pdt_codemp ) AND  
         ( productos.pdt_estado = 'U' ) AND  
         ( productos_moneda.pdm_codmon = @pdm_codmon ) AND  
         ( idiomas.idi_idid = @idi_idid )   
         )
