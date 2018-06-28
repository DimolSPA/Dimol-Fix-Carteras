

Create Procedure Insertar_Perfiles_Comprobantes(@pfc_codemp integer, @pfc_prfid integer, @pfc_tpcid integer) as  
  INSERT INTO perfiles_comprobantes  
         ( pfc_codemp,   
           pfc_prfid,   
           pfc_tpcid )  
  VALUES ( @pfc_codemp,   
           @pfc_prfid,   
           @pfc_tpcid )
