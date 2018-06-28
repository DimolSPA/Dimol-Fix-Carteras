-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Deudor]
(
@CODEMP int 
,@PCLID int
,@ID_CARGA int
,@RUT varchar(9)
,@DIGITO_VERIFICADOR varchar(1)
,@TIPO_PERSONA varchar(7)
,@NOMBRES varchar(50)
,@APELLIDO_PATERNO varchar(50)
,@APELLIDO_MATERNO varchar(50)
,@NOMBRE varchar(80)
,@RAZON_SOCIAL varchar(80)
,@NOMBRE_FANTASIA varchar(80)
,@SEXO varchar(9)
,@SEGMENTO_DEUDOR varchar(20)
,@CUENTA_CORRIENTE varchar(1)
)
AS
BEGIN

	
	INSERT INTO [SITREL_DEUDOR]
           ([CODEMP]
           ,ID_CARGA
           ,[PCLID]
           ,[RUT]
           ,[DIGITO_VERIFICADOR]
           ,[TIPO_PERSONA]
           ,[NOMBRES]
           ,[APELLIDO_PATERNO]
           ,[APELLIDO_MATERNO]
           ,[NOMBRE]
           ,[RAZON_SOCIAL]
           ,[NOMBRE_FANTASIA]
           ,[SEXO]
           ,[SEGMENTO_DEUDOR]
           ,[CUENTA_CORRIENTE])
     VALUES
           (@CODEMP
			,@ID_CARGA
			,@PCLID
			,@RUT
			,@DIGITO_VERIFICADOR
			,@TIPO_PERSONA
			,@NOMBRES
			,@APELLIDO_PATERNO
			,@APELLIDO_MATERNO
			,@NOMBRE
			,@RAZON_SOCIAL
			,@NOMBRE_FANTASIA
			,@SEXO
			,@SEGMENTO_DEUDOR 
			,@CUENTA_CORRIENTE)

END
