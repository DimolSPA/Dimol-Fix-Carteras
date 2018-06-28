-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
create PROCEDURE [dbo].[_Ultima_Accion_Fecha] (@codemp int, @pclid int, @ctcid int, @accid int)
AS
BEGIN
	SET NOCOUNT ON;
	select max(cea_fecha) Fecha
	from cartera_clientes_estados_acciones 
	where cea_codemp = @codemp
	and cea_pclid = @pclid
	and cea_ctcid =  @ctcid
	and cea_accid =  @accid	
END

