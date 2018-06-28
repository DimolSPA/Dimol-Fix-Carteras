CREATE PROCEDURE [dbo].[_Buscar_Nombre_Deudor] (@codemp int, @ctcid int)
AS
BEGIN
	SET NOCOUNT ON;
	select LTRIM(RTRIM(CTC_NOMFANT)) CTC_NOMFANT from DEUDORES 
		where CTC_CODEMP = @codemp
		and CTC_CTCID = @ctcid
			
END
