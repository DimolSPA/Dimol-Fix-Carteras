-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Deudor_Telefono_Sitrel]
(
@CODEMP int 
,@CTCID int
,@NUMERO varchar(20)
,@TIPO_TELEFONO varchar(20)
,@ANEXO varchar(10)
,@CODIGO_AREA varchar(20)
,@ORIGEN VARCHAR(1)
,@ENVIADO VARCHAR(1)
)
AS

declare @existe int=0

set @existe = (select count([CODEMP])    from [DEUDORES_TELEFONOS_SITREL] where  [CODEMP] =@CODEMP  and [CTCID]=@CTCID and [NUMERO]=@NUMERO)  
                                                                  
if @existe = 0
BEGIN
	INSERT INTO [DEUDORES_TELEFONOS_SITREL]
           ([CODEMP]
           ,[CTCID]
           ,[NUMERO]
           ,[TIPO]
           ,[ANEXO]
           ,[CODIGO_AREA]
           ,[FECHA]
           ,[ORIGEN]
           ,[ENVIADO])
     VALUES
           (@CODEMP 
			,@CTCID
			,@NUMERO
			,@TIPO_TELEFONO
			,@ANEXO
			,@CODIGO_AREA
			,GETDATE()
			,@ORIGEN
			,@ENVIADO)


END
