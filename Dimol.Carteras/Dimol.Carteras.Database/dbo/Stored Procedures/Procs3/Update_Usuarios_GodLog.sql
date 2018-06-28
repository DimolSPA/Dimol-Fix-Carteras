

Create Procedure Update_Usuarios_GodLog(@codemp integer, @usuario integer) as
  UPDATE usuarios  
     SET usr_godlog = usr_godlog + 1  ,
            usr_badlog = 0,
            usr_fecblock = null
            
   WHERE ( usuarios.usr_codemp = @codemp ) AND  
         ( usuarios.usr_usrid = @usuario )
