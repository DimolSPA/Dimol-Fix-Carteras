-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Insertar_Log_Error
-- =============================================
CREATE PROCEDURE [dbo].[_Insertar_Log_Error]
(
@mensaje text,  
@stacktrace text,
@pagina char(1000),
@usuario int
)
as
begin

INSERT INTO LOG_ERROR
           ([FECHA]
           ,[EXCEPTION_MESSAGE]
           ,[STACKTRACE]
           ,[PAGINA]
           ,[USER_ID])
     VALUES
           (GETDATE()
           ,@mensaje
           ,@stacktrace
           ,@pagina
           ,@usuario)


end 


