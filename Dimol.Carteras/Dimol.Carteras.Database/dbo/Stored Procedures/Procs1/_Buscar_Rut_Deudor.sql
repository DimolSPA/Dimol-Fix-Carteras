CREATE PROCEDURE [dbo].[_Buscar_Rut_Deudor] (@codemp int, @ctcid int)
AS
BEGIN
	SET NOCOUNT ON;
	select LTRIM(RTRIM(CTC_NUMERO)) + '-' + LTRIM(RTRIM(CTC_DIGITO)) RUT from DEUDORES 
		where CTC_CODEMP = @codemp
		and CTC_CTCID = @ctcid
			
END
