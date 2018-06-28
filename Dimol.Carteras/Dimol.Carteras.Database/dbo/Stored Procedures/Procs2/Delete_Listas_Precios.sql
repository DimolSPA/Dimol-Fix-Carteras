

Create Procedure Delete_Listas_Precios(@ltp_codemp integer, @ltp_ltpid integer) as

  DELETE FROM listas_precios_productos  
   WHERE ( listas_precios_productos.lpp_codemp = @ltp_codemp ) AND  
         ( listas_precios_productos.lpp_ltpid = @ltp_ltpid ) 

  DELETE FROM listas_precios  
   WHERE ( listas_precios.ltp_codemp = @ltp_codemp ) AND  
         ( listas_precios.ltp_ltpid = @ltp_ltpid )
