

Create Procedure Update_Perfiles_Idiomas(@pfi_codemp integer, @pfi_prfid integer, @pfi_idid integer, @pfi_nombre varchar (250)) as  
  UPDATE perfiles_idiomas  
     SET pfi_nombre = @pfi_nombre
   WHERE ( perfiles_idiomas.pfi_codemp = @pfi_codemp ) AND  
         ( perfiles_idiomas.pfi_prfid = @pfi_prfid ) AND  
         ( perfiles_idiomas.pfi_idid = @pfi_idid )
