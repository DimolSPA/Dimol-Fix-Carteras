

Create Procedure Delete_Listas_Precios_Productos(@lpp_codemp integer, @lpp_ltpid integer, @lpp_prodid numeric (15)) as
  DELETE FROM listas_precios_productos  
   WHERE ( listas_precios_productos.lpp_codemp = @lpp_codemp ) AND  
         ( listas_precios_productos.lpp_ltpid = @lpp_ltpid ) AND  
         ( listas_precios_productos.lpp_prodid = @lpp_prodid )
