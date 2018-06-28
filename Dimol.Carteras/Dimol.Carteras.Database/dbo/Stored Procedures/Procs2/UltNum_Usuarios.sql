

Create Procedure UltNum_Usuarios(@usr_codemp integer) as
  SELECT IsNull(Max(usr_usrid)+1, 1)
    FROM usuarios  
   WHERE usuarios.usr_codemp = @usr_codemp
