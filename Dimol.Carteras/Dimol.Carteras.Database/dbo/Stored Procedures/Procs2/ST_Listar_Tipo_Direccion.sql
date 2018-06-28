-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
create PROCEDURE [dbo].[ST_Listar_Tipo_Direccion] 
(
	@codemp as integer,
	@pclid as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	
	SELECT [CODIGO]
      ,[NOMBRE]
  FROM [SITREL_TIPO_DIRECCION]
  where [CODEMP] = @codemp
  and [PCLID] = @pclid
          


END
