CREATE PROCEDURE [dbo].[_Trae_TerceroRutNombre] (@codemp int, @terceroId int)
AS
BEGIN
	SET NOCOUNT ON;
	
		select RUT as rutTercero, NOMBRE as nombreTercero from CARTERA_CLIENTES_CPBT_DOC_TERCEROS (nolock)
		where  CODEMP = @codemp and TERCEROID = @terceroId
	
	
END
