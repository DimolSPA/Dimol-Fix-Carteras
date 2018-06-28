-- =============================================
-- Author:		Rodrigo Garrido
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar categorias para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Listar_Imagenes_Deudor_Cliente](
@codemp int,
@pclid int,
@ctcid int)

AS
BEGIN
	SET NOCOUNT ON;

  SELECT 
	   --[CDI_CODEMP] codemp
    --  ,[CDI_PCLID] pclid
    --  ,[CDI_CTCID] ctcid
    --  ,[CDI_CCBID] ccbid
    --  ,[CDI_CDID] cdid
    --  ,c.CCB_NUMERO numero
    --  ,[CDI_TPCID] tpcid
    --  ,t.TPC_NOMBRE tipoDocumento
      [CDI_RUTA_ARCHIVO] rutaArchivo
      ,t.TPC_NOMBRE  + ' N° ' + c.CCB_NUMERO + ', imagen : ' + convert(varchar,[CDI_CDID]) texto
  FROM [dbo].[CARTERA_CLIENTES_CPBT_DOC_IMAGENES]  , [TIPOS_CPBTDOC] t , [CARTERA_CLIENTES_CPBT_DOC] c
  where [CDI_CODEMP] = @codemp
  and [CDI_PCLID]=@pclid
  and [CDI_CTCID]=@ctcid
  and CDI_RUTA_ARCHIVO is not null
  and [CDI_TPCID] = t.TPC_TPCID
  and t.TPC_CODEMP = @codemp
  and [CDI_CODEMP] = c.CCB_CODEMP
  and [CDI_PCLID]=c.CCB_PCLID
  and [CDI_CTCID]=c.CCB_CTCID
  and [CDI_CCBID]= c.CCB_CCBID
  
  order by [CDI_CODEMP]
      ,[CDI_PCLID]
      ,[CDI_CTCID]
      ,[CDI_CCBID]
      ,[CDI_CDID]

END
