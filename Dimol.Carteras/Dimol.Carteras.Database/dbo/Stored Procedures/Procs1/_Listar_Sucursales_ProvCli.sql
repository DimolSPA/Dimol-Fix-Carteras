-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Lista Categorias
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Sucursales_ProvCli] 
(
	@codemp as integer,
	@idsuc as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select pcs_pcsid as ID, pcs_nombre as Nombre
	from provcli_sucursal 
	where pcs_codemp= @codemp and pcs_pclid = @idsuc
         


END
