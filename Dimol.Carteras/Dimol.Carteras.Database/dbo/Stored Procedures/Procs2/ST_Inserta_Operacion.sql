-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Operacion]
(
@CODEMP int
,@ID_CARGA int
,@PCLID int
,@RUT varchar(9)
,@NUMERO_OPERACION varchar(24)
,@CODIGO_PRODUCTO varchar(20)
,@NOMBRE_PRODUCTO varchar(50)
,@TIPO_DEUDOR varchar(20)
,@MONEDA varchar(20)
,@MONTO_MORA decimal(15,2)
,@SALDO_INSOLUTO decimal(15,2)
,@MONTO_OPERACION decimal(15,2)
,@DEUDA_TOTAL decimal(15,2)
,@DIAS_MORA int
,@EJECUTIVO_CUENTA varchar(50)
,@MONTO_TOTAL_INTERES decimal(15,2)
,@ESTADO_PRODUCTO varchar(11)
,@FECHA_ULTIMO_PAGO int
,@CAMPANIA varchar(20)
,@ACCION varchar(50)
,@CONTACTO varchar(50)
,@RESPUESTA varchar(50)
,@GLOSA varchar(255)
,@FECHA_GESTION datetime
,@CODIGO_SUCURSAL varchar(20)
,@NOMBRE_SUCURSAL varchar(50)
,@DIRECCION_SUCURSAL varchar(255)
,@TELEFONO_SUCURSAL varchar(20)
,@FECHA_VENCIMIENTO int
,@NOMBRE_ESTRATEGIA varchar(50)
,@TIPO_PERSONA varchar(7)
)
AS
BEGIN

DECLARE @EXISTE_SUCURSAL INT = (SELECT COUNT(CODIGO) FROM SITREL_SUCURSAL S WHERE S.CODIGO = @CODIGO_SUCURSAL)

IF @EXISTE_SUCURSAL = 0 
BEGIN
	INSERT INTO SITREL_SUCURSAL VALUES (@CODEMP, @PCLID, @CODIGO_SUCURSAL, @NOMBRE_SUCURSAL)
END 
	
	INSERT INTO [SITREL_OPERACION]
           ([CODEMP]
           ,ID_CARGA
           ,[PCLID]
           ,[RUT]
           ,[NUMERO_OPERACION]
           ,[CODIGO_PRODUCTO]
           ,[NOMBRE_PRODUCTO]
           ,[TIPO_DEUDOR]
           ,[MONEDA]
           ,[MONTO_MORA]
           ,[SALDO_INSOLUTO]
           ,[MONTO_OPERACION]
           ,[DEUDA_TOTAL]
           ,[DIAS_MORA]
           ,[EJECUTIVO_CUENTA]
           ,[MONTO_TOTAL_INTERES]
           ,[ESTADO_PRODUCTO]
           ,[FECHA_ULTIMO_PAGO]
           ,[CAMPANIA]
           ,[ACCION]
           ,[CONTACTO]
           ,[RESPUESTA]
           ,[GLOSA]
           ,[FECHA_GESTION]
           ,[CODIGO_SUCURSAL]
           ,[NOMBRE_SUCURSAL]
           ,[DIRECCION_SUCURSAL]
           ,[TELEFONO_SUCURSAL]
           ,[FECHA_VENCIMIENTO]
           ,[NOMBRE_ESTRATEGIA]
           ,[TIPO_PERSONA])
     VALUES
           (@CODEMP
           ,@ID_CARGA
           ,@PCLID
           ,@RUT
           ,@NUMERO_OPERACION
           ,@CODIGO_PRODUCTO
           ,@NOMBRE_PRODUCTO
           ,@TIPO_DEUDOR
           ,@MONEDA
           ,@MONTO_MORA
           ,@SALDO_INSOLUTO
           ,@MONTO_OPERACION
           ,@DEUDA_TOTAL
           ,@DIAS_MORA
           ,@EJECUTIVO_CUENTA
           ,@MONTO_TOTAL_INTERES
           ,@ESTADO_PRODUCTO
           ,@FECHA_ULTIMO_PAGO
           ,@CAMPANIA
           ,@ACCION
           ,@CONTACTO
           ,@RESPUESTA
           ,@GLOSA
           ,@FECHA_GESTION
           ,@CODIGO_SUCURSAL
           ,@NOMBRE_SUCURSAL
           ,@DIRECCION_SUCURSAL
           ,@TELEFONO_SUCURSAL
           ,@FECHA_VENCIMIENTO
           ,@NOMBRE_ESTRATEGIA
           ,@TIPO_PERSONA)

END
