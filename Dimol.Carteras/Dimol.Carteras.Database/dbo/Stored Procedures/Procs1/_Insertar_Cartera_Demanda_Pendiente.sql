CREATE Procedure [dbo].[_Insertar_Cartera_Demanda_Pendiente](@codemp integer, @pclid numeric (15), @ctcid numeric (15), @ccbid integer, @usrid int) as 
                       
INSERT INTO [CARTERA_CLIENTES_DEMANDA_PENDIENTE]
           ([CDP_CODEMP]
           ,[CDP_PCLID]
           ,[CDP_CTCID]
           ,[CDP_CCBID]
           ,[CDP_FECHA]
           ,[CDP_USRID])
     VALUES
           (@codemp
           ,@pclid
           ,@ctcid
           ,@ccbid
           ,GETDATE()
           ,@usrid)
