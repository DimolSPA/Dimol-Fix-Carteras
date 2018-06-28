Create Procedure [dbo].[_Delete_MotivoCobranza]
(
			@codemp integer, 
			@id integer) 
as  
  
  DELETE FROM motivo_cobranza    
  WHERE MTC_CODEMP = @codemp
  AND MTC_MTCID = @id
