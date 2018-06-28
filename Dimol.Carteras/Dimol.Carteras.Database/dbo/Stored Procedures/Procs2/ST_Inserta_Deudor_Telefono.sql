-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Deudor_Telefono]
(
@CODEMP int 
,@PCLID int
,@ID_CARGA int
,@RUT varchar(9)
,@NUMERO varchar(20)
,@CODIGO_AREA varchar(20)
,@ANEXO varchar(10)
,@TIPO_TELEFONO varchar(20)
)
AS
BEGIN
	
	INSERT INTO [SITREL_TELEFONO]
           ([CODEMP]
           ,ID_CARGA
           ,[PCLID]
           ,[RUT]
           ,[NUMERO]
           ,[CODIGO_AREA]
           ,[ANEXO]
           ,[TIPO_TELEFONO])
     VALUES
           (@CODEMP 
			,@ID_CARGA
			,@PCLID 
			,@RUT 
			,@NUMERO
			,@CODIGO_AREA
			,@ANEXO
			,@TIPO_TELEFONO)


END
