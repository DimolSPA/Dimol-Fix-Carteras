CREATE PROCEDURE _Caja_Reporte_ImputacionGeneral
(
--declare
@codemp int,
@conciliacionId int
)
AS
BEGIN
	SET NOCOUNT ON;
	select cdi.conciliacion_id ConciliacionId, 
	CASE WHEN dc.FEC_DOC is null THEN cm.FEC_MOVIMIENTO ELSE dc.FEC_DOC END FecDoc,
	CASE WHEN dc.TIPODOCUMENTO is null THEN '' ELSE CASE dc.TIPODOCUMENTO 
	WHEN 2 THEN 'Pago en Efectivo'  
	ELSE '' END END TipoDocumento,
	 SUM(saldo) Capital,
	 SUM(cdi.interes) Interes,
	 SUM(cdi.honorario) Honorario,
	 SUM(cdi.gastopre) GastoPre,
	 SUM(cdi.gastojud) GastoJud
	
from CONCILIACION_DOCUMENTO_IMPUTADO cdi
join CONCILIACION_MOVIMIENTOS_DOCUMENTOS cmd
on  cdi.CONCILIACION_ID = cmd.CONCILIACION_ID
left join DOCUMENTOS_CUSTODIA dc
on cmd.CUSTODIA_ID  = dc.CUSTODIA_ID
left join CARTOLA_MOVIMIENTOS cm
on cmd.movimiento_id = cm.movimiento_id
where cdi.codemp = @codemp
and cdi.conciliacion_id = @conciliacionId
group by cdi.conciliacion_id, dc.TIPODOCUMENTO,dc.FEC_DOC, cm.FEC_MOVIMIENTO
END