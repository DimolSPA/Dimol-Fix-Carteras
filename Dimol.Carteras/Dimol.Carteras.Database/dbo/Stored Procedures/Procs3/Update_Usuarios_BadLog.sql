

Create Procedure Update_Usuarios_BadLog(@codemp integer, @usuario integer) as
  UPDATE usuarios  
     SET usr_badlog = usr_badlog + 1  
   WHERE ( usuarios.usr_codemp = @codemp ) AND  
         ( usuarios.usr_usrid = @usuario )
