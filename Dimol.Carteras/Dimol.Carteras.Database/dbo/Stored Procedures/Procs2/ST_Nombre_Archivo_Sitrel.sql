-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
create PROCEDURE [dbo].[ST_Nombre_Archivo_Sitrel]
(
@CODEMP int 
,@TIPO_ARCHIVO int
,@TIPO varchar(2)
)
AS
	  SELECT [FORMATO_NOMBRE]
	  FROM [SITREL_TIPO_ARCHIVO]
	  where CODEMP = @CODEMP
	  and TIPO_ARCHIVO = @TIPO_ARCHIVO
	  and TIPO = @TIPO



