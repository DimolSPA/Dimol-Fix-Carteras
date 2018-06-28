-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Super Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Tipos_ProvCli] 
(
	@codemp as integer,
	@idioma as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select tpc_tpcid as ID, tpi_nombre as Nombre
	from tipos_provcli, tipos_provcli_idiomas 
	where tpc_codemp = tpi_codemp and
	      tpc_tpcid = tpi_tpcid and 
		  tpc_codemp = @codemp and tpi_idid = @idioma
          order by tpi_nombre
         


END
