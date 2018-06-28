

Create Procedure Update_Usuarios_FecBlock(@codemp integer, @usuario integer) as
  UPDATE usuarios  
     SET usr_fecblock = getdate(),
             usr_estado = 'B'
   WHERE ( usuarios.usr_codemp = @codemp ) AND  
         ( usuarios.usr_usrid = @usuario )
