

Create Procedure Find_Usuarios(@usr_codemp integer, @usr_usrid integer) as
  SELECT count(usuarios.usr_usrid)  
    FROM usuarios  
   WHERE usuarios.usr_codemp = @usr_codemp and usr_usrid = @usr_usrid
