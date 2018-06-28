Create Procedure [dbo].[_Insertar_Perfiles](
	@prf_codemp integer, 
	@prf_nombre varchar (200), 
	@prf_administrador char (1),
	@idid int) 

as   
declare @id int                            
set @id = (select IsNull(Max(prf_prfid )+1, 1) from perfiles  
   where prf_codemp = @prf_codemp)                        
 
INSERT INTO perfiles    
         ( prf_codemp,     
           prf_prfid,     
           prf_nombre,     
           prf_administrador )    
  VALUES ( @prf_codemp,     
           @id,     
           @prf_nombre,     
           @prf_administrador )  
           
    INSERT INTO perfiles_idiomas    
         ( pfi_codemp,     
           pfi_prfid,     
           pfi_idid,     
           pfi_nombre )    
  VALUES ( @prf_codemp,     
           @id,     
           @idid,     
           @prf_nombre )
