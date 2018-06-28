-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [_Buscar_Nombre_Producto] (@codemp int, @pclid int, @pdtid int)
AS
BEGIN
	SET NOCOUNT ON;
		SELECT [PDT_NOMBRE]
		  FROM [PROVCLI_PRODUCTO] 
		  where [PDT_CODEMP]= @codemp
		  and [PDT_PCLID]= @pclid
		  and [PDT_PDTID] = @pdtid
		  
	
	
END
