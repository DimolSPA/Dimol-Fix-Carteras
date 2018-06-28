CREATE Procedure [dbo].[_Update_Tipos_Motivos_Castigos]
(
			@codemp integer, 
			@id integer, 
			@nombre varchar(200),
			@idid smallint

) 

as  
  UPDATE tipos_motivos_castigos    
     SET tmc_nombre = @nombre  
   WHERE ( tmc_codemp = @codemp ) 
		AND  ( tmc_tmcid = @id )  
		
   UPDATE tipos_motivos_castigos_idiomas    
     SET tmi_nombre = @nombre    
   WHERE ( tmi_codemp = @codemp ) AND    
         ( tmi_tmcid = @id ) AND    
         ( tmi_idid = @idid )
