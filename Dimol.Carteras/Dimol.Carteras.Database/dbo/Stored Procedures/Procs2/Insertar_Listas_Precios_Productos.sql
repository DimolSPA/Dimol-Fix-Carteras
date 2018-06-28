

Create Procedure Insertar_Listas_Precios_Productos(@lpp_codemp integer, @lpp_ltpid integer, @lpp_prodid numeric (15), @lpp_prereal decimal (10,2),
																@lpp_precio decimal (10,2), @lpp_orden01 smallint, @lpp_orden02 smallint) as
  INSERT INTO listas_precios_productos  
         ( lpp_codemp,   
           lpp_ltpid,   
           lpp_prodid,   
           lpp_prereal,   
           lpp_precio,   
           lpp_orden01,   
           lpp_orden02 )  
  VALUES ( @lpp_codemp,   
           @lpp_ltpid,   
           @lpp_prodid,   
           @lpp_prereal,   
           @lpp_precio,   
           @lpp_orden01,   
           @lpp_orden02 )
