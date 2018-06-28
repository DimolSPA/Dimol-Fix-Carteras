

Create Procedure Update_Usuarios_FecLog(@codemp integer, @usuario integer) as
   UPDATE usuarios  
     SET   usr_fecultlog = getdate()  
   WHERE ( usuarios.usr_codemp = @codemp ) AND  
         ( usuarios.usr_usrid = @usuario )
