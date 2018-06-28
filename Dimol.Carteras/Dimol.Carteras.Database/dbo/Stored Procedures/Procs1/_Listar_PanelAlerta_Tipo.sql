CREATE PROCEDURE [dbo].[_Listar_PanelAlerta_Tipo]
(
@codemp int
)
AS
BEGIN

	SET NOCOUNT ON;
--Aprobación Traspaso v/s Ingreso Judicial
WITH DemandaIngresoJudicial AS (
			SELECT PD.PANEL_ID,
			   CASE WHEN  
				(dbo._Trae_DiasSemana((select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
				where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
				and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 ), PD.FEC_REGISTRO))
				< 0 THEN (dbo._Trae_DiasSemana((select max(CEH_FECHA)-1 from cartera_clientes_estados_historial 
							where ceh_codemp = PD.CODEMP and ceh_pclid = PD.PCLID 
							and ceh_ctcid = PD.CTCID and ceh_estid = 27), PD.FEC_REGISTRO))
				ELSE (dbo._Trae_DiasSemana((select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
				where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
				and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 ), PD.FEC_REGISTRO)) END DiasTranscurso,
				CASE WHEN 
					(dbo._Trae_DiasSemana((select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
					where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
					and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 ), PD.FEC_REGISTRO) -1)
				< 0 THEN (dbo._Trae_DiasSemana((select max(CEH_FECHA)-1 from cartera_clientes_estados_historial 
							where ceh_codemp = PD.CODEMP and ceh_pclid = PD.PCLID 
							and ceh_ctcid = PD.CTCID and ceh_estid = 27), PD.FEC_REGISTRO) -1) ELSE 
				(dbo._Trae_DiasSemana((select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
				where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
				and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 ), PD.FEC_REGISTRO) -1)END As DiasAtraso
			FROM PANEL_DEMANDA PD with (nolock)
			WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
			AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO')
			OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
			PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0))
--Preparación de Datos de Confección
,DemandaPreparacionConfeccion AS (
			Select PD.PANEL_ID,
				CASE WHEN 
				dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDD.FEC_ENVIO, GETDATE())) 
				< 0 THEN 0 ELSE dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDD.FEC_ENVIO, GETDATE())) END As DiasTranscurso,
				CASE WHEN 
					dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDD.FEC_ENVIO, GETDATE()))-5
				< 0 THEN 0 ELSE dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDD.FEC_ENVIO, GETDATE()))-5 END  As DiasAtraso
			from PANEL_DEMANDA PD with (nolock)
			left Join PANEL_DEMANDA_DETALLE PDD with (nolock)
			on PD.PANEL_ID = PDD.PANEL_ID
			WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
			AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO')
			OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
			PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0))
			
--Tiempo de confeccion
,DemandaTiempoConfeccion AS (
			Select PD.PANEL_ID, 
				dbo._Trae_DiasSemana(PDD.FEC_ENVIO, ISNULL(PDD.FEC_ENTREGA, GETDATE())) As DiasTranscurso,
				CASE WHEN 
					dbo._Trae_DiasSemana(PDD.FEC_ENVIO, ISNULL(PDD.FEC_ENTREGA, GETDATE()))-5
				< 0 THEN 0 ELSE dbo._Trae_DiasSemana(PDD.FEC_ENVIO, ISNULL(PDD.FEC_ENTREGA, GETDATE()))-5 END  As DiasAtraso
			from PANEL_DEMANDA PD with (nolock)
			left Join PANEL_DEMANDA_DETALLE PDD with (nolock)
			on PD.PANEL_ID = PDD.PANEL_ID
			WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
			AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO')
			AND PDD.USRID_ENCARGADO IS NOT NULL
			OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
			PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
			AND PDD.USRID_ENCARGADO IS NOT NULL)
			
--Demandas en Produccion
,DemandaProduccion AS (
			Select PD.PANEL_ID,
				CASE WHEN 
				dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDD.FEC_INGRESO_TRIBUNAL, GETDATE())) 
				< 0 THEN 0 ELSE dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDD.FEC_INGRESO_TRIBUNAL, GETDATE())) END As DiasTranscurso,
				CASE WHEN 
					dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDD.FEC_INGRESO_TRIBUNAL, GETDATE()))-14
				< 0 THEN 0 ELSE dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDD.FEC_INGRESO_TRIBUNAL, GETDATE()))-14 END  As DiasAtraso
			from PANEL_DEMANDA PD with (nolock)
			left Join PANEL_DEMANDA_DETALLE PDD with (nolock)
			on PD.PANEL_ID = PDD.PANEL_ID
			WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
			AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO')
			OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
			PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0))
			
SELECT 1 ID, 'Aprobación Traspaso v/s Ingreso Judicial' Item, 
	isnull(ROUND(AVG(CAST(DiasTranscurso AS FLOAT)),0), 0) as PromedioDias, 
	(select COUNT(*) from DemandaIngresoJudicial where DiasAtraso > 0) CantCasos,
	Cast(Cast((
			(select COUNT(*) from DemandaIngresoJudicial where DiasAtraso > 0) * 100.00)/(select COUNT(*) from DemandaIngresoJudicial) as decimal(18,2)) as varchar(5)) + '%' Atraso
	
from DemandaIngresoJudicial

union
SELECT 2 ID, 'Tiempo de Asignacion' Item, 
	isnull(ROUND(AVG(CAST(DiasTranscurso AS FLOAT)),0), 0) as PromedioDias,
	(select COUNT(*) from DemandaPreparacionConfeccion where DiasAtraso > 0) CantCasos,
	Cast(Cast((
			(select COUNT(*) from DemandaPreparacionConfeccion where DiasAtraso > 0) * 100.00)/(select COUNT(*) from DemandaPreparacionConfeccion) as decimal(18,2)) as varchar(5)) + '%' Atraso
	
from 
DemandaPreparacionConfeccion

union
SELECT 3 ID, 'Tiempo de confección' Item, 
	isnull(ROUND(AVG(CAST(DiasTranscurso AS FLOAT)),0), 0) as PromedioDias,
	(select COUNT(*) from DemandaTiempoConfeccion where DiasAtraso > 0) CantCasos,
	Cast(Cast((
			(select COUNT(*) from DemandaTiempoConfeccion where DiasAtraso > 0) * 100.00)/(SELECT COUNT(PANEL_ID)
											FROM PANEL_DEMANDA with (nolock)
											WHERE FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
											AND (CURSODEMANDA is NULL or CURSODEMANDA = 'NO')
											AND CODEMP =  @codemp
											OR FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
											FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
											AND CODEMP =  @codemp) as decimal(18,2)) as varchar(5)) + '%' Atraso
	
from 
DemandaTiempoConfeccion

union
SELECT 4 ID, 'Tiempo de Producción de Demandas Final' Item, 
	isnull(ROUND(AVG(CAST(DiasTranscurso AS FLOAT)),0), 0) as PromedioDias,
	(select COUNT(*) from DemandaProduccion where DiasAtraso > 0) CantCasos,
	Cast(Cast((
			(select COUNT(*) from DemandaProduccion where DiasAtraso > 0) * 100.00)/(select COUNT(*) from DemandaProduccion) as decimal(18,2)) as varchar(5)) + '%' Atraso

from 
DemandaProduccion;
END