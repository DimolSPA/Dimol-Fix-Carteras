
CREATE PROCEDURE [dbo].[_Job_Cargar_Datos_Poder_Judicial_Monitoreo_Sii]
AS
BEGIN
	DELETE PODER_JUDICIAL_MONITOREO_SII
	
	;WITH CarteraSII AS (
				select 
					siic.CTCID SIICTCID,
					siic.FECHA_CONSULTA FECHACONSULTA,
					cpbt.CCB_PCLID PCLID, 
					cpbt.CCB_CTCID CTCID, 
					cpbt.CCB_CCBID CCBID, 
					cpbt.CCB_SALDO SALDO from 
				CARTERA_CLIENTES_CPBT_DOC cpbt with(nolock)
				left join sii..CABECERA siic with(nolock)
				on cpbt.CCB_CTCID = siic.CTCID
				where cpbt.CCB_CODEMP = 1
				and cpbt.CCB_ESTCPBT in ('V','J')
				and siic.CTCID is null
				union 
				select 
					siic.CTCID SIICTCID,
					siic.FECHA_CONSULTA FECHACONSULTA,
					cpbt.CCB_PCLID PCLID, 
					cpbt.CCB_CTCID CTCID, 
					cpbt.CCB_CCBID CCBID, 
					cpbt.CCB_SALDO SALDO from 
				CARTERA_CLIENTES_CPBT_DOC cpbt with(nolock)
				join sii..CABECERA siic with(nolock)
				on cpbt.CCB_CTCID = siic.CTCID
				where cpbt.CCB_CODEMP = 1
				and cpbt.CCB_ESTCPBT in ('V','J')
	)
	INSERT INTO PODER_JUDICIAL_MONITOREO_SII(PCLID,TOTAL_CARTERA,TOTAL_VERDE,TOTAL_AMARILLO,TOTAL_ROJO)
	select DISTINCT CarteraSII.PCLID, 
		SaldoCartera.total TotalCartera,
		Verde.total TotalVerde,
		Amarillo.total TotalAmarillo,
		Rojo.total TotalRojo
	from CarteraSII
	join (SELECT PCLID, SUM(SALDO) total
			FROM CarteraSII
			GROUP BY PCLID)SaldoCartera
	on CarteraSII.PCLID = SaldoCartera.PCLID
	join (SELECT PCLID, SUM(SALDO) total
			FROM CarteraSII
			JOIN (select max(t.anio) maxanio, t.ctcid
						 from sii..timbraje t with(nolock)
						 join sii..tipo_documento td with(nolock)
						 on t.tipo_documento = td.tipo_documento
						 where (td.documento like '%factura%' or td.documento like '%boleta%')
						 group by t.ctcid) CumpleVerde
			ON CarteraSII.CTCID = CumpleVerde.CTCID
			AND DATEPART(YEAR, CarteraSII.FECHACONSULTA) = CumpleVerde.maxanio
			WHERE CarteraSII.SIICTCID IS NOT NULL
			GROUP BY PCLID) Verde
	on CarteraSII.PCLID = Verde.PCLID
	join (SELECT PCLID, SUM(SALDO) total
			FROM CarteraSII
			LEFT JOIN (select distinct(t.CTCID) 
						from sii..TIMBRAJE t with(nolock)) t
			ON CarteraSII.CTCID = t.CTCID
			LEFT JOIN (select distinct(act.CTCID) 
						from sii..ACTIVIDAD_ECONOMICA_RUT act with(nolock)) act
			ON CarteraSII.CTCID = act.CTCID
			WHERE CarteraSII.SIICTCID IS NOT NULL
			and t.CTCID IS NULL
			and act.CTCID IS NULL
			GROUP BY PCLID) Amarillo
	on CarteraSII.PCLID = Amarillo.PCLID
	join (SELECT PCLID, SUM(SALDO) total
			FROM CarteraSII
			LEFT JOIN (select max(t.anio) maxanio, t.ctcid
						 from sii..timbraje t with(nolock)
						 join sii..tipo_documento td with(nolock)
						 on t.tipo_documento = td.tipo_documento
						 where (td.documento like '%factura%' or td.documento like '%boleta%')
						 group by t.ctcid) CumpleRojo
			ON CarteraSII.CTCID = CumpleRojo.CTCID
			AND DATEPART(YEAR, CarteraSII.FECHACONSULTA) = CumpleRojo.maxanio
			WHERE CarteraSII.SIICTCID IS NOT NULL
			AND CumpleRojo.ctcid IS NULL
			GROUP BY PCLID) Rojo
	on CarteraSII.PCLID = Rojo.PCLID

END


