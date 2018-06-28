

Create Procedure Trae_Usuario_Sucursal(@codemp integer, @usuario integer) as
  SELECT usuarios_sucursal.uss_sucid  
    FROM usuarios_sucursal  
   WHERE ( usuarios_sucursal.uss_codemp = @codemp ) AND  
         ( usuarios_sucursal.uss_usrid = @usuario )
