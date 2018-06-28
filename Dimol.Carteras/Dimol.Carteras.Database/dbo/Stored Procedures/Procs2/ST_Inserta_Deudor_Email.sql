-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Deudor_Email]
(
@CODEMP int 
,@PCLID int
,@ID_CARGA int
,@RUT varchar(9)
,@EMAIL varchar(255)
)
AS
BEGIN
	
	INSERT INTO [SITREL_EMAIL]
           ([CODEMP]
           ,ID_CARGA
           ,[PCLID]
           ,[RUT]
           ,[EMAIL])
     VALUES
           (@CODEMP 
			,@ID_CARGA
			,@PCLID 
			,@RUT 
			,@EMAIL)

END
