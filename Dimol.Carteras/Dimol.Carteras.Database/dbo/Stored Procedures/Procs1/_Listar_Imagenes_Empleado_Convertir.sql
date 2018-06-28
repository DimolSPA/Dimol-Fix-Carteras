-- =============================================
-- Author:		Rodrigo Garrido
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar categorias para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Imagenes_Empleado_Convertir]

AS
BEGIN
	SET NOCOUNT ON;

 SELECT [EPL_CODEMP] Codemp
      ,[EPL_EMPLID] Emplid
      ,[EPL_FOTO] Imagen
      ,[EPL_SUCID] Sucid
      ,isnull([EPL_USRID],0) Usrid
      ,[EPL_URL_FOTO] Url
  FROM [EMPLEADOS]
  where [EPL_FOTO] is not null
  --and [EPL_URL_FOTO] is null

END
