

Create Procedure Delete_Usuarios_Sucursal(@uss_codemp integer, @uss_usrid integer) as  
  DELETE FROM usuarios_sucursal  
   WHERE ( usuarios_sucursal.uss_codemp = @uss_codemp ) AND  
         ( usuarios_sucursal.uss_usrid = @uss_usrid )
