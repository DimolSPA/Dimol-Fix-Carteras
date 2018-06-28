

Create Procedure Update_Listas_Precios_Productos_Precios(@lpp_codemp integer, @lpp_ltpid integer, @ltp_desc decimal(10,3)) as
  UPDATE listas_precios_productos  
     SET lpp_precio = lpp_prereal  - (lpp_prereal * @ltp_desc)  
   WHERE ( listas_precios_productos.lpp_codemp = @lpp_codemp ) AND  
         ( listas_precios_productos.lpp_ltpid = @lpp_ltpid )
