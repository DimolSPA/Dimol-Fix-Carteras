-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_Motivo_Cobranza] (@codemp int,@idioma int,  @nombre varchar(100))
AS
BEGIN
	SET NOCOUNT ON;
	Select  mci_mtcid mtcid
	FROM motivo_cobranza_idiomas
	WHERE  mci_codemp =@codemp
	and mci_idid = @idioma
	and mci_nombre = @nombre
	
END
