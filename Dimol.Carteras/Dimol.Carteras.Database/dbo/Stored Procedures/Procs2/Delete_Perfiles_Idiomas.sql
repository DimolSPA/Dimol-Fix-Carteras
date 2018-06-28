

Create Procedure Delete_Perfiles_Idiomas(@pfi_codemp integer, @pfi_prfid integer, @pfi_idid integer) as  
  DELETE FROM perfiles_idiomas  
   WHERE ( perfiles_idiomas.pfi_codemp = @pfi_codemp ) AND  
         ( perfiles_idiomas.pfi_prfid = @pfi_prfid ) AND  
         ( perfiles_idiomas.pfi_idid = @pfi_idid )
