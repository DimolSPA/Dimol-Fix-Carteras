

Create Procedure Update_Usuarios_Cambia_Password(@usr_codemp integer, @usr_usrid integer, @usr_password varchar(100), @dias integer) as
  UPDATE usuarios  
     SET usr_password = @usr_password ,
             usr_feccambio = dateadd(d, @dias, getdate())  
   WHERE ( usuarios.usr_codemp = @usr_codemp ) AND  
         ( usuarios.usr_usrid = @usr_usrid )
