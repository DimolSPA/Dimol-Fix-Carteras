

Create Procedure Find_Usuario_Empresa(@usuario varchar(40)) as
  SELECT count(usuarios.usr_usrid)  
    FROM usuarios  
   WHERE usuarios.usr_login = @usuario
