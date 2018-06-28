CREATE PROCEDURE [dbo].[_Trae_Deudor_Quiebra_RolNumero](
@codemp int,
@rut varchar(20))
	
AS
BEGIN

declare
	@existDeudor int = 0
	
	SET @existDeudor = (select count(RUT) from DEUDOR_QUIEBRA where RUT = @rut)
	if @existDeudor > 0
	begin
		select ROLNUMERO as rolNumero from DEUDOR_QUIEBRA where CODEMP= @codemp and RUT= @rut
		
	end
	else
	begin
		select 0 as rolNumero
	end
END
