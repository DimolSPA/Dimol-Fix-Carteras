-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Bancos] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select bco_bcoid as ID, bco_nombre as Nombre
	from bancos 
	where bco_codemp= @codemp
	order by bco_nombre
         


END
