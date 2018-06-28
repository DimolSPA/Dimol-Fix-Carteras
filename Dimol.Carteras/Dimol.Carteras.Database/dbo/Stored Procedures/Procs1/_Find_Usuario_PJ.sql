
 CREATE Procedure [dbo].[_Find_Usuario_PJ](@usuario varchar(200), @red varchar(150), @local varchar(150)) as
  
  if datediff(mi, (select top 1 FECULTLOG FROM usuarios_pj where login = @usuario), getdate()) > 60 or (select top 1 ip FROM usuarios_pj where login = @usuario) is null 
	begin 
		  update usuarios_pj 
		  set ip = @red + @local 
		  where login = @usuario
	end
		update usuarios_pj 
		set FECULTLOG = getdate() 
		where login = @usuario
  
  SELECT count(usuarios_pj.usrid)  
    FROM usuarios_pj  with (nolock)  
   WHERE usuarios_pj.login = @usuario and ip = @red + @local 
