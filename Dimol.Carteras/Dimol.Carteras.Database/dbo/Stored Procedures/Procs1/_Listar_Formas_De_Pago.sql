-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Formas_De_Pago] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select fpi_frpid as ID, fpi_nombre as Nombre
	from formas_pago_idiomas 
	where fpi_codemp = @codemp and fpi_idid= @idioma
	order by fpi_nombre asc


END
