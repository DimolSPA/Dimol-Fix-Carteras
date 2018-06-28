

Create Procedure Insertar_Perfiles_Estados(@pfe_codemp integer, @pfe_prfid integer, @pfe_estid integer) as
  INSERT INTO perfiles_estados  
         ( pfe_codemp,   
           pfe_prfid,   
           pfe_estid )  
  VALUES ( @pfe_codemp,   
           @pfe_prfid,   
           @pfe_estid )
