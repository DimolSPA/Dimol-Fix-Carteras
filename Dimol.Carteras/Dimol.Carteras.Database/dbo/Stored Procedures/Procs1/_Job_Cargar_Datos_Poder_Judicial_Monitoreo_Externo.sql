CREATE PROCEDURE _Job_Cargar_Datos_Poder_Judicial_Monitoreo_Externo
AS
BEGIN
	INSERT INTO LOG_ERROR VALUES (GETDATE(), 'Actualiza datos en la tabla del monitoreo del demonio externo', 'Comienzo', 'Job 5 AM',0 )
	
	DELETE PODER_JUDICIAL_MONITOREO_EXTERNO
	
	;WITH CausasClientesDemandas AS (
	--Saldo No Demandado
	select externo.ID_LITIGANTE LITIGANTE, externo.FECHA, cpbt.CCB_PCLID PCLID, cpbt.CCB_CTCID CTCID, cpbt.CCB_CCBID CCBID, cpbt.CCB_SALDO SALDO from 
	CARTERA_CLIENTES_CPBT_DOC cpbt with(nolock)
	join DEUDORES d with(nolock)
	on cpbt.CCB_CODEMP = d.CTC_CODEMP
	and cpbt.CCB_CTCID = d.CTC_CTCID
	left join [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_litigante externo with(nolock)
	on d.CTC_RUT = externo.RUT
	where /*cpbt.CCB_PCLID = 90
	and */cpbt.CCB_ESTCPBT in ('V','J')
	and externo.ID_LITIGANTE is null
	union 
	--Saldo Demandado
	select externo.ID_LITIGANTE LITIGANTE, externo.FECHA, cpbt.CCB_PCLID PCLID, cpbt.CCB_CTCID CTCID, cpbt.CCB_CCBID CCBID, cpbt.CCB_SALDO SALDO from 
	CARTERA_CLIENTES_CPBT_DOC cpbt with(nolock)
	join DEUDORES d with(nolock)
	on cpbt.CCB_CODEMP = d.CTC_CODEMP
	and cpbt.CCB_CTCID = d.CTC_CTCID
	join [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_litigante externo with(nolock)
	on d.CTC_RUT = externo.RUT
	where /*cpbt.CCB_PCLID = 90
	and */cpbt.CCB_ESTCPBT in ('V','J')
	)
	INSERT INTO PODER_JUDICIAL_MONITOREO_EXTERNO(PCLID,TOTAL_CARTERA,TOTAL_SINDEMANDA,TOTAL_DEMANDADO,DEMANDADO_DOSANIOS)
	SELECT DISTINCT CausasClientesDemandas.PCLID, 
		SaldoCartera.total TotalCartera, 
		SaldoSinDemanda.total TotalSinDemanda, 
		SaldoDemandado.total TotalDemandado,
		UltimosAnios.total DemandadoDosAnios
	FROM CausasClientesDemandas
	join (SELECT PCLID, SUM(SALDO) total
			FROM CausasClientesDemandas
			GROUP BY PCLID)SaldoCartera
	on CausasClientesDemandas.PCLID = SaldoCartera.PCLID
	join (SELECT PCLID, SUM(SALDO) total
			FROM CausasClientesDemandas
			WHERE LITIGANTE IS NULL
			GROUP BY PCLID) SaldoSinDemanda
	on CausasClientesDemandas.PCLID = SaldoSinDemanda.PCLID
	join (SELECT PCLID, SUM(SALDO) total
			FROM CausasClientesDemandas
			WHERE LITIGANTE IS NOT NULL
			GROUP BY PCLID) SaldoDemandado
	on CausasClientesDemandas.PCLID = SaldoDemandado.PCLID
	join (SELECT PCLID, SUM(SALDO) total
			FROM CausasClientesDemandas
			WHERE LITIGANTE IS NOT NULL AND 
			FECHA >= DATEADD(YEAR, DATEDIFF(YEAR, 0, GETDATE())-1, 0)--Utimos dos años
			GROUP BY PCLID) UltimosAnios
	on CausasClientesDemandas.PCLID = UltimosAnios.PCLID


	  INSERT INTO LOG_ERROR VALUES (GETDATE(), 'Actualiza datos en la tabla del monitoreo del demonio externo', 'Termino', 'Job 5 AM',0 )
END

