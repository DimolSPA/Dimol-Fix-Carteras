-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae Tipos Documento Caja
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Impuestos_ProvCli] 
(
	@codemp as integer
)

AS
BEGIN
	-- Declare the return variable here
	SET NOCOUNT ON;
	select ipt_iptid as ID, ipt_nomcort as Nombre
	from impuestos 
	where ipt_codemp= @codemp
         


END
