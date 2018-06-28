CREATE  FUNCTION [dbo].[_Trae_Valor_Moneda] 
(
	@mnv_codemp INT,
	@mnv_codmon INT
)
RETURNS decimal(15,2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @mnv_valor  decimal(15,2)

	-- Add the T-SQL statements to compute the return value here

	Select @mnv_valor =  monedas_valores.mnv_valor
	from monedas_valores 
	where  monedas_valores.mnv_codemp = @mnv_codemp
	 and monedas_valores.mnv_codmon = @mnv_codmon
	 and Cast(monedas_valores.mnv_fecha  AS DATE) = Cast(getdate() AS DATE) ---aqui debe ser getdate()

	-- Return the result of the function
	RETURN @mnv_valor

END
