CREATE PROCEDURE [dbo].[_Listar_Zonas_Tribunales] 
(
	@codemp as int
)

AS
BEGIN
	SET NOCOUNT ON;
	select distinct ZONAID, ZONA
	from TRIBUNALES_ZONAS 
	where CODEMP= @codemp order by ZONA
END
