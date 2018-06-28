

Create Procedure Delete_Usuarios(@usr_codemp integer, @usr_usrid integer) as  
  DELETE FROM usuarios_sucursal  
   WHERE ( uss_codemp = @usr_codemp ) AND  
         ( uss_usrid = @usr_usrid )   

  DELETE FROM usuarios  
   WHERE ( usuarios.usr_codemp = @usr_codemp ) AND  
         ( usuarios.usr_usrid = @usr_usrid )
