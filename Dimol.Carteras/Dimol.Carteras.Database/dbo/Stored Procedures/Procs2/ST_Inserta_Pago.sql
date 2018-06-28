-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Pago]
(
@CODEMP int
,@ID_CARGA int
,@PCLID int
,@RUT varchar(9)
,@NUMERO_OPERACION varchar(24)
,@CODIGO_PRODUCTO varchar(20)
,@FECHA_PAGO int
,@MONTO_PAGO decimal(15,2)
)
AS
BEGIN
	
	INSERT INTO [SITREL_PAGO]
           ([CODEMP]
           ,ID_CARGA
           ,[PCLID]
           ,[RUT]
           ,[NUMERO_OPERACION]
           ,[CODIGO_PRODUCTO]
           ,[FECHA_PAGO]
           ,[MONTO_PAGO])
     VALUES
           (@CODEMP
           ,@ID_CARGA
           ,@PCLID
           ,@RUT
           ,@NUMERO_OPERACION
           ,@CODIGO_PRODUCTO
           ,@FECHA_PAGO
           ,@MONTO_PAGO)

END
