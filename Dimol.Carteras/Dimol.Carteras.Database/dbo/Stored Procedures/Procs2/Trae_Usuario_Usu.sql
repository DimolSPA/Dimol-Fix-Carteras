

Create Procedure Trae_Usuario_Usu(@usuario varchar(20)) as
  SELECT usuarios.usr_codemp,   
         usuarios.usr_nombre,   
         usuarios.usr_usrid,   
         usuarios.usr_password,   
         usuarios.usr_fecing,   
         usuarios.usr_godlog,   
         usuarios.usr_badlog,   
         usuarios.usr_fecultlog,   
         usuarios.usr_fecblock,   
         usuarios.usr_mail,   
         usuarios.usr_tipquest,   
         usuarios.usr_answer,   
         usuarios.usr_sucid,   
         usuarios.usr_prfid,   
         usuarios.usr_permisos,   
         usuarios.usr_estado  
    FROM usuarios  
   WHERE usuarios.usr_login = @usuario
