CREATE PROCEDURE [dbo].[_Listar_Panel_Quiebra_Grafico](
@codemp int)
AS 
BEGIN
	select 
	1 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Total Casos' as ITEM, 
	NULL AS PARENT
from PANEL_QUIEBRA PQ with(nolock)
UNION
select 2 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Solicita Antecedentes para ND al Cliente' as ITEM, 
	1 AS PARENT 
from PANEL_QUIEBRA PQ with(nolock)
left join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
where PQD.FEC_SOLICITUD_ANTECEDENTE is not null
UNION
select 3 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Pendientes' as ITEM, 
	1 AS PARENT 
from PANEL_QUIEBRA PQ with(nolock)
left join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
where PQD.FEC_SOLICITUD_ANTECEDENTE is null
UNION
select 4 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Recepción de Antecedentes para Solicitar ND' as ITEM, 
	2 AS PARENT 
from PANEL_QUIEBRA PQ with(nolock)
left join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
where PQD.FEC_RECEPCION_ANTECEDENTE is not null
UNION
select 5 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Pendientes' as ITEM, 
	2 AS PARENT 
from PANEL_QUIEBRA PQ with(nolock)
left join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
where PQD.FEC_RECEPCION_ANTECEDENTE is null
UNION
select 6 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Envío de Antecedentes para ND al Liquidador' as ITEM, 
	4 AS PARENT 
from PANEL_QUIEBRA PQ with(nolock)
left join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
where PQD.FEC_ENVIO_ANTECEDENTE is not null
UNION
select 7 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Pendientes' as ITEM, 
	4 AS PARENT 
from PANEL_QUIEBRA PQ with(nolock)
left join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
where PQD.FEC_ENVIO_ANTECEDENTE is null
UNION
select 8 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Emisión de ND' as ITEM, 
	6 AS PARENT 
from PANEL_QUIEBRA PQ with(nolock)
left join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
where PQD.FEC_EMISION_ND is not null
UNION
select 9 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Pendientes' as ITEM, 
	6 AS PARENT 
from PANEL_QUIEBRA PQ with(nolock)
left join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
where PQD.FEC_EMISION_ND is null
END
