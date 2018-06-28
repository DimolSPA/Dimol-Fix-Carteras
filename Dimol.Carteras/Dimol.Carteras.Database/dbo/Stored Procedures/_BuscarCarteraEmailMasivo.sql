CREATE PROCEDURE [dbo].[_BuscarCarteraEmailMasivo]
	@Estados varchar(max),
	@Gestores varchar(max),
	@Codemp int,
	@Pclid int,
	@TipoCartera int,
	@FechaVencimiento date,
	@FechaOperador int,
	@MontoDesde decimal,
	@MontoHasta decimal
AS
	DECLARE @SQL varchar(max)
	DECLARE @SPLITSTATES varchar(200)
	DECLARE @SPLITGESTORES varchar(200)
	SET @SPLITSTATES = (SELECT TOP 1 * FROM SplitString(REPLACE(@Estados, '"', ''), ','))
	SET @SPLITGESTORES = (SELECT TOP 1 * FROM SplitString(REPLACE(@Gestores, '"', ''), ','))

	--Monto total de documentos
	SET @SQL = 'SELECT [CCB_CTCID], [GESTOR].[GES_GESID], SUM([CCB_MONTO]) AS MONTO,[GESTOR].[GES_NOMBRE], [GESTOR].[GES_EMAIL], 
	[GESTOR].[GES_TELEFONO], [DEUDORES].[CTC_NOMBRE], [PROVCLI].[PCL_NOMFANT]
	FROM [dbo].[ESTADOS_CARTERA], [dbo].[CARTERA_CLIENTES_CPBT_DOC]
	RIGHT JOIN [dbo].[GESTOR_CARTERA]
	ON [dbo].[CARTERA_CLIENTES_CPBT_DOC].[CCB_CTCID] = [dbo].[GESTOR_CARTERA].[GSC_CTCID],
	[dbo].[GESTOR],
	[dbo].[PROVCLI],
	[dbo].[DEUDORES] WHERE [CARTERA_CLIENTES_CPBT_DOC].[CCB_CODEMP] = ' + CONVERT(varchar, @Codemp)

	SET @SQL = @SQL + ' AND [CARTERA_CLIENTES_CPBT_DOC].[CCB_ESTID] = [ESTADOS_CARTERA].[ECT_ESTID]'

	SET @SQL = @SQL + ' AND [DEUDORES].[CTC_CTCID] = [CARTERA_CLIENTES_CPBT_DOC].[CCB_CTCID]'

	SET @SQL = @SQL + ' AND (CONVERT(varchar, [CARTERA_CLIENTES_CPBT_DOC].[CCB_ESTCPBT]) NOT IN (''X'',''J'',''F''))'
	
	SET @SQL = @SQL + ' AND [dbo].[GESTOR_CARTERA].[GSC_GESID] = [GESTOR].[GES_GESID]'

	SET @SQL = @SQL + ' AND [dbo].[GESTOR_CARTERA].[GSC_CODEMP] = ' + CONVERT(varchar, @Codemp) 

	SET @SQL = @SQL + ' AND [dbo].[PROVCLI].[PCL_PCLID] = [CARTERA_CLIENTES_CPBT_DOC].[CCB_PCLID]'

	SET @SQL = @SQL + ' AND [dbo].[PROVCLI].[PCL_CODEMP] = [CARTERA_CLIENTES_CPBT_DOC].[CCB_CODEMP]'

	SET @SQL = @SQL + ' AND [CARTERA_CLIENTES_CPBT_DOC].[CCB_PCLID] = ' + CONVERT(varchar, @Pclid)   --FILTRO CLIENTE

	SET @SQL = @SQL + ' AND [CARTERA_CLIENTES_CPBT_DOC].[CCB_SALDO] > 0'

	IF(@Estados IS NOT NULL AND @Estados <> '')
	BEGIN
		SET @SQL = @SQL + ' AND (CONVERT(varchar(max), [CARTERA_CLIENTES_CPBT_DOC].[CCB_ESTID]) IN (' + @SPLITSTATES + '))' --FILTRO DE ESTADOS
	END

	IF(@Gestores IS NOT NULL AND @Gestores <> '')
	BEGIN
		SET @SQL = @SQL + ' AND (CONVERT(varchar(max), [GESTOR_CARTERA].[GSC_GESID]) IN (' + @SPLITGESTORES + '))' --FILTRO DE GESTORES
	END

	--Filtro tipo de cartera
	IF (@TipoCartera IS NOT NULL AND (@TipoCartera = 1 OR @TipoCartera = 2))
	BEGIN
		SET @SQL = @SQL + ' AND ' + CONVERT(varchar, @TipoCartera) + '= [dbo].[CARTERA_CLIENTES_CPBT_DOC].[CCB_TIPCART]'
	END

	--FILTROS DE FECHA
	--WHEN 1 HASTA
	--WHEN 3 VENCIDAS
	--WHEN 2 NO VENCIDAS
	IF (@FechaOperador IS NOT NULL AND (@FechaOperador = 2 OR @FechaOperador = 3))
	BEGIN
		SET @SQL = @SQL + CASE @FechaOperador WHEN 3
					THEN ' AND CONVERT(datetime, [CARTERA_CLIENTES_CPBT_DOC].[CCB_FECVENC]) <= CONVERT(datetime, ''' +  CONVERT(varchar, @FechaVencimiento) + ''')' 
					ELSE '' END --Vencidas
				
		SET @SQL = @SQL + CASE @FechaOperador WHEN 2
					THEN ' AND CONVERT(datetime, [CARTERA_CLIENTES_CPBT_DOC].[CCB_FECVENC]) >= CONVERT(datetime, ''' +  CONVERT(varchar, @FechaVencimiento) + ''')' 
					ELSE '' END --No Vencidas
	END

	SET @SQL = @SQL + ' GROUP BY [CCB_CTCID], [CCB_MONTO], [GES_GESID], [GESTOR].[GES_NOMBRE],
	 [GESTOR].[GES_EMAIL], [GESTOR].[GES_TELEFONO], [DEUDORES].[CTC_NOMBRE], [PROVCLI].[PCL_NOMFANT]'
	
	IF(@MontoDesde >= 0 OR @MontoHasta > 0)
	BEGIN 

	--CASOS DE FILTROS CON MONTOS
	SET @SQL = @SQL + CASE WHEN (@MontoDesde >= 0)
				THEN ' HAVING SUM([CCB_MONTO]) >= ' + CONVERT(VARCHAR, @MontoDesde) 
				ELSE '' END

	SET @SQL = @SQL + CASE WHEN (@MontoHasta > 0)
				THEN ' AND SUM([CCB_MONTO]) <= ' + CONVERT(VARCHAR, @MontoHasta)
				ELSE '' END
	END 

	BEGIN 
		EXEC(@SQL)
	END

RETURN 0