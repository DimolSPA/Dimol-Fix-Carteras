-- =============================================
-- Author:		Rodrigo Garrido
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar categorias para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Trae_Imagen_Convertir](
@codemp int,
@pclid int,
@ctcid int,
@ccbid int,
@cdid int)

AS
BEGIN
	SET NOCOUNT ON;

 SELECT  [CDI_CODEMP] Codemp
      ,[CDI_PCLID] Pclid
      ,[CDI_CTCID] Ctcid
      ,[CDI_CCBID] Ccbid
      ,[CDI_CDID] Cdid
      ,[CDI_IMAGEN] Imagen

  FROM [dbo].[CARTERA_CLIENTES_CPBT_DOC_IMAGENES]
  where [CDI_CODEMP]= @Codemp
      and [CDI_PCLID] = @Pclid
      and [CDI_CTCID] = @Ctcid
      and [CDI_CCBID] = @Ccbid
      and [CDI_CDID] = @Cdid

END
