CREATE Procedure [dbo].[_Insertar_MotivoCobranza]
			(
			@codemp integer, 
			@nombre varchar (80)
			)
as  
declare @idMotivo int  
  
set @idMotivo = (select IsNull(Max(MTC_MTCID)+1, 1) from motivo_cobranza where MTC_CODEMP = @codemp)  
  
  INSERT INTO motivo_cobranza    
         (
			MTC_CODEMP,
			MTC_MTCID, 
		    MTC_NOMBRE )    

  VALUES ( @codemp,     
           @idMotivo,     
           @nombre   
			)
			
INSERT INTO [MOTIVO_COBRANZA_IDIOMAS]
           ([MCI_CODEMP]
           ,[MCI_MTCID]
           ,[MCI_IDID]
           ,[MCI_NOMBRE])
     VALUES
           (@codemp
           ,@idMotivo
           ,1
           ,@nombre)
