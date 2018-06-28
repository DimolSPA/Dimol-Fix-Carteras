-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Borradores] 
(
	@codemp as integer,
	@area as varchar(3) = 'JUD'
)
AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	
	SELECT [ID_BORRADOR] ID ,[DESC_BORRADOR] NOMBRE
	FROM [BORRADOR]
	WHERE 
		CODEMP = 1 AND 
		ESTADO = 'A' AND 
		AREA = @area
	ORDER BY ORDEN ASC, DESC_BORRADOR ASC
END
