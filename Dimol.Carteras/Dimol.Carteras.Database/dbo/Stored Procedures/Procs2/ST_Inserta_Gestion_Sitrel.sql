-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Gestion_Sitrel]
(
@CODEMP int
,@PCLID int
,@CTCID int
,@FECHA datetime
,@ACCID int
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
,@TELEFONO_CONTACTO varchar(20)
)
AS
BEGIN
	INSERT INTO [CARTERA_CLIENTES_ESTADOS_SITREL]
           ([CODEMP]
           ,[PCLID]
           ,[CTCID]
           ,[FECHA]
           ,[ACCID]
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
           ,[TELEFONO_CONTACTO])
     VALUES
           (@CODEMP
           ,@PCLID
           ,@CTCID
           ,@FECHA
           ,@ACCID
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
           ,LTRIM(RTRIM(@TELEFONO_CONTACTO))
           )
           
END
