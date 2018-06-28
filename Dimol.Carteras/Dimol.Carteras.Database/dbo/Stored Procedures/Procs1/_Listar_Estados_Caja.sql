-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Estados_Caja] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	SELECT edi_edcid,   
edi_nombre
FROM estados_documentos_diarios_idiomas
WHERE  edi_codemp =  @codemp
and edi_idiid =@idioma
         


END
