

 Create Procedure Update_Perfiles_Comprobantes(@pfc_codemp integer, @pfc_prfid integer, @pfc_tpcid integer) as
  UPDATE perfiles_comprobantes  
     SET pfc_tpcid = @pfc_tpcid  
   WHERE ( perfiles_comprobantes.pfc_codemp = @pfc_codemp ) AND  
         ( perfiles_comprobantes.pfc_prfid = @pfc_prfid ) AND  
         ( perfiles_comprobantes.pfc_tpcid = @pfc_tpcid )
