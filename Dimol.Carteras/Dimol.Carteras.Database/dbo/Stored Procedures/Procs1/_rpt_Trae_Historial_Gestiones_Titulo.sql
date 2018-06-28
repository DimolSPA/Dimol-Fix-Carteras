CREATE Procedure [dbo].[_rpt_Trae_Historial_Gestiones_Titulo](
	@codemp integer,
	@pclid integer,
	@ctcid integer
) as  
	declare @cliente varchar(400)    = ''
	declare @deudor varchar(400)     = ''
	declare @rut_deudor varchar(20)  = ''
	declare @rut_cliente varchar(20) = ''
	declare @TasaInteresPesos decimal(18,2) = 0
	declare @TasaInteresUF decimal(18,2)    = 0
	declare @TasaInteresDolar decimal(18,2) = 0
  
	-- CLIENTE
	if @pclid is not null
	begin  
		SELECT @cliente = PCL_NOMFANT, @rut_cliente = PCL_RUT  
		FROM PROVCLI   
		WHERE PCL_CODEMP=@codemp   
		AND PCL_PCLID= @pclid  
	end  
	
	-- DEUDOR
	if @ctcid is not null  
	begin  
		SELECT @rut_deudor= left(convert(varchar,CTC_NUMERO),8) +'-'+ CTC_DIGITO,@deudor=CTC_NOMFANT  
		FROM DEUDORES   
		WHERE CTC_CODEMP=@codemp   
		AND CTC_CTCID= @ctcid  
	end  
	
	-- OBTENCIÓN DEL VALOR DE LAS TASAS DE INTERES
	SELECT @TasaInteresPesos = a.PESOS, @TasaInteresUF = a.UF, @TasaInteresDolar = a.DOLAR
	FROM
	(
		SELECT 'AverageCost' AS
			TiposTasa, [PESOS], [UF], [DOLAR]
		FROM
		(
			SELECT [MON_NOMBRE], [MON_PORCINT] TasaInteres
			FROM [dbo].[MONEDAS]
			WHERE 
				MON_CODEMP = 1
		) AS SourceTable
		PIVOT
		(AVG(TasaInteres)
		FOR MON_NOMBRE IN ([PESOS], [UF], [DOLAR])
		) AS PivotTable
	) a
	
	-- RETURN
	SELECT 
		@cliente Cliente,
		@deudor Deudor,
		@rut_deudor RutDeudor,
		@rut_cliente RutCliente,
		@TasaInteresPesos TasaInteresPesos,
		@TasaInteresUF TasaInteresUF,
		@TasaInteresDolar TasaInteresDolar
