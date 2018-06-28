
-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[ST_Listar_Respuesta] 
(
	@codemp as integer,
	@pclid as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	
	SELECT codigo as CODIGO
      ,[NOMBRE]
  FROM [SITREL_RESPUESTA]
  where [CODEMP] =@codemp
  and [PCLID] = @pclid
  AND ESTADO = 'V'
          


END
