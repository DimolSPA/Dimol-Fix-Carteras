-- =============================================
-- Author:		Rodrigo Garrido
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar categorias para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Imagenes_Convertir](@inicio int,
@limite int)

AS
BEGIN
	SET NOCOUNT ON;

  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY Pclid ,Ctcid,Ccbid,Cdid) as row from    
  (SELECT  [CDI_CODEMP] Codemp
      ,[CDI_PCLID] Pclid
      ,[CDI_CTCID] Ctcid
      ,[CDI_CCBID] Ccbid
      ,[CDI_CDID] Cdid
      --,[CDI_IMAGEN] Imagen
      ,[CDI_TPCID] Tpcid
  FROM [dbo].[CARTERA_CLIENTES_CPBT_DOC_IMAGENES] where CDI_RUTA_ARCHIVO is null)as tabla ) as t
  where  row >= @inicio and row <= @limite 
  order by [CODEMP],[PCLID],[CTCID],[CCBID],[CDID]

END
