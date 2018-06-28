
CREATE  FUNCTION [dbo].[_Trae_Valor_Rango] 
(
	@clc_codemp INT, 
	@clc_clcid INT,
	@tipoRango INT,
	@diasvenc INT,
	@diasAsign INT,
	@regId INT,
	@ccb_asignado DECIMAL(15,2),
	@ccb_fecvenc DATETIME
	
	
)
RETURNS decimal(15,3)
AS
BEGIN
DECLARE
@sql NVARCHAR(4000),
@valorRango  decimal(15,2)


	SET @sql = N'SELECT @valorRango =clausulas_contcart_rangos.clr_valor
					FROM clausulas_contcart_rangos
					JOIN clausulas_contcart 
					ON clausulas_contcart_rangos.clr_codemp = clausulas_contcart.clc_codemp 
					and clausulas_contcart_rangos.clr_clcid = clausulas_contcart.clc_clcid
					WHERE clausulas_contcart.clc_codemp = ' + Cast(@clc_codemp AS VARCHAR) + '
					and clausulas_contcart.clc_clcid = ' + Cast(@clc_clcid AS VARCHAR) + ''

	IF @tipoRango = 1
		BEGIN
			SET @sql+=' AND clausulas_contcart_rangos.clr_fecfin >= Cast('''+ Cast(@ccb_fecvenc as varchar)  +''' AS DATE)'
		END

	IF @tipoRango = 2
		BEGIN
			SET @sql+=' AND clausulas_contcart_rangos.clr_hasta >= ' + Cast(@diasAsign AS VARCHAR) + ''
		END

	IF @tipoRango = 3
		BEGIN
			SET @sql+=' AND clausulas_contcart_rangos.clr_hasta >= ' + Cast(@ccb_asignado AS VARCHAR) + ''
		END

	IF @tipoRango = 4
		BEGIN
			SET @sql+=' AND clausulas_contcart_rangos.clr_regid = ' + Cast(@regId AS VARCHAR) + ''
		END

	IF @tipoRango = 5
		BEGIN
			SET @sql+=' AND clausulas_contcart_rangos.clr_hasta >= ' + Cast(@diasvenc AS VARCHAR) + ''
		END

	IF @tipoRango = 6
		BEGIN
			SET @sql+=' AND clausulas_contcart_rangos.clr_hasta >= ' + Cast(@diasvenc AS VARCHAR) + '
						AND clausulas_contcart_rangos.clr_regid = ' + Cast(@regId AS VARCHAR) + ''
		END

	IF @tipoRango = 7
		BEGIN
			SET @sql+=' AND clausulas_contcart_rangos.clr_hasta >= ' + Cast(@diasAsign AS VARCHAR) + '
						AND clausulas_contcart_rangos.clr_regid = ' + Cast(@regId AS VARCHAR) + ''
		END

	
	EXECUTE sp_executesql @sql, N'@valorRango int OUTPUT', @valorRango OUTPUT;
	
	-- Return the result of the function
	RETURN @valorRango

END
