

Create Procedure Delete_Rol_Demandados(@rld_codemp integer, @rld_rolid integer, @rld_rut varchar (20)) as
  DELETE FROM rol_demandados  
   WHERE ( rol_demandados.rld_codemp = @rld_codemp ) AND  
         ( rol_demandados.rld_rolid = @rld_rolid ) AND  
         ( rol_demandados.rld_rut = @rld_rut )
