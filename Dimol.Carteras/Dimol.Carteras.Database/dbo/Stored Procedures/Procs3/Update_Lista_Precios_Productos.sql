

Create Procedure Update_Lista_Precios_Productos(@lpp_codemp integer, @lpp_ltpid integer, @lpp_prodid integer, @lpp_precio decimal(15,2)) as
  UPDATE listas_precios_productos  
     SET lpp_precio = @lpp_precio  
   WHERE ( listas_precios_productos.lpp_codemp = @lpp_codemp ) AND  
         ( listas_precios_productos.lpp_ltpid = @lpp_ltpid ) AND  
         ( listas_precios_productos.lpp_prodid = @lpp_prodid ) AND  
         ( listas_precios_productos.lpp_precio <> @lpp_precio )
