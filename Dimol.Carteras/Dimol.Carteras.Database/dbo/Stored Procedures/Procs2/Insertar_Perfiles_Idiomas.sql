

Create Procedure Insertar_Perfiles_Idiomas(@pfi_codemp integer, @pfi_prfid integer, @pfi_idid integer, @pfi_nombre varchar (250)) as  
  INSERT INTO perfiles_idiomas  
         ( pfi_codemp,   
           pfi_prfid,   
           pfi_idid,   
           pfi_nombre )  
  VALUES ( @pfi_codemp,   
           @pfi_prfid,   
           @pfi_idid,   
           @pfi_nombre )
