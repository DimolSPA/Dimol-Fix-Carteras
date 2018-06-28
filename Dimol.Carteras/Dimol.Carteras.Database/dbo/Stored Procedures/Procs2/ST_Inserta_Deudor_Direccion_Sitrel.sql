-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Deudor_Direccion_Sitrel]
(
@CODEMP int 
,@CTCID int
,@DIRECCION varchar(255)
,@COMID varchar(20)
,@TIPO_DIRECCION varchar(20)
,@ORIGEN VARCHAR(1)
,@ENVIADO VARCHAR(1)
)
AS
BEGIN

DECLARE @EXISTE INT = (SELECT COUNT([CODEMP]) FROM [DEUDORES_DIRECCION_SITREL] WHERE [CODEMP] = @CODEMP AND [CTCID]= @CTCID AND [DIRECCION]=@DIRECCION)


IF @EXISTE = 0
BEGIN
INSERT INTO [DEUDORES_DIRECCION_SITREL]
           ([CODEMP]
           ,[CTCID]
           ,[DIRECCION]
           ,[COMID]
           ,[TIPO]
           ,[FECHA]
           ,[ORIGEN]
           ,[ENVIADO])
     VALUES
           (@CODEMP
			,@CTCID 
			,@DIRECCION 
			,@COMID 
			,@TIPO_DIRECCION 
			,GETDATE()
			,@ORIGEN
			,@ENVIADO)
	END
END
