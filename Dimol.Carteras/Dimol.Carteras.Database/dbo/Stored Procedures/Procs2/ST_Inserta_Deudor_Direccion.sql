-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Deudor_Direccion]
(
@CODEMP int 
,@PCLID int
,@ID_CARGA int
,@RUT varchar(9)
,@DIRECCION varchar(255)
,@ZONA_GEOGRAFICA varchar(20)
,@TIPO_DIRECCION varchar(20)
,@TIPO_PERSONA varchar(7)
)
AS
BEGIN
	
	INSERT INTO [SITREL_DIRECCION]
           ([CODEMP]
           ,ID_CARGA
           ,[PCLID]
           ,[RUT]
           ,[DIRECCION]
           ,[ZONA_GEOGRAFICA]
           ,[TIPO_DIRECCION]
           ,[TIPO_PERSONA])
     VALUES
           (@CODEMP 
			,@ID_CARGA
			,@PCLID 
			,@RUT 
			,@DIRECCION 
			,@ZONA_GEOGRAFICA
			,@TIPO_DIRECCION
			,@TIPO_PERSONA)



END
