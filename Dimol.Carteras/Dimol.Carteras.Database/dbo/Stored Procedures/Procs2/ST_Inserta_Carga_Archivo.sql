-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 20/10/2015
-- =============================================
CREATE PROCEDURE [dbo].[ST_Inserta_Carga_Archivo]
(
@codemp int ,
@pclid int,
@id_carga int,
@codigo_archivo int,
@nombre_archivo varchar(200),
@estado varchar(2),
@error varchar(300)
)
AS
BEGIN

INSERT INTO [SITREL_ARCHIVO]
           ([CODEMP]
           ,[PCLID]
           ,[ID_CARGA]
           ,[CODIGO_ARCHIVO]
           ,[FECHA_ARCHIVO]
           ,[NOMBRE_ARCHIVO]
           ,[ESTADO]
           ,[ERROR])
     VALUES
           (@codemp,
			@pclid,
			@id_carga,
			@codigo_archivo,
			GETDATE(),
			@nombre_archivo,
			@estado,
			@error)          

END
