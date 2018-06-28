Create Procedure [dbo].[_Update_Perfiles](
		@prf_codemp integer, 
		@prf_prfid integer, 
		@prf_nombre varchar (200), 
		@prf_administrador char (1),
		@idid int) 

as    
  UPDATE perfiles    
     SET prf_nombre = @prf_nombre,     
         prf_administrador = @prf_administrador    
   WHERE ( perfiles.prf_codemp = @prf_codemp ) AND    
         ( perfiles.prf_prfid = @prf_prfid )  
         
         
     UPDATE perfiles_idiomas    
     SET pfi_nombre = @prf_nombre  
   WHERE ( perfiles_idiomas.pfi_codemp = @prf_codemp ) AND    
         ( perfiles_idiomas.pfi_prfid = @prf_prfid ) AND    
         ( perfiles_idiomas.pfi_idid = @idid )
