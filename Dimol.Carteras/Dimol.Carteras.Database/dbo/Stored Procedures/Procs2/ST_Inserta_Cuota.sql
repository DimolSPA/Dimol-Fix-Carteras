-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Cuota]
(
@CODEMP int
,@ID_CARGA int
,@PCLID int
,@RUT varchar(9)
,@NUMERO_OPERACION varchar(24)
,@PRODUCTO varchar(20)
,@NUMERO_CUOTA int
,@FECHA_VENCIMIENTO int
,@MONTO_DETALLE decimal(15,2)
,@CAPITAL decimal(15,2)
,@INTERESES decimal(15,2)
,@GASTOS decimal(15,2)
,@DIAS_MORA int
)
AS
BEGIN

	
	INSERT INTO [SITREL_CUOTA]
           ([CODEMP]
           ,ID_CARGA
           ,[PCLID]
           ,RUT
           ,[NUMERO_OPERACION]
           ,[PRODUCTO]
           ,[NUMERO_CUOTA]
           ,[FECHA_VENCIMIENTO]
           ,[MONTO_DETALLE]
           ,[CAPITAL]
           ,[INTERESES]
           ,[GASTOS]
           ,[DIAS_MORA])
     VALUES
           (@CODEMP
           ,@ID_CARGA
           ,@PCLID
           ,@RUT
           ,@NUMERO_OPERACION
           ,@PRODUCTO
           ,@NUMERO_CUOTA
           ,@FECHA_VENCIMIENTO
           ,@MONTO_DETALLE
           ,@CAPITAL
           ,@INTERESES
           ,@GASTOS
           ,@DIAS_MORA)

END
