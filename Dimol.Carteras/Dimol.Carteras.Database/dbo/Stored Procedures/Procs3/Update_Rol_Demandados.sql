

Create Procedure Update_Rol_Demandados(@rld_codemp integer, @rld_rolid integer, @rld_rut varchar (20),
														@rld_nombre varchar (350), @rld_repleg char (1)) as  
  UPDATE rol_demandados  
     SET rld_codemp = @rld_codemp,   
         rld_rolid = @rld_rolid,   
         rld_rut = @rld_rut,   
         rld_nombre = @rld_nombre,   
         rld_repleg = @rld_repleg  
   WHERE ( rol_demandados.rld_codemp = @rld_codemp ) AND  
         ( rol_demandados.rld_rolid = @rld_rolid ) AND  
         ( rol_demandados.rld_rut = @rld_rut )
