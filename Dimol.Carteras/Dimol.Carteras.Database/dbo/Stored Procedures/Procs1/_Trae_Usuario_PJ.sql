
CREATE Procedure [dbo].[_Trae_Usuario_PJ](@usuario varchar(200), @red varchar(150), @local varchar(150)) as
  
  SELECT usuarios_pj.usrid,   
         usuarios_pj.nombre,   
         usuarios_pj.login,   
         usuarios_pj.password,   
         usuarios_pj.fecing,   
         usuarios_pj.fecultlog,   
         usuarios_pj.pclid,   
         usuarios_pj.activa,   
         usuarios_pj.enuso,   
         usuarios_pj.ip,
         usuarios_pj.adm		 
    FROM usuarios_pj with (nolock) 
   WHERE usuarios_pj.login = @usuario

