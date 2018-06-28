CREATE Procedure [dbo].[_Update_Tipos_Retiro_Entrega]
			(
			@codemp integer, 
			@id integer, 
			@nombre varchar (80),
			@idid smallint) 
	as  
  UPDATE tipos_retiro_entrega    
     SET tre_nombre = @nombre    
   WHERE ( tre_codemp = @codemp ) 
		AND ( tre_treid = @id )  
		
 UPDATE tipos_retiro_entrega_idiomas    
     SET tri_nombre = @nombre    
   WHERE ( tri_codemp = @codemp ) 
		AND ( tri_treid = @id )  
		AND ( tri_idid = @idid )
