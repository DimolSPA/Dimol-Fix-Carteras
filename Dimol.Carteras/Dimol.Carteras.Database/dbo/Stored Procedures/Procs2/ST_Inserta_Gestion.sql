-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Gestion]
(
@CODEMP int
,@PCLID int
,@RUT varchar(9)
,@TIPO_DEUDOR varchar(20)
,@CODIGO_PRODUCTO varchar(20)
,@NUMERO_OPERACION varchar(24)
,@FECHA_HORA_GESTION datetime
,@CODIGO_MONEDA varchar(20)
,@CODIGO_EMPRESA varchar(6)
,@CODIGO_ACCION varchar(20)
,@CODIGO_CONTACTO varchar(20)
,@CODIGO_RESPUESTA varchar(20)
,@GLOSA_GESTION varchar(255)
,@FECHA_COMPROMISO varchar(8)
,@MONTO_COMPROMISO decimal(15,2)
,@MONTO_GESTION decimal(15,2)
,@NOMBRE_CONTACTO varchar(255)
,@PROGRAMACION_LLAMADA varchar(15)
,@TELEFONO_SUCURSAL varchar(20)
,@CAMPANIA varchar(20)
,@ENVIADO varchar(1)
)
AS
BEGIN
	
	INSERT INTO [SITREL_GESTION]
           ([CODEMP]
           ,[PCLID]
           ,[RUT]
           ,[TIPO_DEUDOR]
           ,[CODIGO_PRODUCTO]
           ,[NUMERO_OPERACION]
           ,[FECHA_HORA_GESTION]
           ,[CODIGO_MONEDA]
           ,[CODIGO_EMPRESA]
           ,[CODIGO_ACCION]
           ,[CODIGO_CONTACTO]
           ,[CODIGO_RESPUESTA]
           ,[GLOSA_GESTION]
           ,[FECHA_COMPROMISO]
           ,[MONTO_COMPROMISO]
           ,[MONTO_GESTION]
           ,[NOMBRE_CONTACTO]
           ,[PROGRAMACION_LLAMADA]
           ,[TELEFONO_SUCURSAL]
           ,[CAMPANIA]
           ,[ENVIADO])
     VALUES
           (@CODEMP
           ,@PCLID
           ,@RUT
           ,@TIPO_DEUDOR
           ,@CODIGO_PRODUCTO
           ,@NUMERO_OPERACION
           ,@FECHA_HORA_GESTION
           ,@CODIGO_MONEDA
           ,@CODIGO_EMPRESA
           ,@CODIGO_ACCION
           ,@CODIGO_CONTACTO
           ,@CODIGO_RESPUESTA
           ,@GLOSA_GESTION
           ,@FECHA_COMPROMISO
           ,@MONTO_COMPROMISO
           ,@MONTO_GESTION
           ,@NOMBRE_CONTACTO
           ,@PROGRAMACION_LLAMADA
           ,@TELEFONO_SUCURSAL
           ,@CAMPANIA
           ,@ENVIADO)
           
END
