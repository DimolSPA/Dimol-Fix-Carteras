Create Procedure [dbo].[_Update_Tipos_Tribunal](
		@ttb_codemp integer, 
		@ttb_ttbid integer, 
		@ttb_nombre varchar (50),
		@idid integer
) 

as    
  UPDATE tipos_tribunal    
     SET  
         ttb_nombre = @ttb_nombre    
   WHERE ( tipos_tribunal.ttb_codemp = @ttb_codemp ) AND    
         ( tipos_tribunal.ttb_ttbid = @ttb_ttbid )  
         
         
     UPDATE tipos_tribunal_idiomas    
     SET    
         tbi_nombre = @ttb_nombre    
   WHERE ( tipos_tribunal_idiomas.tbi_codemp = @ttb_codemp ) AND    
         ( tipos_tribunal_idiomas.tbi_ttbid = @ttb_ttbid ) AND    
         ( tipos_tribunal_idiomas.tbi_idid = @idid )
