CREATE PROCEDURE [dbo].[_Listar_Panel_Quiebra_Proyeccion](
@codemp int)
AS 
BEGIN

select 1 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Sin Emisión de ND' as ITEM
from PANEL_QUIEBRA PQ with(nolock)
left join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
where PQD.FEC_EMISION_ND is null
UNION
select 2 As ID, 
	count(PQ.QUIEBRA_ID) As TOTAL, 
	cast(sum(PQ.CUANTIA) * 19 / 100.00 as decimal(18,2)) MONTO, 
	'Pendientes' as ITEM
from PANEL_QUIEBRA PQ with(nolock)
left join PANEL_QUIEBRA_DETALLE PQD with(nolock)
on PQ.QUIEBRA_ID = PQD.QUIEBRA_ID
where PQD.FEC_SOLICITUD_ANTECEDENTE is null

END
