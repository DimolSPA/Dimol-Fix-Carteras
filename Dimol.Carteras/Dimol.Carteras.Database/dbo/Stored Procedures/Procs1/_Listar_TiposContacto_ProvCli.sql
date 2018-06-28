-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_TiposContacto_ProvCli] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select tci_ticid as ID, tci_nombre as Nombre
	from tipos_contacto_idiomas 
	where tci_codemp = @codemp 
	and tci_idid= @idioma order by tci_nombre
         


END
