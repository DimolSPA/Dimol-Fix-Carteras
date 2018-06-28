

Create Procedure Delete_Listas_Precios_Productos_Tipo(@lpp_codemp integer, @lpp_ltpid integer, @pdt_tipo smallint) as
  DELETE listas_precios_productos  
    FROM listas_precios_productos,   
         productos  
   WHERE ( productos.pdt_codemp = listas_precios_productos.lpp_codemp ) and  
         ( productos.pdt_prodid = listas_precios_productos.lpp_prodid ) and  
         ( ( listas_precios_productos.lpp_codemp = @lpp_codemp ) AND  
         ( listas_precios_productos.lpp_ltpid = @lpp_ltpid ) AND  
         ( productos.pdt_tipo = @pdt_tipo )   
         )
