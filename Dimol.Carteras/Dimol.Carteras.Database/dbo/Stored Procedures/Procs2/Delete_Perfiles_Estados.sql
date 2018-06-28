

Create Procedure Delete_Perfiles_Estados(@pfe_codemp integer, @pfe_prfid integer, @pfe_estid integer) as
  DELETE FROM perfiles_estados  
   WHERE ( perfiles_estados.pfe_codemp = @pfe_codemp ) AND  
         ( perfiles_estados.pfe_prfid = @pfe_prfid ) AND  
         ( perfiles_estados.pfe_estid = @pfe_estid )
