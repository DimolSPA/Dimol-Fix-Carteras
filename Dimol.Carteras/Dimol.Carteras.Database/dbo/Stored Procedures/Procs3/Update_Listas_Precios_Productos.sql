

Create Procedure Update_Listas_Precios_Productos(@lpp_codemp integer, @lpp_ltpid integer, @lpp_prodid numeric (15), @lpp_prereal decimal (10,2),
																@lpp_precio decimal (10,2), @lpp_orden01 smallint, @lpp_orden02 smallint) as
  UPDATE listas_precios_productos  
     SET lpp_codemp = @lpp_codemp,   
         lpp_ltpid = @lpp_ltpid,   
         lpp_prodid = @lpp_prodid,   
         lpp_prereal = @lpp_prereal,   
         lpp_precio = @lpp_precio,   
         lpp_orden01 = @lpp_orden01,   
         lpp_orden02 = @lpp_orden02  
   WHERE ( listas_precios_productos.lpp_codemp = @lpp_codemp ) AND  
         ( listas_precios_productos.lpp_ltpid = @lpp_ltpid ) AND  
         ( listas_precios_productos.lpp_prodid = @lpp_prodid )
