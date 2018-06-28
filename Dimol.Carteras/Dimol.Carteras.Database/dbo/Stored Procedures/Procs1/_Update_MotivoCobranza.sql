CREATE Procedure [dbo].[_Update_MotivoCobranza]
			(
			@codemp integer, 
			@id integer, 
			@nombre varchar (80)
			) 
as  
  UPDATE motivo_cobranza    
     SET MTC_NOMBRE = @nombre     
  WHERE ( MTC_CODEMP = @codemp ) AND    
         ( MTC_MTCID = @id )
