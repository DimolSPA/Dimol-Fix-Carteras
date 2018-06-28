

Create Procedure Update_Usuarios_Sucursal(@uss_codemp integer, @uss_usrid integer, @uss_sucid integer, @uss_default char (1)) as  
  UPDATE usuarios_sucursal  
     SET uss_default = @uss_default  
   WHERE ( usuarios_sucursal.uss_codemp = @uss_codemp ) AND  
         ( usuarios_sucursal.uss_usrid = @uss_usrid ) AND  
         ( usuarios_sucursal.uss_sucid = @uss_sucid )
